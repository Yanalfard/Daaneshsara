using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Controllers
{
    public class CategoryController : Controller
    {
        private Heart _db = new Heart();

        // GET: Category
        public ActionResult Index()
        {
            return PartialView(_db.Cat.Get());
        }
    }
}