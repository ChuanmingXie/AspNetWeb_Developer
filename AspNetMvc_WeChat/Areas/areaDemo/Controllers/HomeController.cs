using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc_WeChat.Areas.areaDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: areaDemo/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}