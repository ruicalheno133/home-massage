using HomeMassageWeb.Models;
using Nexmo.Api;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
            var servicos = (from m in db.Servicoes
                            where m.Id_Servico == Id_Servico
                            select m);

            if (servicos.ToList<Servico>().Count > 0)
            {
                Servico servico = servicos.ToList<Servico>().ElementAt<Servico>(0);
                servico.Estado = true;

                var clientes = (from m in db.Clientes
                                where m.Id_Cliente == servico.Cliente
                                select m);

                var massagens = (from m in db.Massagems
                                 where m.Id_Massagem == servico.Massagem
                                 select m);

                try
                {
                    db.SaveChanges();
                    if (clientes.ToList<Cliente>().Count > 0)
                    {
                        Cliente cliente = clientes.ToList<Cliente>().ElementAt<Cliente>(0);
                        db.SaveChanges();

                        if (massagens.ToList<Massagem>().Count > 0)
                        {
                            Massagem massagem = massagens.ToList<Massagem>().ElementAt<Massagem>(0);
                            sendEmail(servico, cliente, massagem);
                        }
                    }
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Id_Servico = Id_Servico;
            return View();
        }

        public ActionResult ObterDirecoes(string Address)
        {
            ViewBag.Address = Address;
            return View();
        }

        [HttpPost]
        public ActionResult ReportarOcorrencia(int Id_Servico, string Ocorrencia)
        {
            if (Ocorrencia != null)
            {
                var servicos = (from m in db.Servicoes
                                where m.Id_Servico == Id_Servico
                                select m);

                if (servicos.ToList<Servico>().Count > 0)
                {
                    Servico servico = servicos.ToList<Servico>().ElementAt<Servico>(0);
                    servico.Ocorrencias = Ocorrencia;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine(ex);
                        return RedirectToAction("Index");
                    }
                }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        private void sendEmail(Servico s, Cliente c, Massagem m)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp-mail.outlook.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("homemassage_li4@hotmail.com", "homemassage27");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;


            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("homemassage_li4@hotmail.com");
            mail.Subject = "Fatura #" + s.Id_Servico;
            mail.To.Add(new MailAddress(c.Email));
            mail.Body = "<p><b>Nº Serviço: </b>" + s.Id_Servico + "</p>";
            smtpClient.Send(mail);

            SMS.Send(new SMS.SMSRequest { from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"], to = "351915049712", text = "GORDIXA" });
        }
    }
}