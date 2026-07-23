using LuxVideoDet.Core.Common;

namespace LuxVideoDet.Core.VideoSource;

public interface IVideoSource : IDisposable
{
    string SourceType { get; }
    bool IsOpened { get; }
    bool IsLoop { get; }

    void Open();
    Frame? ReadFrame();
    void Reset();
    void Close();

    int GetWidth();
    int GetHeight();
    double GetFps();
}
