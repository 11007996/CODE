#include "..\AHKtool\FindText.ahk"

::ft::{
	; ImageFile := "D:\ruanjian\AHK\ahk_script\test_script\入门.png"
	; iffind :=FindText().ImageSearch(&OutputVarX , &OutputVarY, 0, 0, 1300, 1000, ImageFile)
	; if(iffind)
	; {
	; 	MouseMove(OutputVarX,OutputVarY)
	; }
	; else
	; {
	; 	MsgBox "no find"
	; }



	Text :="|<tu1>*143$48.000004UCTzlzz4bkE0FE04Y0E0FE04Y0LzFE04Y0EEFbzbrwEEFEE464LzFEE474EIF9G7Z4EGF/H4Z8LzF+F4YsE0FqFYckE0F4EgcsTzl0E8vAE0H1k8q2U"
	if (ok:=FindText(&X, &Y, 603-150000, 190-150000, 603+150000, 190+150000, 0, 0, Text))
	{
	  ; FindText().Click(X, Y, "L")
	  MouseMove(X,Y)
	}
	else
	{
		MsgBox "no find"
	}
}




; returnArray := FindText(
;     OutputX --> 该变量用于存储找到的第一张图片中心的 X 坐标。OutputX 也可以设为"wait"、"wait1" (appear) 或"wait0" (disappear)，以使 FindText 要么等待文本出现或消失。如果将 OutputX 留空，则不进行搜索，并返回一个 FindTextClass 实例：在 FindText().Click 示例中使用了这样的代码，等价于 FindText(,,,,,,,,Text).Click （Text 会被忽略）。调用 FindText 执行搜索的最简单形式为 FindText(X,,,,,,,,Text).
;     , OutputY --> 该变量用于存储找到的第一张图片中心的 Y 坐标。如果 OutputX 设为 "wait"，则 OutputY 可以设置为等待的秒数，负数表示无限等待。[/list]
;     , X1 --> 搜索区域左上角的 X 坐标
;     , Y1 --> 搜索区域左上角的 Y 坐标
;     , X2 --> 搜索区域右下角的 X 坐标
;     , Y2 --> 搜索区域右下角的 Y 坐标
;     如果 X1, Y1, X2, Y2 都设为 0 （默认值），则 X1 和 Y1 会被设为 -150000，X2 和 Y2 被设为 150000。这样会将搜索区域设为整个屏幕以及所有不在屏幕上的东西也包括在内。
;     , err1 --> 文本的容错比例（text=黑色="o"）（0.1=10%）。
;     , err0 --> 背景的容错比例（background=白色="_"） （0.1=10%）
;     如果 err1 和 err0 都为 0，并且没有找到匹配项，则 FindText 会自动使用 0.05=5% 错误容限再次进行搜索。为避免这种情况，可以将 err1 和 err0 指定为很小的一个值（如 0.000001，很接近 0，但是不等于 0）。
;     , Text --> 可以是多个文本形式的图片，图片之间用 "|" 分隔
;     , ScreenShot --> 取值为 0 时，使用上一次截屏，默认为 1（每次都重新截屏）
;     , FindAll --> 取值为 0 时，找到第一个结果后立刻返回，默认为 1（查找全部结果）
;     , JoinText --> 用于组合查询：可以是 1，也可以是要查找的单词数组，默认为 0
;     , offsetX --> 设置组合查找时 X 方向最大文本偏移量
;     , offsetY --> 设置组合查找时 Y 方向最大文本偏移量
;     , dir --> 指定搜索方向，共9种取值：
;     1 ==> ( 从左到右 ) 从上到下
;     2 ==> ( 从右到左 ) 从上到下
;     3 ==>（从左到右）从下到上
;     4 ==>（从右到左）从下到上
;     5 ==>（从上到下）从左到右
;     6 ==>（从下到上）从左到右
;     7 ==>（从上到下）从右到左
;     8 ==>（从下到上）从右到左
;     9 ==>从中心向外
; )