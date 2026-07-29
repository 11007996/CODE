using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BartenderPrint
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Flag = false;
            string barcode = "035838223A05003W";

            //传递打印内容，Key-Vaule
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"QRcode",barcode},
            };

            Flag = BarTenderPrint.Print("Microsoft Print to PDF", "C:\\Users\\mh.guo\\Desktop\\moban.btw", dic, 1);//表示有一个名称为SN的打印机，然后打印模板的路径，打印内容，打印数量
        }
    }
}
