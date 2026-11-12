using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        C# 快速排序算法示例");
            Console.WriteLine("========================================");
            Console.WriteLine();

            // 生成50个随机整数
            Random random = new Random();
            //int[] arr = new int[5];
            int[] arr = { 4,6,20,10,8};

            //for (int i = 0; i < 5; i++)
            //{
            //    arr[i] = random.Next(1, 10); // 生成1-50之间的随机数
            //}

            // 显示排序前的数组
            Console.WriteLine("【排序前】50个随机整数：");
            PrintArray(arr);

            // 执行快速排序
            Console.WriteLine();
            Console.WriteLine("正在执行快速排序...");
            QuickSort(arr, 0, arr.Length - 1);

            // 显示排序后的数组
            Console.WriteLine();
            Console.WriteLine("【排序后】50个整数：");
            PrintArray(arr);

            // 验证排序结果
            Console.WriteLine();
            if (IsSorted(arr))
            {
                Console.WriteLine("✓ 排序验证通过！数组已正确排序。");
            }
            else
            {
                Console.WriteLine("✗ 排序验证失败！");
            }

            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("          程序执行完成");
            Console.WriteLine("========================================");
            Console.ReadKey();
        }


        /// <summary>
        /// 快速排序主方法
        /// </summary>
        /// <param name="arr">要排序的数组</param>
        /// <param name="low">起始索引</param>
        /// <param name="high">结束索引</param>
        static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                // 分区操作，返回基准值的正确位置
                int pivotIndex = Partition(arr, low, high);

                // 递归排序基准值左边的元素
                QuickSort(arr, low, pivotIndex - 1);

                // 递归排序基准值右边的元素
                QuickSort(arr, pivotIndex + 1, high);
            }
        }

        /// <summary>
        /// 分区操作：选取最后一个元素作为基准值，将数组分为两部分
        /// 左边的元素都小于基准值，右边的元素都大于基准值
        /// </summary>
        /// <param name="arr">要分区的数组</param>
        /// <param name="low">起始索引</param>
        /// <param name="high">结束索引（基准值位置）</param>
        /// <returns>基准值的最终位置</returns>
        static int Partition(int[] arr, int low, int high)
        {
            // 选择最后一个元素作为基准值
            int pivot = arr[high];

            // i 用于追踪小于基准值的元素的最终位置
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    // 交换 arr[i] 和 arr[j]
                    Swap(ref arr[i], ref arr[j]);
                }
            }

            // 将基准值放到正确的位置
            Swap(ref arr[i + 1], ref arr[high]);

            // 返回基准值的正确位置
            return i + 1;
        }

        /// <summary>
        /// 交换两个元素的位置
        /// </summary>
        static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// 打印数组（每行10个元素）
        /// </summary>
        static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
                if ((i + 1) % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
            // 如果不足10的倍数，补一行换行
            if (arr.Length % 10 != 0)
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 验证数组是否已正确排序
        /// </summary>
        static bool IsSorted(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
