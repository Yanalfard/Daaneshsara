using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.User.Controllers
{
    public class LibraryController : Controller
    {
        private Heart _db = new Heart();
        // GET: User/Library
        TblUser SelectUser()
        {
            TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Email == User.Identity.Name);
            return selectUser;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IsBookMark()
        {
            List<TblVideo> list = _db.UserVideoRel.Get().Where(i => i.UserId == SelectUser().UserId).Select(i => i.TblVideo).ToList();
            return PartialView(list);
        }
    }
}