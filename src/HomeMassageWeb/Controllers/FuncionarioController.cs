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
    public class FuncionarioController : Controller
    {
        private HomeMassageContext db = new HomeMassageContext();

        // GET: Funcionario
        public ActionResult Index()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            string UserName = authTicket.Name;

            var funcionarios = (from m in db.Funcionarios
                                where m.Username == UserName
                                select m);

            if (funcionarios.ToList<Funcionario>().Count > 0)
            {
                Funcionario funcionario = funcionarios.ToList<Funcionario>().ElementAt<Funcionario>(0);
                var servicos = (from m in db.Servicoes
                                where m.Funcionario == funcionario.Id_Funcionario
                                && m.Estado == false
                                select m
                           );

                return View(servicos);
            }

            return View();
        }



        public ActionResult Servico(int Id_Servico)
        {
            var servicos = (from m in db.Servicoes
                            where m.Id_Servico == Id_Servico
                            select m);

            if (servicos.ToList<Servico>().Count > 0)
            {
                Servico servico = servicos.ToList<Servico>().ElementAt<Servico>(0);

                var clientes = (from m in db.Clientes
                                where m.Id_Cliente == servico.Cliente
                                select m);

                if (clientes.ToList<Cliente>().Count > 0)
                {
                    Cliente cliente = clientes.ToList<Cliente>().ElementAt<Cliente>(0);
                    ViewBag.nome = cliente.Nome;
                }
                return View(servico);
            }

            return View();
        }


        [HttpPost]
        public ActionResult FinalizarServico(int Id_Servico)
        {
            ViewBag.Id_Servico = Id_Servico;
            return View();
        }

    }
}