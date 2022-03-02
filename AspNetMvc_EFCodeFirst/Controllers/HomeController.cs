using AspNetMvc_EFCodeFirst.DAL;
using AspNetMvc_EFCodeFirst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc_EFCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //注释掉原先的代码，使用原生的SQL语句测试查询
            //IQueryable<EnrollmentDateGroup> data = from student in db.Students
            //            group student by student.EnrollmentDate into dateGroup
            //            select new EnrollmentDateGroup()
            //            {
            //                EnrollmentDate = dateGroup.Key,
            //                StudentCount = dateGroup.Count()
            //            };
            string query = "SELECT EnrollmentDate,COUNT(*) AS StudentCount" +
                " FROM Person" +
                " WHERE Discriminator='Student'" +
                " GROUP BY EnrollmentDate";
            var data = db.Database.SqlQuery<EnrollmentDateGroup>(query);
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}