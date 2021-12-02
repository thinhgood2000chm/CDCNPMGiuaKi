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

        // demo
        WebSubjectDAO dao = new WebSubjectDAO();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View(dao.GetAllSubjects());
        }
    }
}
