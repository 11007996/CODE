using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.Data;
using Modbus.Device;
using Modbus.Utility;
namespace modbusSlaveDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ModbusSlave slave;
        private void btnStart_Click(object sender, EventArgs e)
        {
            ushort slaveId = 1;        // 从站 ID
            int port = 502;            // 监听端口
            IPAddress ip =IPAddress.Parse("127.0.0.1");

            //using (TcpListener listener = new TcpListener(ip, port))
            {
                TcpListener listener = new TcpListener(ip, port);
                listener.Start();
                //Console.WriteLine($"Modbus TCP 从站已启动，监听端口 {port}...");

                slave = ModbusTcpSlave.CreateTcp((byte)slaveId, listener);
                slave.DataStore = DataStoreFactory.CreateDefaultDataStore(); // 初始化数据存储

                // 模拟数据更新（例如传感器数据）
                System.Timers.Timer timer = new System.Timers.Timer(1000);
                timer.Elapsed += (s, r) =>
                {
                    ModbusDataCollection<ushort> holdingRegs = slave.DataStore.HoldingRegisters;    //模拟保持寄存器
                    for (int i = 1; i < holdingRegs.Count; i++)
                    {
                        holdingRegs[i] = (ushort)(i + (int)(DateTime.Now.Second * 0.5)); // 动态数据
                    }
                };
                timer.Start();
                slave.Listen(); // 开始监听请求
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            slave.Dispose();
        }
    }
}
