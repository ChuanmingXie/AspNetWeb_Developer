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
        public static string Post(string uri, string postData)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
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
    }
}
