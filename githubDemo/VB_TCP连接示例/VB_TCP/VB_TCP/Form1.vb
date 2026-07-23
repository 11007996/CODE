Imports System.Net.Sockets
'使用到TcpListen类
Imports System.IO
'使用到StreamWriter类
Imports System.Net
'使用IPAddress类、IPHostEntry类等
Imports System.Threading

Public Class Form1
    Private swWriter As StreamWriter
    Private srRead As StreamReader
    '用以向网络基础数据流传送数据
    Private nsStream As NetworkStream
    '创建发送数据的网络基础数据流
    Private tcpClient As TcpClient
    '通过它实现向远程主机提出TCP连接申请
    Private tcpConnect As Boolean = False
    '定义标识符，用以表示TCP连接是否建立
    Private Sub butconnect_Click(sender As Object, e As EventArgs) Handles butconnect.Click
        Dim ipRemote As IPAddress
        'Dim tcpClient As TcpClient
        Try
            ipRemote = IPAddress.Parse(txtip.Text)
        Catch
            MessageBox.Show("输入的IP地址不合法！", "错误提示！")
            Return
            '判断给定的IP地址的合法性
        End Try
        Try
            tcpClient = New TcpClient(txtip.Text, Integer.Parse(txtport.Text))
            '对远程主机的8000端口提出TCP连接申请
            nsStream = tcpClient.GetStream()
            '通过申请，并获取传送数据的网络基础数据流
            swWriter = New StreamWriter(nsStream)
            srRead = New StreamReader(nsStream)
            '使用获取的网络基础数据流来初始化StreamWriter实例
            butconnect.Enabled = False
            butsend.Enabled = True
            butdis.Enabled = True
            tcpConnect = True
            txtmsg.AppendText("已经连接")

            Dim workerThread As New Thread(New ThreadStart(AddressOf DoWork))
            ' 开始执行线程  
            workerThread.IsBackground = True
            workerThread.Start()

            ' 等待工作线程完成  
            'workerThread.Join()

        Catch ex As Exception
            MessageBox.Show("无法和远程主机端口建立连接！", "错误提示！")
            Return
        End Try
    End Sub

    Private Sub butsend_Click(sender As Object, e As EventArgs) Handles butsend.Click
        swWriter.WriteLine(txtsend.Text)
        txtmsg.AppendText(txtsend.Text)
        swWriter.Flush()
    End Sub

    Private Sub DoWork()
        Dim receiveMessage As String = String.Empty
        ''''''''''''''''''''''''''''''''''''''''''''''''
        Try
            While (tcpConnect)
                While (True)
                    receiveMessage = srRead.ReadLine()
                    If String.IsNullOrEmpty(receiveMessage) Then
                        Continue While
                    End If
                    Exit While
                End While


                If (String.IsNullOrEmpty(receiveMessage)) Then
                    If Not tcpConnect Then
                        srRead.Close()
                        tcpClient.Close()
                        Exit While
                    End If
                    Continue While
                End If

                'If String.IsNullOrEmpty(receiveMessage.Trim()) Then
                '    Thread.Sleep(100)
                '    Continue While
                'End If
                'txtmsg.AppendText(receiveMessage)
                'ShowMsg(receiveMessage)

                'Me.Invoke(Sub() txtmsg.AppendText(receiveMessage))
                Me.Invoke(New VoidDelegate(AddressOf UpdateText), receiveMessage)
            End While
        Catch ex As Exception
            'MsgBox("提示")
            MessageBox.Show("无法和远程主机端口建立连接！", "错误提示！")
        End Try
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



        'While (Not tcpConnect)
        '    '客户端主动断开连接前检查流中是否还是数据
        '        If (tcpClient.Available <= 0) Then
        '        If (Not IsOnline(tcpClient)) Then
        '            Exit While
        '        End If
        '        Continue While
        '    End If

        '    'if (client.Client == null) return;

        '    Dim d As New ReceiveMessageDelegate(AddressOf receiveMessage), receiveString)

        '    'ReceiveMessageDelegate d = New ReceiveMessageDelegate(receiveMessage)
        '        IAsyncResult result = d.BeginInvoke(out receiveString, null, null)
        '    '使用轮询方式来判断异步操作是否完成
        '    While (Not result.IsCompleted)
        '        If (tcpConnect) Then
        '            Exit While
        '        End If
        '            Thread.Sleep(50);
        '    End While
        '    '获取Begin方法的返回值和所有输入/输出参数
        '        d.EndInvoke(out receiveString, result)
        '    If (String.IsNullOrEmpty(receiveString)) Then
        '        If (Not tcpConnect) Then
        '            tcpClient.Close()
        '            swWriter.Close()
        '            srRead.Close()
        '            Exit While
        '        End If
        '        Continue While
        '    End If
        'End While


    End Sub

    Private Function IsOnline(ByRef c As TcpClient) As Boolean
        Try
            Return Not (c.Client.Poll(1000, SelectMode.SelectRead) And c.Client.Available = 0 Or c.Client Is Nothing)
        Catch
            Return False
        End Try
    End Function

    Public Delegate Sub VoidDelegate(ByVal txtmsg As String)
    Public Delegate Sub ReceiveMessageDelegate(ByRef receiveMessage As String)

    Public Sub UpdateText(ByVal receiveMessage As String)
        txtmsg.AppendText(receiveMessage)
    End Sub

    Private Sub butdis_Click(sender As Object, e As EventArgs) Handles butdis.Click

        If tcpConnect Then
            tcpConnect = False
            tcpClient.Close()
            swWriter.Close()
            srRead.Close()
            butconnect.Enabled = True
            butdis.Enabled = False
            butsend.Enabled = False
        End If

    End Sub
End Class
