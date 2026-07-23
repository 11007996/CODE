using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("MyLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int addition(int a, int b);

    static void Main(string[] args)
    {
        int result = addition(1, 2);
        Console.WriteLine("The sum is: " + result);
        Console.ReadKey();
    }
}
