using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HomeMassageWeb.Models;

namespace HomeMassageWeb.Controllers
{
    //[Authorize(Roles = "user")]
    public class ClienteController : Controller
    {
        private HomeMassageContext db = new HomeMassageContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaginaInicial()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            ViewBag.NomeCliente = authTicket.Name;
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarCliente([Bind(Include = "Username,Password,Nome,Email,Contacto,Data_Nascimento,Numero_Contribuinte")] Cliente cliente)
        {
            if(ModelState.IsValid)
            {
                cliente.Password = MyHelpers.HashPassword(cliente.Password);
                cliente.Role = "user";
                try
                {
                    db.Clientes.Add(cliente);
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex);
                    return RedirectToAction("insucessAction");
                }
            }
            return RedirectToAction("sucessAction");
        }
        public ActionResult sucessAction()
        {
            ViewBag.title = "Sucesso";
            ViewBag.mensagem = "Registo efetuado com sucesso!";
            ViewBag.controller = "Login";
            return View("_sucessView");
        }
        public ActionResult insucessAction()
        {
            ViewBag.title = "Insucesso";
            ViewBag.mensagem = "Erro no registo!";
            ViewBag.controller = "Cliente";
            return View("_insucessView");
        }
    }
}