using DataLayer.Models;
using DataLayer.Services;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AminWeb.Controllers
{
    public class AccountController : Controller
    {
        private Heart _db = new Heart();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Login(VmLogin user)
        {
            if (ModelState.IsValid)
            {
                string hashPass = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password,"SHA256");
                if (_db.User.Get().Any(i => i.Email == user.Email && i.Password == hashPass))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, user.RememberMe);
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError("Email","کاربری یافت نشد");
                }
            }
            return PartialView("Login",user);
        }

        public ActionResult SignUp()
        {
            return PartialView();
        }

        public ActionResult RecoverPassword()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}