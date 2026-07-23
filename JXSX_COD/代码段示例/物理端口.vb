'打开端口
 Private Sub CommOpenSerialPort()
        Dim mBaudRate As Integer              '波特率
        Dim mParity As IO.Ports.Parity        '奇偶校验位
        Dim mDataBit As Integer               '数据位
        Dim mStopBit As IO.Ports.StopBits     '停止位
        Dim mPortName As String               '端口名

        mPortName = Me.CbbCOM.Text.Trim
        mBaudRate = "9600"
        mParity = Parity.None
        mDataBit = 8
        mStopBit = StopBits.One
        comm = New IO.Ports.SerialPort(mPortName, mBaudRate, mParity, mDataBit, mStopBit)
        Try
            If Not comm.IsOpen Then
                comm.Open()
                comm.Encoding = Encoding.UTF8
            End If
        Catch ex As Exception
            lblMessage.Text = "端口打开时发生错误..."
        End Try
    End Sub
	
	
	
'端口发送信息
If Me.comm.IsOpen Then
                comm.Write(buf.ToArray(), 0, buf.Count)         '(信息源，开始字节，发送长度)
                Threading.Thread.Sleep(20)
                comm.Write(buf.ToArray(), 0, buf.Count)
                '发送二进制PASS数据
                'Me.comm.Write(New Byte() {80, &H41, &H53, &H53}, 0, 4)
                'Threading.Thread.Sleep(20)
                'Me.comm.Write(New Byte() {80, &H41, &H53, &H53}, 0, 4)
                If comm Is Nothing OrElse Not comm.IsOpen Then
                    lblMessage.Text = "读取序号发生错误：COM3端口未开启..."
                Else
                    comm.Close()
                    comm = Nothing
                End If
            Else
                Me.lblMessage.Text = "发送串口数据失败！请检查！"
            End If
			
			
			
			
			
			
'从端口接收信息
Function ReceiveSerialData() As String
    ' Receive strings from a serial port.
    Dim returnStr As String = ""

    Dim com1 As IO.Ports.SerialPort = Nothing
    Try
        com1 = My.Computer.Ports.OpenSerialPort("COM1")
        com1.ReadTimeout = 10000
        Do
            Dim Incoming As String = com1.ReadLine()
            If Incoming Is Nothing Then
                Exit Do
            Else
                returnStr &= Incoming & vbCrLf
            End If
        Loop
    Catch ex As TimeoutException
        returnStr = "Error: Serial Port read timed out."
    Finally
        If com1 IsNot Nothing Then com1.Close()
    End Try

    Return returnStr
End Function




'SerialPort类OPEN方法源码
public void Open()
        {
           //省略部分代码...
            internalSerialStream = new SerialStream(portName, baudRate, parity, dataBits, stopBits, readTimeout,
                writeTimeout, handshake, dtrEnable, rtsEnable, discardNull, parityReplace);
 
            internalSerialStream.SetBufferSizes(readBufferSize, writeBufferSize); 
            internalSerialStream.ErrorReceived += new SerialErrorReceivedEventHandler(CatchErrorEvents);
            internalSerialStream.PinChanged += new SerialPinChangedEventHandler(CatchPinChangedEvents);
            internalSerialStream.DataReceived += new SerialDataReceivedEventHandler(CatchReceivedEvents);
        } 
		
'SerialPort类Close方法源码
  public void Close()
        {
            Dispose();
        }
        
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected override void Dispose( bool disposing )
        {
            if( disposing ) {
                if (IsOpen) {
                    internalSerialStream.Flush();
                    internalSerialStream.Close();
                    internalSerialStream = null;
                }
            }
            base.Dispose( disposing );
        }        
		
		
		
		
		
		
		
'SerialPort类CatchReceivedEvents方法源码	
private void CatchReceivedEvents(object src, SerialDataReceivedEventArgs e)
        {
            SerialDataReceivedEventHandler eventHandler = DataReceived;
            SerialStream stream = internalSerialStream;
 
            if ((eventHandler != null) && (stream != null)){
                lock (stream) {
                    bool raiseEvent = false;
                    try {
                        raiseEvent = stream.IsOpen && (SerialData.Eof == e.EventType || BytesToRead >= receivedBytesThreshold);    
                    }
                    catch {
                        // Ignore and continue. SerialPort might have been closed already! 
                    }
                    finally {
                        if (raiseEvent)
                            eventHandler(this, e);  // here, do your reading, etc. 
                    }
                }
            }
        }




-------------------------------------------------------------------------------------------------------------------------------
//ComHelper类库
/// <summary>初始化串行端口</summary>
private SerialPort _serialPort;

public SerialPort serialPort
{
get { return _serialPort; }
set { _serialPort = value; }
}

/// <summary>
/// COM口通信构造函数
/// </summary>
/// <param name="PortID">通信端口</param>
/// <param name="baudRate">波特率</param>
/// <param name="parity">奇偶校验位</param>
/// <param name="dataBits">标准数据位长度</param>
/// <param name="stopBits">每个字节的标准停止位数</param>
/// <param name="readTimeout">获取或设置读取操作未完成时发生超时之前的毫秒数</param>
/// <param name="writeTimeout">获取或设置写入操作未完成时发生超时之前的毫秒数</param>
public ComHelper(string PortID, int baudRate, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One, int readTimeout = 100, int writeTimeout = 100)
{
try
{
serialPort = new SerialPort();
serialPort.PortName = "COM" + PortID;//通信端口
serialPort.BaudRate = baudRate;//波特率
serialPort.Encoding = Encoding.ASCII;
serialPort.Parity = parity;//奇偶校验位
serialPort.DataBits = dataBits;//标准数据位长度
serialPort.StopBits = stopBits;//每个字节的标准停止位数
serialPort.ReadTimeout = readTimeout;//获取或设置读取操作未完成时发生超时之前的毫秒数
serialPort.WriteTimeout = writeTimeout;//获取或设置写入操作未完成时发生超时之前的毫秒数
}
catch (Exception ex) { throw new Exception(ex.Message); }
}

