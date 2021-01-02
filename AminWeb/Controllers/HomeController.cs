using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer.Models;
using DataLayer.MetaData;
using DataLayer.Services;

namespace AminWeb.Controllers
{
    public class HomeController : Controller
    {
        private Heart _db = new Heart();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            return View(_db.Config.Get().FirstOrDefault());
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Rules()
        {
            return View(_db.Config.Get().FirstOrDefault());

        }
    }
}