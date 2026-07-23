using System;
using System.Collections;

namespace CollectionsApplication
{
    class Program
    {
        static void Main(string[] args)
        {
			//堆栈后进先出
            Stack st = new Stack();

            st.Push('A');  //往堆栈中添入对象
            st.Push('M');
            st.Push('G');
            st.Push('W');
           
            Console.WriteLine("Current stack: ");
            foreach (char c in st)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
           
            st.Push('V');
            st.Push('H');
            Console.WriteLine("The next poppable value in stack: {0}",
            st.Peek());  //Peek获取堆栈顶部的对象，即最后添入的对象
            Console.WriteLine("Current stack: ");          
            foreach (char c in st)
            {
               Console.Write(c + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Removing values ");
            st.Pop();  //移除最顶部的对象
            st.Pop();
            st.Pop();
            //st.Clear();移除所有对象
			//bool YS = st.Contains("H");判断对象是否在堆栈中
			//char[] a = st.ToArray();堆栈赋值给一个数组
            Console.WriteLine("Current stack: ");
            foreach (char c in st)
            {
               Console.Write(c + " ");
            }
        }
    }
}