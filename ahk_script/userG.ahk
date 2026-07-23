    ; 绝对对路径引用脚本
; #Include "%A_ScriptDir%\test_script\tt.ahk"
    ; 引用脚本目录下的脚本
; #Include "tt.ahk"
    ; 相对路径引用脚本
; #Include "test_script\pixocr.ahk"
; #Include "using_script\autoClick.ahk"
#Include "using_script\变量.ahk"
#Include "using_script\Navicat.ahk"
#Include "using_script\Sublime.ahk"
#Include "using_script\Anx.ahk"
#Include "using_script\Mes.ahk"
#Include "using_script\DO.ahk"
; #Include "using_script\Typora.ahk"
#Include "using_script\Google.ahk"
#Include "using_script\VNC.ahk"
#Include "using_script\obsidian.ahk"
#Include "using_script\Listary.ahk"
#Include "using_script\pixpin.ahk"


;;; 公共方法 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
class C_YD    ; 有道翻译
{
    query()   ; 有道查询
    {
        send("{LCtrl DOWN}c{LCtrl UP}")
        Send("{LCtrl DOWN}{LAlt DOWN}m{LCtrl UP}{LAlt UP}")
        Sleep 500
        send("{LCtrl DOWN}v{LCtrl UP}")
        send("{enter}")
    }
    YDtranslation()
    {
        if(ProcessExist("YoudaoDict.exe"))
            {
                this.query()
            }
            else
            {
                Run("C:\Users\mh.guo\Desktop\软件\网易有道词典.lnk")
                WinWaitActive("网易有道词典")
                Sleep 4000
                this.query()
            }
    }
}


class IMG    ;  图片定位处理
{
    imgfind(ImagePath)
    {
        ; 1. 按钮截图路径（建议裁剪按钮核心区域，避免背景干扰）
        ; ImagePath := A_ScriptDir "\" path  ; A_ScriptDir 是脚本所在目录
        if !FileExist(ImagePath) {
            MsgBox "截图文件不存在：" ImagePath
            return
        }
        
        ; 2. 搜索范围：整个屏幕（X1=0, Y1=0, X2=屏幕宽, Y2=屏幕高）
        SearchArea := [0, 0, A_ScreenWidth, A_ScreenHeight]
        ; ImageSearch(&FoundX, &FoundY, X1, Y1, X2, Y2, ImageFile, *Variation := 0, Direction := "Fast")
        ; ImageSearch(输出变量数组, 搜索范围, 图片路径, 颜色偏差*,搜索方向)   ; Fast 快速 /Slow 精准，默认 Fast）
        Found := ImageSearch(&OutputX, &OutputY, SearchArea[1], SearchArea[2], SearchArea[3], SearchArea[4], ImagePath)
        ; &OutputX/&OutputY：V2 中通过引用传递获取结果；*50=允许50级颜色偏差
        
        ; 3. 搜索到后点击按钮中心
        if Found {  ; Found 为 true 表示找到图像
            ; 假设截图宽30、高20（根据实际截图调整）
            ButtonCenterX := OutputX + 5  ; 截图宽度/2
            ButtonCenterY := OutputY + 5  ; 截图高度/2
            coordinates := [ButtonCenterX,ButtonCenterY]
            ; Click ButtonCenterX, ButtonCenterY
            return coordinates
        } else {
            return false
        }
    }
}



;;; 窗口置顶
class WinTopCtrl {
    static state := Map()
    static SetTop(wid) {
        ; DetectHiddenWindows(1)
        if not WinExist('ahk_id' wid) {
            ToolTip "该窗口不存在"
            Sleep 2000
            ToolTip
            return
        }
        if WinTopCtrl.state.Has(wid) {
            isTop := false
            WinTopCtrl.state.Delete(wid)
        }
        else {
            isTop := true
            WinTopCtrl.state.Set(wid, true)
        }
        WinSetAlwaysOnTop(-1, 'ahk_id' wid)
        title := Trim(WinGetTitle('ahk_id' wid))
        raw := '[ ' (title || WinGetClass('ahk_id' wid)) ' ] '
            . (isTop ? '置顶' : '取消置顶')
            . '了喵~☆'
            . WinTopCtrl.GetTopWinList(wid)
        ToolTip raw
        Sleep 2000
        ToolTip
    }
 
    static GetTopWinList(wid) {
        ; if no top window ,ruturn
        if not this.state.Count
            return ''
        for k in this.state {
            ; skip closed window
            if not WinExist('ahk_id' k)
                continue
            ; skip current window
            if k = wid
                continue
            titleList .= '`n- [ ' WinGetTitle('ahk_id' k) ' ]'
        }
        return IsSet(titleList) && '`n此外置顶还有' titleList || ''
    }
 
    static CancelAll() {
        if not this.state.Count {
            ToolTip "没有置顶窗口喵~"
            Sleep 2000
            ToolTip
            return
        }
        for k in this.state {
            try
                WinSetAlwaysOnTop 0, 'ahk_id' k
            catch
                errInfo := 'Some of the window is closed'
        }
        this.state := Map()
        ToolTip "取消所有置顶了喵~"  ; (IsSet(errInfo) && '`n但是-->' errInfo || ''))
        Sleep 2000
        ToolTip
    }
}


; 用 COM 获取当前网络名称（WiFi/有线都可以）
GetNetworkName() {
    try {
        ; 实例化网络列表管理器
        nlm := ComObject("{DCB00C01-570F-4A9B-8D69-199FDBA5723B}")
        ; 1 = 仅遍历 已连接 的网络
        enumNet := nlm.GetNetworks(1)
        
        ; 遍历所有已连接网络
        for net in enumNet {
            netName := net.GetName()
            desc := net.GetDescription()
            ; 返回真实网络名，不再过滤未识别网络
            return netName
        }
    } catch {
        return "读取失败"
    }
    return "无网络连接"
}
;;; 公共方法 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;





;;; 实例对象 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
yd := C_YD()   ; 有道查询
;;; 实例对象 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;






;;; 全局快捷键 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
^!a::
{
    Reload  ; 重新加载脚本
}
^+c::ExitApp  ; 退出脚本
;;; 常用文件
::ff::
{
    run "D:\work\Chroma\02C1\code"
}


#Hotif not WinActive("Sublime Text")
;;; 设置或取消置顶
#`:: WinTopCtrl.SetTop(WinGetID('A')) ; ^Pause
;;; 取消所有置顶
!`:: WinTopCtrl.CancelAll()
#Hotif not WinActive("Sublime Text")



::wifi::
{   
    if GetNetworkName()=="luxshare.com.cn"
    {
        Run "CMD.exe"
        Sleep 100
        SendInput "netsh wlan connect name=`"BUD-VIP`""
        SendInput "{enter}"
        SendInput "exit"
        SendInput "{enter}"
    }
    Else
    {
        Run "CMD.exe"
        Sleep 100
        SendInput "netsh wlan connect name=`"Luxshare-Office`""
        SendInput "{enter}"
        SendInput "exit"
        SendInput "{enter}"
    }
}
;;; 全局快捷键 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;