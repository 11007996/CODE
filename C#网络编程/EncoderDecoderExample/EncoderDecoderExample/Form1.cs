using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncoderDecoderExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxOldText.Text = "测试数据：abc,123,我";
            textBoxEncoder.ReadOnly = textBoxDecoder.ReadOnly = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                //获取所有的编码方案
                Encoding en = ei.GetEncoding();
                comboBoxType.Items.Add(string.Format("{0}[{1}]",en.HeaderName,en.EncodingName));
            }
            //设置默认的编码方案
            comboBoxType.SelectedIndex = comboBoxType.FindString("gb2312");
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            String codeType = this.comboBoxType.SelectedItem.ToString();
            //截取编码方案编码
            codeType = codeType.Substring(0, codeType.IndexOf('['));
            Encoding en = Encoding.GetEncoding(codeType);
            //获取编码方案的编码器
            Encoder encoder = en.GetEncoder();
            //将字符串改为字符数组
            char[] chars = this.textBoxOldText.Text.ToCharArray();
            //GetBytes方法要求转换数据源为byte数组，此处实例化一个数组，GetByteCount方法返回转换后的精确字节长度
            Byte[] bytes = new Byte[encoder.GetByteCount(chars, 0, chars.Length,true)];
            //用指定编码方案将字符数组转为字节序列(要转换的单字符数组，转换的第1个索引位置，要转换的长度，结果存入的变量，开始写入的索引位置，是否在转换后清除编码器的内部状态)
            encoder.GetBytes(chars, 0, chars.Length, bytes, 0, true);
            //将字节序列以字符串形式输出
            textBoxEncoder.Text = Convert.ToBase64String(bytes);
            //获取指定编码方案的解码器
            Decoder decoder = Encoding.GetEncoding(codeType).GetDecoder();
            //将字节序列转为单字符序列，字符存入chars变量
            int charLen = decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
            string strResult = "";
            foreach (char c in chars)
            {
                //将单字符序列合并为字符串
                strResult = strResult + c.ToString();
                textBoxDecoder.Text = strResult;
            }
        }
    }
}
