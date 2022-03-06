using AspNetMvc_WeChat_Base.WeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc_WeChat.Areas.Chapter04.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter04/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuCreate()
        {
            ViewBag.menuCreate = WeChatMenuService.Create(Server.MapPath("~/Script/wechat-menu.json"));
            return View();
        }
    }
}