/// <summary>
/// 打开COM口
/// </summary>
/// <returns>true 打开成功；false 打开失败；</returns>
public bool Open()
{
try
{
if (serialPort.IsOpen == false)
{
serialPort.Open();
return true;
}
}
catch (Exception ex)
{
LogImpl.Debug(ex.ToString());
}
return false;
}

/// <summary>
/// 关闭COM口
/// </summary>
/// <returns>true 关闭成功；false 关闭失败；</returns>
public bool Close()
{
try
{
serialPort.Close();
return true;
}
catch
{
return false;
}
}

/// <summary>
/// 判断端口是否打开
/// </summary>
/// <returns></returns>
public bool IsOpen()
{
try
{
return serialPort.IsOpen;
}
catch { throw; }
}

/// <summary>
/// 向COM口发送信息
/// </summary>
/// <param name="sendData">16进制的字节</param>
public void WriteData(byte[] sendData)
{
try
{
if (IsOpen())
{
Thread.Sleep(5);
serialPort.Write(sendData, 0, sendData.Length);
}
}
catch { throw; }
}

/// <summary>
/// 接收来自COM的信息
/// </summary>
/// <returns>返回收到信息的数组</returns>
public string[] ReceiveDataArray()
{

try
{
Thread.Sleep(5);
if (!serialPort.IsOpen) return null;
int DataLength = serialPort.BytesToRead;
byte[] ds = new byte[DataLength];
int bytecount = serialPort.Read(ds, 0, DataLength);
return ByteToStringArry(ds);
}
catch (Exception ex)
{
LogImpl.Debug(""+ex.ToString());
throw;
}
}

/// <summary>
/// 把字节型转换成十六进制字符串
/// </summary>
/// <param name="bytes"></param>
/// <returns></returns>
public static string[] ByteToStringArry(byte[] bytes)
{
try
{
string[] strArry = new string[bytes.Length];
for (int i = 0; i < bytes.Length; i++)
{
strArry[i] = String.Format("{0:X2} ", bytes[i]).Trim();
}
return strArry;
}
catch { throw; }
}

/// <summary>
/// 清除缓存数据
/// </summary>
public void ClearDataInBuffer()
{
try
{
serialPort.DiscardInBuffer();
serialPort.DiscardOutBuffer();
}
catch { throw; }
}


/// <summary>
/// 注册 数据接收事件，在接收到数据时 触发
/// </summary>
/// <param name="serialPort_DataReceived"></param>
public void AddReceiveEventHanlder(SerialPortDataReceivedDelegate serialPort_DataReceived)
{
try
{
serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
}
catch { throw; }
}

//接收事件是否有效 true开始接收，false停止接收。默认true

public static bool ReceiveEventFlag = true;
/// <summary>
/// 接收数据触发，将接收的数据，通过一个定义的数据接收事件，传递出去。
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
{
if (ReceiveEventFlag == false)
{
return;
}
string strReceive = ReceiveDataString();
if (!string.IsNullOrEmpty(strReceive))
{
OnReceiveDataHanlder(new ReceiveEventArgs() { ReceiveData = strReceive });
}
}

#region 数据接收事件
public event EventHandler<ReceiveEventArgs> ReceiveDataHandler;

protected void OnReceiveDataHanlder(ReceiveEventArgs e)
{
EventHandler<ReceiveEventArgs> handler = ReceiveDataHandler;
if (handler != null) handler(this, e);
}
#endregion






//数据接收
public ComHelper comHelp;

string PortID = System.Configuration.ConfigurationManager.AppSettings["PeopleAdr"];
int baudRate = 9600;
int dataBits = 8;
System.IO.Ports.StopBits oStopBits = System.IO.Ports.StopBits.One;
//无奇偶校验位
System.IO.Ports.Parity oParity = System.IO.Ports.Parity.None;
int ReadTimeout = 100;
int WriteTimeout = -1;
comHelp = new ComHelper(PortID, baudRate, oParity, dataBits, oStopBits, ReadTimeout, WriteTimeout);
if (!comHelp.IsOpen()) comHelp.Open();

comHelp.AddReceiveEventHanlder(comHelp.serialPort_DataReceived);//将接收到数据，处理数据的方法注册进去
comHelp.ReceiveDataHandler += new EventHandler<ReceiveEventArgs>(rds_ReceiveDataHandler);//将传递接收数据的方法注册进去，如果接收到数据，触发事件，自动存串口数据。

public string strComReciveData = "";//从COM口接收到的数据，如果接收到数据，通过事件触发，会自动有值。
void rds_ReceiveDataHandler(object sender, ReceiveEventArgs e)
{
try
{
strComReciveData = e.ReceiveData;
Thread.Sleep(200);
//清空缓存
comHelp.ClearDataInBuffer();
}
catch (Exception ex)
{
LogImpl.Debug(ex.ToString());
Thread.Sleep(200);
}
}




//数据发送
byte[] send = new byte[] { 0x7F, 0x02, 0x8F };
comHelp.WriteData(send);
-------------------------------------------------------------------------------------------------------------------------------