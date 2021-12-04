using CenterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CenterManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Trang chủ";
            return View();
        }

        public ActionResult Subject()
        {
            ViewBag.Title = "Môn học";
            return View();
        }
        public ActionResult Class()
        {
            ViewBag.Title = "Lớp học";
            return View();
        }
        public ActionResult Student()
        {
            ViewBag.Title = "student page";

            return View();
        }

        public ActionResult Teacher()
        {
            ViewBag.Title = "teacher page";

            return View();
        }
    }
}
