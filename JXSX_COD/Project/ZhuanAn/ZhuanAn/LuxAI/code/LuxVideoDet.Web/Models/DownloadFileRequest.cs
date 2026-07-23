namespace LuxVideoDet.Web.Models;

public class DownloadFileRequest
{
    public string RemotePath { get; set; } = string.Empty;
    public string FileType { get; set; } = "plugin";
}
