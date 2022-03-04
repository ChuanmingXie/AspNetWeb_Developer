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
                Gender=Gender.Male
            };
            return View(student);
        }

        public ActionResult Contact()
        {
            People people = new People()
            {
                Name = "chuanming",
                Sex = "男",
                Age = 28
            };
            return View(people);
        }

        [HttpPost]
        public ActionResult Contact(People people)
        {
            var people2 = new People();
            people2.Name = people.Name;
            people2.Sex = people.Sex;
            people2.Age = people.Age;
            return View(people2);
        }
    }
}