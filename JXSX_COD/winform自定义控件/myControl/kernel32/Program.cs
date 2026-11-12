using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace kernel32
{
    class Program
    {
        # region kernel32声明
        /// <summary>
        /// 获取计算机名
        /// </summary>
        /// <param name="lpBuffer">以字节存储计算机名</param>
        /// <param name="lpnSize">lpBuffer的长度</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool GetComputerNameA([Out] byte[] lpBuffer,ref uint lpnSize);

        /// <summary>
        /// 获取文件的短文件名
        /// </summary>
        /// <param name="lpszLongPath">长文件名</param>
        /// <param name="lpszShortPath">短文件名缓存区</param>
        /// <param name="cchBuffer">缓存区大小</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetShortPathNameA", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern uint GetShortPathNameA(string lpszLongPath,[Out] byte[] lpszShortPath,uint cchBuffer);


        #endregion
        static void Main(string[] args)
        {
            #region GetComputerNameA获取计算机名
            byte[] buf = new byte[256];
            uint size = (uint)buf.Length;
            bool ok = GetComputerNameA(buf, ref size);
            if (ok)
            {
                string computerName = System.Text.Encoding.ASCII.GetString(buf, 0, (int)size);
                Console.WriteLine(computerName);
            }
            else
            {
                Console.WriteLine("获取计算机名失败");
            }
            #endregion




            Console.Read();
        }
    }
}
