using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HomeMassageWeb.Controllers
{
    public class ServicoController : Controller
    {
        // GET: Servico
        public ActionResult Index()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            ViewBag.NomeCliente = authCookie.Name;
            ViewBag.Title = "Requisitar Massagem";
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
            return View();
        }
    }
}