;;; Chroma 浏览器 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; #hotif WinActive("ahk_class Chrome_WidgetWin_1")
; ::dj::   ; 图片出现自动点击
; {
;     MsgBox "开始"
;     Loop{
;     fpath := "D:\ruanjian\AHK\ahk_script\test_script\国际版.png"
;     imgU := IMG()
;     points := imgU.imgfind(fpath)
;     if(points == 0)
;     {
;         Sleep 5000  ; 循环点击可实现无人值守自动点击
;         continue
;     }
;     else
;     {
;         Click(points[1], points[2])
;     }
;     }
; }



;;; Chroma 浏览器 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;