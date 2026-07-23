;;; MES 快捷键 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
#Hotif ProcessExist("SajetMESReport.exe") or ProcessExist("SajetMES58.exe") or ProcessExist("SajetMES.exe")
    ; ::ex::
    ; {
    ;     ; 1. 创建 Excel 实例
    ;     Excel := ComObject("Excel.Application")
    ;     Excel.Visible := false
    ;     Excel.DisplayAlerts := false
        
    ;     ; 2. 打开指定文件（路径需准确，支持相对路径或绝对路径）
    ;     ; 文件路径 := A_Desktop "\rule_violations.xlsx"
    ;     Loop Files, A_Desktop "\rule_violations*.xlsx"
    ;         文件路径 := A_Desktop "\" A_LoopFileName
    ;     if (!FileExist(文件路径)) {  ; 检查文件是否存在
    ;         MsgBox "文件不存在：" 文件路径
    ;         Excel.Quit()
    ;         Excel := ""
    ;         return
    ;     }
    ;     工作簿 := Excel.Workbooks.Open(文件路径)
        
    ;     ; 3. 选择工作表并读取内容
    ;     工作表 := 工作簿.Worksheets(1)
    ;     使用区域 := 工作表.UsedRange
    ;     ; MsgBox 使用区域.Address
    ;     rowNum := StrSplit(使用区域.Address, "$")[-1]
    ;     数据数组 := 工作表.Range("C3:C" rowNum).Value   ; 批量读取数据
    ;     ; MsgBox 数据数组[1,1]
    ;     if !IsObject(数据数组) {  ; 处理区域无数据的情况
    ;             MsgBox "目标区域无数据！"
    ;             return
    ;         }
    ;     ; str :=""
    ;     loop rowNum-2{
    ;         str .=数据数组[A_Index,1] "`n"
    ;     }
    ;     A_Clipboard :=str
    ;     ; 5. 关闭（无需保存可直接关闭）
    ;     工作簿.Close(SaveChanges := false)  ; SaveChanges=false 不保存修改
    ;     Excel.Quit()
    ;     Excel := ""
    ; }

    ::dl::
    {
        SendInput userNo
        ; Sleep 100
        SendInput "{tab}"
        Sleep 100
        SendInput mesmm
        Sleep 100
        ; SendInput "{enter}{enter}{enter}"    
        SendInput "{enter}"
        SendInput "{esc}{esc}"
        Sleep 500
        ControlClick "x19 y141", "SajetMES"
        Sleep 300
        ControlClick "x77 y149", "SajetMES"
        Sleep 300
        MouseMove(85 ,165)
        if(WinActive(,"52"))
            Click
    }
#hotif
;;; MES 快捷键 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;