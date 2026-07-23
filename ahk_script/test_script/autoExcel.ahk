; 写入EXCEL
; !a::
; {
; 	; 1. 创建 Excel 实例（Visible := true 显示Excel窗口，false 后台运行）
;     ; Excel := ComObjCreate("Excel.Application")
;     Excel := ComObject("Excel.Application")
;     Excel.Visible := true  ; 显示Excel（调试时建议开启，最终可设为false）
;     ; Excel.DisplayAlerts := false  ; 禁用警告框（如覆盖文件时不弹窗）
    
;     ; 2. 新建空白工作簿（默认1个工作表：Sheet1）
;     工作簿 := Excel.Workbooks.Add()
    
;     ; 3. 选择工作表（通过名称或索引，索引从1开始）
;     工作表 := 工作簿.Worksheets("Sheet1")  ; 按名称选择
;     ; 工作表 := 工作簿.Worksheets(1)  ; 按索引选择（第1个工作表）
    
;     ; 4. 写入单元格内容（3种常用方式）
;     工作表.Range("A1").Value := "姓名"  ; 方式1：通过单元格地址（推荐）
;     工作表.Cells(1, 2).Value := "年龄"  ; 方式2：通过行号+列号（行1，列2 = B1）
;     ; 工作表.Range("A2:B3").Value := [["张三", 25], ["李四", 30]]  ; 方式3：批量写入数组

;     ; 3. 定义要写入的数据（AHK V2 嵌套数组）
;         数据 := [["张三", 25], ["李四", 30], ["王五", 35]]  ; 3行2列
;         总行数 := 数据.Length  ; 3行
;         总列数 := 数据[1].Length  ; 2列（假设每行列数相同）
        
;         ; 4. 创建 COM 二维安全数组（参数：数据类型=VT_VARIANT(12), 行数, 列数）
;         ; VT_VARIANT(12) 支持任意数据类型（文本、数字等）
;         安全数组 := ComObjArray(12, 总行数, 总列数)
        
;         ; 5. 将 AHK 数组数据写入安全数组（COM 数组索引从0开始！）
;         for 行索引, 行数据 in 数据 {
;             com行索引 := 行索引 - 1  ; AHK 数组索引1→COM数组索引0
;             for 列索引, 单元格值 in 行数据 {
;                 com列索引 := 列索引 - 1  ; AHK 数组索引1→COM数组索引0
;                 安全数组[com行索引, com列索引] := 单元格值
;             }
;         }
;         ; 6. 批量赋值给 Excel 区域（A2:B4 对应3行2列）
;         目标区域 := 工作表.Range("A2:B" (1+总行数))  ; A2 到 B4（1+3=4）
;         目标区域.Value := 安全数组
    
;     ; 5. 保存文件（指定路径，需确保路径存在）
;     保存路径 := A_Desktop "\测试文件.xlsx"  ; A_Desktop 是桌面路径
;     工作簿.SaveAs(保存路径)
    
;     ; 6. 关闭工作簿和Excel实例（避免后台残留进程）
;     工作簿.Close()
;     Excel.Quit()
;     Excel := ""  ; 释放COM对象
;     MsgBox "excel创建完成"
; }







; 读取EXCEL
; !a::
; {
; 	; 1. 创建 Excel 实例
;     Excel := ComObject("Excel.Application")
;     Excel.Visible := true
;     Excel.DisplayAlerts := false
    
;     ; 2. 打开指定文件（路径需准确，支持相对路径或绝对路径）
;     文件路径 := A_Desktop "\测试文件.xlsx"
;     if (!FileExist(文件路径)) {  ; 检查文件是否存在
;         MsgBox "文件不存在：" 文件路径
;         Excel.Quit()
;         Excel := ""
;         return
;     }
;     工作簿 := Excel.Workbooks.Open(文件路径)
    
;     ; 3. 选择工作表并读取内容
;     工作表 := 工作簿.Worksheets("Sheet1")
;     姓名1 := 工作表.Range("A2").Value  ; 读取 A2 单元格
;     年龄2 := 工作表.Cells(3, 2).Value  ; 读取第3行第2列（B3）

;     数据数组 := 工作表.Range("A2:B3").Value   ; 批量读取数据
;     MsgBox "第1行第1列：" 数据数组[1,1] "第2行第2列：" 数据数组[2,2]
    
;     ; 4. 显示读取结果
;     MsgBox "读取结果：A2：" 姓名1 "B3：" 年龄2

;     ; 获取工作表中已使用的区域（从A1开始到最后一个有内容的单元格）
;     使用区域 := 工作表.UsedRange
;     msgbox "使用区域：" 使用区域.Address
    
;     ; 5. 关闭（无需保存可直接关闭）
;     工作簿.Close(SaveChanges := false)  ; SaveChanges=false 不保存修改
;     Excel.Quit()
;     Excel := ""
; }







