;;; Typora ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
#hotif WinActive("Typora")
:?:sd::
{
    SendInput "$$"
    SendInput "{Left}"
}
:?:ss::
{
    SendInput "$$"
    SendInput "{enter}"
}
#hotif
;;; Typora ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;