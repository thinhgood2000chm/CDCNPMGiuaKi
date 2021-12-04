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
            if (Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Title = "Trang chủ";
            return View();
        }

        public ActionResult Subject()
        {
            if (Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Title = "Môn học";
            return View();
        }
        public ActionResult Class()
        {
            if (Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Title = "Lớp học";
            return View();
        }
        public ActionResult Student()
        {
            if (Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Title = "student page";

            return View();
        }

        public ActionResult Teacher()
        {
            if (Request.Cookies["token"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Title = "teacher page";

            return View();
        }
        public ActionResult Login()
        {
            if (Request.Cookies["token"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "login page";

            return View();
        }

        public ActionResult Introduce()
        {

            ViewBag.Title = "introduce page";

            return View();
        }

        public ActionResult Register()
        {

            ViewBag.Title = "Rgister page";

            return View();
        }
    }
}