; 修改EXCEL
; !a::
; {
;     ; 1. 创建 Excel 实例
;     Excel := ComObject("Excel.Application")
;     Excel.Visible := true
;     Excel.DisplayAlerts := false
    
;     ; 2. 打开指定文件（路径需准确，支持相对路径或绝对路径）
;     文件路径 := A_Desktop "\测试文件.xlsx"
;     if (!FileExist(文件路径)) {  ; 检查文件是否存在
;         MsgBox "文件不存在：" 文件路径
;         Excel.Quit()
;         Excel := ""
;         return
;     }
;     工作簿 := Excel.Workbooks.Open(文件路径)
;     工作表 := 工作簿.Worksheets("Sheet1")

;     ; 单元格写入公式
;     ; 工作表.Range("C2").Formula := "=B2+10"  ; 计算年龄+10（Excel中显示公式结果）
;     ; 工作表.Range("C3").FormulaLocal := "=SUM(B2:B3)"  ; FormulaLocal 支持中文函数名（如“求和”）

;     ; 选择 A1:B1 标题行，设置格式
;     ; 标题区域 := 工作表.Range("A1:B1")
;     ; 标题区域.Font.Name := "微软雅黑"  ; 字体
;     ; 标题区域.Font.Size := 12  ; 字号
;     ; 标题区域.Font.Bold := true  ; 加粗
;     ; 标题区域.Font.Color := 0xFFFFFF  ; 字体颜色（白色，十六进制RGB）
;     ; 标题区域.Interior.Color := 0x0066CC  ; 背景色（蓝色）
;     ; 标题区域.HorizontalAlignment := -4108  ; 水平居中（-4108 = xlCenter）
;     ; 标题区域.VerticalAlignment := -4108  ; 垂直居中
;     ;; 水平居中    xlCenter    -4108
;     ;; 水平左对齐   xlLeft  -4131
;     ;; 水平右对齐   xlRight -4152
;     ;; 垂直居中    xlCenter    -4108
;     ; 标题区域.RowHeight := 25  ; 行高
;     ; 标题区域.ColumnWidth := 12  ; 列宽
      ; 工作表.UsedRange.EntireColumn.AutoFit()   ; 自动调整列宽


;     工作簿.Close(SaveChanges := true)  ; SaveChanges=false 不保存修改
;     Excel.Quit()
;     Excel := ""
; }








; 文件管理
; !a::
; {
;     Excel := ComObject("Excel.Application")
;     Excel.Visible := false
;     工作簿 := Excel.Workbooks.Add()
    
;     ; 1. 新建工作表
;     新工作表 := 工作簿.Worksheets.Add()
;     新工作表.Name := "员工数据"  ; 重命名工作表（不能包含特殊字符）
    
;     ; 2. 复制工作表（在当前工作表后复制）
;     新工作表.Copy(, 新工作表)  ; 逗号前是“复制到哪个工作表之前”，逗号后是“复制到哪个工作表之后”
;     工作簿.Worksheets(3).Name := "员工数据备份"  ; 给复制的工作表重命名
    
;     ; 3. 删除工作表（需禁用警告框，否则会弹窗确认）
;     工作簿.Worksheets(2).Delete()  ; 删除默认的Sheet1
;     新工作表.Copy(, 新工作表)
;     工作簿.Worksheets("员工数据 (2)").Delete()  ; 删除默认的Sheet1
    
;     ; 4. 激活工作表（切换到指定工作表）
;     工作簿.Worksheets("员工数据").Activate()
    
;     工作簿.SaveAs(A_Desktop "\工作表管理.xlsx")
;     工作簿.Close()
;     Excel.Quit()
;     Excel := ""
;     MsgBox "工作表管理完成！"
; }