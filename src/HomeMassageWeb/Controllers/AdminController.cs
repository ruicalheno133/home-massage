using HomeMassageWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace HomeMassageWeb.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private static Random random = new Random();
        private HomeMassageContext db = new HomeMassageContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistarFuncionario(string Username, string Nome, string Email)
        {
            string password = RandomString(10);
            Funcionario funcionario = new Funcionario();

            funcionario.Username = Username;
            funcionario.Password = MyHelpers.HashPassword(password);
            funcionario.Nome = Nome;
            funcionario.Email = Email;
            funcionario.Estado = true;
            funcionario.Role = "employee";

            try
            {
                db.Funcionarios.Add(funcionario);
                db.SaveChanges();
                sendEmail(Email, Username, password);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("insucessAction");
            }
            return RedirectToAction("Index");
        }

        public ActionResult insucessAction()
        {
            ViewBag.title = "Insucesso";
            ViewBag.mensagem = "Erro no registo!";
            ViewBag.Shared = "~/Views/Shared/_Administrador.cshtml";
            ViewBag.controller = "Admin";
            ViewBag.view = "Index";
            return View("_insucessView");
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void sendEmail(string email, string username, string password)
        {
            try
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
                mail.To.Add(new MailAddress(email));
                mail.Body = "Username: " + username + "\n\nPassword: " + password;
                smtpClient.Send(mail);
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}