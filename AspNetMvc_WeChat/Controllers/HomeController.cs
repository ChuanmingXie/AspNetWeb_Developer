using AspNetMvc_WeChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc_WeChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewData["newTitle"] = "Home-WeChat";
            //ViewBag.Time = DateTime.Now.ToString("yyyy-MM-dd");
            return View();

        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test()
        {
            string id = Request.Form["PersonID"];
            return View();
        }

        [HttpPost]
        public ActionResult Test(FormCollection form)
        {
            string id = form["PersonID"];
            return View();
        }
    }
}