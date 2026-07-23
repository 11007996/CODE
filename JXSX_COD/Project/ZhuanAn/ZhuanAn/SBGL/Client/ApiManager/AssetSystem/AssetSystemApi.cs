using ApiManager.AssetSystem.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace ApiManager.AssetSystem
{

    public class AssetSystemApi
    {
#if DEBUG
        private static readonly string ROOT_URI = "http://localhost:13792";
#else
        private static readonly string ROOT_URI = "http://sbgl.luxshare-ict.com:8090";
#endif

        private static HttpClient _httpClient;
        private static string Token;

        static AssetSystemApi()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = new TimeSpan(0, 0, 0, 0, 1000 * 60 * 1);
        }

        /// <summary>
        /// 初始Token
        /// </summary>
        /// <param name="workcode"></param>
        /// <param name="password"></param>
        public static void InitToken(string workcode, string password)
        {
            try
            {
                string url = ROOT_URI + $"/Home/MachineUserLogin?workcode={workcode}&password={password}";
                var result = _httpClient.PostAsync(url, null).Result.Content.ReadAsStringAsync().Result;
                ApiResultMsg jsonResult = JsonConvert.DeserializeObject<ApiResultMsg>(result);
                if (jsonResult != null && jsonResult.msgCode == "0")
                {
                    JObject jobj = JObject.FromObject(jsonResult.data);
                    Token = jobj.SelectToken("token").ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static bool UploadFile(string filePath, string replaceFileId, FileClassEnum? fileClassEnum, ref string resultMsg)
        {
            resultMsg = "";
            if (!File.Exists(filePath))
            {
                resultMsg = "未找到文件,路径:" + filePath;
            }
            else
            {
                string url = ROOT_URI + $"/File/UploadFile?replaceFileId={replaceFileId}&fileClass={(int?)fileClassEnum}";// (int)fileClassEnum;
                _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Token);
                // 以MultipartFormData格式上传
                using (var form = new MultipartFormDataContent())
                {
                    var fileContent = new StreamContent(File.OpenRead(filePath));
                    var fileName = Path.GetFileName(filePath);
                    string fileNameparam = Uri.EscapeDataString(fileName); // .NET 4.x及更高版本推荐使用Uri.EscapeDataString
                    var contentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "file",
                        FileName = fileNameparam,
                    };
                    fileContent.Headers.ContentDisposition = contentDisposition;
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                    form.Add(fileContent);
                    try
                    {
                        // 上传文件,获取返回的字符串内容
                        HttpResponseMessage response = _httpClient.PostAsync(url, form).Result;
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;
                            ApiResultMsg jsonResult = JsonConvert.DeserializeObject<ApiResultMsg>(result);
                            if (jsonResult != null && jsonResult.msgCode == "0")
                                return true;
                            else
                            {
                                resultMsg = jsonResult.msgInfo;
                                return false;
                            }
                        }
                        else
                        {
                            resultMsg = response.Content.ReadAsStringAsync().Result;
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        resultMsg = $"上传文件出错！,FullFileNames={fileName},异常:{ex.Message}";
                        return false;
                    }
                }
            }
            return resultMsg.Length <= 0;
        }



        /// <summary>
        /// 下载预览图片
        /// </summary>
        /// <param name="fileId">预览文件的ID</param>
        /// <returns></returns>
        public static Stream DownloadPreview(int fileId)
        {
            try
            {
                string url = ROOT_URI + $"/File/DownloadPreview?fileId={fileId}";
                _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Token);
                HttpResponseMessage res = _httpClient.GetAsync(url).Result;
                if (res != null && res.IsSuccessStatusCode)
                {
                    return res.Content.ReadAsStreamAsync().Result;
                }
                return null;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

    }




}





