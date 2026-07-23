using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.VideoSource.Camera;
using LuxVideoDet.Core.VideoSource.LocalVideo;
using LuxVideoDet.Core.VideoSource.Rtsp;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.VideoSource;

public class VideoSourceFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<VideoSourceFactory> _logger;

    public VideoSourceFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _logger = loggerFactory.CreateLogger<VideoSourceFactory>();
    }

    public IVideoSource CreateVideoSource(VideoSourceConfig config)
    {
        _logger.LogInformation("[视频源·配置] 类型={Type} | 来源={Source}",
            config.Type, config.Source);

        var source = config.Type switch
        {
            VideoSourceType.LocalVideo => new LocalVideoSource(
                config.Source, 
                config.Loop, 
                _loggerFactory.CreateLogger<LocalVideoSource>()) as IVideoSource,
            
            VideoSourceType.Camera => CreateCameraSource(config),
            
            VideoSourceType.Rtsp => new RtspSource(
                config.Source, 
                _loggerFactory.CreateLogger<RtspSource>()),
            
            _ => throw new ArgumentException($"不支持的视频源类型: {config.Type}")
        };

        return source;
    }

    private CameraSource CreateCameraSource(VideoSourceConfig config)
    {
        if (!int.TryParse(config.Source, out var cameraId))
        {
            throw new ArgumentException($"无效的摄像头 ID: {config.Source}");
        }

        return new CameraSource(cameraId, _loggerFactory.CreateLogger<CameraSource>());
    }
}
