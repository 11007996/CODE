using System;
using System.Collections.Generic;
using System.Linq;
// using System.Threading.Tasks;
using System.Threading;
namespace pro
{
    class proce
    {
        static void Main(string[] Args)
        {
            string str = "12334567qwertyuiasdfghjzxcvbnm,asdf";
            List<List<char>> LIstGroup = new List<List<char>>();
            int k = str.Length / 5;
            int j = str.Length / 5;
            for (int i = 0; i < str.Length; i += k)
            {
                List<char> cLIst = new List<char>();
                //跳前i位取j位字母1
                cLIst = str.Take(j).Skip(i).ToList();
                j += k;
                LIstGroup.Add(cLIst);
            }
            foreach (List<char> Li in LIstGroup)
            {
                foreach(char a in Li)
                {
                    Console.Write(a);
                }
                Thread.Sleep(2000);
            }
            Console.ReadKey();
        }
    }
}