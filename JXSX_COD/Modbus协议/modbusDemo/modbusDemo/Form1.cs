using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.Device;


namespace modbusDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //主站
        private void btnCon_Click(object sender, EventArgs e)
        {
            string ipAddress = txtIP.Text; // 从站 IP
            int port =Convert.ToInt32( txtPort.Text);                   // Modbus TCP 端口
            ushort unitId =Convert.ToUInt16(txtID.Text);                // 从站地址
            ushort startId = 0;   //起始位置
            ushort Rcount = 1;    //读取数量

            using (TcpClient client = new TcpClient())
            {
                client.Connect(ipAddress, port);
                //await client.ConnectAsync(ipAddress, port);  //异步

                ModbusMaster master = ModbusIpMaster.CreateIp(client);
                //master.Transport.ReadTimeout = 1000;//读取连接（串口）数据超时为
                //master.Transport.WriteTimeout = 1000;//写入连接（串口）数据超时
                //master.Transport.Retries = 3;//重试次数
                //master.Transport.WaitToRetryMilliseconds = 250;//重试间隔


                // await master.ReadHoldingRegistersAsync((byte)unitId, startId, Rcount);
                ushort[] value = master.ReadHoldingRegisters((byte)unitId, startId, Rcount);//从站1，0寄存器读取1位  读取保持寄存器
                //bool[] b master.ReadCoils((byte)unitId, startId, Rcount);  //读取线圈
                //master.WriteMultipleRegisters(1, 2, value);//从站1，2寄存器写入数组value    //写入多寄存器
                MessageBox.Show(value[0].ToString());
                txtRead.Text = value[0].ToString();
            }
        }
    }
}
