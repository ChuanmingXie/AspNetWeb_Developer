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
            return View();
        }
    }
}