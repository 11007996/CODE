# LuxVideoDet.Algorithms

该目录用于独立编译业务场景算法插件（`dll`），主程序无需重新打包。

## 编译输出

- 所有算法项目统一输出到仓库根目录 `plugins/`
- 主程序启动后会自动扫描 `plugins/*.dll`

## 新增一个场景算法项目（开发新逻辑）

假设新场景项目名为 **xxx**（与仓库内其他算法一致，程序集名约定为 `LuxVideoDet.Algorithm.xxx`）。在**仓库根目录**依次执行：

```bash
# 1. 新建类库项目（生成在 LuxVideoDet.Algorithms 下）
dotnet new classlib -n LuxVideoDet.Algorithm.xxx -o LuxVideoDet.Algorithms/LuxVideoDet.Algorithm.xxx -f net8.0

# 2. 添加对 LuxVideoDet.Core 的项目引用
dotnet add LuxVideoDet.Algorithms/LuxVideoDet.Algorithm.xxx/LuxVideoDet.Algorithm.xxx.csproj reference LuxVideoDet.Core/LuxVideoDet.Core.csproj

# 3. 将项目加入解决方案 LuxVideoDet.sln
dotnet sln LuxVideoDet.sln add LuxVideoDet.Algorithms/LuxVideoDet.Algorithm.xxx/LuxVideoDet.Algorithm.xxx.csproj
```

把命令里的 `xxx` 换成你的场景简称即可（例如 `U7Lite`、`Example` 等已有项目名）。

说明：

- `TargetFramework`、`OutputPath`（输出到仓库根目录 `plugins/`）等由 `LuxVideoDet.Algorithms/Directory.Build.props` 统一继承；若模板生成的 `csproj` 与 `Directory.Build.props` 有重复项，可删减负重复，保留对 `LuxVideoDet.Core` 的引用即可。
- 加入 `sln` 不是运行时硬性必须（主程序按 `plugins/*.dll` 扫描），但强烈建议加入，方便 IDE/CI 统一管理。

可选：编译验证

```bash
dotnet build LuxVideoDet.Algorithms/LuxVideoDet.Algorithm.xxx/LuxVideoDet.Algorithm.xxx.csproj
```

编译成功后应在仓库根目录 `plugins/` 下看到 `LuxVideoDet.Algorithm.xxx.dll`。

## 当前已拆分的算法项目

- `LuxVideoDet.Algorithm.U7Lite`
- `LuxVideoDet.Algorithm.UCS`
- `LuxVideoDet.Algorithm.UCSHand`
- `LuxVideoDet.Algorithm.MidFrameScanLoose`
- `LuxVideoDet.Algorithm.MidFrameScanLooseHand`
- `LuxVideoDet.Algorithm.MidFrameScanStrict`
- `LuxVideoDet.Algorithm.SafetyPpeWork`
- `LuxVideoDet.Algorithm.TapeWrapCount`
- `LuxVideoDet.Algorithm.Example`
- `LuxVideoDet.Algorithm.Glove`
- `LuxVideoDet.Algorithm.FrontShellAppearance`
