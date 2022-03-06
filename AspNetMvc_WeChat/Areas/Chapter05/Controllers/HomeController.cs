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
            return View();
        }
    }
}