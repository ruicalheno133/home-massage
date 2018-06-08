using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HomeMassageWeb.Controllers
{
    public class LoginController : Controller
    {
        private HomeMassageContext db = new HomeMassageContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                var clientes = (from m in db.Clientes
                               where m.Email == Email
                               select m);

                if (clientes.ToList<Cliente>().Count > 0)
                {
                    Cliente cliente = clientes.ToList<Cliente>().ElementAt<Cliente>(0);
                    using (MD5 md5Hash = MD5.Create())
                    {
                        if (MyHelpers.VerifyMd5Hash(md5Hash, Password, cliente.Password))
                        {
                            HttpCookie cookie = MyHelpers.CreateAuthorizeTicket(cliente.Id_Cliente.ToString(), cliente.Role);
                            Response.Cookies.Add(cookie);
                            return RedirectToAction("sucessAction");
                        }
                        else
                        {
                            ModelState.AddModelError("password", "Password incorreta!");
                            return View("Index");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Endereço de email incorreto!");
                    return View("Index");

                }
            }
            return RedirectToAction("insucessAction");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult sucessAction()
        {
            ViewBag.title = "Sucesso";
            ViewBag.mensagem = "Login realizado com sucesso";
            ViewBag.controller = "Home";
            return View("_sucessView");
        }
        public ActionResult insucessAction()
        {
            ViewBag.title = "Insucesso";
            ViewBag.mensagem = "Erro ao efetuar login!";
            ViewBag.controller = "Login";
            return View("_insucessView");
        }
    }
}