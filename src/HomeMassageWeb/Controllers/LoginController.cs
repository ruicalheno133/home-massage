﻿using HomeMassageWeb.Models;
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
        public ActionResult Login(string Username, string Password)
        {
            if (ModelState.IsValid)
            {
                var clientes = (from m in db.Clientes
                                where m.Username == Username
                                select m);

                if (clientes.ToList<Cliente>().Count == 0)
                {
                    var funcionarios = (from m in db.Funcionarios
                                        where m.Username == Username
                                        select m);

                    if (funcionarios.ToList<Funcionario>().Count > 0)
                    {
                        Funcionario funcionario = funcionarios.ToList<Funcionario>().ElementAt<Funcionario>(0);
                        using (MD5 md5Hash = MD5.Create())
                        {
                            if (MyHelpers.VerifyMd5Hash(md5Hash, Password, funcionario.Password))
                            {
                                HttpCookie cookie = MyHelpers.CreateAuthorizeTicket(funcionario.Username, funcionario.Role);
                                Response.Cookies.Add(cookie);
                                if (Username.Equals("admin"))
                                    return RedirectToAction("Index", "Admin");
                                else
                                    return RedirectToAction("Index", "Funcionario");
                            }
                            else
                            {
                                ModelState.AddModelError("password", "Password incorreta!");
                                return View("Index");
                            }
                        }
                    }
                }
                else
                {
                    Cliente cliente = clientes.ToList<Cliente>().ElementAt<Cliente>(0);
                    using (MD5 md5Hash = MD5.Create())
                    {
                        if (MyHelpers.VerifyMd5Hash(md5Hash, Password, cliente.Password))
                        {
                            HttpCookie cookie = MyHelpers.CreateAuthorizeTicket(cliente.Username, cliente.Role);
                            Response.Cookies.Add(cookie);
                            return RedirectToAction("Index", "Cliente");
                        }
                        else
                        {
                            ModelState.AddModelError("password", "Password incorreta!");
                            return View("Index");
                        }
                    }
                }     
            }
            ModelState.AddModelError("", "Username incorreto!");
            return View("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}