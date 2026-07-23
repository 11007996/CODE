; #	Win(Windows 徽标键)
; !	Alt
; ^	Ctrl
; +	Shift
; &	用于连接两个按键(含鼠标按键) 合并成一个自定义热键.

; ::ww::{!}a::"{""{Enter}""}"

;;; 打开网页
; #z::Run "https://www.baidu.com"  ; Win+Z

;;; 打开文件夹
; #z::Run "D:\\work\\Chroma\\02C1\\code"

;;; 打开记事本
; !n::  ;alt+n
; {
; 	Run "notepad.exe"
; 	return
; }

; CapsLock & Backspace::!NumpadUp

; {
; 	"{LCtrl} {NumpadUp}"
; 	; msgbox "hello"

; }

;;; 单按键热键
; Esc::
; {
;     MsgBox "Escape!!!!"
; }

;;; 文本sa替换sajet，标点符号触发
; ::sa::sajet

;;; 字符串作为热键
; :*:btw::    ; 需要按下结束符才触发
; {
;     MsgBox "You typed btw."
; }
; :*:btw::   ; *号是修饰符,不需要按下结束符即触发
; {
;     MsgBox "You typed btw."
; }


;;; 一个热键可编写多条语句
; ^j::
; {
;     MsgBox "Wow!"   ; 弹窗
;     MsgBox "There are"
;     Run "notepad.exe"    ; 打开记事本7 lines!inside自助CTRL+J
;     Send "7 lines{!}{Enter}"  ; 输出 lines! + 回车
;     SendInput "inside the CTRL{+}J hotkey." ; 输出inside the CTRL+J hotkey.
; }


;;; 同时按小键盘0和1 或 小键盘0和2
; Numpad0 & Numpad1::
; {
;     MsgBox "You pressed Numpad1 while holding down Numpad0."
; }

; Numpad0 & Numpad2::
; {
;     Run "notepad.exe"
; }


;;; 在特定窗口使用此热键
; #HotIf WinActive("1.txt - 记事本")   ; 检查窗口是否被激活
; #Space::   ; 只作用于1.txt
; {
;     MsgBox "You pressed WIN+SPACE in Notepad."
; }
; #a::   ; 只作用于2.txt
; {
;     MsgBox "qqqq"
; }
; #HotIf WinActive("2.txt - 记事本")
; #q::   ; 只作用于2.t
; {
;     MsgBox "qqqq"
; }
; #HotIf

;;; 复制粘贴
; ^b::  ; CTRL+B 热键
; {
;     Send "{Ctrl down}c{Ctrl up}"  ; 复制选定的文本. 也可以使用 ^c, 但这种方法更加可靠.
;     click
;     SendInput "{Enter}"
;     SendInput "{Ctrl down}v{Ctrl up}" ;  发送按键, 模拟打字或按键操作
; }  ; 热键内容结束, 当按下热键时, 下面的代码将不会被执行.


;;; 或左Ctrl键
; LCtrl::
; {
;     Send "AutoHotkey"
; }

;;; 大括号
; #a::
; {
; 	send "hello"
;   SendInput "{Enter}"   ; 特殊按键用大括号表示回车
; 	SendInput "hello{!}"   ; 大括号表示转义

; 下面这个例子表示按下一个键的时候再按下另一个键(或多个键)..
; 如果其中一个方法不奏效, 试试另一个.
; Send "^s"                     ; 表示发送 CTRL+S
; Send "{Ctrl down}s{Ctrl up}"  ; 表示发送 CTRL+S
; Send "{Ctrl down}c{Ctrl up}"
; Send "{b down}{b up}"
; Send "{Tab down}{Tab up}"
; Send "{Up down}"  ; 按下向上键.
; Sleep 1000        ; 保持 1 秒.
; Send "{Up up}"    ; 然后松开向上键.
; Send "(Line 1 Line 2 Apples are a fruit.)"  ; 发送超长文本
; }

;;; 调用函数，变量赋值
; !c::
; {
; 	MyVar := SubStr("I'm scripting, awesome!", 16)
; 	SendInput MyVar
; 	Send "{Enter}"
; }

;;; 关闭脚本
; !c::
; {
;     ExitApp   ; 关闭整个脚本
; 	  Exit()   ; 关闭当前函数
; }

;;; 赋值 if语句
; !a::
; {
; 	vars :=5
; 	if(vars=5)
; 	{
; 		MsgBox "ok! " vars
; 	}
; }


;;; 输入框
; !a::
; {
; 	OutputVar := InputBox("What is your first name?", "Question 1").Value     ; 输入值
; if (OutputVar = "Bill")
;     MsgBox "That's an awesome name, " OutputVar "."
; OutputVar2 := InputBox("Do you like AutoHotkey?", "Question 2").Value
; if (OutputVar2 = "yes")
;     MsgBox "Thank you for answering " OutputVar2 ", " OutputVar "! We will become great friends."
; else
;     MsgBox OutputVar ", That makes me sad."
; }

;;; 弹出框
; !a::
; {
; 	Result := MsgBox("Would you like to continue?","Question", 1)
; if Result = "No"
;     return  ; 如果选择 No, 则停止代码继续.
; MsgBox "You pressed YES."  ; 否则, 用户选择了 YES.
; }


; !a::
; {
; 	  MsgBox A_AhkVersion   ; AHK的版本号
; }


; MyObject := ["one", "two", "three", 17]  ; 方括号创建数组
; MyObject := Array("one", "two", "three", 17)  ; 创建数组
; Banana := {"Color": "Yellow", "Taste": "Delicious", "Price": 3}    ; 创建字典
; MyMap := Map("KeyA", ValueA, "KeyB", ValueB, ..., "KeyZ", ValueZ)    ; 创建映射
; Banana["Pickled"] := True   ; 字典赋值
; Banana.Consistency := "Mushy"   ; 字典赋值
; MyObject.InsertAt(Index, Value1, Value2, Value3...)  ; 插入数组
; MyObject.Push(Value1, Value2, Value3...)   ; 压入数组元素
; MyObject.Pop()   ; 弹出数组元素
; RemovedValue := MyObject.Delete(AnyKey)   ; 删除数组元素，被删的元素存储在变量中
; NumberOfRemovedKeys := MyObject.Delete(FirstKey, LastKey)   ; 批量删除元素
; RemovedValue := MyObject.RemoveAt(Index)   ; 删除指定元素
; NumberOfRemovedKeys := MyObject.RemoveAt(Index, Length)  ; 批量删除数组元素

;;;屏幕虚拟键盘
; ; On-Screen Keyboard (based on the v1 script by Jon)
; ; https://www.autohotkey.com
; ; This script creates a mock keyboard at the bottom of your screen that shows
; ; the keys you are pressing in real time. I made it to help me to learn to
; ; touch-type (to get used to not looking at the keyboard). The size of the
; ; on-screen keyboard can be customized at the top of the script. Also, you
; ; can double-click the tray icon to show or hide the keyboard.

; ;---- Configuration Section: Customize the size of the on-screen keyboard and
; ; other options here.

; ; Changing this font size will make the entire on-screen keyboard get
; ; larger or smaller:
; k_FontSize := 10
; k_FontName := "Verdana"  ; This can be blank to use the system's default font.
; k_FontStyle := "Bold"    ; Example of an alternative: Italic Underline

; ; Names for the tray menu items:
; k_MenuItemHide := "Hide on-screen &keyboard"
; k_MenuItemShow := "Show on-screen &keyboard"

