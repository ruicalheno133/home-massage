using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeMassageWeb;

namespace HomeMassageWeb.Controllers
{
    public class ClienteController : Controller
    {
        HomeMassageContext db = new HomeMassageContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarCliente([Bind(Include = "Username,Password,Nome,Email,Contacto,Numero_Contribuinte")] Cliente cliente)
        {
            if(ModelState.IsValid)
            {
                cliente.Password = MyHelpers.HashPassword(cliente.Password);
                try
                {
                    db.Clientes.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("sucessAction");
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex);
                    return RedirectToAction("insucessAction");
                }
            }
            return RedirectToAction("insucessAction");
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