using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeMassageWeb.Controllers
{
    public class ServicoController : Controller
    {
        // GET: Servico
        public ActionResult Index()
        {
            ViewBag.Title = "Requisitar Massagem";
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
            return View();
        }
    }
}