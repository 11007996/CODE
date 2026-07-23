;;; Sublime Text快捷键 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
#HotIf WinActive("Sublime Text")
	^!/::
	{
		; python块注释
		send("{LCtrl DOWN}c{LCtrl UP}")
		SendInput "'''{enter}{enter}'''"
		SendInput "{Up}"
		send("{LCtrl DOWN}v{LCtrl UP}")
		send("{DOWN}{enter}")
	}
	::rem::
	{
		; python块空注释
		SendInput "'''{enter}{enter}'''"
		SendInput "{Up}"
	}

	; 无穷符号
	:*:./::∞

#HotIf
;;; Sublime Text快捷键 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

