using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Listen.Utils
{
    /// <summary>
    /// DESEncrypt加密解密算法
    /// </summary>
    public class DESEncrypt
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <param name="sKey">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string str, string sKey)
        {
            if (str.Length > 0 && sKey.Length > 0)
            {
                try
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    byte[] inputByteArray = Encoding.Default.GetBytes(str);
                    des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);    //密钥
                    des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);    //初始化向量
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception ex)
                {
                    LogHelper.Error(typeof(DESEncrypt), null, ex);
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// 需要解密的
        /// </summary>
        /// <param name="str">需要解密的密文</param>
        /// <param name="sKey">密钥</param>
        /// <returns></returns>
        public static string Decrypt(string str, string sKey)
        {
            if (str.Length > 0 && sKey.Length > 0)
            {
                try
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    byte[] inputByteArray = Convert.FromBase64String(str);
                    des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                    des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    //如果两次密钥不一样，这一步可能会引发异常
                    cs.FlushFinalBlock();
                    return System.Text.Encoding.Default.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    LogHelper.Error(typeof(DESEncrypt), null, ex);
                    return null;
                }
            }
            return null;
        }

    }
}
