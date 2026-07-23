using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi.HandLandmark;

/// <summary>
/// MediaPipe Hand 21 点连线与颜色（BGR），与 luxvideopyplugin Python 侧骨架一致。
/// </summary>
internal static class HandLandmarkTopology
{
    private static readonly Scalar BgrPalmLine = new(145, 145, 145);
    private static readonly Scalar BgrThumb = new(200, 230, 255);
    private static readonly Scalar BgrIndex = new(155, 70, 185);
    private static readonly Scalar BgrMiddle = new(0, 220, 255);
    private static readonly Scalar BgrRing = new(80, 230, 100);
    private static readonly Scalar BgrPinky = new(255, 120, 50);
    private static readonly Scalar BgrBase = new(60, 60, 220);

    private static readonly (int Start, int End)[] EdgesPalm =
    [
        (0, 1), (1, 5), (9, 13), (13, 17), (5, 9), (0, 17),
    ];

    private static readonly (int Start, int End)[] EdgesThumb = [(1, 2), (2, 3), (3, 4)];
    private static readonly (int Start, int End)[] EdgesIndex = [(5, 6), (6, 7), (7, 8)];
    private static readonly (int Start, int End)[] EdgesMiddle = [(9, 10), (10, 11), (11, 12)];
    private static readonly (int Start, int End)[] EdgesRing = [(13, 14), (14, 15), (15, 16)];
    private static readonly (int Start, int End)[] EdgesPinky = [(17, 18), (18, 19), (19, 20)];

    internal static readonly (int Start, int End)[] HandConnections = ConcatEdges(
        EdgesPalm, EdgesThumb, EdgesIndex, EdgesMiddle, EdgesRing, EdgesPinky);

    private static readonly Dictionary<(int, int), Scalar> LineBgrByEdge = CreateLineColors();

    private static (int Start, int End)[] ConcatEdges(params (int Start, int End)[][] parts)
    {
        var n = parts.Sum(p => p.Length);
        var arr = new (int Start, int End)[n];
        var o = 0;
        foreach (var p in parts)
        {
            p.AsSpan().CopyTo(arr.AsSpan(o));
            o += p.Length;
        }

        return arr;
    }

    private static Dictionary<(int, int), Scalar> CreateLineColors()
    {
        var m = new Dictionary<(int, int), Scalar>();
        void AddRange((int Start, int End)[] pairs, Scalar color)
        {
            foreach (var p in pairs)
                m[p] = color;
        }

        AddRange(EdgesPalm, BgrPalmLine);
        AddRange(EdgesThumb, BgrThumb);
        AddRange(EdgesIndex, BgrIndex);
        AddRange(EdgesMiddle, BgrMiddle);
        AddRange(EdgesRing, BgrRing);
        AddRange(EdgesPinky, BgrPinky);
        return m;
    }

    internal static Scalar GetConnectionLineBgr(int start, int end) =>
        LineBgrByEdge.TryGetValue((start, end), out var c) ? c : BgrPalmLine;

    internal static Scalar GetLandmarkFillBgr(int landmarkIndex)
    {
        return landmarkIndex switch
        {
            0 or 1 => BgrBase,
            2 or 3 or 4 => BgrThumb,
            5 => BgrBase,
            6 or 7 or 8 => BgrIndex,
            9 => BgrBase,
            10 or 11 or 12 => BgrMiddle,
            13 => BgrBase,
            14 or 15 or 16 => BgrRing,
            17 => BgrBase,
            _ => BgrPinky,
        };
    }

    internal static readonly Scalar OutlineBgr = new(255, 255, 255);
}
