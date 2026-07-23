using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace apiqian
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient hc = new HttpClient();
            string result = hc.GetStringAsync("http://localhost:28387/api/Person?age=13").Result;
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
