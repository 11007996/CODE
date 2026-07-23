using LuxVideoDet.Core.Notification;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification.Speaker;

/// <summary>
/// 本地喇叭通知 — 注册元数据与实例创建（供工厂反射发现）。
/// </summary>
public sealed class SpeakerNotificationDescriptor : INotificationDescriptor
{
    public string TypeKey => "speaker";

    public string DisplayName => "喇叭（本地）";

    public IReadOnlyList<NotificationParameterDefinition> ParameterDefinitions => Definitions;

    /// <summary>供 <see cref="SpeakerNotificationService.GetRequiredParameters"/> 与工厂共用同一份定义。</summary>
    public static readonly List<NotificationParameterDefinition> Definitions =
    [
        new NotificationParameterDefinition
        {
            Name = "speakerMode",
            DisplayName = "输出模式",
            Description =
                "fixedAudio：仅播放下方「提示音文件」；tts：仅系统 TTS 朗读。也可写 mode。未写 speakerMode 时仍可读旧配置 ttsEnabled（true=tts）。",
            ParameterType = "string",
            DefaultValue = "fixedAudio",
            Required = false,
            Example = "fixedAudio 或 tts"
        },
        new NotificationParameterDefinition
        {
            Name = "soundFile",
            DisplayName = "提示音文件",
            Description = "固定音频模式下的本地文件绝对路径。Windows/macOS 常用 .wav；macOS 也可用系统自带路径。",
            ParameterType = "string",
            DefaultValue = "",
            Required = false,
            Example = @"C:\Sounds\alarm.wav 或 /System/Library/Sounds/Glass.aiff"
        },
        new NotificationParameterDefinition
        {
            Name = "repeatCount",
            DisplayName = "重复次数",
            Description = "一轮告警内播放或朗读的次数",
            ParameterType = "int",
            DefaultValue = 2,
            Required = false,
            Example = "2"
        },
        new NotificationParameterDefinition
        {
            Name = "repeatGapMs",
            DisplayName = "重复间隔（毫秒）",
            ParameterType = "int",
            DefaultValue = 300,
            Required = false,
            Example = "300"
        },
        new NotificationParameterDefinition
        {
            Name = "cooldownSeconds",
            DisplayName = "冷却时间（秒）",
            Description = "两次告警之间的最小间隔，避免连续触发",
            ParameterType = "double",
            DefaultValue = 1.0,
            Required = false,
            Example = "1.0"
        },
        new NotificationParameterDefinition
        {
            Name = "ttsUseNotificationText",
            DisplayName = "TTS 使用通知正文",
            Description = "TTS 模式下：为 true 时朗读本次通知标题+内容；为 false 时仅朗读「TTS 固定文案」",
            ParameterType = "bool",
            DefaultValue = true,
            Required = false,
            Example = "true"
        },
        new NotificationParameterDefinition
        {
            Name = "ttsFixedText",
            DisplayName = "TTS 固定文案",
            Description = "TTS 模式且「TTS 使用通知正文」为 false 时朗读此段文字",
            ParameterType = "string",
            DefaultValue = "",
            Required = false,
            Example = "检测异常，请现场查看"
        },
        new NotificationParameterDefinition
        {
            Name = "ttsVoice",
            DisplayName = "TTS 语音（可选）",
            Description =
                "Windows：SAPI 已安装语音的完整名称（如中文 Microsoft Huihui Desktop）。macOS：「say -v」名称（如 Ting-Ting）。Linux espeak-ng：「-v」语言/变体代码（如越南语 vi、中文 zh、英文 en）；不填则用引擎默认，多语言文案可能被按错误语言拼读。",
            ParameterType = "string",
            DefaultValue = "",
            Required = false,
            Example = "vi（Linux 越南语）或 Ting-Ting（macOS）"
        }
    ];

    public INotificationService Create(NotificationServiceFactoryContext context)
    {
        return new SpeakerNotificationService(
            context.LoggerFactory.CreateLogger<SpeakerNotificationService>());
    }
}
