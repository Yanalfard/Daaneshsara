using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;

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
        public ActionResult ListCategory()
        {
            var list = _db.Cat.Get().Select(i => i.Name).ToList();
            return Json(new { success = true, responseText = list }, JsonRequestBehavior.AllowGet);
        }
    }
}