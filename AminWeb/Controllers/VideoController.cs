using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Controllers
{
    public class VideoController : Controller
    {
        private Heart _db = new Heart();

        // GET: Video
        [Route("VideoView/{id}/{name}")]
        public ActionResult VideoView(int id, string title)
        {
            ViewBag.Name = title;
            return View(_db.Video.GetById(id));
        }

        public ActionResult ListVideos()
        {
            List<TblVideo> selectAllVideos = _db.Video.Get().Where(i => i.IsHome && i.IsActive).Take(20).ToList();
            return PartialView(selectAllVideos);
        }
        public ActionResult SelectVideos()
        {
            List<TblVideo> selectAllVideos = _db.Video.Get(i => i.IsHome && i.IsActive).OrderByDescending(i => i.DateSubmited).Take(15).ToList();
            return PartialView("ListVideos", selectAllVideos);
        }
        public ActionResult SelectVideosByCategory(string name)
        {
            TblCatagory selectCatById = _db.Cat.Get(i => i.Name == name).Single();
            List<TblVideo> selectVideoByCategory = _db.Video.Get(i => i.CatagoryId == selectCatById.CatagoryId && i.IsActive).ToList();
            return PartialView("ListVideos", selectVideoByCategory);
        }
        public ActionResult SelectVideoBySearch(int id)
        {
            List<TblVideo> selectVideoByCategory = new List<TblVideo>();
            if (id == 1)
            {
                selectVideoByCategory = _db.Video.Get(i => i.IsActive).OrderByDescending(i => i.DateSubmited).Take(15).ToList();
            }
            else if (id == 2)
            {
                selectVideoByCategory = _db.Video.Get(i => i.IsActive).OrderByDescending(i => i.ViewCount).Take(15).ToList();
            }
            else if (id == 3)
            {
                selectVideoByCategory = _db.Video.Get(i => i.IsActive).OrderByDescending(i => i.LikeCount).Take(15).ToList();
            }
            return PartialView("ListVideos", selectVideoByCategory);
        }
        [Route("SelectVideoName/{val}")]
        public ActionResult SelectVideoName(string val)
        {
            var list = _db.Video.Get(i => i.Title.Contains(val)).Select(i => i.Title).ToList();
            return Json(new { success = true, responseText = list }, JsonRequestBehavior.AllowGet);
        }
        [Route("SearchVideoByName/{name}")]
        public ActionResult SearchVideoByName(string name)
        {
            List<TblVideo> selectVideoByCategory = new List<TblVideo>();
            selectVideoByCategory = _db.Video.Get(i => i.IsActive && i.Title.Contains(name)).OrderByDescending(i => i.DateSubmited).ToList();
            return PartialView("ListVideos", selectVideoByCategory);
        }
    }
}