using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            Session["User"] = check;
            return RedirectToAction("Index","Home");
        }
        public ActionResult Logout()
        {
            Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }
    }
}