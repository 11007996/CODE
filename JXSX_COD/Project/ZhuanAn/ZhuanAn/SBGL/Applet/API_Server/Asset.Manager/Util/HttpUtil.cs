using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Manager.Util
{
    class HttpUtil
    {

        /// 调用http请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="method">请求方式</param>
        /// <param name="data">body数据（JSON）</param>
        /// <param name="headerParam">header数据</param>
        /// <returns></returns>
        public static string Send(string url, string method, Dictionary<string, string> headerParam, string data)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);//webrequest请求api地址
            request.Accept = "text/html,application/xhtml+xml,*/*";
            request.ContentType = "application/json";
            request.Method = method.ToUpper().ToString();//get或者post  
            //请求头数据
            if (headerParam != null)
            {
                foreach (KeyValuePair<string, string> item in headerParam)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
            //请求内容数据
            if (data != null)
            {
                byte[] buffer = encoding.GetBytes(data);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
            }
           
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
