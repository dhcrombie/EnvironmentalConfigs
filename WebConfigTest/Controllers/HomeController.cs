using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebConfigTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Config = System.Configuration.EnvironmentalConfigurationManager.AppSettings["User"];
            return View();
        }
    }
}
