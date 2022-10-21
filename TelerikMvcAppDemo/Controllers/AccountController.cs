using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TelerikMvcAppDemo.Models;
using TelerikMvcAppDemo.Repositories;

namespace TelerikMvcAppDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(Employee employee)
        {
            var check = unitOfWork.Employees.CheckLogin(employee.Email, employee.Password);

            if (check != null)
            {

                FormsAuthentication.SetAuthCookie(check.Email, false);
                FormsAuthentication.SetAuthCookie(Convert.ToString(check.EmployeeID), false);
                var authTicket = new FormsAuthenticationTicket(1, check.Email, DateTime.Now, DateTime.Now.AddMinutes(20), false, check.Role.Trim());
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Email or password is incorrect";
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}