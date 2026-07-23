using EAM.Listen.Common.Config;
using EAM.Listen.Common.Utils;
using EAM.Listen.Communication;
using EAM.Listen.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace EAM.Listen.DataProcessing
{
    public class TcpReceiveHandle
    {
        //通过TCP通信接收数据的处理方法
        [Obsolete("此方式已过时，请使用TcpIotReceiveHandle的TCP_HandleReceiveMsg")]
        public static byte[] TCP_HandleReceiveMsg(TcpStateEventArgs arg)
        {
            byte[] data = arg.buffer;
            return HandleMsg(data, arg.ip.ToString());
        }

        /// <summary>
        /// 解析接收数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="itemCodes"></param>
        /// <returns></returns>
        public static ReceiveData ParseData(byte[] data, List<ItemCode> itemCodes)
        {
            //【字节说明】参考文档在系统配置中，系统的配置通过svn中的Docment文件：【设备系统数据采集编码约定】
            //【示例数据】ED-ED-00-45-00-02-0D-00-01-2E-00-00-01-00-0D-EC-EC
            //检查数据长度
            int len = itemCodes.Sum(it => it.ByteLen);
            if (data.Length != len) return null;

            int index = 0;
            string tempHexCode;
            ReceiveData rd = new ReceiveData();
            //循环配置的编码项目，判断是否合法并解析数据
            foreach (ItemCode item in itemCodes)
            {
                //获取指定位置的16进制字符值
                tempHexCode = BitConverter.ToString(data, index, item.ByteLen);
                switch (item.ItemName)
                {
                    case ReceiveItemNameConstant.前缀:
                        //检查固定码
                        if (tempHexCode != item.FixedCode)
                            return null;
                        break;

                    case ReceiveItemNameConstant.后缀:
                        //检查固定码
                        if (tempHexCode != item.FixedCode)
                            return null;
                        break;

                    case ReceiveItemNameConstant.设备编码:
                        rd.equipmentCode = Convert.ToInt32(tempHexCode.Replace("-", ""), 16);
                        break;

                    case ReceiveItemNameConstant.操作指令:
                        rd.operateCode = Convert.ToInt32(tempHexCode.Replace("-", ""), 16);
                        break;

                    case ReceiveItemNameConstant.运行状态:
                        rd.runState = Convert.ToInt32(tempHexCode.Replace("-", ""), 16);
                        break;

                    case ReceiveItemNameConstant.产线编码:
                        rd.lineCode = Convert.ToInt32(tempHexCode.Replace("-", ""), 16);
                        break;

                    case ReceiveItemNameConstant.产能数量:
                        rd.productCount = Convert.ToInt32(tempHexCode.Replace("-", ""), 16);
                        break;

                    case ReceiveItemNameConstant.不良数量:
                        rd.defectCount = Convert.ToInt32(tempHexCode.Replace("-", ""), 16);
                        break;

                    case ReceiveItemNameConstant.报警状态:
                        rd.warnState = Convert.ToInt32(tempHexCode.Replace("-", ""), 16);
                        break;

                    case ReceiveItemNameConstant.报警代码:
                        rd.warnCode = Convert.ToInt32(tempHexCode.Replace("-", ""), 16);
                        break;

                    default: break;
                }
                index += item.ByteLen;
            }

            //获取设备id
            int? equipmentId = EquipmentService.GetEquipmentIdByCode(rd.equipmentCode);
            if (equipmentId == null)
                return null;
            else
                rd.equipmentId = equipmentId.Value;
            return rd;
        }

        /// <summary>
        /// 处理操作指令
        /// </summary>
        /// <param name="rd"></param>
        /// <returns>返回发送给plc的数据（Hex）</returns>
        public static byte[] HandleOperateCode(ReceiveData rd)
        {
            //当没有指定返回指令时，主动检查是否要返回其他指令结果
            OperateCodeEnum operate = (OperateCodeEnum)Enum.Parse(typeof(OperateCodeEnum), rd.operateCode.ToString());
            if (operate == OperateCodeEnum.NONE)
            {
                //没有操作指令时，判断是否检查保养状态
                operate = EquipmentService.IsCheckedMaintenance(rd.equipmentId) ? OperateCodeEnum.NONE : OperateCodeEnum.Maintenance;
            }

            //根据操作指令返回结果
            Dictionary<string, byte[]> extendData = new Dictionary<string, byte[]>();
            byte[] response = null;
            switch (operate)
            {
                case OperateCodeEnum.Maintenance://保养
                    MaintenanceCodeEnum statusCode = EquipmentService.GetEquipmentMaintanStatus(rd.equipmentId);
                    extendData.Add(SendItemNameConstant.操作结果, BitConverter.GetBytes((int)statusCode));
                    response = CreateReturnData(FactoryBaseConfig.SignalConfig.SendCodeItems, rd, extendData);
                    break;
            }
            return response;
        }

        //数据解析校验，存入数据库
        private static byte[] HandleMsg(byte[] data, string ip)
        {
            try
            {
                //解析数据
                ReceiveData rd = ParseData(data, FactoryBaseConfig.SignalConfig.ReceiveCodeItems);
                //判断是否有解析数据
                if (rd == null) return null;
                rd.ip = ip;

                //检查数据是否异常
                if (!EquipmentService.CheckReceiveData(rd)) return null;

                //上传接收数据
                EquipmentService.UploadReceiveData(rd);

                //处理操作指令，得到返回数据（Hex）字符串
                return HandleOperateCode(rd);
            }
            catch (Exception ex)
            {
                string msg = $"{Environment.NewLine}端点:{ip},接收数据:{BitConverter.ToString(data)}";
                LogHelper.Error(typeof(TcpReceiveHandle), ex.Message + msg);
                return null;
            }
        }

        /// <summary>
        /// 生成返回数据
        /// </summary>
        /// <param name="itemCodes">返回项目编码配置</param>
        /// <param name="receiveData">接收数据</param>
        /// <param name="extendData">扩展数据</param>
        /// <returns></returns>
        private static byte[] CreateReturnData(List<ItemCode> itemCodes, ReceiveData receiveData, Dictionary<string, byte[]> extendData)
        {
            // 示例数据： EE-EE-设备编码(2字节)-操作指令(1字节)-操作结果(1字节)-00-FF-FF
            if (itemCodes == null || itemCodes.Count <= 0)
                return null;

            byte[] resultData = new byte[itemCodes.Sum(it => it.ByteLen)];
            byte[] tempData = null;
            int index = 0;
            foreach (ItemCode item in itemCodes)
            {
                switch (item.ItemName)
                {
                    case SendItemNameConstant.前缀:
                    case SendItemNameConstant.后缀:
                        tempData = SoftBasic.HexStringToBytes(item.FixedCode);
                        break;

                    case SendItemNameConstant.其他:
                        if (string.IsNullOrEmpty(item.FixedCode))
                            tempData = new byte[item.ByteLen];
                        else
                            tempData = SoftBasic.HexStringToBytes(item.FixedCode);
                        break;

                    case SendItemNameConstant.设备编码:
                        tempData = BitConverter.GetBytes((uint)receiveData.equipmentCode);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(tempData);
                        break;

                    case SendItemNameConstant.操作指令:
                        tempData = BitConverter.GetBytes(receiveData.operateCode);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(tempData);
                        break;

                    case SendItemNameConstant.操作结果:
                        tempData = extendData[SendItemNameConstant.操作结果];
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(tempData);
                        break;

                    default:
                        tempData = null;
                        break;
                }
                if (tempData != null)
                {
                    Array.Copy(tempData, tempData.Length - item.ByteLen, resultData, index, item.ByteLen);
                    index += item.ByteLen;
                }
            }
            return resultData;
        }
    }
}