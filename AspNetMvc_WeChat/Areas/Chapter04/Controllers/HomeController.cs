using AspNetMvc_WeChat_Base.APIHelper;
using AspNetMvc_WeChat_Base.Model;
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
            ViewBag.menuCreate = WeChatMenuService.Create(Server.MapPath("~/Scripts/wechat-menu.json"));
            return View();
        }

        public ActionResult MenuSearch()
        {
            ViewBag.menuSearch = WeChatMenuService.Search();
            return View();
        }

        public ActionResult MenuDelete()
        {
            WeChatResult weChatResult = JSONHelper.JSONToObject<WeChatResult>(WeChatMenuService.Delete());
            ViewBag.menuDelete = weChatResult.errcode == "0" ? "操作成功" : "操作失败" + weChatResult.errmsg;
            return View();
        }

        public ActionResult MenuParam()
        {
            WeChatMenuConfig menuConfig = JSONHelper.JSONToObject<WeChatMenuConfig>(WeChatMenuService.Param());
            return View(menuConfig);
        }

        public ActionResult MenuPersonalCreate()
        {
            ViewBag.menuCreatePersonal = WeChatMenuService.CreatePersonalMenu(Server.MapPath("~/Scripts/wechat-menu-personal.json"));
            return View(); 
        }

        public ActionResult MenuPersonalDelete()
        {
            ViewBag.menuDeletePersonal = WeChatMenuService.DeletePersonalMenu("417142809");
            return View();
        }

        public ActionResult MenuPersonalMatch()
        {
            ViewBag.menuMatchPersonal = WeChatMenuService.MatchPersonalMenu("obotH60QgZm7LBI6wGBpLaOWCnHk");
            return View();
        }
    }
}