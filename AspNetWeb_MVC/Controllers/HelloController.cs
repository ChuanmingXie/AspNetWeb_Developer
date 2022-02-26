using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetWeb_MVC.Controllers
{
    public class HelloController : Controller
    {
        // GET: Hello
        //public string Index()
        //{
        //    return "这是我的<b>默认</b>动作方法...";
        //}

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome(string name,int numTimes = 1)
        {
            ViewBag.Message = "Hello " + name;
            ViewBag.NumTimes = numTimes;
            return View();
        }

        //
        //Get :/HelloWord/Welcome/
        public string WelcomeUrlStr()
        {
            return "这是Welcome()动作方法...";
        }

        public string WelcomeUrlParam(string name,int numTimes = 1)
        {
            /*HttpUtility.HtmlEncode 方法用于保护应用程序免受js的恶意输入*/
            return HttpUtility.HtmlEncode("Hello " + name + ", NumTimes is :" + numTimes);
        }

        public string WelcomeUrlID(string name,int ID = 1)
        {
            return HttpUtility.HtmlEncode("Hello " + name + ", ID:" + ID);
        }
    }
}