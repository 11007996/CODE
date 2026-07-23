using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace EAM.Listen.Common.Utils
{
    public class HttpHelper
    {
        //发送post请求
        public static string Post(string url, object data)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            Encoding encoding = Encoding.GetEncoding("UTF-8");

            // 准备请求,设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 10 * 1000;
            request.ReadWriteTimeout = 30 * 1000;

            string strData = ConvertFormData(data);
            byte[] datas = Encoding.UTF8.GetBytes(strData);
            //请求数据的流
            using (Stream stream = request.GetRequestStream())
            {
                //写入发送的信息
                stream.Write(datas, 0, datas.Length);
            }

            HttpWebResponse response = null;
            string strResult = "";
            //接收响应
            using (response = (HttpWebResponse)request.GetResponse())
            {
                //读取响应信息
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    strResult = sr.ReadToEnd();
                }
            }
            return strResult;
        }

        public static string PostJson(string url, object data, string token = null, int timeout = 10)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            Encoding encoding = Encoding.GetEncoding("UTF-8");

            // 准备请求,设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Timeout = timeout * 1000;
            request.ReadWriteTimeout = 10 * 1000;
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Add("Authorization", "Bearer " + token);
            }

            string strData = JsonConvert.SerializeObject(data);
            byte[] datas = Encoding.UTF8.GetBytes(strData);
            //请求数据的流
            using (Stream stream = request.GetRequestStream())
            {
                //写入发送的信息
                stream.Write(datas, 0, datas.Length);
            }

            HttpWebResponse response = null;
            string strResult = "";
            //接收响应
            using (response = (HttpWebResponse)request.GetResponse())
            {
                //读取响应信息
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    strResult = sr.ReadToEnd();
                }
            }
            return strResult;
        }

        public static string Get(string url, object data)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            // 准备请求,设置参数
            if (data != null)
            {
                url += "?" + ConvertFormData(data);
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.Timeout = 3000;
            request.ReadWriteTimeout = 30 * 1000;

            string strResult = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //读取响应信息
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    strResult = sr.ReadToEnd();
                }
            }
            return strResult;
        }

        //参数转换
        private static string ConvertFormData(object data)
        {
            var buff = new StringBuilder(string.Empty);
            if (data == null) return null;
            foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(data))
            {
                var value = p.GetValue(data);
                if (value != null)
                {
                    buff.Append(WebUtility.UrlEncode(p.Name) + "=" + WebUtility.UrlEncode(value.ToString() + "") + "&");
                }
            }
            return buff.ToString().Trim('&');
        }
    }
}