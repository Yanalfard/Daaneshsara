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
            List<TblVideo> selectVideoInUser = _db.Video.Get().Where(i => i.UserId == id && i.IsActive).OrderByDescending(i => i.DateSubmited).Take(12).ToList();
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
            ViewBag.Name = name;
            return View(_db.Playlist.Get().Where(i => i.UserId == id && i.IsActive).OrderByDescending(i => i.DateSubmited).ToList());
        }
        public ActionResult SearchResults(int id, string q)
        {
            List<TblVideo> search = new List<TblVideo>();
            if (q == "")
            {
                ViewBag.Names = "متن مورد نظر تایپ شود";
                search.AddRange(_db.Video.Get(i => i.UserId == id && i.IsActive).OrderByDescending(i => i.DateSubmited).ToList());
                return View(search);
            }
            search.AddRange(_db.Video.Get(i => i.Title.Contains(q) || i.Description.Contains(q)));
            search.AddRange(_db.VideoPlaylistKeyword.Get(i => i.Name.Contains(q)).Select(i => i.TblVideo));
            search.Distinct();
            ViewBag.Names = q;
            if (search.Count == 0)
            {
                ViewBag.Names = "نتیجه ای یافت نشد";
                search.AddRange(_db.Video.Get(i => i.UserId == id && i.IsActive).OrderByDescending(i => i.DateSubmited).ToList());
            }
            return View(search);
        }

    }
}