using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return PartialView();
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