;;; DO 资料管理器 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
#hotif WinActive("ahk_class dopus.lister")
!r::   ; 设置只读
{
    SendInput "{CTRL down}{enter}{CTRL up}"
    Sleep 200
    SendInput "r"
    Sleep 200
    SendInput "{enter}"
}
#hotif
;;; DO 资料管理器 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;