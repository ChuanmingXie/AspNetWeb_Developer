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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        public static string HttpUPloadFile(string url, string file)
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
        public static void PostDownLoad(string url, string file)
        {
            Logging.LogService.RecordLog("传值的Url:\n" + url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
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
        public static string HttpAddMaterial(string url, string path)
        {
            var boundary = "fbce142e-4e8e-4bf3-826d-cc3cf506cccc"; // 分隔符
            //使用HttpClient对象实现其他类型素材的上传
            HttpClient client = new HttpClient();
            //设置请求包头部
            client.DefaultRequestHeaders.Add("User-Agent", "KnowledgeCenter");
            client.DefaultRequestHeaders.Remove("Expect");
            client.DefaultRequestHeaders.Remove("Connection");
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.DefaultRequestHeaders.ConnectionClose = true;
            //设置请求包主体
            var content = new MultipartFormDataContent(boundary);
            content.Headers.Remove("Content-Type");
            content.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data;boundary=" + boundary);
            //转换图片素材的content_type
            Image image = Image.FromFile(path);
            byte[] ImageByte = ImageToByte(image);
            var contentByte = new ByteArrayContent(ImageByte);
            content.Add(contentByte);
            string fileName = Path.GetFileName(path);
            string ext = Path.GetExtension(path).ToLower();
            string content_type = string.Empty;
            switch (ext)
            {
                case ".jpeg":
                case ".jpg": content_type = "image/jpeg"; break;
                case ".png": content_type = "image/png"; break;
                case ".gif": content_type = "image/gif"; break;
            }
            contentByte.Headers.Remove("Content-Disposition");
            contentByte.Headers.TryAddWithoutValidation("Content-Disposition", "form-data;name=\"media\";filename=\"" + fileName + "\"" + "");
            contentByte.Headers.Remove("Content-Type");
            contentByte.Headers.TryAddWithoutValidation("Content-Type", content_type);
            //上传文件
            try
            {
                var result = client.PostAsync(url, content);
                var callBack = result.Result.Content.ReadAsStringAsync().Result;
                if (result.Result.StatusCode != HttpStatusCode.OK)
                {
                    Logging.LogService.RecordLog("HttpStatusCode错误:" + callBack);
                }
                if (callBack.Contains("media_id"))
                {
                    return callBack;
                }
            }
            catch (Exception ex)
            {
                Logging.LogService.RecordLog("新增其他永久素材错误:\n" + ex.Message);
            }

            return string.Empty;
        }

        private static byte[] ImageToByte(Image image)
        {
            ImageFormat format = image.RawFormat;
            ImageFormat IsPicFormat(ImageFormat param)
            {
                return param.Equals(ImageFormat.Jpeg)
                ? ImageFormat.Jpeg : param.Equals(ImageFormat.Png)
                ? ImageFormat.Png : param.Equals(ImageFormat.Bmp)
                ? ImageFormat.Bmp : param.Equals(ImageFormat.Gif)
                ? ImageFormat.Gif : param.Equals(ImageFormat.Icon)
                ? ImageFormat.Icon : ImageFormat.Jpeg;
            }
            //可简化 bool IsPicFormat(param)

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, IsPicFormat(format));
                byte[] buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public static void HttpGetMaterial(string url,string meidaId, string file)
        {
            //转换 mediaId 参数的编码类型，获取byte[]数组
            byte[] byteArray = Encoding.UTF8.GetBytes(meidaId);
            // 1.创建 HttpWebRequest 对象
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            // 2.初始化 HttpWebReques 对象
            webRequest.Method = "POST";
            webRequest.ContentType = "text/html";
            webRequest.ContentLength = byteArray.Length;
            // 3.
            Stream stream = webRequest.GetRequestStream();
            stream.Write(byteArray, 0, byteArray.Length);
            stream.Close();
            //4.处理数据返回
            using (WebResponse webResponse=webRequest.GetResponse())
            {
                Stream inStream = webResponse.GetResponseStream();
                byte[] buffer = new byte[2048];
                byte[] result = null;
                using (MemoryStream memory=new MemoryStream())
                {
                    while (true)
                    {
                        int read = memory.Read(buffer, 0, buffer.Length);
                        if (read <= 0)
                        {
                            result = memory.ToArray();break;
                        }
                        memory.Write(buffer, 0, read);
                    }
                }
                File.WriteAllBytes(file, result);
            }
        }
    }
}
