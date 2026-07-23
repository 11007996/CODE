# LuxVideoDet.Web

Web 控制台 — 基于 ASP.NET Core 的多路视频检测管理平台

---

## 简介

LuxVideoDet.Web 是 LuxVideoDet 系统的 Web 运行模式，提供浏览器端的管理界面和 RESTful API，适合多摄像头远程监控、局域网多端查看等场景。

### 核心能力

- **Web 管理界面** — 配置管理、会话控制、实时状态监控
- **MJPEG 视频流** — 浏览器实时查看检测画面，支持多客户端同时观看
- **RESTful API** — 完整的配置 CRUD、检测会话控制、算法元数据查询
- **多路并发** — 同时运行多个检测会话，独立管理生命周期
- **配置导入/导出** — 支持 JSON 格式的批量配置迁移

---

## 快速启动

### 前置要求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- 已编译的 LuxVideoDet.Core 项目

### 运行

```bash
# 从解决方案根目录启动
dotnet run --project LuxVideoDet.Web

# 或进入项目目录
cd LuxVideoDet.Web
dotnet build
dotnet run
```

启动成功后访问：

| 地址 | 说明 |
|------|------|
| `http://localhost:5050` | Web 管理控制台 |
| `http://localhost:5050/view.html?config=<配置名称>` | 单路视频流查看页 |
| `http://<本机IP>:5050` | 局域网内其他设备访问 |

---

## 配置

### 端口配置

默认监听端口 5050，可在 `appsettings.json` 中修改：

```json
{
  "WebServer": {
    "Port": 5050
  }
}
```

> **注意**：如果启动时报 `address already in use` 错误，说明端口被占用。可以用 `netstat -ano | findstr ":5050"`（Windows）或 `lsof -i :5050`（macOS/Linux）查找占用进程，或修改端口号。

### 日志配置

日志文件写入 `logs/web-<日期>.log`，保留最近 7 天，单文件上限 10MB。控制台和文件同时输出。

---

## 项目结构

```
LuxVideoDet.Web/
├── Program.cs                  # 应用入口，API 路由定义
├── Services/
│   ├── WebDetectionService.cs  # 多路检测会话管理
│   └── MjpegStreamManager.cs  # MJPEG 视频流推送
├── wwwroot/
│   ├── index.html              # 管理控制台页面
│   ├── view.html               # 单路视频流查看页
│   ├── css/style.css           # 样式
│   └── js/
│       ├── app.js              # 控制台前端逻辑
│       └── region-editor.js    # 检测区域可视化编辑器
├── Properties/
│   └── launchSettings.json     # 开发环境启动配置
├── configs.json                # 检测配置持久化存储
├── appsettings.json            # 应用配置
└── LuxVideoDet.Web.csproj      # 项目文件
```

---

## API 文档

所有 API 以 `/api` 为前缀。

### 配置管理

| 方法 | 路径 | 说明 |
|------|------|------|
| GET | `/api/configs` | 获取所有配置列表（含运行状态） |
| GET | `/api/configs/{id}` | 获取单个配置详情 |
| POST | `/api/configs` | 创建新配置 |
| PUT | `/api/configs/{id}` | 更新配置（运行中不可修改） |
| DELETE | `/api/configs/{id}` | 删除配置（运行中不可删除） |
| POST | `/api/configs/validate` | 验证配置有效性 |
| POST | `/api/configs/import` | 导入配置（支持单个/数组/包装对象） |
| GET | `/api/configs/export` | 导出全部配置 |

### 检测会话

| 方法 | 路径 | 说明 |
|------|------|------|
| POST | `/api/sessions/{configId}/start` | 启动检测会话 |
| POST | `/api/sessions/{configId}/stop` | 停止检测会话 |
| GET | `/api/sessions` | 获取所有运行中的会话 |
| GET | `/api/sessions/{configId}` | 获取单个会话详情 |
| POST | `/api/sessions/start-all` | 启动所有已启用的配置 |
| POST | `/api/sessions/stop-all` | 停止全部会话 |

### 算法与视频流

| 方法 | 路径 | 说明 |
|------|------|------|
| GET | `/api/algorithms/types` | 获取支持的算法类型列表 |
| GET | `/api/algorithms/{type}/regions` | 获取算法所需的检测区域定义 |
| POST | `/api/capture-frame` | 从视频源捕获单帧（用于区域编辑器） |
| GET | `/api/stream/{configId}` | MJPEG 实时视频流 |
| GET | `/api/snapshot/{configId}` | 获取最新帧快照（JPEG） |
| GET | `/api/resolve?config=<名称>` | 按配置名称查找 ID 和流地址 |

### 示例请求

```bash
# 获取所有配置
curl http://localhost:5050/api/configs

# 启动检测
curl -X POST http://localhost:5050/api/sessions/{configId}/start

# 查看 MJPEG 视频流（浏览器直接打开）
# http://localhost:5050/api/stream/{configId}

# 导入配置
curl -X POST http://localhost:5050/api/configs/import \
  -H "Content-Type: application/json" \
  -d @my-config.json
```

---

## 技术栈

| 组件 | 技术 |
|------|------|
| Web 框架 | ASP.NET Core 8.0 Minimal API |
| 视频流 | MJPEG over HTTP (multipart/x-mixed-replace) |
| 日志 | Serilog (Console + File) |
| 核心依赖 | LuxVideoDet.Core（检测引擎、配置管理） |
| 前端 | 原生 HTML/CSS/JS（无框架依赖） |

---

## 常见问题

### 端口被占用 (address already in use)

```powershell
# 查找占用端口 5050 的进程
netstat -ano | findstr ":5050"

# 杀掉对应进程
taskkill /F /PID <PID>

# 或修改 appsettings.json 中的端口号
```

### MJPEG 流无画面

确保对应配置的检测会话已启动（`POST /api/sessions/{configId}/start`），且视频源可正常连接。

### 视频流查看页

打开 `http://localhost:5050/view.html?config=配置名称`，系统会自动按名称解析配置 ID 并展示实时画面。
