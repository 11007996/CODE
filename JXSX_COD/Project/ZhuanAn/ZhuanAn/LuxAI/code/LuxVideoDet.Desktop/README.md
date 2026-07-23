# LuxVideoDet.Desktop

LuxVideoDet 桌面客户端 — 基于 Avalonia UI 的跨平台视频检测应用。

## 技术栈

| 组件 | 技术 |
|------|------|
| UI 框架 | Avalonia 11.3 (FluentTheme) |
| 架构模式 | MVVM (CommunityToolkit.Mvvm) |
| 依赖注入 | Microsoft.Extensions.DependencyInjection |
| 日志 | Serilog |
| 图像处理 | OpenCvSharp4 → Avalonia WriteableBitmap |
| 目标框架 | .NET 8.0 |

## 项目结构

```
LuxVideoDet.Desktop/
├── Assets/                  # 图标与静态资源
├── Models/                  # 数据模型与展示模型
│   ├── DetectionSession.cs        # 检测会话状态
│   ├── DetectionResultEventArgs.cs# 检测结果事件参数
│   ├── PerformanceData.cs         # 系统性能快照
│   ├── LogEntry.cs                # 日志条目（可观察，用于 DataGrid 绑定）
│   └── RecentDetection.cs         # 最近检测记录（可观察，用于列表绑定）
├── Services/                # 桌面端服务
│   ├── DetectionService.cs        # 检测会话管理（多算法池）
│   ├── ImageRenderService.cs      # OpenCV Mat → WriteableBitmap
│   ├── LogService.cs              # 日志收集与过滤
│   └── PerformanceMonitorService.cs # CPU/内存/GPU 实时监控
├── ViewModels/              # MVVM ViewModels
│   ├── MainWindowViewModel.cs     # 主窗口逻辑
│   ├── ConfigEditorViewModel.cs   # 配置编辑器逻辑
│   ├── ConfigItemViewModel.cs     # 配置卡片项
│   ├── AlgorithmConfigViewModel.cs# 算法参数配置
│   └── RegionEditorViewModel.cs   # 区域可视化编辑
├── Views/                   # Avalonia AXAML 视图
│   ├── MainWindow.axaml           # 主界面（三栏布局）
│   ├── ConfigEditorWindow.axaml   # 配置编辑对话框
│   ├── RegionEditorWindow.axaml   # 区域编辑对话框
│   └── FullscreenPreviewWindow.axaml # 全屏预览
├── App.axaml                # 应用入口与全局样式
├── ServiceConfiguration.cs  # DI 容器注册
├── ViewLocator.cs           # ViewModel → View 自动映射
└── Program.cs               # 程序入口
```

## 界面布局

主窗口采用**三栏 + 底部**布局：

| 区域 | 说明 |
|------|------|
| 顶部工具栏 | Logo、快捷操作（新建/导入/全部启动/停止）、主题切换、设置 |
| 左侧面板 | 配置列表搜索、配置卡片（启动/编辑/删除） |
| 中央预览 | 实时视频画面、检测框/区域/标签叠加层、全屏与截图 |
| 右侧面板 | 系统状态、性能指标（CPU/内存/GPU/FPS）、最近检测、今日统计 |
| 底部面板 | 可折叠日志查看器（级别过滤 + 关键字搜索 + 导出） |

## 功能列表

### 配置管理
- 新建检测配置（视频源 + 算法池 + 区域 + 通知）
- 编辑/删除已有配置
- 从 JSON 文件导入配置
- 配置搜索过滤

### 检测引擎
- 单配置支持多算法并行（算法池）
- 支持 RTSP/本地视频/摄像头 视频源
- 实时帧处理与 FPS 统计

### 实时预览
- OpenCV Mat 到 Avalonia WriteableBitmap 高效渲染
- 检测框 / 区域 / 标签 叠加层切换
- 全屏预览（ESC 退出）
- 一键截图保存 PNG/JPEG

### 性能监控
- CPU / 内存使用率实时进度条
- GPU 使用率（如可用）
- 平均帧率统计
- 运行时间计时

### 日志系统
- 可折叠日志面板
- 按级别过滤（Debug / Info / Warning / Error）
- 关键字搜索
- 一键清空 / 导出为文本文件

### 主题
- 浅色 / 深色主题一键切换

## 运行

```bash
# 从解决方案根目录
dotnet run --project LuxVideoDet.Desktop

# 或进入项目目录
cd LuxVideoDet.Desktop
dotnet run
```

## 构建

```bash
# Debug
dotnet build LuxVideoDet.Desktop

# Release
dotnet build LuxVideoDet.Desktop -c Release

# 发布独立可执行文件 (Windows x64)
dotnet publish LuxVideoDet.Desktop -c Release -r win-x64 --self-contained

# 发布独立可执行文件 (Linux x64)
dotnet publish LuxVideoDet.Desktop -c Release -r linux-x64 --self-contained
```

## 依赖

| 包 | 版本 | 用途 |
|----|------|------|
| Avalonia | 11.3.12 | UI 框架 |
| Avalonia.Desktop | 11.3.12 | 桌面运行时 |
| Avalonia.Themes.Fluent | 11.3.12 | Fluent 主题 |
| Avalonia.Fonts.Inter | 11.3.12 | Inter 字体 |
| Avalonia.Controls.DataGrid | 11.3.12 | 日志 DataGrid |
| CommunityToolkit.Mvvm | 8.2.1 | MVVM 基础设施 |
| System.Diagnostics.PerformanceCounter | 8.0.0 | CPU 性能计数器 |
| LuxVideoDet.Core | - | 核心检测引擎 |

## 全局样式

自定义样式定义在 `App.axaml` 中：

- **`ToggleButton.toolbar`** — 工具栏图标按钮，未选中时透明，选中时 accent 高亮
- **`ToggleButton.logfilter`** — 日志级别过滤标签，未选中时半透明，选中时 accent 高亮
