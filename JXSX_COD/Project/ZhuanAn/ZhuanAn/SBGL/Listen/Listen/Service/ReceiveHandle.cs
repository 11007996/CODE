using Listen.Utils;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Listen.Service;
using Listen.Base;

namespace Listen.Service
{
    public class ReceiveHandle
    {
        //通过TCP通信接收数据的处理方法
        public static byte[] TCP_HandleReceiveMsg(TcpStateEventArgs arg)
        {
            byte[] data = arg.buffer;
            return HandleMsg(data, arg.remoteEndPoint.ToString());
        }

        //通过COM串口通信接收的数据的处理方法
        public static byte[] SerialPort_HandleReceiveMsg(byte[] data, SerialDataReceivedEventArgs e)
        {
           return  HandleMsg(data, null);
        }

        //数据解析校验，存入数据库
        private static byte[] HandleMsg(byte[] data, string remoteEndPoint)
        {
            try
            {
                string hexStr = BitConverter.ToString(data);
                if (data.Length == BaseInfo.HexCodeByteSize && hexStr.StartsWith(BaseInfo.PrefixHexCode) && hexStr.EndsWith(BaseInfo.SuffixHexCode))
                {
                    //【字节说明】  首部字节"ED-ED"(2字节,0-1)  -机台编码(2字节,2-3)  -操作指令(1字节,4)  -运行状态(1字节,5)  -产线编码(1字节,6) -备用(1字节,7)  -产能(2字节,8-9)  -不良(2字节,10-11)  -报警状态(1字节,12)  -报警详情(2字节,13-14)  -结束字节"EC-EC"(2字节,15-16)
                    // Console.WriteLine(arg.remoteEndPoint.ToString() + Encoding.Default.GetString(arg.buffer));
                    //过滤前后缀无效数据
                    int prefixSize = string.IsNullOrWhiteSpace(BaseInfo.PrefixHexCode) ? 0 : BaseInfo.PrefixHexCode.Split('-').Length;//前缀字节个数
                    int suffixSize = string.IsNullOrWhiteSpace(BaseInfo.SuffixHexCode) ? 0 : BaseInfo.SuffixHexCode.Split('-').Length;//后缀字节个数
                    int dataSize = BaseInfo.HexCodeByteSize - prefixSize - suffixSize;//有效数据的字节个数
                    string[] sourceCode = hexStr.Split('-');
                    string[] codes = new string[dataSize];
                    Array.Copy(sourceCode, prefixSize, codes, 0, dataSize);
                    //解析对应数据
                    ushort machineCode = Convert.ToUInt16(codes[0] + codes[1], 16);
                    byte operateCode = Convert.ToByte(codes[2], 16);
                    byte runState = Convert.ToByte(codes[3], 16);
                    byte lineCode = Convert.ToByte(codes[4], 16);
                    byte remark = Convert.ToByte(codes[5], 16);
                    ushort productCount = Convert.ToUInt16(codes[6] + codes[7], 16);
                    ushort failedCount = Convert.ToUInt16(codes[8] + codes[9], 16);
                    byte warnState = Convert.ToByte(codes[10], 16);
                    short warnCode = Convert.ToInt16(codes[11] + codes[12], 16);
                    //检查当前记录的数据是否正常
                    if (!AssetHelper.CheckCurrReport(machineCode, lineCode, productCount, failedCount)) return null;

                    //当没有指定返回指令时，检查是否要返回其他指令结果
                    OperateCode operate = (OperateCode)Enum.Parse(typeof(OperateCode), operateCode.ToString());
                    if (operate == OperateCode.NONE)
                    {
                        //没有操作指令时判断是否检查保养状态
                        operate = AssetHelper.IsCheckedMaintenance(machineCode) ? OperateCode.NONE : OperateCode.Maintenance;
                    }
                    //根据指令返回结果
                    string responseHexStr = null;
                    switch (operate)
                    {
                        case OperateCode.Maintenance://保养
                            MaintenanceCode statusCode = AssetHelper.GetAssetStatus(machineCode);
                            string opreateHexStr = BitConverter.ToString(new byte[] { (byte)operate });
                            string maintenanceHexStr = BitConverter.ToString(new byte[] { (byte)statusCode });
                            responseHexStr = string.Join("-", new string[] { "EE", "EE", codes[0], codes[1], opreateHexStr, maintenanceHexStr, "00", "FF", "FF" });
                            break;
                    }

                    string sql = string.Format(@"INSERT INTO M_MachineReport_T (MachineCode,OperateCode,RunState,ProductCount,FailedCount,WarnState,WarnCode,CreateTime,RemoteEndPoint,HexCode,LineCode,ReturnHexCode)
                                           Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}',GETDATE(),'{7}','{8}','{9}',NULLIF('{10}','')) ",
                                  machineCode, operateCode, runState, productCount, failedCount, warnState, warnCode, remoteEndPoint, hexStr, lineCode, responseHexStr);
                    DBUtil.ExecSQL(sql);
                    return responseHexStr == null ? null : SoftBasic.HexStringToBytes(responseHexStr);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(ReceiveHandle), ex.Message);
                return null;
            }
            return null;
        }
    }
}
