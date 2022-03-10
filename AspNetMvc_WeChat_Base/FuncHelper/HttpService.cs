/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.FuncHelper
*项目描述:
*类 名 称:HttpService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/5 7:26:00
*修 改 人:
*修改时间:
*作用描述:向平台的接口提交数据
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AspNetMvc_WeChat_Base.FuncHelper
{
    public static class HttpService
    {
        /// <summary>
        /// GET传值方式提交数据
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string Get(string uri)
        {
            string strLine = string.Empty, data = string.Empty;
            using (WebClient client = new WebClient())
            {
                try
                {
                    using (Stream stream = client.OpenRead(uri))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            while ((strLine = reader.ReadLine()) != null)
                            {
                                data += strLine;
                            }
                            reader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                client.Dispose();
            }
            return data;
        }

        /// <summary>
        /// POST传值方法提交数据
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string Post(string url, string postData)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            webRequest.Method = "post";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            Stream stream = webRequest.GetRequestStream();
            stream.Write(byteArray, 0, byteArray.Length);
            stream.Close();
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            string data = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            return data;
        }


        /// <summary>
        /// 上传图文素材-将素材POST到服务器
        /// </summary>
        /// <param name="url">上传素材开发接口</param>
        /// <param name="file">本地图片路径</param>
        /// <returns></returns>
        public static string HttpUPloadFile(string url,string file)
        {
            try
            {
                Logging.LogService.RecordLog("传值的Url:\n" + url);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                CookieContainer cookieContainer = new CookieContainer();
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.AllowAutoRedirect = true;
                httpWebRequest.Method = "POST";
                string boundary = DateTime.Now.Ticks.ToString("X");
                httpWebRequest.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
                byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

                int pos = file.LastIndexOf('\\');
                string fileName = file.Substring(pos + 1);

                //请求数据的头部信息
                StringBuilder strBuilderHeader = new StringBuilder(
                    string.Format("Content-Disposition:form-data;name=\"file\"" +
                    ";filename=\"{0}\"\r\nContent-Type;application/octet-stream\r\n\r\n", fileName));
                byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strBuilderHeader.ToString());

                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                byte[] array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                fileStream.Close();

                Stream postStream = httpWebRequest.GetRequestStream();
                postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                postStream.Write(array, 0, array.Length);
                postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);

                //发送请求并获取响应的数据
                HttpWebResponse webResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                //直到 httpWebRequest.GetResponse() 程序才开始向目标网页发送POST请求
                Stream inStream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(inStream, Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (Exception ex)
            {
                Logging.LogService.RecordLog("将图片POST到服务器错误:\n" + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取图片素材-读取POST传回来的值
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        public static void PostDownLoad(string url,string file)
        {
            Logging.LogService.RecordLog("传值的Url:\n" + url);
            HttpWebRequest webRequest=(HttpWebRequest)WebRequest.Create(new Uri(url));
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                Stream stream = webResponse.GetResponseStream();
                byte[] buffer = new byte[2048];
                byte[] result = null;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    while (true)
                    {
                        int read = stream.Read(buffer, 0, buffer.Length);
                        if (read <= 0)
                        {
                            result = memoryStream.ToArray();
                            break;
                        }
                        memoryStream.Write(buffer, 0, read);
                    }
                }
                File.WriteAllBytes(file, result);
            }
        }


        /// <summary>
        /// 新增其他永久素材-HttpClient方式传递数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        public static string HttpAddMaterial(string url,string path)
        {
            var boundary = "";
            //使用HttpClient对象实现其他类型素材的上传
            HttpClient client = new HttpClient();
            //设置请求包头部
            client.DefaultRequestHeaders.Add("User-Agent", "KnowledageCenter");
            client.DefaultRequestHeaders.Remove("Expect");
            client.DefaultRequestHeaders.Remove("Connection");
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.DefaultRequestHeaders.ConnectionClose = true;
            //设置请求包主体

            //转换图片素材的content_type

            //上传文件

            return string.Empty;
        }
    }
}
