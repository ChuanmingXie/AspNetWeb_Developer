using AspNetMvc_WeChat_Base.APIHelper;
using AspNetMvc_WeChat_Base.Logging;
using AspNetMvc_WeChat_Base.Model;
using AspNetMvc_WeChat_Base.WeChat;
using System;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace AspNetMvc_WeChat.Areas.Chapter05.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter05/Home
        public ActionResult Index()
        {
            if (Request.RequestType.ToUpper() == "POST")
            {
                string xmlMessage = PostInput();
                WeChatMessage weChatData = new WeChatMessage();
                weChatData.XMLToMessage(xmlMessage);
                WeChatMessageService.ShowMessage(weChatData);
                WeChatMessageService.ReplyMessage(weChatData);
                Response.Write("");
                Response.End();
            }
            else
            {
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
                    //注意这里必须使用Response.Write()和Response.End(),否则配置出现错误
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
            return View();
        }

        private string PostInput()
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                Stream stream = Request.InputStream;
                int count = 0;
                byte[] buffer = new byte[1024];
                while ((count = stream.Read(buffer, 0, 1024)) > 0)
                {
                    stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, count));
                }
                LogService.RecordLog("接收POST数据：" + stringBuilder.ToString());
                stream.Flush();
                stream.Close();
                stream.Dispose();
            }
            catch (Exception ex)
            {
                LogService.RecordLog("接收POST数据错误:" + ex.Message);
                return string.Empty;
            }
            return stringBuilder.ToString();
        }
    }
}