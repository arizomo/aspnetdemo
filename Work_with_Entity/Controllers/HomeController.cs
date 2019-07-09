using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work_with_Entity.Models;
using Work_with_Entity.ViewModel;

namespace Work_with_Entity.Controllers
{
    public class HomeController : Controller
    {

        ShoolContext db;
        public HomeController()
        {
            db = new ShoolContext();

        }

        public ActionResult Index()
        {
            ViewBag.Message = "Добро пожаловать на сайт студентов!!!!";
            return View();
        }

        public ActionResult About()
        {
            var data = from student in db.Students
                       group student by student.EnrollmentDate into dateGroup
                       select new EnrollmentDateGroup()
                       {
                           EnrollmentDate = dateGroup.Key,
                           StudentCount = dateGroup.Count()
                       };
            return View(data);
        }
    }
}