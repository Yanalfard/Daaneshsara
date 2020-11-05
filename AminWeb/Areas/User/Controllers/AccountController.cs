using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.User.Controllers
{
    public class AccountController : Controller
    {
        // GET: User/Account
        public ActionResult UserData()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult Wallet()
        {
            return View();
        }

    }
}