using HomeMassageWeb.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        public ActionResult Recibo()
        {
            /*Response.ContentType = "application/png";
            Response.AppendHeader("Content-Disposition", "attachment; filename=logo.png");
            Response.TransmitFile(Server.MapPath("~/Content/images/logo.png"));
            Response.End();*/
            return RedirectToAction("Pedidos");
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

        [HttpPost]
        public ActionResult CancelarServico(int ID)
        {
            try
            {
                var servicos = (from m in db.Servicoes
                                where m.Id_Servico == ID
                                select m);

                if (servicos.ToList<Servico>().Count > 0)
                {
                    Servico servico = servicos.ToList<Servico>().ElementAt<Servico>(0);
                    db.Servicoes.Remove(servico);
                    db.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Pedidos");
            }
            return RedirectToAction("Pedidos");
        }


        public ActionResult Classificar(int Id_Servico)
        {
            ViewBag.Id_Servico = Id_Servico;
            return View();
        }


        [HttpPost]
        public ActionResult ClassificarServico(int Classificacao, int Id_Servico)
        {
            var servicos = (from m in db.Servicoes
                            where m.Id_Servico == Id_Servico
                            select m);

            if (servicos.ToList<Servico>().Count > 0)
            {
                Servico servico = servicos.ToList<Servico>().ElementAt<Servico>(0);
                servico.Classificacao = Classificacao;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex);
                    return RedirectToAction("Pedidos");
                }
            }
            return RedirectToAction("Pedidos");
        }


        public ActionResult EditarPerfil(string Username)
        {
            var clientes = (from m in db.Clientes
                            where m.Username == Username
                            select m);

            if (clientes.ToList<Cliente>().Count > 0)
            {
                Cliente cliente = clientes.ToList<Cliente>().ElementAt<Cliente>(0);
                ViewBag.Id_Cliente = cliente.Id_Cliente;
                ViewBag.Email = cliente.Email;
                ViewBag.Contacto = cliente.Contacto;
                return View(cliente);
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditarDados(int Id_Cliente, string Email, string Password, string Contacto)
        {
            var clientes = (from m in db.Clientes
                            where m.Id_Cliente == Id_Cliente
                            select m);

            if (clientes.ToList<Cliente>().Count > 0)
            {
                Cliente cliente = clientes.ToList<Cliente>().ElementAt<Cliente>(0);

                if (Email != null)
                    cliente.Email = Email;
                if (Password != null)
                    cliente.Password = Password;
                if (Contacto != null)
                    cliente.Contacto = Int32.Parse(Contacto);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex);
                    return RedirectToAction("insucessAction");
                }
            }
            return RedirectToAction("PaginaInicial");
        }
    }
}