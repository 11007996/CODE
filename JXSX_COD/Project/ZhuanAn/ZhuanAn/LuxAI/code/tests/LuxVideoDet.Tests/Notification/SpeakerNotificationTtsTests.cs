using System.Diagnostics;
using System.Text;
using LuxVideoDet.Core.Notification;
using LuxVideoDet.Core.Notification.Speaker;
using Microsoft.Extensions.Logging.Abstractions;

namespace LuxVideoDet.Tests.Notification;

/// <summary>
/// <see cref="SpeakerNotificationService"/> 的 TTS 相关测试：含可移植的配置/行为断言，
/// 以及仅在 Windows 上执行的 System.Speech 冒烟测试（与本机喇叭 TTS 实现一致）。
/// </summary>
public class SpeakerNotificationTtsTests
{
    private static SpeakerNotificationService CreateService()
    {
        return new SpeakerNotificationService(NullLogger<SpeakerNotificationService>.Instance);
    }

    [Theory]
    [InlineData("speakerMode", "tts")]
    [InlineData("mode", "TTS")]
    [InlineData("speaker_mode", "voice")]
    public async Task SendAsync_TtsMode_AcceptsModeConfigKeys(string key, string mode)
    {
        var svc = CreateService();
        svc.Initialize(new Dictionary<string, object>
        {
            [key] = mode,
            ["ttsUseNotificationText"] = false,
            ["ttsFixedText"] = "测",
            ["repeatCount"] = 1,
            ["repeatGapMs"] = 0,
            ["cooldownSeconds"] = 0.0
        });

        var ok = await svc.SendAsync(new NotificationMessage());
        Assert.True(ok);
    }

    [Fact]
    public async Task SendAsync_TtsMode_LegacyTtsEnabledTrue_UsesTts()
    {
        var svc = CreateService();
        svc.Initialize(new Dictionary<string, object>
        {
            ["ttsEnabled"] = true,
            ["ttsUseNotificationText"] = false,
            ["ttsFixedText"] = "测",
            ["repeatCount"] = 1,
            ["repeatGapMs"] = 0,
            ["cooldownSeconds"] = 0.0
        });

        var ok = await svc.SendAsync(new NotificationMessage());
        Assert.True(ok);
    }

    [Fact]
    public async Task SendAsync_TtsMode_EmptyText_ReturnsTrue()
    {
        var svc = CreateService();
        svc.Initialize(new Dictionary<string, object>
        {
            ["speakerMode"] = "tts",
            ["repeatCount"] = 1,
            ["repeatGapMs"] = 0,
            ["cooldownSeconds"] = 0.0
        });

        var ok = await svc.SendAsync(new NotificationMessage { Title = "", Content = "" });
        Assert.True(ok);
    }

    [Fact]
    public async Task SendAsync_TtsMode_FixedText_ReturnsTrue()
    {
        var svc = CreateService();
        svc.Initialize(new Dictionary<string, object>
        {
            ["speakerMode"] = "tts",
            ["ttsUseNotificationText"] = false,
            ["ttsFixedText"] = "单元测试",
            ["repeatCount"] = 1,
            ["repeatGapMs"] = 0,
            ["cooldownSeconds"] = 0.0
        });

        var ok = await svc.SendAsync(new NotificationMessage { Title = "x", Content = "y" });
        Assert.True(ok);
    }

    /// <summary>
    /// 与 <see cref="SpeakerNotificationService"/> 中 Windows TTS 相同调用方式，
    /// 用于在本机验证 System.Speech 是否可用（CI 无音频也可 exit 0）。
    /// 非 Windows 环境跳过执行（测试仍通过）。
    /// </summary>
    [Fact]
    public void Windows_SystemSpeech_TtsSmoke_ExitsSuccessfully()
    {
        if (!OperatingSystem.IsWindows())
            return;

        var text = "LuxVideoDet TTS smoke test.";
        var tmp = Path.Combine(Path.GetTempPath(), $"luxvd_tts_smoke_{Guid.NewGuid():N}.txt");
        try
        {
            File.WriteAllText(tmp, text, Encoding.UTF8);
            var escaped = tmp.Replace("'", "''");
            var psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments =
                    "-NoProfile -NonInteractive -Command \"Add-Type -AssemblyName System.Speech; " +
                    "$s = New-Object System.Speech.Synthesis.SpeechSynthesizer; " +
                    "$s.Speak([System.IO.File]::ReadAllText('" + escaped + "', [System.Text.UTF8Encoding]::new($false)))\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true
            };

            using var p = Process.Start(psi);
            Assert.NotNull(p);
            var finished = p.WaitForExit(120_000);
            Assert.True(finished, "System.Speech TTS 未在超时内结束");
            Assert.Equal(0, p.ExitCode);
        }
        finally
        {
            try
            {
                if (File.Exists(tmp))
                    File.Delete(tmp);
            }
            catch
            {
                // ignore
            }
        }
    }
}
