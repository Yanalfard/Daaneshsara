using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Videos()
        {
            return View();
        }

        public ActionResult Classes()
        {
            return View();
        }

        public ActionResult SearchResults()
        {
            return View();
        }

    }
}