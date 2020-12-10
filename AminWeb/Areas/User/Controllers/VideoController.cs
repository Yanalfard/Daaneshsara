using DataLayer.MetaData;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.User.Controllers
{
    public class VideoController : Controller
    {
        private Heart _db = new Heart();

        // GET: User/Video
        public ActionResult YourVideos()
        {
            return View();
        }

        public ActionResult UploadVideo()
        {
            return PartialView();
        }

        public ActionResult EditVideo()
        {
            return PartialView();
        }
        public ActionResult Create()
        {
            ViewBag.PlaylistId = new SelectList(_db.Playlist.Get(), "PlaylistId", "Title");
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(MdVideo video)
        {
            if (ModelState.IsValid)
            {

            }
            return PartialView(video);
        }
    }
}
