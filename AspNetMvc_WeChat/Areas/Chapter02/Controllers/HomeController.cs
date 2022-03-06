using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMvc_WeChat.Models;

namespace AspNetMvc_WeChat.Areas.Chapter02.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter02/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HelloMVC()
        {
            ViewBag.HelloMVC = "Hello ASP.NET MVC";
            return View();
        }

        public ActionResult DesignViewByRazor()
        {
            return View();
        }

        public ActionResult DataTransferInCV()
        {
            People people = new People()
            {
                Name = "赵宏文",
                Sex = "男",
                Age = 29
            };
            return View(people);
        }
    }
}