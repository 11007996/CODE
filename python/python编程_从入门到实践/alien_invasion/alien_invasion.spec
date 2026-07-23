# -*- mode: python ; coding: utf-8 -*-

# 定义要分析的脚本和依赖
a = Analysis(
    ['alien_invasion.py'],
    pathex=[],   # 指定脚本路径
    binaries=[],  # 列出需要包含的二进制文件，如动态链接库
    datas=[('img', 'img')],  # 指定需要打包的资源文件(源路径,目标路径)
    hiddenimports=[],  # 显示声明未被自动检测到的依赖模块，主动说明需要编译的模块
    hookspath=[],
    hooksconfig={},
    runtime_hooks=[],
    excludes=['numpy'],  # 排除该模块，不编译该模块
    noarchive=False,   # 是否清理编译过程中产生的临时文件
    optimize=0,
)

# 如何打包python字节码
pyz = PYZ(a.pure)

# 定义如何打包成可执行文件
exe = EXE(
    pyz,
    a.scripts,
    [],
    exclude_binaries=True,
    name='alien_invasion',  # 可执行文件的名称
    debug=False,     # 是否生成调试文件
    bootloader_ignore_signals=False,
    strip=False,
    upx=True,    # exe是否压缩
    console=False,  # 是否显示控制台窗口
    disable_windowed_traceback=False,
    argv_emulation=False,
    target_arch=None,
    codesign_identity=None,
    entitlements_file=None,
    icon=''   # 设置可执行文件的图标
)

# 定义如何打包成目录
coll = COLLECT(
    exe,
    a.binaries,
    a.datas,
    strip=False,
    upx=True,
    upx_exclude=[],
    name='alien_invasion',
)
