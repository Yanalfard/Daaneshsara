using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class PlaylistController : Controller
    {
        // GET: Admin/Playlist
        public ActionResult PlaylistList()
        {
            return View();
        }

        public ActionResult ViewSinglePlaylist()
        {
            return View();
        }

        public ActionResult EditPlaylist()
        {
            return View();
        }

        public ActionResult CreatePlaylist()
        {
            return View();
        }

    }
}