using HomeMassageWeb.Models;
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

                if (clientes.ToList<Cliente>().Count > 0)
                {
                    Cliente cliente = clientes.ToList<Cliente>().ElementAt<Cliente>(0);
                    using (MD5 md5Hash = MD5.Create())
                    {
                        if (MyHelpers.VerifyMd5Hash(md5Hash, Password, cliente.Password))
                        {
                            HttpCookie cookie = MyHelpers.CreateAuthorizeTicket(cliente.Username, cliente.Role);
                            Response.Cookies.Add(cookie);
                            return RedirectToAction("PaginaInicial", "Cliente");
                        }
                        else
                        {
                            ModelState.AddModelError("password", "Password incorreta!");
                            return View("Index");
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Endereço de email incorreto!");
            return View("Index");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult sucessAction()
        {
            ViewBag.title = "Sucesso";
            ViewBag.mensagem = "Login realizado com sucesso!";
            ViewBag.controller = "Login";
            return View();
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