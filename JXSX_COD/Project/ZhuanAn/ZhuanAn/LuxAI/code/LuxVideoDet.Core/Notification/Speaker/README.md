# 喇叭（本地）通知

## 作用

在**运行 LuxVideoDet 的机器本地**发出可听提示，适合机房、监控室等需要**即时听觉告警**的场景，不依赖外网或第三方服务。

支持两种输出方式（二选一）：

| 模式 | 说明 |
|------|------|
| **固定音频** (`fixedAudio`) | 播放本地文件作为提示音（如 `.wav`）。 |
| **文字转语音 TTS** (`tts`) | 用系统语音朗读告警内容或固定文案。 |

告警触发时可配置**重复播放次数**、**重复间隔**以及**冷却时间**，避免短时间内连续刷屏。

## 典型场景

- 检测异常时本地播放警报声或语音说明，无需看手机。
- 离线环境或不便使用网络通知时的兜底方式。

## 实现要点

- Windows：固定音频优先通过 PowerShell 播放；TTS 优先使用进程内 `SpeechSynthesizer`，失败时可回退到临时文件 + PowerShell。
- macOS：音频可用 `afplay`；TTS 使用系统 `say`。
- Linux：音频可用 `paplay` / `aplay`；TTS 依赖 `espeak-ng` 或 `espeak`。

### 多语言（越南语 / 中文等）

TTS **不会**根据文本自动猜语言：若未配置 **`ttsVoice`**，Linux 上 `espeak-ng` 会用默认语言读 UTF-8 文本，越南语等可能听起来像“按字拼读”或带错误口音。

- **Linux**：在通知参数里设置 `ttsVoice` 为 `espeak-ng` 的 **`-v` 语音代码**（可用 `espeak-ng --voices` 查看）。例如越南语常用 **`vi`**，中文常用 **`zh`**，英文 **`en`**。
- **Windows**：需安装对应语言的 **OneCore 或 SAPI 语音**，再把 `ttsVoice` 设为**系统里该语音的完整名称**（在“设置 → 语音”或 `SpeechSynthesizer.GetInstalledVoices()` 可查）。
- **macOS**：`ttsVoice` 传给 `say -v`，需为系统已安装的语音名（终端 `say -v '?'` 列出）。

配置项与参数说明见 `SpeakerNotificationDescriptor` 中的 `Definitions`（如 `speakerMode`、`soundFile`、`repeatCount`、`cooldownSeconds`、`ttsUseNotificationText`、`ttsVoice` 等）。
