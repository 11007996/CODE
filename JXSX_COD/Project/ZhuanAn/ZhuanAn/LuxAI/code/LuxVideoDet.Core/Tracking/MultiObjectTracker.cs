using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Inference.Results;

namespace LuxVideoDet.Core.Tracking;

/// <summary>
/// 多目标跟踪：同类 IoU + 线性外推预测，为 <see cref="Detection.TrackId"/> 赋值（仅用 <see cref="Detection.BoundingBox"/> / <see cref="Detection.ClassId"/>，若有掩膜则忽略）。
/// <c>TrackId</c> 按 <see cref="Detection.ClassId"/> 分别计数：每类在 <c>0</c>～<see cref="TrackingConfig.MaxTrackId"/> 间独立循环，互不占用对方编号。
/// </summary>
public sealed class MultiObjectTracker
{
    private readonly float _iouThreshold;
    private readonly int _maxMissedFrames;
    private readonly int _minHits;
    private readonly int _maxTrackId;
    private readonly List<InternalTrack> _tracks = new();
    /// <summary>每类的下一候选 ID，范围 0～<c>MaxTrackId</c>，超过后回到 0。</summary>
    private readonly Dictionary<int, int> _nextIdByClass = new();

    public MultiObjectTracker(TrackingConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
        _iouThreshold = Math.Clamp(config.TrackIouThreshold, 0.01f, 0.99f);
        _maxMissedFrames = Math.Max(1, config.MaxMissedFrames);
        _minHits = Math.Max(1, config.MinHits);
        _maxTrackId = Math.Clamp(config.MaxTrackId, 7, 65535);
    }

    public void Update(IReadOnlyList<Detection> detections)
    {
        foreach (var d in detections)
            d.TrackId = null;

        foreach (var t in _tracks)
            t.Predict();

        if (detections.Count == 0)
        {
            foreach (var t in _tracks)
                t.MarkMissed();
            PruneStaleTracks();
            return;
        }

        var pairs = new List<(float Iou, int Ti, int Dj)>(_tracks.Count * detections.Count);
        for (int ti = 0; ti < _tracks.Count; ti++)
        {
            var tr = _tracks[ti];
            for (int dj = 0; dj < detections.Count; dj++)
            {
                var det = detections[dj];
                if (det.ClassId != tr.ClassId) continue;
                var iou = tr.PredictedIou(det);
                if (iou < _iouThreshold) continue;
                pairs.Add((iou, ti, dj));
            }
        }

        pairs.Sort(static (a, b) => b.Iou.CompareTo(a.Iou));

        var usedT = new bool[_tracks.Count];
        var usedD = new bool[detections.Count];
        foreach (var (_, ti, dj) in pairs)
        {
            if (usedT[ti] || usedD[dj]) continue;
            usedT[ti] = true;
            usedD[dj] = true;
            _tracks[ti].Update(detections[dj].BoundingBox);
            if (_tracks[ti].HitCount >= _minHits)
                detections[dj].TrackId = _tracks[ti].TrackId;
        }

        for (int ti = 0; ti < _tracks.Count; ti++)
        {
            if (usedT[ti]) continue;
            _tracks[ti].MarkMissed();
        }

        for (int dj = 0; dj < detections.Count; dj++)
        {
            if (usedD[dj]) continue;
            _tracks.Add(InternalTrack.FromDetection(TakeTrackId(detections[dj].ClassId), detections[dj]));
        }

        PruneStaleTracks();
    }

    /// <summary>在该类的 0～MaxTrackId 上循环；仅与同类的活跃轨迹冲突时才顺延。</summary>
    private int TakeTrackId(int classId)
    {
        if (!_nextIdByClass.TryGetValue(classId, out var cursor))
            cursor = 0;

        var span = _maxTrackId + 1;
        for (var k = 0; k < span; k++)
        {
            var id = cursor;
            cursor = cursor >= _maxTrackId ? 0 : cursor + 1;
            _nextIdByClass[classId] = cursor;
            if (!IsTrackIdActive(id, classId))
                return id;
        }

        return 0;
    }

    private bool IsTrackIdActive(int id, int classId)
    {
        for (var i = 0; i < _tracks.Count; i++)
        {
            var t = _tracks[i];
            if (t.ClassId == classId && t.TrackId == id)
                return true;
        }

        return false;
    }

    private void PruneStaleTracks()
    {
        for (int i = _tracks.Count - 1; i >= 0; i--)
        {
            if (_tracks[i].MissedCount > _maxMissedFrames)
                _tracks.RemoveAt(i);
        }
    }

    public void Reset()
    {
        _tracks.Clear();
        _nextIdByClass.Clear();
    }

    private sealed class InternalTrack
    {
        private float _cx, _cy, _w, _h;
        private float _vx, _vy, _vw, _vh;

        public int TrackId { get; }
        public int ClassId { get; }
        public int MissedCount { get; private set; }
        public int HitCount { get; private set; }

        private InternalTrack(int trackId, int classId, float cx, float cy, float w, float h)
        {
            TrackId = trackId;
            ClassId = classId;
            _cx = cx;
            _cy = cy;
            _w = Math.Max(w, 1f);
            _h = Math.Max(h, 1f);
        }

        public static InternalTrack FromDetection(int trackId, Detection d)
        {
            var b = d.BoundingBox;
            var cx = b.CenterX;
            var cy = b.CenterY;
            var t = new InternalTrack(trackId, d.ClassId, cx, cy, b.Width, b.Height);
            t.HitCount = 1;
            return t;
        }

        public void Predict()
        {
            _cx += _vx;
            _cy += _vy;
            _w = Math.Max(_w + _vw, 1f);
            _h = Math.Max(_h + _vh, 1f);
        }

        public float PredictedIou(Detection d)
        {
            var pb = ToBBox();
            return pb.CalculateIou(d.BoundingBox);
        }

        public void Update(BoundingBox b)
        {
            var ncx = b.CenterX;
            var ncy = b.CenterY;
            var nw = Math.Max(b.Width, 1f);
            var nh = Math.Max(b.Height, 1f);

            const float beta = 0.35f;
            _vx = (1f - beta) * _vx + beta * (ncx - _cx);
            _vy = (1f - beta) * _vy + beta * (ncy - _cy);
            _vw = (1f - beta) * _vw + beta * (nw - _w);
            _vh = (1f - beta) * _vh + beta * (nh - _h);

            _cx = ncx;
            _cy = ncy;
            _w = nw;
            _h = nh;
            MissedCount = 0;
            HitCount++;
        }

        public void MarkMissed() => MissedCount++;

        private BoundingBox ToBBox() =>
            new()
            {
                X = _cx - _w * 0.5f,
                Y = _cy - _h * 0.5f,
                Width = _w,
                Height = _h
            };
    }
}
