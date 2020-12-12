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
    public class ClassesController : Controller
    {
        private Heart _db = new Heart();
        // GET: User/Classes
        public ActionResult Classes()
        {
            return PartialView();
        }
        TblUser SelectUser()
        {
            TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Email == User.Identity.Name);
            return selectUser;
        }

        public ActionResult ListClasses()
        {
            return PartialView(_db.Playlist.Get());
        }
        public ActionResult Create()
        {
            ViewBag.CatagoryId = new SelectList(_db.Cat.Get(), "CatagoryId", "Name");
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(MdPlaylist playList)
        {
            if (ModelState.IsValid)
            {
                if (!_db.Playlist.Get().Any(i => i.UserId == SelectUser().UserId && i.Title == playList.Title.Trim()))
                {
                    TblPlaylist addPlaylist = new TblPlaylist();
                    addPlaylist.Title = playList.Title.Trim();
                    addPlaylist.Description = playList.Description;
                    addPlaylist.IsActive = playList.IsActive;
                    addPlaylist.IsHome = playList.IsHome;
                    addPlaylist.Link = playList.Link;
                    addPlaylist.IsCharity = playList.IsCharity;
                    addPlaylist.Price = playList.Price;
                    addPlaylist.CertificateURL = playList.CertificateURL;
                    addPlaylist.CatagoryId = playList.CatagoryId;
                    addPlaylist.UserId = SelectUser().UserId;
                    addPlaylist.ViewCount = 0;
                    addPlaylist.DateSubmited = DateTime.Now.ToShortDateString();
                    _db.Playlist.Add(addPlaylist);
                    _db.Playlist.Save();
                    // return JavaScript("doUpload()");
                    var isAjax = this.Request.IsAjaxRequest();
                    return Json(new { result = "ok", Id = addPlaylist.PlaylistId }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.AddModelError("Title", "نام کلاس تکراریست");
                }
            }
            ViewBag.CatagoryId = new SelectList(_db.Cat.Get(), "CatagoryId", "Name", playList.CatagoryId);
            return PartialView("Create", playList);
        }
        public ActionResult Edit(int id)
        {
            TblPlaylist selectPlayList = _db.Playlist.GetById(id);
            if (selectPlayList == null)
            {
                return View();
            }
            MdPlaylist playList = new MdPlaylist();
            playList.PlaylistId = selectPlayList.PlaylistId;
            playList.Title = selectPlayList.Title;
            playList.Description = selectPlayList.Description;
            playList.IsActive = selectPlayList.IsActive;
            playList.IsHome = selectPlayList.IsHome;
            playList.Link = selectPlayList.Link;
            playList.IsCharity = selectPlayList.IsCharity;
            playList.Price = selectPlayList.Price;
            playList.CertificateURL = selectPlayList.CertificateURL;
            playList.UserId = selectPlayList.UserId;
            playList.ViewCount = selectPlayList.ViewCount;
            playList.DateSubmited = selectPlayList.DateSubmited;
            playList.CatagoryId = selectPlayList.CatagoryId;
            ViewBag.CatagoryId = new SelectList(_db.Cat.Get(), "CatagoryId", "Name", selectPlayList.CatagoryId);
            return PartialView(playList);
        }
        [HttpPost]
        public ActionResult Edit(MdPlaylist playList)
        {
            if (ModelState.IsValid)
            {
                if (!_db.Playlist.Get().Any(i => i.UserId == SelectUser().UserId && i.Title == playList.Title.Trim() && i.PlaylistId != playList.PlaylistId))
                {
                    TblPlaylist UpdatePlaylist = _db.Playlist.GetById(playList.PlaylistId);
                    UpdatePlaylist.Title = playList.Title.Trim();
                    UpdatePlaylist.Description = playList.Description;
                    UpdatePlaylist.IsActive = playList.IsActive;
                    UpdatePlaylist.IsHome = playList.IsHome;
                    UpdatePlaylist.Link = playList.Link;
                    UpdatePlaylist.IsCharity = playList.IsCharity;
                    UpdatePlaylist.Price = playList.Price;
                    UpdatePlaylist.CertificateURL = playList.CertificateURL;
                    UpdatePlaylist.CatagoryId = playList.CatagoryId;
                    UpdatePlaylist.UserId = playList.UserId;
                    UpdatePlaylist.ViewCount = playList.ViewCount;
                    UpdatePlaylist.DateSubmited = playList.DateSubmited;
                    _db.Playlist.Update(UpdatePlaylist);
                    _db.Playlist.Save();
                    return JavaScript("showClasses()");
                }
                else
                {
                    ModelState.AddModelError("Title", "نام کلاس تکراریست");
                }
            }
            ViewBag.CatagoryId = new SelectList(_db.Cat.Get(), "CatagoryId", "Name", playList.CatagoryId);
            return PartialView(playList);
        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase Certificate, int id)
        {
            TblPlaylist UpdatePlaylist = _db.Playlist.GetById(id);
            UpdatePlaylist.CertificateURL = Guid.NewGuid().ToString() + Path.GetExtension(Certificate.FileName);
            Certificate.SaveAs(Server.MapPath("/Resources/Classes/" + UpdatePlaylist.CertificateURL));
            //return JavaScript("showClasses();");
            return Json(new { FileName = "/Uploads/filename.ext" }, "text/html", JsonRequestBehavior.AllowGet);

        }
    }
}