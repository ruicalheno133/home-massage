using System;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Mail;
using HomeMassageWeb.Models;

namespace HomeMassageWeb.Controllers
{
    public class ServicoController : Controller
    {
        private HomeMassageContext db = new HomeMassageContext();



        // GET: Servico
        public ActionResult Index()
        {
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
            return View();
        }

        public ActionResult EfetuarServico(string Cartao_Credito, string Endereco, string Codigo_Postal, string Data, int Massagem)
        {
            if (ModelState.IsValid)
            {
                HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string clienteUsername = authTicket.Name;

                Servico servico = new Servico();
                var clientes = (from m in db.Clientes
                                where m.Username == clienteUsername
                                select m);

                if (clientes.ToList<Cliente>().Count > 0)
                {
                    Cliente cliente = clientes.ToList<Cliente>().ElementAt<Cliente>(0);
                    servico.Cliente = cliente.Id_Cliente;
                    servico.Funcionario = 2; //MUDAR
                    servico.Massagem = Massagem;
                    servico.Data = DateTime.Parse(Data);
                    servico.Cartao_Credito = Cartao_Credito;
                    servico.Estado = false;
                    servico.Endereco = Endereco;
                    servico.Codigo_Postal = Codigo_Postal;

                    try
                    {
                        db.Servicoes.Add(servico);
                        db.SaveChanges();
                        sendEmail(cliente.Email, cliente.Nome, servico.Data, servico.Endereco);
                        return RedirectToAction("sucessAction");
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine(ex);
                        return RedirectToAction("insucessAction");
                    }
                }               
            }
            return RedirectToAction("insucessAction");
        }

        public ActionResult sucessAction()
        {
            ViewBag.title = "Sucesso";
            ViewBag.mensagem = "Serviço requisitado com sucesso!";
            ViewBag.subMensagem = "Confirmação enviada por email!";
            ViewBag.controller = "Cliente";
            ViewBag.view = "PaginaInicial";
            return View("_sucessView");
        }
        public ActionResult insucessAction()
        {
            ViewBag.title = "Insucesso";
            ViewBag.mensagem = "Erro ao efetuar requisição!";
            ViewBag.controller = "Servico";
            return View("_insucessView");
        }


        private void sendEmail(string clientEmail, string clientName, DateTime date, string local)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp-mail.outlook.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("homemassage_li4@hotmail.com", "homemassage27");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("homemassage_li4@hotmail.com");
            mail.Subject = "Confirmação da requisição da massagem";
            mail.To.Add(new MailAddress(clientEmail));
            mail.Body = "Bom dia " + clientName + ",\n\nInformamos que a sua massagem foi confirmada para o dia " + date.ToString() + ", " + local + ".\n\nCumprimentos, Zen+";
            smtpClient.Send(mail);
        }
    }
}