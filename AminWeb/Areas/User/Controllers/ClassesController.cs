using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.User.Controllers
{
    public class ClassesController : Controller
    {
        // GET: User/Classes
        public ActionResult Classes()
        {
            return PartialView();
        }
        public ActionResult Create()
        {
            return PartialView();
        }
    }
}