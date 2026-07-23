using OpenCvSharp;
using SysPoint = System.Drawing.Point;

namespace LuxVideoDet.Core.Region;

public class Region
{
    public string Name { get; set; } = string.Empty;
    public List<SysPoint> Points { get; set; } = [];
    public Scalar Color { get; set; } = new Scalar(0, 255, 0);
    public int Thickness { get; set; } = 2;

    public bool ContainsPoint(SysPoint point)
    {
        if (Points.Count < 3) return false;
        
        var cvPoints = Points.Select(p => new Point(p.X, p.Y)).ToArray();
        var result = Cv2.PointPolygonTest(cvPoints, new Point2f(point.X, point.Y), false);
        return result >= 0;
    }

    public bool ContainsPoint(float x, float y)
    {
        if (Points.Count < 3) return false;
        
        var cvPoints = Points.Select(p => new Point(p.X, p.Y)).ToArray();
        var result = Cv2.PointPolygonTest(cvPoints, new Point2f(x, y), false);
        return result >= 0;
    }

    public bool ContainsBox(float[] box)
    {
        if (box.Length < 4) return false;
        
        var centerX = (int)((box[0] + box[2]) / 2);
        var centerY = (int)((box[1] + box[3]) / 2);
        
        return ContainsPoint(new SysPoint(centerX, centerY));
    }

    public bool ContainsBox(float[] box, float overlapThreshold)
    {
        if (box.Length < 4 || Points.Count < 3) return false;
        
        var boxRect = new Rect(
            (int)box[0], 
            (int)box[1], 
            (int)(box[2] - box[0]), 
            (int)(box[3] - box[1])
        );
        
        var cvPoints = Points.Select(p => new Point(p.X, p.Y)).ToArray();
        var regionRect = Cv2.BoundingRect(cvPoints);
        
        var intersection = boxRect & regionRect;
        if (intersection.Width <= 0 || intersection.Height <= 0)
            return false;
        
        var boxArea = boxRect.Width * boxRect.Height;
        var intersectionArea = intersection.Width * intersection.Height;
        var overlapRatio = (float)intersectionArea / boxArea;
        
        return overlapRatio >= overlapThreshold;
    }

    /// <summary>
    /// 轴对齐矩形 [minX,maxX]×[minY,maxY]（与坐标轴平行）与区域多边形是否有非空交集。
    /// </summary>
    public bool IntersectsAxisAlignedRectangle(double minX, double minY, double maxX, double maxY)
    {
        if (Points.Count < 3)
            return false;

        if (maxX < minX)
            (minX, maxX) = (maxX, minX);
        if (maxY < minY)
            (minY, maxY) = (maxY, minY);

        var poly = Points.Select(p => new Point(p.X, p.Y)).ToArray();
        var pr = Cv2.BoundingRect(poly);
        if (maxX < pr.X || minX > pr.X + pr.Width || maxY < pr.Y || minY > pr.Y + pr.Height)
            return false;

        var rCorners = new (double X, double Y)[]
        {
            (minX, minY), (maxX, minY), (maxX, maxY), (minX, maxY),
        };

        foreach (var c in rCorners)
        {
            var t = Cv2.PointPolygonTest(poly, new Point2f((float)c.X, (float)c.Y), false);
            if (t >= 0)
                return true;
        }

        foreach (var vtx in poly)
        {
            if (vtx.X >= minX && vtx.X <= maxX && vtx.Y >= minY && vtx.Y <= maxY)
                return true;
        }

        for (var i = 0; i < 4; i++)
        {
            var r1 = rCorners[i];
            var r2 = rCorners[(i + 1) % 4];
            var r1x = r1.X;
            var r1y = r1.Y;
            var r2x = r2.X;
            var r2y = r2.Y;
            for (var j = 0; j < poly.Length; j++)
            {
                var p1 = poly[j];
                var p2 = poly[(j + 1) % poly.Length];
                if (SegmentsIntersect(r1x, r1y, r2x, r2y, p1.X, p1.Y, p2.X, p2.Y))
                    return true;
            }
        }

        return false;
    }

    private static bool SegmentsIntersect(
        double ax, double ay, double bx, double by,
        double cx, double cy, double dx, double dy)
    {
        const double eps = 1e-9;
        var denom = (dy - cy) * (bx - ax) - (dx - cx) * (by - ay);
        var na = (dx - cx) * (ay - cy) - (dy - cy) * (ax - cx);
        var nb = (bx - ax) * (ay - cy) - (by - ay) * (ax - cx);

        if (Math.Abs(denom) < eps)
            return false;

        var ua = na / denom;
        var ub = nb / denom;
        return ua >= -eps && ua <= 1 + eps && ub >= -eps && ub <= 1 + eps;
    }

    public Rect GetBoundingRect()
    {
        if (Points.Count == 0)
            return new Rect(0, 0, 0, 0);
        
        var cvPoints = Points.Select(p => new Point(p.X, p.Y)).ToArray();
        return Cv2.BoundingRect(cvPoints);
    }

    public static Region FromPoints(List<List<int>> pointsList, string name, Scalar? color = null, int thickness = 2)
    {
        var points = pointsList.Select(p => new SysPoint(p[0], p[1])).ToList();
        
        return new Region
        {
            Name = name,
            Points = points,
            Color = color ?? new Scalar(0, 255, 0),
            Thickness = thickness
        };
    }
}
