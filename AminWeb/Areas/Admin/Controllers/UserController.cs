using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult UserList()
        {
            return View();
        }

        public ActionResult ViewSingleUser()
        {
            return View();
        }

        public ActionResult EditUser()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            return View();
        }
    }
}