using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeMassageWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            ViewBag.Message = "Zen+";
            return View();
        }

        public ActionResult Massagens()
        {
            ViewBag.Message = "Todas as massagens disponíveis!";
            return View();
        }

        public ActionResult Localizacao()
        {
            ViewBag.Message = "Onde estamos localizados!";
            return View();
        }
    }
}