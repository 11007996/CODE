using System;
using System.Collections.Generic;

namespace aa
{
    public class Base34Converter
    {
        // 你指定的 34 进制字符集
        private const string Chars = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";
        private const int Base = 34;


        // 34进制字符串 转 10进制数字
        public long Base34ToDecimal(string base34Str)
        {
            if (string.IsNullOrWhiteSpace(base34Str))
            return 0;

            base34Str = base34Str.Trim().ToUpper();
            long result = 0;

            foreach (char c in base34Str)
            {
                int index = Chars.IndexOf(c);
                result = result * Base + index;
            }

            return result;
        }

        // 10进制数字 转34 进制字符串
        public string DecimalToBase34(long num)
        {
            if (num < 0)
            throw new ArgumentOutOfRangeException();
            if (num == 0)
            return "0";

            string result = "";
            while (num > 0)
            {
                int index = (int)(num % Base);
                result = Chars[index] + result;
                num /= Base;
            }
            return result;
        }
    }

    
}