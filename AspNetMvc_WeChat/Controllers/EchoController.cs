using AspNetMvc_WeChat_Base.Logging;
using AspNetMvc_WeChat_Base.WeChat;
using System;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace AspNetMvc_WeChat.Controllers
{
    public class EchoController : Controller
    {
        // GET: Echo
        public ActionResult Index()
        {
            /*可以使用下面的代码将两个Index合并*/
            //if (Request.RequestType.ToUpper() == "POST") { } else {  }
            string echoString = Request.QueryString["echoStr"];
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            LogService.RecordLog("echoString:" + echoString);
            LogService.RecordLog("signature:" + signature);
            LogService.RecordLog("timestamp:" + timestamp);
            LogService.RecordLog("nonce:" + nonce);

            //对token，timestamp，nonce加密生成singnature
            var signatureTemp = WeChatTookenService.MakeSignature(timestamp, nonce);
            LogService.RecordLog("signatureTemp:" + signatureTemp);

            if (!string.IsNullOrEmpty(echoString)&&signature==signatureTemp)
            {
                Response.Write(echoString);
                Response.End();
            }
            else
            {
                Response.Write("无效的信息传递");
            }
            return View();
        }

        [HttpPost,ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult IndexPost()
        {
            try
            {
                Stream stream = Request.InputStream;
                int count = 0;
                byte[] buffer = new byte[1024];
                StringBuilder stringBuilder = new StringBuilder();
                while ((count = stream.Read(buffer, 0, 1024)) > 0)
                {
                    stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, count));
                }
                stream.Flush();
                stream.Close();
                stream.Dispose();
                LogService.RecordLog("接收POST数据：<br/>"+stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                LogService.RecordLog("接收POST数据错误:"+ex.Message);
            }
            return View();
        }
    }

}