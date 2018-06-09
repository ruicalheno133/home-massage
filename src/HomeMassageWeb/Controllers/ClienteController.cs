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

        public ActionResult Pedidos()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            string UserName = authTicket.Name;

            var clientes = (from m in db.Clientes
                            where m.Username == UserName
                            select m);

            //var servicos = new System.Linq.IQueryable<>();
            if (clientes.ToList<Cliente>().Count > 0)
            {
                Cliente cliente = clientes.ToList<Cliente>().ElementAt<Cliente>(0);
                var servicos = (from m in db.Servicoes
                                where m.Cliente == cliente.Id_Cliente
                                select m
                           );
                return View(servicos);
            }
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
            ViewBag.view = "Index";
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