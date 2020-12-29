using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Controllers
{
    public class ProfileController : Controller
    {
        private Heart _db = new Heart();
        // GET: Profile
        [Route("Dashboard/{id}/{name}")]
        public ActionResult Dashboard(int id, string name)
        {
            ViewBag.Name = name;
            List<TblVideo> selectVideoInUser = _db.Video.Get().Where(i => i.UserId == id && i.IsActive).OrderByDescending(i=>i.DateSubmited).Take(12).ToList();
            return View(selectVideoInUser);
        }
        [Route("Videos/{id}/{name}")]
        public ActionResult Videos(int id, string name)
        {
            ViewBag.Name = name;
            return View(_db.Video.Get().Where(i => i.UserId == id && i.IsActive).OrderByDescending(i => i.DateSubmited));
        }
        [Route("Classes/{id}/{name}")]
        public ActionResult Classes(int id, string name)
        {
            return View(_db.Playlist.Get().Where(i => i.UserId == id && i.IsActive).OrderByDescending(i => i.DateSubmited).ToList());
        }

        public ActionResult SearchResults()
        {
            return View();
        }

    }
}