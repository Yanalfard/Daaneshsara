using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Controllers
{
    public class PlayListController : Controller
    {
        private Heart _db = new Heart();

        // GET: PlayList
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListPlayList(int UserId)
        {
            List<TblPlaylist> selectVideoInUser = _db.Playlist.Get().Where(i => i.UserId == UserId && i.IsActive).OrderByDescending(i => i.DateSubmited).ToList();
            if (selectVideoInUser.Count != 0)
            {
                return PartialView(selectVideoInUser.First());
            }
            return PartialView(null);
        }
    }
}