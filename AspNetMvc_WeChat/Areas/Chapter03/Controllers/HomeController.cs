using AspNetMvc_WeChat_Base.WeChat;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AspNetMvc_WeChat.Areas.Chapter03.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter03/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAccessToken()
        {
            ViewBag.token = "access_token:\n" + WeChatTookenService.Access_token;
            return View();
        }

        public ActionResult WeChatIPAddress()
        {
            List<string> listIP = WeChatTookenService.GetCallbackIP();
            return View(listIP);
        }
    }
}