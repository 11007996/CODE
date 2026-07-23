using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Speech.Synthesis;
using System.Text;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification.Speaker;

/// <summary>
/// 本地喇叭通知：固定音频文件播放，或系统文字转语音（TTS），二选一。
/// </summary>
public class SpeakerNotificationService : INotificationService
{
    private enum SpeakerOutputMode
    {
        FixedAudio,
        Tts
    }

    private readonly ILogger<SpeakerNotificationService> _logger;

    private SpeakerOutputMode _mode = SpeakerOutputMode.FixedAudio;
    private string? _soundFilePath;
    private int _repeatCount = 2;
    private int _repeatGapMs = 300;
    private double _cooldownSeconds = 1.0;
    private DateTime _lastPlayUtc = DateTime.MinValue;

    private bool _ttsUseNotificationText = true;
    private string? _ttsFixedText;
    private string? _ttsVoice;

    public string NotificationType => "Speaker";

    public SpeakerNotificationService(ILogger<SpeakerNotificationService> logger)
    {
        _logger = logger;
    }

    public List<NotificationParameterDefinition> GetRequiredParameters() =>
        [.. SpeakerNotificationDescriptor.Definitions];

    public void Initialize(Dictionary<string, object> config)
    {
        _soundFilePath = ReadString(config, "soundFile", "sound_file");
        _repeatCount = Math.Clamp(ReadInt(config, "repeatCount", 2), 1, 20);
        _repeatGapMs = Math.Clamp(ReadInt(config, "repeatGapMs", 300), 0, 10_000);
        _cooldownSeconds = ReadDouble(config, "cooldownSeconds", 1.0);

        _mode = ReadSpeakerMode(config);

        _ttsUseNotificationText = ReadBool(config, "ttsUseNotificationText", true);
        _ttsFixedText = ReadString(config, "ttsFixedText", "tts_fixed_text");
        _ttsVoice = ReadString(config, "ttsVoice", "tts_voice");

        if (_mode == SpeakerOutputMode.FixedAudio)
        {
            NotificationChannelInitRules.ThrowIfNullOrWhiteSpace(
                _soundFilePath,
                "喇叭为固定音频模式（speakerMode=fixedAudio 等）时须配置 soundFile（本地音频文件路径）。");
        }
        else if (_mode == SpeakerOutputMode.Tts && !_ttsUseNotificationText)
        {
            NotificationChannelInitRules.ThrowIfNullOrWhiteSpace(
                _ttsFixedText,
                "喇叭为 TTS 且关闭「使用通知正文」（ttsUseNotificationText=false）时须配置 ttsFixedText。");
        }

        _logger.LogInformation(
            "喇叭通知已初始化：Mode={Mode}, SoundFile={SoundFile}, Repeat={Repeat}x, Cooldown={Cd}s",
            _mode,
            string.IsNullOrEmpty(_soundFilePath) ? "(无)" : _soundFilePath,
            _repeatCount,
            _cooldownSeconds);
    }

    public Task<bool> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        if ((now - _lastPlayUtc).TotalSeconds < _cooldownSeconds)
        {
            _logger.LogDebug("喇叭通知处于冷却中，跳过");
            return Task.FromResult(true);
        }

        _lastPlayUtc = now;

        var title = message.Title;
        var level = message.Level;
        _logger.LogInformation("喇叭告警：Level={Level}, Title={Title}", level, title);

