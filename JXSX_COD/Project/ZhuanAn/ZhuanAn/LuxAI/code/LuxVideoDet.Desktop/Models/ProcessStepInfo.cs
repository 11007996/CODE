namespace LuxVideoDet.Desktop.Models
{
    public class ProcessStepInfo
    {
        // 显示文本
        public string DisplayText { get; set; } = string.Empty;
        // 前缀 OK / >> / --
        public string Prefix { get; set; } = string.Empty;
        // 颜色十六进制 #FFFF00 / #00C800 / #646464
        public string ColorHex { get; set; } = "#646464";
        // 字号
        //public double FontSize { get; set; } = 22;
    }
}
