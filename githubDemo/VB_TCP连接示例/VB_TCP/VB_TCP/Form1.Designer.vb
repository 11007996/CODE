<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtip = New System.Windows.Forms.TextBox()
        Me.txtport = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.butconnect = New System.Windows.Forms.Button()
        Me.txtmsg = New System.Windows.Forms.RichTextBox()
        Me.butsend = New System.Windows.Forms.Button()
        Me.txtsend = New System.Windows.Forms.TextBox()
        Me.butdis = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtip
        '
        Me.txtip.Location = New System.Drawing.Point(97, 12)
        Me.txtip.Name = "txtip"
        Me.txtip.Size = New System.Drawing.Size(206, 25)
        Me.txtip.TabIndex = 0
        Me.txtip.Text = "172.17.208.1"
        '
        'txtport
        '
        Me.txtport.Location = New System.Drawing.Point(309, 12)
        Me.txtport.Name = "txtport"
        Me.txtport.Size = New System.Drawing.Size(64, 25)
        Me.txtport.TabIndex = 0
        Me.txtport.Text = "2002"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "IP:Port"
        '
        'butconnect
        '
        Me.butconnect.Location = New System.Drawing.Point(394, 12)
        Me.butconnect.Name = "butconnect"
        Me.butconnect.Size = New System.Drawing.Size(61, 24)
        Me.butconnect.TabIndex = 2
        Me.butconnect.Text = "连接"
        Me.butconnect.UseVisualStyleBackColor = True
        '
        'txtmsg
        '
        Me.txtmsg.Location = New System.Drawing.Point(31, 43)
        Me.txtmsg.Name = "txtmsg"
        Me.txtmsg.Size = New System.Drawing.Size(491, 198)
        Me.txtmsg.TabIndex = 3
        Me.txtmsg.Text = ""
        '
        'butsend
        '
        Me.butsend.Enabled = False
        Me.butsend.Location = New System.Drawing.Point(461, 250)
        Me.butsend.Name = "butsend"
        Me.butsend.Size = New System.Drawing.Size(61, 24)
        Me.butsend.TabIndex = 2
        Me.butsend.Text = "发送"
        Me.butsend.UseVisualStyleBackColor = True
        '
        'txtsend
        '
        Me.txtsend.Location = New System.Drawing.Point(31, 249)
        Me.txtsend.Name = "txtsend"
        Me.txtsend.Size = New System.Drawing.Size(424, 25)
        Me.txtsend.TabIndex = 0
        '
        'butdis
        '
        Me.butdis.Enabled = False
        Me.butdis.Location = New System.Drawing.Point(461, 12)
        Me.butdis.Name = "butdis"
        Me.butdis.Size = New System.Drawing.Size(61, 24)
        Me.butdis.TabIndex = 2
        Me.butdis.Text = "断开"
        Me.butdis.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(546, 300)
        Me.Controls.Add(Me.txtmsg)
        Me.Controls.Add(Me.butsend)
        Me.Controls.Add(Me.butdis)
        Me.Controls.Add(Me.butconnect)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtport)
        Me.Controls.Add(Me.txtsend)
        Me.Controls.Add(Me.txtip)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtip As System.Windows.Forms.TextBox
    Friend WithEvents txtport As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents butconnect As System.Windows.Forms.Button
    Friend WithEvents txtmsg As System.Windows.Forms.RichTextBox
    Friend WithEvents butsend As System.Windows.Forms.Button
    Friend WithEvents txtsend As System.Windows.Forms.TextBox
    Friend WithEvents butdis As System.Windows.Forms.Button

End Class
