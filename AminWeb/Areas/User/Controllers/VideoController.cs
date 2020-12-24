using AminWeb.Utilities;
using DataLayer.MetaData;
using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.User.Controllers
{
    public class VideoController : Controller
    {
        private Heart _db = new Heart();
        TblUser SelectUser()
        {
            TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Email == User.Identity.Name);
            return selectUser;
        }
        // GET: User/Video
        public ActionResult YourVideos()
        {
            return View();
        }

        public ActionResult EditVideo()
        {
            return PartialView();
        }
        public ActionResult Create()
        {
           // ViewBag.PlaylistId = new SelectList(_db.Playlist.Get(), "PlaylistId", "Title");
            ViewBag.CatagoryId = new SelectList(_db.Cat.Get(), "CatagoryId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(MdVideo video, HttpPostedFileBase MainImage, HttpPostedFileBase VideoDemoUrl, HttpPostedFileBase VideoUrl)
        {
            if (ModelState.IsValid)
            {
                if (MainImage == null && !MainImage.IsImage())
                {
                    ModelState.AddModelError("MainImage", "عکس آپلود کنید");
                    return View(video);
                }
                if (VideoUrl == null)
                {
                    ModelState.AddModelError("VideoUrl", "ویدیو آپلود کنید");
                    return View(video);
                }
                if (VideoUrl.ContentLength > 104857600)
                {
                    ModelState.AddModelError("VideoUrl", "حجم ویدیو بیشتر از 100 مگابایات است");
                    return View(video);
                }
                if (VideoDemoUrl.ContentLength > 104857600)
                {
                    ModelState.AddModelError("VideoDemoUrl", "حجم دموی ویدیو بیشتر از 100 مگابایات است");
                    return View(video);
                }
                if (VideoDemoUrl != null)
                {
                    video.VidioDemoUrl = Guid.NewGuid().ToString() + Path.GetExtension(VideoDemoUrl.FileName);
                }
                video.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                MainImage.SaveAs(Server.MapPath("/Resources/Video/Image/" + video.MainImage));
                TblVideo addVideo = new TblVideo();
                addVideo.Description = video.Description;
                addVideo.Price = video.Price;
                addVideo.IsCharity = video.IsCharity;
                addVideo.PlaylistId = video.PlaylistId;
                addVideo.IsActive = false;
                addVideo.IsHome = false;
                addVideo.PlaylistId=video.PlaylistId;
                //addVideo.CatagoryId=video.;
            }
            return PartialView(video);
        }
    

        public ActionResult ShowPlaylist(int id)
        {
            List<TblPlaylist> tblPlaylist = _db.Playlist.Get(i => i.UserId == SelectUser().UserId).ToList();
            return PartialView(tblPlaylist);
        }
    }
}
