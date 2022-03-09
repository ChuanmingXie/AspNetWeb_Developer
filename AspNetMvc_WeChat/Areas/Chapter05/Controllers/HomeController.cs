using AspNetMvc_WeChat_Base.FuncHelper;
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
            LogService.RecordLog("微信平台数据参数:\n"+ Request.Url);
            WeChatBeginAPI beginAPI = new WeChatBeginAPI
            {
                SignatureOrigin = Request.QueryString["signature"],
                Timestamp = Request.QueryString["timestamp"],
                Nonce = Request.QueryString["nonce"],
                Encrypt_Type = (!string.IsNullOrEmpty(Request.QueryString["encrypt_type"]) ? Request.QueryString["encrypt_type"] : ""),
                Msg_SignatureOrigin = (!string.IsNullOrEmpty(Request.QueryString["msg_signature"]) ? Request.QueryString["msg_signature"] : ""),
                EchoStr = (!string.IsNullOrEmpty(Request.QueryString["echoStr"]) ? Request.QueryString["echoStr"] : ""),
            };
            //对token，timestamp，nonce加密生成singnature
            beginAPI.SignatureConfirm = WeChatTookenService.MakeSignature(beginAPI.Timestamp, beginAPI.Nonce);
            LogService.RecordLog(ReflectionHelper.GetModelByGeneric(beginAPI));

            if (Request.RequestType.ToUpper() == "POST")
            {
                string postMessage = PostInput();
                WeChatMessage weChatData = new WeChatMessage();
                weChatData.XMLToMessage(postMessage);
                if (beginAPI.Encrypt_Type != "")
                {
                    LogService.RecordLog("密文信息:\n" + weChatData.Encrypt);

                    WeChatCryptService weChatCrypt = new WeChatCryptService(
                        WeChatTookenService.Token,
                        WeChatTookenService.EncodingAESKey,
                        WeChatTookenService.AppID);

                    string messageXmlData = string.Empty;
                    int result = weChatCrypt.DecryptMsg(
                        beginAPI.Msg_SignatureOrigin, beginAPI.Timestamp, beginAPI.Nonce,
                        postMessage, ref messageXmlData);

                    if (result != 0)
                        LogService.RecordLog("解密错误代码:\n" + result);

                    LogService.RecordLog("密文解密:\n" + messageXmlData);
                    weChatData.XMLToMessage(messageXmlData);
                }
                WeChatMessageService.ShowMessage(weChatData);
                WeChatMessageService.ReplyMessage(weChatData);
                Response.Write("");
                Response.End();
            }
            else
            {
                if (beginAPI.SignatureConfirm == beginAPI.SignatureOrigin)
                {
                    //注意这里必须使用Response.Write()和Response.End(),否则配置出现错误
                    Response.Write(beginAPI.EchoStr);
                    Response.End();
                    ViewBag.CheckFromWeChat = beginAPI.EchoStr;
                }
                else
                {
                    Response.Write("");
                    //Response.Write("Invalid request!");
                    ViewBag.CheckFromWeChat = "Invalid request!";
                }
                return View(beginAPI);
            }
            return View();
        }

        /// <summary>
        /// 流数据接收来自微信平台POST过俩的数据
        /// </summary>
        /// <returns></returns>
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
                LogService.RecordLog("接收POST数据:\n" + stringBuilder.ToString());
                stream.Flush();
                stream.Close();
                stream.Dispose();
            }
            catch (Exception ex)
            {
                LogService.RecordLog("接收POST数据错误:\n" + ex.Message);
                return string.Empty;
            }
            return stringBuilder.ToString();
        }
    }
}