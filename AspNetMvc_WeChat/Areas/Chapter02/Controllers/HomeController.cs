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
            string msgStr = "现在预设一个元素:<input>";
            ViewBag.ElementStr = msgStr;
            ViewBag.Element = new MvcHtmlString(msgStr);
            ViewBag.Name = "张桃芳";
            var student = new Student()
            {
                StudentName = "李芳",
                Password = "123",
                Age = 22,
                Birthday = DateTime.Parse("2000-02-21"),
                isNewlyEnrolled = true,
                Gender = Gender.Male
            };
            return View(student);
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

        [HttpPost]
        public ActionResult DataTransferInCV(People people)
        {
            var people2 = new People();
            people2.Name = people.Name;
            people2.Sex = people.Sex;
            people2.Age = people.Age;
            return View(people2);
        }
    }
}