        return Task.Run(() =>
        {
            try
            {
                for (var i = 0; i < _repeatCount; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (_mode == SpeakerOutputMode.Tts)
                    {
                        var text = BuildSpeakText(message);
                        if (string.IsNullOrWhiteSpace(text))
                            _logger.LogWarning("TTS 模式但无可朗读文本，已跳过本轮");
                        else if (!TrySpeakText(text))
                            _logger.LogWarning("文字转语音失败");
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(_soundFilePath))
                            _logger.LogWarning("固定音频模式但未配置 soundFile，已跳过本轮");
                        else if (!File.Exists(_soundFilePath))
                            _logger.LogWarning("固定音频文件不存在：{Path}", _soundFilePath);
                        else if (!TryPlaySoundFile(_soundFilePath))
                            _logger.LogWarning("提示音文件播放失败：{Path}", _soundFilePath);
                    }

                    if (i < _repeatCount - 1 && _repeatGapMs > 0)
                        Thread.Sleep(_repeatGapMs);
                }

                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "喇叭通知执行异常");
                return false;
            }
        }, cancellationToken);
    }

    /// <summary>
    /// 优先 <c>speakerMode</c> / <c>mode</c>；未设置时兼容旧配置 <c>ttsEnabled</c>。
    /// </summary>
    private static SpeakerOutputMode ReadSpeakerMode(Dictionary<string, object> config)
    {
        var raw = ReadString(config, "speakerMode", "mode", "speaker_mode");
        if (!string.IsNullOrEmpty(raw))
        {
            if (IsTtsMode(raw))
                return SpeakerOutputMode.Tts;
            if (IsFixedAudioMode(raw))
                return SpeakerOutputMode.FixedAudio;
        }

        if (ReadBool(config, "ttsEnabled", false))
            return SpeakerOutputMode.Tts;

        return SpeakerOutputMode.FixedAudio;
    }

    private static bool IsTtsMode(string s) =>
        s.Equals("tts", StringComparison.OrdinalIgnoreCase)
        || s.Equals("voice", StringComparison.OrdinalIgnoreCase);

    private static bool IsFixedAudioMode(string s) =>
        s.Equals("fixedAudio", StringComparison.OrdinalIgnoreCase)
        || s.Equals("fixed_audio", StringComparison.OrdinalIgnoreCase)
        || s.Equals("audio", StringComparison.OrdinalIgnoreCase)
        || s.Equals("sound", StringComparison.OrdinalIgnoreCase);

    private string BuildSpeakText(NotificationMessage message)
    {
        if (_ttsUseNotificationText)
        {
            var parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(message.Title))
                parts.Add(message.Title.Trim());
            if (!string.IsNullOrWhiteSpace(message.Content))
                parts.Add(message.Content.Trim());
            return string.Join("，", parts);
        }

        return _ttsFixedText?.Trim() ?? "";
    }

    /// <summary>
    /// Windows 优先使用进程内 <see cref="SpeechSynthesizer"/>，避免每次启动 PowerShell 加载程序集带来的 1～3 秒延迟；
    /// 其它平台仍通过临时文件调用外部 TTS。
    /// </summary>
    private bool TrySpeakText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        text = text.Trim();
        if (text.Length > 4000)
            text = text.Substring(0, 4000);

        try
        {
            if (OperatingSystem.IsWindows())
            {
                if (SpeakWindowsInProcess(text))
                    return true;
                _logger.LogDebug("进程内 TTS 未成功，回退到 PowerShell + 临时文件");
                return SpeakWindowsTtsViaTempFile(text);
            }

            return SpeakViaTempFileNonWindows(text);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "文字转语音失败");
            return false;
        }
    }

    /// <summary>
    /// 使用当前进程内的 SAPI，无 PowerShell 冷启动开销。
    /// </summary>
    [SupportedOSPlatform("windows")]
    private bool SpeakWindowsInProcess(string text)
    {
        try
        {
            using var synth = new SpeechSynthesizer();
            if (!string.IsNullOrWhiteSpace(_ttsVoice))
            {
                try
                {
                    synth.SelectVoice(_ttsVoice);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex, "无法选用语音 {Voice}，将使用系统默认", _ttsVoice);
                }
            }

            synth.Speak(text);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "进程内 TTS 异常");
            return false;
        }
    }

    private bool SpeakWindowsTtsViaTempFile(string text)
    {
        var tmp = Path.Combine(Path.GetTempPath(), $"luxvd_tts_{Guid.NewGuid():N}.txt");
        try
        {
            File.WriteAllText(tmp, text, Encoding.UTF8);
            return SpeakWindowsTtsFile(tmp);
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

    private bool SpeakViaTempFileNonWindows(string text)
    {
        var tmp = Path.Combine(Path.GetTempPath(), $"luxvd_tts_{Guid.NewGuid():N}.txt");
        try
        {
            File.WriteAllText(tmp, text, Encoding.UTF8);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                if (!string.IsNullOrWhiteSpace(_ttsVoice))
                    return RunAndWaitMultiple("say", new[] { "-v", _ttsVoice!, "-f", tmp }, TimeSpan.FromMinutes(2));
                return RunAndWaitMultiple("say", new[] { "-f", tmp }, TimeSpan.FromMinutes(2));
            }

            // Linux / 其他非 macOS 的 Unix：espeak-ng；ttsVoice 映射为 -v（语言/变体），如越南语 vi、中文 zh、英文 en
            var espeakArgs = string.IsNullOrWhiteSpace(_ttsVoice)
                ? new[] { "-f", tmp }
                : new[] { "-v", _ttsVoice!, "-f", tmp };
            if (RunAndWaitMultiple("espeak-ng", espeakArgs, TimeSpan.FromMinutes(2)))
                return true;
            if (RunAndWaitMultiple("espeak", espeakArgs, TimeSpan.FromMinutes(2)))
                return true;

            _logger.LogWarning("未找到可用的 TTS（Linux 请安装 espeak-ng 或 espeak）");
            return false;
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

    private static bool SpeakWindowsTtsFile(string utf8FilePath)
    {
        var escaped = utf8FilePath.Replace("'", "''");
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
        if (p == null) return false;
        return p.WaitForExit(120_000) && p.ExitCode == 0;
    }

    private bool TryPlaySoundFile(string path)
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return PlayOnWindows(path);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return RunAndWait("afplay", path, TimeSpan.FromSeconds(60));

            // Linux 及其他：优先 paplay（PulseAudio），其次 aplay
            if (RunAndWait("paplay", path, TimeSpan.FromSeconds(60)))
                return true;
            if (RunAndWait("aplay", path, TimeSpan.FromSeconds(60)))
                return true;

            _logger.LogWarning("未找到 paplay/aplay，无法播放文件：{Path}", path);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "播放提示音失败：{Path}", path);
            return false;
        }
    }

    private bool PlayOnWindows(string path)
    {
        // 仅对 .wav 使用 SoundPlayer；其它格式可安装 ffmpeg 后自行扩展
        if (!path.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("Windows 下内置播放仅保证 .wav，当前扩展名可能无法播放：{Path}", path);
        }

        var escaped = path.Replace("'", "''");
        var psi = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-NoProfile -NonInteractive -Command \"(New-Object System.Media.SoundPlayer '{escaped}').PlaySync()\"",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardError = true
        };

        using var p = Process.Start(psi);
        if (p == null) return false;
        return p.WaitForExit(120_000) && p.ExitCode == 0;
    }

    private static bool RunAndWait(string fileName, string argument, TimeSpan timeout)
    {
        return RunAndWaitMultiple(fileName, new[] { argument }, timeout);
    }

    private static bool RunAndWaitMultiple(string fileName, string[] arguments, TimeSpan timeout)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true
            };
            foreach (var a in arguments)
                psi.ArgumentList.Add(a);

            using var p = Process.Start(psi);
            if (p == null) return false;
            return p.WaitForExit((int)timeout.TotalMilliseconds) && p.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }

    private static bool ReadBool(Dictionary<string, object> config, string key, bool defaultValue)
    {
        if (!config.TryGetValue(key, out var v) || v == null)
            return defaultValue;
        if (v is bool b)
            return b;
        var s = v.ToString()?.Trim();
        if (string.IsNullOrEmpty(s))
            return defaultValue;
        if (bool.TryParse(s, out var parsed))
            return parsed;
        if (s == "1")
            return true;
        if (s == "0")
            return false;
        if (s.Equals("yes", StringComparison.OrdinalIgnoreCase))
            return true;
        if (s.Equals("no", StringComparison.OrdinalIgnoreCase))
            return false;
        return defaultValue;
    }

    private static string? ReadString(Dictionary<string, object> config, params string[] keys)
    {
        foreach (var key in keys)
        {
            if (config.TryGetValue(key, out var v) && v != null)
            {
                var s = v.ToString();
                if (!string.IsNullOrWhiteSpace(s))
                    return s.Trim();
            }
        }

        return null;
    }

    private static int ReadInt(Dictionary<string, object> config, string key, int defaultValue)
    {
        if (!config.TryGetValue(key, out var v) || v == null)
            return defaultValue;
        try
        {
            return Convert.ToInt32(v, System.Globalization.CultureInfo.InvariantCulture);
        }
        catch
        {
            return defaultValue;
        }
    }

    private static double ReadDouble(Dictionary<string, object> config, string key, double defaultValue)
    {
        if (!config.TryGetValue(key, out var v) || v == null)
            return defaultValue;
        try
        {
            return Convert.ToDouble(v, System.Globalization.CultureInfo.InvariantCulture);
        }
        catch
        {
            return defaultValue;
        }
    }
}
