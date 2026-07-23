::jp::
{
	SendInput "{F1}"   ; 利用pixpin快键键截图并OCR识别
	Sleep 500
	; MouseMove(100,100)
	Click("down")
	MouseMove(1000,1000)
	Click("up")
	Sleep 500
	Send "{Shift down}c{Shift up}"
}