; ; To have the keyboard appear on a monitor other than the primary, specify
; ; a number such as 2 for the following variable. Leave it blank to use
; ; the primary:
; k_Monitor := ""

; ;---- End of configuration section. Don't change anything below this point
; ; unless you want to alter the basic nature of the script.

; ;---- Create a Gui window for the on-screen keyboard:
; MyGui := Gui("-Caption +ToolWindow +AlwaysOnTop +Disabled")
; MyGui.SetFont("s" k_FontSize " " k_FontStyle, k_FontName)
; MyGui.MarginY := 0, MyGui.MarginX := 0

; ;---- Alter the tray icon menu:
; A_TrayMenu.Delete
; A_TrayMenu.Add k_MenuItemHide, k_ShowHide
; A_TrayMenu.Add "&Exit", (*) => ExitApp()
; A_TrayMenu.Default := k_MenuItemHide

; ;---- Add a button for each key:

; ; The keyboard layout:
; k_Layout := [
;     ["``", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=", "Backspace:3"],
;     ["Tab:3", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[", "]", "\"],
;     ["CapsLock:3", "A", "S", "D", "F", "G", "H", "J", "K", "L", ";", "'", "Enter:2"],
;     ["LShift:3", "Z", "X", "C", "V", "B", "N", "M", ",", ".", "/", "Shift:3"],
;     ["LCtrl:2", "LWin:2", "LAlt:2", "Space:2", "RAlt:2", "RWin:2", "AppsKey:2", "RCtrl:2"]
; ]

; ; Traverse the keys of the keyboard layout:
; for n, k_Row in k_Layout
;     for i, k_Key in k_Row
;     {
;         k_KeyWidthMultiplier := 1
;         ; Get custom key width multiplier:
;         if RegExMatch(k_Key, "(.+):(\d)", &m)
;         {
;             k_Key := m[1]
;             k_KeyWidthMultiplier := m[2]
;         }
;         ; Get localized key name:
;         k_KeyNameText := GetKeyNameText(k_Key, 0, 1)
;         ; Windows key names start with left or right so replace it:
;         if (k_Key = "LWin" || k_Key = "RWin")
;             k_KeyNameText := "Win"
;          ; Truncate the key name:
;         if (StrLen(k_Key) > 1)
;             k_KeyNameText := Trim(SubStr(k_KeyNameText, 1, 5))
;         else
;             k_KeyNameText := k_Key
;         ; Convert to uppercase:
;         k_KeyNameText := StrUpper(k_KeyNameText)
;         ; Calculate object dimensions based on chosen font size:
;         k_KeyHeight := k_FontSize * 3
;         opt := "h" k_KeyHeight " w" k_KeyHeight * k_KeyWidthMultiplier " -Wrap x+m" 
;         if (i = 1)
;             opt .= " y+m xm"
;         ; Add the button:
;         Btn := MyGui.Add("Button", opt, k_KeyNameText)
;         ; When a key is pressed by the user, click the corresponding button on-screen:
;         Hotkey("~*" k_Key, k_KeyPress.bind(Btn))
;     }

; ;---- Position the keyboard at the bottom of the screen (taking into account
; ; the position of the taskbar):
; MyGui.Show("Hide") ; Required to get the window's calculated width and height.
; ; Calculate window's X-position:
; MonitorGetWorkArea(k_Monitor, &WL,, &WR, &WB)
; MyGui.GetPos(,, &k_width, &k_height)
; k_xPos := (WR - WL - k_width) / 2 ; Calculate position to center it horizontally.
; ; The following is done in case the window will be on a non-primary monitor
; ; or if the taskbar is anchored on the left side of the screen:
; k_xPos += WL
; ; Calculate window's Y-position:
; k_yPos := WB - k_height

; ;---- Show the window:
; MyGui.Show("x" k_xPos " y" k_yPos " NA")

; ;---- Function definitions:
; k_KeyPress(BtnCtrl, *)
; { 
;     BtnCtrl.Opt("Default") ; Highlight the last pressed key.
;     ControlClick(, BtnCtrl,,,, "D")
;     KeyWait(SubStr(A_ThisHotkey, 3))
;     ControlClick(, BtnCtrl,,,, "U")
; }

; k_ShowHide(*)
; {
;     static isVisible := true
;     if isVisible
;     {
;         MyGui.Hide
;         A_TrayMenu.Rename k_MenuItemHide, k_MenuItemShow
;         isVisible := false
;     }
;     else
;     {
;         MyGui.Show
;         A_TrayMenu.Rename k_MenuItemShow, k_MenuItemHide
;         isVisible := true
;     }
; }

; GetKeyNameText(Key, Extended := false, DoNotCare := false)
; {
;     Params := (GetKeySC(Key) << 16) | (Extended << 24) | (DoNotCare << 25)
;     KeyNameText := Buffer(64, 0)
;     DllCall("User32.dll\GetKeyNameText", "Int", Params, "Ptr", KeyNameText, "Int", 32)
;     return StrGet(KeyNameText)
; }

; !a::
; {
; 	if(WinExist("1.txt - 记事本"))   ; 检查是否存在此窗口
; 	{
; 		; WinClose("1.txt - 记事本")  ; 检查此窗口
; 		WinActivate  ; 激活此窗口
; 	}
; 	else
; 	{
; 		msgbox("窗口未打开")
; 	}
; }



; !a::
; {
	; msgbox Type("hello")
	; msgbox false
	; send "hello" "world" "{Enter}"   ; 字符串隐式连接
	; send("hello" . "world")  ; . 字符串显示连接
	; MsgBox Format("You are using AutoHotkey v{1} {2}-bit.", A_AhkVersion, A_PtrSize*8)   ; 字符串格式化
	; SayHello()  ; 调用 SayHello 函数.

	; target := 42
	; second := "target"
	; MsgBox  second,"普通(单重) 变量引用"   ; 普通(单重) 变量引用 => target
	; MsgBox %second%,"双重解引"  ; 双重解引 => 42

	; MyArray1 := "A"
	; MyArray2 := "B"
	; MyArray3 := "C"
	; Loop 3
 	; MsgBox MyArray%A_Index%  ; 通过解引用来实现伪数组

;  	Goto MyLabel   ; goto语句
; 	MyLabel:
; 	send("hello" . "world")

; 	MsgBox ThisHotkey   ; 显示热键
; }

;;; ~同时保存右键快捷菜单
; ~RButton::MsgBox "You clicked the right mouse button."

;;; *通配符
; *s::
; {
; 	MsgBox ThisHotkeyx
; }


; SayHello()  ; 定义 SayHello 函数.
; {
;     MsgBox "Hello!"
; }


; WheelUp::Send "{Volume_Up}"  ; 提高音量
; WheelDown::Send "{Volume_Down}"   ; 降低音量

;;; 创建一个UI界面
; HkGui := Gui()
; HkGui.Add("Text", "xm", "Prefix key:")   ; 新建一个文本
; HkGui.Add("Edit", "yp x100 w100 vPrefix", "Space")   ; 新建一个文本框
; HkGui.Add("Text", "xm", "Suffix hotkey:")
; HkGui.Add("Edit", "yp x100 w100 vSuffix", "f & j")
; HkGui.Add("Button", "Default", "Register").OnEvent("Click", RegisterHotkey)  ; 创建一个按钮，并注册一个click事件
; HkGui.OnEvent("Close", (*) => ExitApp())
; HkGui.OnEvent("Escape", (*) => ExitApp())
; HkGui.Show()

; RegisterHotkey(*)
; {
;     Saved := HkGui.Submit(false)
;     HotIf (*) => GetKeyState(Saved.Prefix)
;     Hotkey Saved.Suffix, (ThisHotkey) => MsgBox(ThisHotkey)
; }


;;; 调试断点用
; !a::
; {
; 	Var := "
;     (
;     一行文字.
;     默认情况下, ;行与行之间的硬回车(`n) 也会被储存.
;     	这一行用制表符缩进; 默认情况下, 该制表符也会被储存.
;     此外, "引号" 会在适当时自动转义.
;     )"
; 	send Var
; 	Pause ; 暂停脚本，可以作断点用
; 	msgbox "断点继续"
; }



; !a::
; {
; 	aa := "hello"
; 	; msgbox show(&aa)   ; 引用传值
; 	show(&aa)
; 	msgbox aa
; }
; show(msg)
; {
; 	%msg% := "world"   ; 解引用
;   	return msg
; }


;;;  异常捕捉
; !a::
; {
; 	try
; 	{
; 		var := 1/0
; 		msgbox var
; 	}
; 	catch as error
; 	{
; 		msgbox error.message
; 	}
; }


;;; 内置变量
; !a::
; {
;	msgbox A_WorkingDir   ; 当前工作目录
;	send A_ScriptName "`n"
;	send A_ScriptDir "`n"
;	send A_WorkingDir "`\" A_ScriptName
;	sendInput A_ScriptHwnd
;	send A_YYYY "-" a_mm "-" a_dd " " A_Hour ":" A_Min ":" A_Sec ; 年月日
;	send A_Now   ; 时间
;	send A_OSVersion   ; 操作系统版本
;	send A_UserName   ;电脑用户名
;	send A_ScreenWidth " - " A_ScreenHeight   ; 屏幕尺寸
;	send A_Clipboard   ;剪切板
	; A_Clipboard := "my text"   ; 将文本写入剪切板
	; A_Clipboard := ""  ; 清空剪切板
	; A_Clipboard .= " Text to append."  ; 追加一些文本到剪贴板.
	; msgbox EnvGet("HOME")  ;检索环境变量.
	; EnvSet("PATH","C:\Users\mh.guo\Desktop\2.txt")  ;写入环境变量

	; varList := SysGetIPAddresses()   ; 获取IP
	; for v in varList
	; 	msgbox v
; }

;;; 监听剪切板内容
; OnClipboardChange ClipChanged
; ClipChanged(clip_type) {
;     ToolTip "剪贴板数据类型: " clip_type
;     Sleep 2000
;     ToolTip  ; 关闭工具提示.
; }

;;; 默认参数，可选参数
; !a::
; {
; 	msgbox Add(2,3)
; 	msgbox Add(2,3,1)
; }
; Add(X, Y, Z := 0) {
;     return X + Y + Z
; }

;;; 判断可先参数是否带了参数
; !a::
; {
; 	MyFunc(p := unset) {
;     if IsSet(p)
;         MsgBox "Caller passed " p
;     else
;         MsgBox "Caller did not pass anything"
; 	}
; 	MyFunc(42)
; 	MyFunc
; }



;;; 可变参数
; !a::
; {
; 	Join(sep, params*) {   ; *号表示参数可变
;     for index,param in params
;         str .= param . sep
;     return SubStr(str, 1, -StrLen(sep))
; 	}
; 	MsgBox Join("`n", "one", "two", "three","可变参数")
; }


;;; 编写文件
; !a::
; {
; 	LogToFile("C:\\Users\\mh.guo\\Desktop\\2.txt")
; }
; LogToFile(TextToLog)
; {
; 	; LogFileName 是之前在这个函数之外的某个地方被赋予的值.
; 	; FileAppend 是一个包含内置函数的预定义全局变量.
; 	FileAppend "TextToLog", TextToLog
; }


;;; 动态调用函数，类似于函数指针
; add1()
; {
; 	return 2+2
; }
; add2()
; {
; 	return 3+3
; }
; !a::
; {
; 	var := InputBox("function name?", "input").Value
; 	msgbox %var%   ; 解引用
; }


;;; 短路型布尔值
; !a::
; {
; 	if(1!=1 and check())  ; 短路型布尔值，函数check不会运行
; 	{
; 		msgbox  "hello"
; 	}
; 	else
; 	{
; 		msgbox "NO"
; 	}
; }
; check()
; {
; 	msgbox "world"
; 	return false
; }


;;; 支持嵌套函数，一个函数定义在另一个函数中
; !a::
; {
; 	check()
; 	{
; 		msgbox "function1"
; 		show()
; 		show()   ; 嵌套函数
; 		{
; 			msgbox "function2"
; 		}
; 	}
; 	check()
; }

;;; 闭包
; !a::
; {
; 	make_greeter(f)
; 	{
; 	    greet(subject)  ; 这是 f 的闭包.
; 	    {
; 	        MsgBox Format(f, subject)
; 	    }
; 	    return greet  ; 返回闭包.
; 	}
; 	g := make_greeter("Hello, {}!")
; 	g(A_UserName)
; 	g("World")
; }

;;; 弹出变量窗口
; !a::
; {
; 	var := "hello"
; 	aaa := "world"
; 	ListVars  ; 弹出变量窗口
; }


;;; goto语句
; !a::
; {
; 	goto Lab
; 	Lab:
; 	{
; 		msgbox "hello"
; 	}
; }


;;; for循环
; !a::
; {
; 	MyArray := ["aa","bb","cc"]
; 	For value in MyArray
;    		 MsgBox "Item " A_Index " is '" value "'"
; }


;;; Map映射
; !a::
; {
; 	MyMap := Map("KeyA", 1, "KeyB", 2)
; 	msgbox MyMap["KeyA"]
; 	MyMap["KeyC"] := 3
; 	msgbox MyMap["KeyC"]
; 	RemovedValue := MyMap.Delete("KeyC")
; 	for key,value in MyMap
; 		msgbox key ":" value
; }

;;; 对象二维数组
; !a::
; {
; 	array1 := [1,2,3,4]
; 	array2 := [5,6,7,8]
; 	array3 := [array1,array2]
; 	msgbox array3[2][3]
; }


;;; 自定义对象
; !a::
; {
; 	thing := {}  ; 创建对象.
; 	thing.foo := "bar" ; 创建属性值
; 	thing.test := thing_test  ; 定义一个对象方法
; 	thing.test()  ; 调用方法.
; 	thing_test(this) {
; 	   MsgBox this.foo
; 	}
; }


;;;  类对象
; class ClassName extends BaseClassName    ; 一个类继承于另一个基类
; {
;     InstanceVar := 表达式   ; 变量

;     static ClassVar := 表达式  ; 静态变量

;     class NestedClass   ; 嵌套类
;     {
;         ...
;     }

;     Method()    ; 方法
;     {
;         ...
;     }

;     static Method()  ;  静态方法
;     {
;         ...
;     }

;     Property[Parameters] ; 仅在有参数时使用方括号.    ; 属性
;     {
;         get {
;             return 属性的值
;         }
;         set {
;             存储或以其他方式处理 值
;         }
;     }

;      ShortProperty
;     {
;         get => 计算属性值的表达式
;         set => 存储或以其他方式处理 值 的表达式
;     }

;      ShorterProperty => 计算属性值的表达式


;      __New(aFlags, aSize)   ; 构造函数
;     {
;         this.ptr := DllCall("GlobalAlloc", "UInt", aFlags, "Ptr", aSize, "Ptr")
;         if !this.ptr
;             throw MemoryError()
;         MsgBox "New GMem of " aSize " bytes at address " this.ptr "."
;     }

;     __Delete()  ; 析构函数
;     {
;         MsgBox "Delete GMem at address " this.ptr "."
;         DllCall("GlobalFree", "Ptr", this.ptr)
;     }
; }


;;; 实例类
; class CC
; {
; 	__New()   ; 首先执行构造函数
; 	{
; 		var1 := ""
; 		var2 := ""
; 		msgbox "hello"
; 	}
; 	var1 := "AA"   ; 再执行类的结构体
; 	var2 := "BB"
; 	show()
; 	{
; 		return this.var1 "-" this.var2
; 	}
; 	__Delete()   ; 再执行析构函数
; 	{
; 		msgbox "delete"
; 	}
; }
; !a::
; {
; 	C := CC()
; 	msgbox C.show()
; 	pause
; }


;;; 类继承
; class cc{
; 	var := "hello"
;   show()
;   {
;   	msgbox this.var
;   }
; }
; class dd extends cc{   ; 继承类
; 	show()   ; 重写类方法
; 	{
; 		msgbox "world"
; 	}
; }

; !a::
; {
; 	c := cc()
; 	c.show()
; 	d := dd()
; 	d.show()
; }

;;; 驱动器
; !a::
; {
; 	; msgbox DriveGetCapacity("d:")   ; 显示驱动器的容量
; 	; msgbox DriveGetFileSystem("d:")  ; 显示驱动器文件系统的类型
; 	; msgbox DriveGetLabel("d:")  ; 返回指定驱动器的卷标.
; 	; msgbox DriveGetList()  ; 返回一串字母, 系统中的每个驱动器字母对应一个字符.
; 	; msgbox DriveGetSerial("c:")  ; 返回指定驱动器的卷序列号.
; 	; msgbox DriveGetSpaceFree("c:")   ; 显示剩余可用容量
; 	; msgbox DriveGetStatus("d:")   ; 返回包含指定路径的驱动器的状态.
; 	; msgbox DriveGetStatusCD("c:")  ; 返回指定 CD/DVD 驱动器的媒体状态.
; 	; DriveSetLabel Drive , NewLabel   ; 更改指定驱动器的卷标签，Drive表示驱动器字母后跟着冒号和可选的反斜杠，NewLabel表示要设置的新标签. 如果省略, 驱动器将没有标签.
; }


; !a::
; {
    ; tooltip FileGetVersion("D:\work\Chroma\02C1\MES_Client\ASSY\YMLaser.dll")   ; 显示文件版本

    ; try
    ; {
    ; 	Value := IniRead("D:\work\Chroma\02C1\MES_Client\sajet.ini", "FCT", "File Path" )   ; 读取ini文件
    ; 	msgbox value
    ; }
    ; catch  OSError as e
    ; {
    ; 	msgbox e.message
    ; }

	; Loop Files, "D:\work\Chroma-MES数据备份\FA100图表导入\FA100资料图片\*.png"
	;     send A_LoopFileName "`n"

	; 获取所有信息:
	; SplitPath "C:\Users\mh.guo\Desktop\2.txt", &name, &dir, &ext, &name_no_ext, &drive
	; send name "`n"   ; 文件名
	; send dir "`n"   ; 文件目录
	; send ext "`n"   ; 文件后缀名
	; send name_no_ext "`n"   ; 文件名无后缀
	; send drive   ; 驱动器
; }



; #include "note.ahk"


; #space::  ; Win+Space 热键.
; {
;     Critical   ; 表示线程不可中断
;     ToolTip "No new threads will launch until after this ToolTip disappears."
;     Sleep 3000
;     ToolTip  ; 关闭提示.
;     return  ; 从热键子程序中返回. 根据定义, 要恢复的任何底层线程都是非关键的.
; }


; !a::
; {
; 	; throw ValueError("抛出异常", -1, "异常错误1")

; 	; Loop Parse "abcdefghijk"
; 	; 	send A_LoopField "`n"

; 	; msgbox "world"
; }


;;; 定时器
; SetTimer CloseMailWarnings, 1000
; CloseMailWarnings()
; {
;     send a_now "`n"
; }

;;;  switch语句
; !a::
; {
; 	var := InputBox("input number", "提示").Value
; 	Switch var
; 	{
; 	Case 1:
; 	    send "hello"
; 	Case 2:
; 	    send "world"
; 	Default:
; 	    send "hello world"
; 	}
; }


; !a::
; {
	;;选择某个文件夹
	; SelectedFolder := DirSelect("d:", 3)
	; if SelectedFolder = ""
	;     MsgBox "You didn't select a folder."
	; else
	;     MsgBox "You selected folder '" SelectedFolder "'."

	;;选择某个文件
	; SelectedFile := FileSelect(3, , "Open a file", "Text Documents (*.txt; *.doc)")
	; if SelectedFile = ""
	;     MsgBox "The user didn't select anything."
	; else
	;     MsgBox "The user selected the following:`n" SelectedFile

	
; }

; !a::
; {
; 	HkGui := Gui(,"UI界面")

; 	HkGui.Add("Text", "xm", "文本")   ; 新建一个文本
; 	HkGui.Add("Edit", "r1 vMyEdit w235 Uppercase", "文本框")  ;创建文本框 r9表示9行，w135表示宽度，Uppercase表示转换为大写
	
; 	HkGui.Add("Text", "xm", "列表框")
; 	HkGui.Add("Edit")
; 	HkGui.Add("UpDown", "vMyUpDown Range1-10", 5)   ; UpDown 控件自动依附于前一个添加的控件上
	
	; MyGui.Add("Picture", "w300 h-1", "C:\Users\mh.guo\Desktop\PixPin_2025-10-20_14-58-34.png")

	; HkGui.Add("Button",, "&Pause")  ; 按钮

	; HkGui.Add("CheckBox", "vShipToBillingAddress", "Ship to billing address?")   ; 多选框

	; HkGui.Add("Radio", "vMyRadioGroup", "Wait 1")   ; 单选框
	; HkGui.Add("Radio", "vMyRadioGroup1", "Wait 2")

	; HkGui.Add("DropDownList", "vColorChoice", ["Black","White","Red","Green","Blue"])   ; 下拉列表

	; HkGui.Add("ComboBox", "vComboBox", ["Red","Green","Blue","Black","White"])  ; 下拉框

	; HkGui.Add("ListBox", "r5 vListBox", ["Red","Green","Blue","Black","White"])  ;多行列表

	; HkGui.Add("Link",, 'This is a <a href="https://www.autohotkey.com">link</a>')  ; 链接

	; HkGui.Add("Hotkey", "vChosenHotkey")   ; 热键框，可接受同时按多个按键

	; dt := HkGui.Add("DateTime", "vMyDateTime", "LongDate")   ; 时间
	; dt.SetFormat("yyyy-MM-dd HH:mm:ss")

	; HkGui.Add("MonthCal", "vMyCalendar")  ; 日历

	; HkGui.Add("Slider", "vMySlider", 50)  ; 滑块

	; pro := HkGui.Add("Progress", "w200 h20 cBlue vMyProgress", 30)  ; 进度条

	; HkGui.Add("GroupBox", "w200 h100", "Geographic Criteria")   ; GroupBox

	; HkGui.Add("Tab3",, ["General","View","Settings"])   ;tab选项卡
	; HkGui.Add("Tab2",, ["General","View","Settings"])   ;tab选项卡

	; SB := HkGui.Add("StatusBar",, "Bar's starting text.")   ;状态栏
	; SB.SetText("There are 3 rows selected.")

	; WB := HkGui.Add("ActiveX", "w980 h640", "Shell.Explorer").Value  ; 最后一个参数是 ActiveX 组件的名称.
	; WB.Navigate("https://www.baidu.com")  ; 这是特定于网页浏览器控件的.

	; HkGui.Add("Custom", "ClassComboBoxEx32")  ; 添加 ComboBoxEx 控件.

	; HkGui.Title := "UI界面"  ; title

	; HkGui.OnEvent("Close", (*) => Exit())
	; HkGui.OnEvent("Escape", (*) => Exit())
	; HkGui.Show()

; }


;;; ListView控件
; !a::
; {
; 	; 创建窗口:
; 	MyGui := Gui()
	
; 	; 创建含名称和大小两列的 ListView:
; 	LV := MyGui.Add("ListView", "r20 w700", ["Name","Size (KB)"])
	
; 	; 每当用户双击某行时, 通知脚本:
; 	LV.OnEvent("DoubleClick", LV_DoubleClick)
	
; 	; 从文件夹中获取文件名列表并把它们放入 ListView:
; 	Loop Files, A_MyDocuments "\*.*"
; 	    LV.Add(, A_LoopFileName, A_LoopFileSizeKB)
	
; 	LV.ModifyCol  ; 根据内容自动调整每列的大小.
; 	LV.ModifyCol(2, "Integer")  ; 为了进行排序, 指出列 2 是整数.
	
; 	; 显示窗口:
; 	MyGui.Show
	
; 	LV_DoubleClick(LV, RowNumber)
; 	{
; 	    RowText := LV.GetText(RowNumber)  ; 从行的第一个字段中获取文本.
; 	    ToolTip("You double-clicked row number " RowNumber ". Text: '" RowText "'")
; 	}
; }

;;; GUI示例
; MyGui := Gui()
; Tab := MyGui.Add("Tab3",, ["First Tab","Second Tab","Third Tab"])
; MyGui.Add("Checkbox", "vMyCheckbox", "Sample checkbox") 
; Tab.UseTab(2)
; MyGui.Add("Radio", "vMyRadio", "Sample radio1")
; MyGui.Add("Radio",, "Sample radio2")
; Tab.UseTab(3)
; MyGui.Add("Edit", "vMyEdit r5")  ; r5 表示 5 行的高度.
; Tab.UseTab()  ; 即后续添加的控件将不属于前面那个选项卡控件.
; Btn := MyGui.Add("Button", "default xm", "OK")  ; xm 把它放置在左下角.
; Btn.OnEvent("Click", ProcessUserInput)
; MyGui.OnEvent("Close", ProcessUserInput)
; MyGui.OnEvent("Escape", ProcessUserInput)
; MyGui.Show()

; ProcessUserInput(*)
; {
;     Saved := MyGui.Submit()  ; 将命名控件的内容保存到一个对象中.
;     MsgBox("You entered:`n" Saved.MyCheckbox "`n" Saved.MyRadio "`n" Saved.MyEdit)
; }

;;; 鼠标坐标
; MyGui := Gui()
; MyGui.Opt("+AlwaysOnTop -Caption +ToolWindow")  ; +ToolWindow 避免显示任务栏按钮和 alt-tab 菜单项.
; MyGui.BackColor := "EEAA99"  ; 可以是任何 RGB 颜色(下面会变成透明的).
; MyGui.SetFont("s32")  ; 设置大字体(32 磅).
; CoordText := MyGui.Add("Text", "cLime", "XXXXX YYYYY")  ; XX & YY 用来自动调整窗口大小.
; ; 让此颜色的所有像素透明且让文本显示为半透明(150):
; WinSetTransColor(MyGui.BackColor " 150", MyGui)
; SetTimer(UpdateOSD, 200)
; UpdateOSD()  ; 立即进行第一次更新而不等待计时器.
; MyGui.Show("x0 y400 NoActivate")  ; NoActivate 让当前活动窗口继续保持活动状态.

; UpdateOSD(*)
; {
;     MouseGetPos &MouseX, &MouseY
;     CoordText.Value := "X" MouseX ", Y" MouseY
; }


;;; 进度条
; MyGui := Gui()
; MyGui.BackColor := "White"
; MyGui.Add("Picture", "x0 y0 h350 w450", A_WinDir "\Web\Wallpaper\Windows\img0.jpg")
; MyBtn := MyGui.Add("Button", "Default xp+20 yp+250", "Start the Bar Moving")
; MyBtn.OnEvent("Click", MoveBar)
; MyProgress := MyGui.Add("Progress", "w416")
; MyText := MyGui.Add("Text", "wp")  ; wp 表示 "使用之前的宽度".
; MyGui.Show()

; MoveBar(*)
; {
;     Loop Files, A_WinDir "\*.*"
;     {
;         if (A_Index > 100)
;             break
;         MyProgress.Value := A_Index
;         MyText.Value := A_LoopFileName
;         Sleep 50
;     }
;     MyText.Value := "Bar finished."
; }



;;; 图片查看器
; MyGui := Gui("+Resize")
; MyBtn := MyGui.Add("Button", "default", "&Load New Image")
; MyBtn.OnEvent("Click", LoadNewImage)
; MyRadio := MyGui.Add("Radio", "ym+5 x+10 checked", "Load &actual size")
; MyGui.Add("Radio", "ym+5 x+10", "Load to &fit screen")
; MyPic := MyGui.Add("Pic", "xm")
; MyGui.Show()

; LoadNewImage(*)
; {
;     Image := FileSelect(,, "Select an image:", "Images (*.gif; *.jpg; *.bmp; *.png; *.tif; *.ico; *.cur; *.ani; *.exe; *.dll)")
;     if Image = ""
;         return
;     if (MyRadio.Value)  ; 按实际大小显示图像.
;     {
;         Width := 0
;         Height := 0
;     }
;     else ; 选择了第二个单选按钮: 按照屏幕的大小显示图像.
;     {
;         Width := A_ScreenWidth - 28  ; 减去的 28 是用来显示边框和内边缘的空间.
;         Height := -1  ; "保持高宽比" 应该是最好的.
;     }
;     MyPic.Value := Format("*w{1} *h{2} {3}", Width, Height, Image)  ; 载入图像.
;     MyGui.Title := Image
;     MyGui.Show("xCenter y0 AutoSize")  ; 调整窗口以适应图片尺寸.
; }





;;; 创建 MyGui 窗口:
; MyGui := Gui("+Resize", "Untitled")  ; 使窗口可以调整大小.

; ; 为菜单栏创建子菜单:
; FileMenu := Menu()
; FileMenu.Add("&New", MenuFileNew)
; FileMenu.Add("&Open", MenuFileOpen)
; FileMenu.Add("&Save", MenuFileSave)
; FileMenu.Add("Save &As", MenuFileSaveAs)
; FileMenu.Add() ; 分隔线.
; FileMenu.Add("E&xit", MenuFileExit)
; HelpMenu := Menu()
; HelpMenu.Add("&About", MenuHelpAbout)

; ; 通过附加子菜单来创建菜单栏:
; MyMenuBar := MenuBar()
; MyMenuBar.Add("&File", FileMenu)
; MyMenuBar.Add("&Help", HelpMenu)

; ; 添加菜单栏到窗口:
; MyGui.MenuBar := MyMenuBar

; ; 创建主编辑控件:
; MainEdit := MyGui.Add("Edit", "WantTab W600 R20")

; ; 应用事件:
; MyGui.OnEvent("DropFiles", Gui_DropFiles)
; MyGui.OnEvent("Size", Gui_Size)

; MenuFileNew()  ; 应用默认设置.
; MyGui.Show()  ; 显示窗口.

; MenuFileNew(*)
; {
;     MainEdit.Value := ""  ; 清空编辑控件.
;     FileMenu.Disable("3&")  ; 使 &Save 灰色禁用.
;     MyGui.Title := "Untitled"
; }

; MenuFileOpen(*)
; {
;     MyGui.Opt("+OwnDialogs")  ; 强制用户响应 FileSelectFile 对话框后才能返回到主窗口.
;     SelectedFileName := FileSelect(3,, "Open File", "Text Documents (*.txt)")
;     if SelectedFileName = "" ; 没有选择文件.
;         return
;     global CurrentFileName := readContent(SelectedFileName)
; }

; MenuFileSave(*)
; {
;     saveContent(CurrentFileName)
; }

; MenuFileSaveAs(*)
; {
;     MyGui.Opt("+OwnDialogs")  ; 强制用户响应 FileSelectFile 对话框后才能返回到主窗口.
;     SelectedFileName := FileSelect("S16",, "Save File", "Text Documents (*.txt)")
;     if SelectedFileName = "" ; 没有选择文件.
;         return
;     global CurrentFileName := saveContent(SelectedFileName)
; }

; MenuFileExit(*)  ; 用户从 File 菜单中选择 "Exit".
; {
;     WinClose()
; }

; MenuHelpAbout(*)
; {
;     About := Gui("+owner" MyGui.Hwnd)  ; 让主窗口成为 "about box" 的父窗口.
;     MyGui.Opt("+Disabled")  ; 禁用主窗口.
;     About.Add("Text",, "Text for about box.")
;     About.Add("Button", "Default", "OK").OnEvent("Click", About_Close)
;     About.OnEvent("Close", About_Close)
;     About.OnEvent("Escape", About_Close)
;     About.Show()

;     About_Close(*)
;     {
;         MyGui.Opt("-Disabled")  ; 重新启用主窗口(必须在下一步之前进行).
;         About.Destroy()  ; 销毁关于对话框.
;     }
; }

; readContent(FileName)
; {
;     try
;         FileContent := FileRead(FileName)  ; 读取文件的内容到变量中.
;     catch
;     {
;         MsgBox("Could not open '" FileName "'.")
;         return
;     }
;     MainEdit.Value := FileContent  ; 在控件中显示文本.
;     FileMenu.Enable("3&")  ; Re-enable &Save.
;     MyGui.Title := FileName  ; 在标题栏显示文件名.
;     return FileName
; }

; saveContent(FileName)
; {
;     try
;     {
;         if FileExist(FileName)
;             FileDelete(FileName)
;         FileAppend(MainEdit.Value, FileName)  ; 保存内容到文件.
;     }
;     catch
;     {
;         MsgBox("The attempt to overwrite '" FileName "' failed.")
;         return
;     }
;     ; 成功时在标题栏显示文件名(以防 MenuFileSaveAs 被调用的情况):
;     MyGui.Title := FileName
;     return FileName
; }

; Gui_DropFiles(thisGui, Ctrl, FileArray, *)  ; 支持拖拽.
; {
;     CurrentFileName := readContent(FileArray[1])  ; 仅获取首个文件(如果有多个文件的时候).
; }

; Gui_Size(thisGui, MinMax, Width, Height)
; {
;     if MinMax = -1  ; 窗口被最小化了. 无需进行操作.
;         return
;     ; 否则, 窗口的大小被调整过或被最大化了. 调整编辑控件的大小以匹配窗口.
;     MainEdit.Move(,, Width-20, Height-20)
; }



;;; 下面的文件夹为 TreeView 的根文件夹. 请注意, 如果指定整个驱动器例如 C:\
; 那么可能需要很长加载时间:
; TreeRoot := A_MyDocuments
; TreeViewWidth := 280
; ListViewWidth := A_ScreenWidth/2 - TreeViewWidth - 30

; ;  创建 Gui 窗口并在标题栏中显示源目录(TreeRoot):
; MyGui := Gui("+Resize", TreeRoot)  ; 让用户可以最大化或拖动调整窗口大小.

; ; 创建图像列表并在其中放入一些标准的系统图标:
; ImageListID := IL_Create(5)
; Loop 5 
;     IL_Add(ImageListID, "shell32.dll", A_Index)
; ; 创建 TreeView 和 ListView, 让它们像在 Windows 资源管理器中那样靠在一起:
; TV := MyGui.Add("TreeView", "r20 w" TreeViewWidth " ImageList" ImageListID)
; LV := MyGui.Add("ListView", "r20 w" ListViewWidth " x+10", ["Name","Modified"])

; ; 创建状态栏/, 显示文件夹数及其总大小的信息:
; SB := MyGui.Add("StatusBar")
; SB.SetParts(60, 85)  ; 在状态栏中创建三个部分(第三部分占用所有剩余宽度).

; ; 添加文件夹及其子文件夹到树中. 如果加载需要很长时间, 则显示提示信息:
; M := Gui("ToolWindow -SysMenu Disabled AlwaysOnTop", "Loading the tree..."), M.Show("w200 h0")
; DirList := AddSubFoldersToTree(TreeRoot, Map())
; M.Hide()

; ; 每当有新的项目被选中时, 调用 TV_ItemSelect:
; TV.OnEvent("ItemSelect", TV_ItemSelect)

; ; 每当窗口被调整大小时, 调用 Gui_Size:
; MyGui.OnEvent("Size", Gui_Size)

; ; 设置 ListView 的列宽(这是可选的):
; Col2Width := 70  ; 缩小到只显示 YYYYMMDD 部分.
; LV.ModifyCol(1, ListViewWidth - Col2Width - 30)  ; 允许垂直滚动条.
; LV.ModifyCol(2, Col2Width)

; ; 显示窗口并返回. 每当用户执行符合条件的动作时, 操作系统会通知脚本:
; MyGui.Show

; AddSubFoldersToTree(Folder, DirList, ParentItemID := 0)
; {
;     ; 该函数将指定文件夹中的所有子文件夹添加到 TreeView 中,
;     ; 并将它们与 ID 相关联的路径保存到一个对象中, 供以后使用.
;     ; 它还可以递归地调用自己来收集任意深度的嵌套文件夹.
;     Loop Files, Folder "\*.*", "D"  ; 获取所有文件夹的子文件夹.
;     {
;         ItemID := TV.Add(A_LoopFileName, ParentItemID, "Icon4")
;         DirList[ItemID] := A_LoopFilePath
;         DirList := AddSubFoldersToTree(A_LoopFilePath, DirList, ItemID)
;     }
;     return DirList
; }

; TV_ItemSelect(thisCtrl, Item)  ; 当选择一个新的项目时, 该函数被调用.
; {
;     ; 将文件放入 ListView 中:
;     LV.Delete  ; 清除所有行.
;     LV.Opt("-Redraw")  ; 通过在加载过程中禁止重绘来提高性能.
;     TotalSize := 0  ; 在下面循环之前初始化.
;     Loop Files, DirList[Item] "\*.*"  ; 为了简化, 这里省略了文件夹, 所以只在 ListView 中显示文件.
;     {
;         LV.Add(, A_LoopFileName, A_LoopFileTimeModified)
;         TotalSize += A_LoopFileSize
;     }
;     LV.Opt("+Redraw")

;     ; 更新状态栏的三个部分, 让它们显示当前选择的文件夹的信息:
;     SB.SetText(LV.GetCount() " files", 1)
;     SB.SetText(Round(TotalSize / 1024, 1) " KB", 2)
;     SB.SetText(DirList[Item], 3)
; }

; Gui_Size(thisGui, MinMax, Width, Height)  ; 当用户改变窗口大小时扩展/收缩 ListView 和 TreeView.
; {
;     if MinMax = -1  ; 窗口被最小化了. 无需进行操作.
;         return
;     ; 否则, 窗口的大小被调整过或被最大化了. 调整控件大小以适应.
;     TV.GetPos(,, &TV_W)
;     TV.Move(,,, Height - 30)  ; -30 用于状态栏和边距.
;     TV.Move(,, Width - TV_W - 30, Height - 30)
; }



; !a::
; {
; 	TrayTip "Multiline`nText", "My Title", "Iconi Mute"   ; 显示多行气球消息
; }


;;; 鼠标事件
; !a::
; {
	; Click "100 200 0"   ; 移动光标到坐标点
	; Click   ; 在当前位置点击
	; Click "100 200"  ; 在指定坐标处点击鼠标左键
	; Click "100 200 Right"   ; 在指定坐标处点击鼠标右键
	; Click 2   ; 在当前位置双击
	; Click "Down"   ; 按下鼠标左键不放.
	; Click "Up Right"  ; 释放鼠标右键
	; MouseClick "right", 55, 233   ; 点击鼠标右键
; }


;;; 向控件发送消息
; Run "Notepad",, "Min", &PID  ; 最小化运行记事本.
; WinWait "ahk_pid " PID  ; 等待记事本进程的出现.
; ; 将文本发送到不活动的记事本编辑控件.
; ; 省略了第三个参数, 因此使用最后一个找到的窗口.
; ControlSend "This is a line of text in the notepad window.{Enter}", "Edit1"
; ControlSendText "Notice that {Enter} is not sent as an Enter keystroke with ControlSendText.", "Edit1"

; Msgbox "Press OK to activate the window to see the result."
; WinActivate "ahk_pid " PID  ; 显示结果.


;;; 启动CMD命令
; SetTitleMatchMode 2
; Run A_ComSpec,,, &PID  ; 打开命令提示符.
; WinWait "ahk_pid " PID  ; 等待它的出现.
; ControlSend "ipconfig{Enter}",, "cmd.exe"  ; 直接发送到命令提示符窗口.


;;; 获取鼠标坐标
; !a::
; {
; 	MouseGetPos &OutputVarX, &OutputVarY
; 	MsgBox "x:" OutputVarX " y:"  OutputVarY
; }

; SendEvent "{Click 100 200}"  ; SendEvent 使用更老更传统的点击方法. 在指定位置点击


;;; 下载文件
; !a::
; {
	; Download "https://www.autohotkey.com/download/2.0/version.txt", "C:\AutoHotkey Latest Version.txt"  ; 从网络下载文本文件
; }


;;; 发送GET请求
; !a::
; {
	; whr := ComObject("WinHttp.WinHttpRequest.5.1")
	; whr.Open("GET", "http://autohotkey.com/download/2.0/version.txt")   
	; whr.Send()
	; ; 使用 'true'(上面) 和调用下面的函数, 允许脚本保持响应.
	; whr.WaitForResponse()
	; version := whr.ResponseText
	; MsgBox version
; }


;;;  发出异步 HTTP 请求.
; !a::
; {
; 	req := ComObject("Msxml2.XMLHTTP")
; 	; 打开启用异步的请求.
; 	req.open("GET", "https://www.autohotkey.com/download/2.0/version.txt", true)
; 	; 设置回调函数.
; 	req.onreadystatechange := Ready
; 	; 发送请求. Ready() 将在其完成后被调用.
; 	req.send()
	
; 	; 如果你要一直等待到下载完毕, 就不需要 onreadystatechange 了.
; 	; 设置 async=true 和像这样等待, 可以在下载过程中允许脚本保留响应
; 	; 而 async=false 将使脚本无响应.
; 	while req.readyState != 4
; 	    sleep 100
	
; 	Persistent
	
; 	Ready() {
; 	    if (req.readyState != 4)  ; 没有完成.
; 	        return
; 	    if (req.status == 200) ; OK.
; 	        MsgBox "Latest AutoHotkey version: " req.responseText
; 	    else
; 	        MsgBox "Status " req.status,, 16
; 	    ExitApp
; 	}
; }


;;; Persistent   ;防止脚本在最后一个线程完成后自动退出, 允许它在空闲状态下运行.

;;; 进程
; !a::{
	; WinClose "记事本"  ; 关闭某个进程
	; ProcessClose "notepad.exe"  ; 强制关闭某个进程
	; ProcessExist("notepad.exe")  ; 检查某个进程是否存在

	/*Run "notepad.exe", , , &NewPID
	ProcessSetPriority "High", NewPID   ; 设置进程优先级
	MsgBox "The newly launched Notepad's PID is " NewPID*/

	; ProcessWait("notepad.exe", 5.5)  ; 等待某个进程出现
	; ProcessWaitClose("notepad.exe" , 5.5)  ;等待匹配进程关闭.

	/*RunAs "Administrator", "MyPassword"   ; 指定在后续所有的 Run 和 RunWait 中使用的一组用户凭据.
	Run "RegEdit.exe"
	RunAs  ; 恢复为普通行为.*/

	; Shutdown 6  ; 关机, 重启或注销系统. 0 = 注销 1 = 关机 2 = 重启 4 = 强制 8 = 关闭电源
; }


; !a::
; {
	; CoordMode "Mouse", "Screen"
	;获取鼠标位置的颜色,坐标相对于活动窗口
	; MouseGetPos &MouseX, &MouseY
	; MsgBox MouseX "," MouseY
    ; MsgBox PixelGetColor(MouseX, MouseY)

    ; WinMaximize("2.txt - 记事本")   ; 最大化窗口
    ; WinMove 0, 0,,, "2.txt - 记事本"   ; 移动窗口
    ; WinSetEnabled(-1,"2.txt - 记事本")  ; 锁定窗口

    ; WinSetRegion "50-0 W200 H250", "2.txt - 记事本"  ; 将指定窗口的形状改为指定的矩形, 椭圆或多边形.
    ; WinSetRegion , "2.txt - 记事本"   ;将窗口恢复原来/默认的形状.

    ; WinSetTransColor "White", "2.txt - 记事本"   ;使所选颜色的所有像素在指定的窗口内不可见(透明).
    ; WinSetTransColor "off", "2.txt - 记事本"   ;关闭不透明度

    ; WinSetTransparent 10, "2.txt - 记事本"    ; 指定窗口改为半透明
    ; WinSetTransparent "off", "2.txt - 记事本" ; 关闭半透明
; }


; !a::{
		; send("{LCtrl DOWN}c{LCtrl UP}")
		; Sleep 50
		; MsgBox A_Clipboard      ; 获取选择的文本
		; if(not RegExMatch(A_Clipboard, "'''\w+'''"))
		; {
		; 	MsgBox "1"
		; }
		; else
		; {
		; 	MsgBox "2"
		; }

		; if(InStr("123abc789", "abc")>0)
		; {
		; 	send "{BS}"
		; 	send RegExReplace(A_Clipboard, "'''", "")
		; }
		; else
		; {
		; 	MsgBox "2"
		; }
; }


; 点击控件
; ControlClick Control-or-Pos, WinTitle, WinText, WhichButton, ClickCount, Options, ExcludeTitle, ExcludeText   ; 点击某个控件
; ^+d::
; {
	; ControlClick "查询", "SajetMES - [复判时间验证]"    ; 通过控件Text + window Title定位按钮
	; ControlClick "WindowsForms10.BUTTON.app.0.378734a10", "SajetMES - [复判时间验证]"   ; 通过控件ClassNN + window Title定位按钮
	; ControlClick "x281 y862", "Anx Reader"   ; 通过控件Client坐标 + window Title定位按钮
; }




; ^+d::
; {
	; 目标窗口标题 := "新建文本文档.txt - 记事本"
 ;    ; 激活窗口（确保窗口在前台，坐标计算准确）
 ;    WinActivate(目标窗口标题)
 ;    WinWaitActive(目标窗口标题)
    
 ;    ; 获取窗口左上角的屏幕坐标（WinGetPos）
 ;    WinGetPos(&窗口X, &窗口Y, , , 目标窗口标题)
    
 ;    ; 计算按钮的屏幕坐标（窗口相对坐标 + 窗口左上角坐标）
 ;    按钮相对X := 34  ; 按钮在窗口内的X偏移（从Window Spy查看“Position: Client”）
 ;    按钮相对Y := 39  ; 按钮在窗口内的Y偏移
 ;    屏幕X := 窗口X + 按钮相对X
 ;    屏幕Y := 窗口Y + 按钮相对Y
 ;    ; 执行点击
 ;    ; Click "屏幕X 屏幕Y"
    ; MouseMove(41,-14)   ; 相对于活动窗口的坐标

    ; WinActivate("记事本")
    ; WinWaitActive("记事本")
    ; Click(41,-14)   ; 相对于活动窗口的坐标
    ; Sleep 200
    ; Click(80,100)
    ; send "1111.txt"
    ; Sleep 200
    ; Click(581,414)
; }




;;;浏览器搜索
; ^+d::
; {
; 	run "https://cn.bing.com/"
; 	WinActivate("Google Chrome")
;     WinWaitActive("Google Chrome")
;     Sleep 500
;     ControlClick "Chrome_RenderWidgetHostHWND1", "Google Chrome"
;     sendInput "anx reader"
;     sendInput "{Enter}"
;     ; ExitApp
; }



; class IMG
; {
; 	__New()   ; 首先执行构造函数
; 	{
; 	}
; 	imgfind(ImagePath)    ; 图片定位
; 	{
; 		; 1. 按钮截图路径（建议裁剪按钮核心区域，避免背景干扰）
; 	    ; ImagePath := A_ScriptDir "\" path  ; A_ScriptDir 是脚本所在目录
; 	    if !FileExist(ImagePath) {
; 	        MsgBox "截图文件不存在：" ImagePath
; 	        return
; 	    }
	    
; 	    ; 2. 搜索范围：整个屏幕（X1=0, Y1=0, X2=屏幕宽, Y2=屏幕高）
; 	    SearchArea := [0, 0, A_ScreenWidth, A_ScreenHeight]
; 	    ; ImageSearch(&FoundX, &FoundY, X1, Y1, X2, Y2, ImageFile, *Variation := 0, Direction := "Fast")
; 	    ; ImageSearch(输出变量数组, 搜索范围, 图片路径, 颜色偏差*,搜索方向)   ; Fast 快速 /Slow 精准，默认 Fast）
; 	    Found := ImageSearch(&OutputX, &OutputY, SearchArea[1], SearchArea[2], SearchArea[3], SearchArea[4], ImagePath)
; 	    ; &OutputX/&OutputY：V2 中通过引用传递获取结果；*50=允许50级颜色偏差
	    
; 	    ; 3. 搜索到后点击按钮中心
; 	    if Found {  ; Found 为 true 表示找到图像
; 	        ; 假设截图宽30、高20（根据实际截图调整）
; 	        ButtonCenterX := OutputX + 15  ; 截图宽度/2
; 	        ButtonCenterY := OutputY + 10  ; 截图高度/2
; 	        coordinates := [ButtonCenterX,ButtonCenterY]
; 	        ; Click ButtonCenterX, ButtonCenterY
; 	        return coordinates
; 	    } else {
; 	        MsgBox "未找到按钮！"
; 	    }
; 	}
; 	__Delete()   ; 再执行析构函数
; 	{
; 	}
; }
; ^+d::
; {
; 	imgU := IMG()
; 	coordinates := imgU.imgfind("tu.png")
; 	Click(coordinates[1], coordinates[2], 2)
; }


; 纯字母快捷键，区分大小写
/*:*C:fg::
{
	if(GetKeyState("f"))  ; 按住f不松触发快捷键
	{
		MsgBox "12345"
	}
	else    ; 松开正常输入字母
	{
		SendInput "fg"
	}
}*/

; 包括在整个字符串中依然生效
/*:?:h6::
{
    SendInput "{CTRL down}p{CTRL up}"
    SendInput "小标题6"
    SendInput "{enter}"
}
*/


/*
::qw::
{
	; Run "C:\Users\mh.guo\Desktop\1.txt"
	; Sleep 500
	; aa:=ControlGetText("Edit1","ahk_class Notepad")   ;获取控件文本
	; MsgBox aa
}
*/


; 切换网络连接
/*
; AHK V2 用 COM 获取当前网络名称（WiFi/有线都可以）
; GetNetworkName() {
;     try {
;         ; 实例化网络列表管理器
;         nlm := ComObject("{DCB00C01-570F-4A9B-8D69-199FDBA5723B}")
;         ; 1 = 仅遍历 已连接 的网络
;         enumNet := nlm.GetNetworks(1)
        
;         ; 遍历所有已连接网络
;         for net in enumNet {
;             netName := net.GetName()
;             desc := net.GetDescription()
;             ; 返回真实网络名，不再过滤未识别网络
;             return netName
;         }
;     } catch {
;         return "读取失败"
;     }
;     return "无网络连接"
; }
; ::wl::
; {	
; 	if GetNetworkName()=="luxshare.com.cn"
; 	{
; 		Run "CMD.exe"
; 		Sleep 500
; 			SendInput "netsh wlan connect name=`"BUD-VIP`""
; 			SendInput "{enter}"
; 			SendInput "exit"
; 			SendInput "{enter}"
; 	}
; 	Else
; 	{
; 		Run "CMD.exe"
; 		Sleep 500
; 			SendInput "netsh wlan connect name=`"Luxshare-Office`""
; 			SendInput "{enter}"
; 			SendInput "exit"
; 			SendInput "{enter}"
; 	}
; }
*/


; 显示多行气球消息
/*
^!l::
{
	TrayTip("hello","提示","Iconx")
	Sleep 3000   ; 让它显示 3 秒钟.
	; TrayTip
	HideTrayTip()
}
; 将此函数复制到脚本中使用.
HideTrayTip() {
    TrayTip  ; 尝试以普通的方式隐藏它.
    if SubStr(A_OSVersion,1,3) = "10." {
        A_IconHidden := true
        Sleep 200  ; 可能有必要调整 sleep 的时间.
        A_IconHidden := false
    }
}
*/