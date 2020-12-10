using DataLayer.MetaData;
using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.User.Controllers
{
    public class ClassesController : Controller
    {
        private Heart _db = new Heart();
        // GET: User/Classes
        public ActionResult Classes()
        {
            return PartialView();
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(MdPlaylist playList)
        {
            if (ModelState.IsValid)
            {
                TblPlaylist addPlaylist = new TblPlaylist();
                addPlaylist.Title = playList.Title;
                addPlaylist.Description = playList.Description;
                addPlaylist.IsActive = playList.IsActive;
                addPlaylist.IsCharity = playList.IsCharity;
                addPlaylist.Price = playList.Price;
                addPlaylist.CertificateURL = playList.CertificateURL;
                addPlaylist.Title = playList.Title;
                addPlaylist.Title = playList.Title;
                return JavaScript("");
            }
            return PartialView(playList);
        }
    }
}