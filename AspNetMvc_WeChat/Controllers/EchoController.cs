using AspNetMvc_WeChat_Base.APIHelper;
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
            WeChatBeginAPI beginAPI = new WeChatBeginAPI
            {
                EchoStr = Request.QueryString["echoStr"],
                Signature = Request.QueryString["signature"],
                Timestamp = Request.QueryString["timestamp"],
                Nonce = Request.QueryString["nonce"],
                //Encrypt_Type = Request.QueryString["encrypt_type"]
            };
            //对token，timestamp，nonce加密生成singnature
            beginAPI.SignatureTemp = WeChatTookenService.MakeSignature(beginAPI.Timestamp, beginAPI.Nonce);
            LogService.RecordLog(ReflectionHelper.GetModelByGeneric(beginAPI));
            if (!string.IsNullOrEmpty(beginAPI.EchoStr) && beginAPI.SignatureTemp == beginAPI.Signature)
            {
                //注意是同Response.Write()和Response.End()否则配置出现错误
                Response.Write(beginAPI.EchoStr);
                Response.End();
                ViewBag.CheckFromWeChat = beginAPI.EchoStr;
            }
            else
            {
                Response.Write("无效的信息传递");
                ViewBag.CheckFromWeChat = "Invalid request!";
            }
            return View(beginAPI);
        }

        [HttpPost, ActionName("Index")]
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
                LogService.RecordLog("接收POST数据：<br/>" + stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                LogService.RecordLog("接收POST数据错误:" + ex.Message);
            }
            return View();
        }
    }
}

