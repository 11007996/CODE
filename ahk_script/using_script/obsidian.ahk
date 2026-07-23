;;; obsidian快捷键 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
#HotIf WinActive("Obsidian")
;; 自动关闭多余窗口
	; #SingleInstance Force
	; Persistent
	; ; 循环检测 Obsidian 窗口
	; 	Loop {
 ;    		Sleep 1000
 ;    		; 获取所有 Obsidian 窗口
 ;    		wins := WinGetList("ahk_exe Obsidian.exe")
 ;    		if (wins.Length <= 1)
 ;        		continue

 ;    		; 保留当前激活窗口，关闭其他所有
 ;    		activeID := WinExist("A")
 ;    		for hwnd in wins {
 ;        		if (hwnd != activeID) {
 ;       	 	    WinClose(hwnd)
 ;        		}
 ;    		}
	; 	}


:?:1h::
{
    SendInput "{CTRL down}p{CTRL up}"
    SendInput "设为小标题1"
    SendInput "{enter}"
    SendInput "{CTRL down}{DOWN}{CTRL up}"
}
:?:2h::
{
    SendInput "{CTRL down}p{CTRL up}"
    SendInput "设为小标题2"
    SendInput "{enter}"
    SendInput "{CTRL down}{DOWN}{CTRL up}"
}
:?:3h::
{
    SendInput "{CTRL down}p{CTRL up}"
    SendInput "设为小标题3"
    SendInput "{enter}"
    SendInput "{CTRL down}{DOWN}{CTRL up}"
}
:?:4h::
{
    SendInput "{CTRL down}p{CTRL up}"
    SendInput "设为小标题4"
    SendInput "{enter}"
    SendInput "{CTRL down}{DOWN}{CTRL up}"
}
:?:5h::
{
    SendInput "{CTRL down}p{CTRL up}"
    SendInput "设为小标题5"
    SendInput "{enter}"
    SendInput "{CTRL down}{DOWN}{CTRL up}"
}
:?:6h::
{
    SendInput "{CTRL down}p{CTRL up}"
    SendInput "设为小标题6"
    SendInput "{enter}"
    SendInput "{CTRL down}{DOWN}{CTRL up}"
}


^!left::
{
    SendInput "『』"
    SendInput "{left}"
    SendInput "[["
}

#HotIf
;;; obsidian快捷键 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;