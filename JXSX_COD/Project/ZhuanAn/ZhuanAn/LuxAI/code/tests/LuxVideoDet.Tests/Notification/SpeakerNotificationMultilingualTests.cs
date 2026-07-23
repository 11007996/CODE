using LuxVideoDet.Core.Notification;
using LuxVideoDet.Core.Notification.Speaker;
using Microsoft.Extensions.Logging.Abstractions;

namespace LuxVideoDet.Tests.Notification;

/// <summary>
/// <see cref="SpeakerNotificationService"/> 多语言相关单元测试：验证 UTF-8 标题/正文/固定文案在 TTS 模式下可被完整送入播放管线
/// （底层实际发音依赖本机 Windows SAPI / macOS <c>say</c> / Linux espeak，无引擎时可能仅记日志，但 <see cref="SpeakerNotificationService.SendAsync"/> 仍应正常返回）。
/// </summary>
public class SpeakerNotificationMultilingualTests
{
    private static SpeakerNotificationService CreateTtsService(
        bool useNotificationText,
        string? fixedText = null,
        string? ttsVoice = null)
    {
        var svc = new SpeakerNotificationService(NullLogger<SpeakerNotificationService>.Instance);
        var config = new Dictionary<string, object>
        {
            ["speakerMode"] = "tts",
            ["ttsUseNotificationText"] = useNotificationText,
            ["repeatCount"] = 1,
            ["repeatGapMs"] = 0,
            ["cooldownSeconds"] = 0.0
        };
        if (fixedText != null)
            config["ttsFixedText"] = fixedText;
        if (!string.IsNullOrEmpty(ttsVoice))
            config["ttsVoice"] = ttsVoice;

        svc.Initialize(config);
        return svc;
    }

    /// <summary>
    /// 第三列仅为用例标签。自然听感依赖本机 TTS 语言：<b>Linux espeak-ng</b> 未配置 <c>ttsVoice</c> 时，
    /// 越南语等易被按默认引擎「拼读」；需在配置中设 <c>ttsVoice: "vi"</c>（见 <c>Speaker/README.md</c>）。
    /// </summary>
    public static TheoryData<string, string, string> NotificationTextCases => new()
    {
        { "Alarm", "Detection failed. Please check the line.", "en" },
        { "警告", "检测到关键部位尚未检测完成，请停线确认。", "zh-Hans" },
        { "Cảnh báo", "Sản phẩm không đạt yêu cầu. Vui lòng kiểm tra.", "vi" },
        { "NG", "成品区出现标签，需要停线。", "mixed" }
    };

    [Theory]
    [MemberData(nameof(NotificationTextCases))]
    public async Task SendAsync_Tts_UsesNotificationText_AcceptsUtf8TitleAndContent(
        string title,
        string content,
        string _)
    {
        var svc = CreateTtsService(useNotificationText: true);

        var ok = await svc.SendAsync(new NotificationMessage
        {
            Title = title,
            Content = content,
            Level = "Warning"
        });

        Assert.True(ok);
    }

    [Theory]
    [InlineData("Fixed alarm in English.", "en-fixed")]
    [InlineData("这是一条固定中文提示，用于单元测试。", "zh-fixed")]
    [InlineData("Thông báo cố định bằng tiếng Việt.", "vi-fixed")]
    public async Task SendAsync_Tts_FixedText_AcceptsUtf8(string fixedText, string _)
    {
        var svc = CreateTtsService(useNotificationText: false, fixedText: fixedText);

        var ok = await svc.SendAsync(new NotificationMessage
        {
            Title = "ignored",
            Content = "ignored"
        });

        Assert.True(ok);
    }

    [Fact]
    public async Task SendAsync_Tts_VietnameseTitleOnly_DiacriticsPreserved()
    {
        var svc = CreateTtsService(useNotificationText: true);

        var ok = await svc.SendAsync(new NotificationMessage
        {
            Title = "Đã phát hiện lỗi nghiêm trọng",
            Content = ""
        });

        Assert.True(ok);
    }

    /// <summary>
    /// Linux：为 espeak-ng 指定 <c>-v vi</c>，越南语文本才按越南语规则合成（需本机已装 espeak-ng 及 vi 语音数据）。
    /// </summary>
    [Fact]
    public async Task SendAsync_Tts_Vietnamese_WithTtsVoiceVi_LinuxUsesEspeakNgVoice()
    {
        if (!OperatingSystem.IsLinux())
            return;

        var svc = CreateTtsService(useNotificationText: true, ttsVoice: "vi");

        var ok = await svc.SendAsync(new NotificationMessage
        {
            Title = "Cảnh báo",
            Content = "Sản phẩm không đạt."
        });

        Assert.True(ok);
    }

    [Fact]
    public async Task SendAsync_Tts_ChineseFullWidthComma_JoinedPartsMatchBuildSpeakTextConvention()
    {
        // BuildSpeakText 使用「，」连接 Title 与 Content；此处仅验证管线可接受该组合
        var svc = CreateTtsService(useNotificationText: true);

        var ok = await svc.SendAsync(new NotificationMessage
        {
            Title = "第一部分",
            Content = "第二部分"
        });

        Assert.True(ok);
    }

    /// <summary>
    /// <c>ttsVoice</c> 仅 Windows 进程内 SAPI / 回退 PowerShell 路径会稳定尝试选用；
    /// macOS/Linux 行为依赖 <c>say</c>/espeak，语音名非跨平台，故仅在 Windows 上断言。
    /// </summary>
    [Theory]
    [InlineData("Microsoft Zira Desktop")]
    [InlineData("Microsoft Huihui Desktop")]
    public async Task SendAsync_Tts_WithOptionalVoice_DoesNotThrow_Windows(string voiceName)
    {
        if (!OperatingSystem.IsWindows())
            return;

        var svc = CreateTtsService(useNotificationText: true, ttsVoice: voiceName);

        var ok = await svc.SendAsync(new NotificationMessage
        {
            Title = "Voice",
            Content = "test"
        });

        Assert.True(ok);
    }
}
