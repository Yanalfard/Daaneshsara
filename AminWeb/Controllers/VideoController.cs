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
            List<TblVideo> selectAllVideos = _db.Video.Get().Where(i => i.IsHome).Take(9).ToList();
            return PartialView(selectAllVideos);
        }
        public ActionResult SelectVideosByCategory(int id)
        {
            List<TblVideo> selectVideoByCategory = _db.Video.Get().Where(i => i.CatagoryId == id && i.IsHome).ToList();
            return PartialView("ListVideos", selectVideoByCategory);
        }
    }
}