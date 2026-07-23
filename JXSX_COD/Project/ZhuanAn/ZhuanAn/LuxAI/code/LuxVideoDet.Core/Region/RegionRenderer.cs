using LuxVideoDet.Core.Utils;
using OpenCvSharp;
using SysPoint = System.Drawing.Point;

namespace LuxVideoDet.Core.Region;

public static class RegionRenderer
{
    public static void DrawRegion(Mat image, Region region)
    {
        if (region.Points.Count < 2) return;

        var cvPoints = region.Points.Select(p => new Point(p.X, p.Y)).ToArray();
        Cv2.Polylines(image, [cvPoints], true, region.Color, region.Thickness);
    }

    public static void DrawRegions(Mat image, IEnumerable<Region> regions)
    {
        foreach (var region in regions)
        {
            DrawRegion(image, region);
            DrawRegionName(image, region);
        }
    }

    public static void DrawRegionName(Mat image, Region region)
    {
        if (region.Points.Count == 0) return;

        var centerX = region.Points.Average(p => p.X);
        var centerY = region.Points.Average(p => p.Y);
        var centerPos = new SysPoint((int)centerX, (int)centerY);
        
        ChineseTextRenderer.DrawChineseText(
            image, 
            region.Name, 
            centerPos,
            fontSize: 20,
            color: region.Color,
            lineSpacing: 6,
            maxWidth: null,
            align: "left"
        );
    }
}
