using AspNetMvc_WeChat_Base.APIHelper;
using AspNetMvc_WeChat_Base.Logging;
using AspNetMvc_WeChat_Base.WeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc_WeChat.Areas.Chapter05.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter05/Home
        public ActionResult Index()
        {
            WeChatBeginAPI beginAPI = new WeChatBeginAPI
            {
                EchoStr = Request.QueryString["echoStr"],
                Signature = Request.QueryString["signature"],
                Timestamp = Request.QueryString["timestamp"],
                Nonce = Request.QueryString["nonce"],
                Encrypt_Type = Request.QueryString["encrypt_type"]
            };
            LogService.RecordLog(ReflectionHelper.GetModelByGeneric(beginAPI));
            var signatureTemp = WeChatTookenService.MakeSignature(beginAPI.Timestamp, beginAPI.Nonce);
            if (!string.IsNullOrEmpty(beginAPI.EchoStr) && signatureTemp == beginAPI.Signature)
            {
                ViewBag.CheckFromWeChat = beginAPI.EchoStr;
            }
            else
            {
                ViewBag.CheckFromWeChat = "Invalid request!";
            }
            return View(beginAPI);
        }
    }
}