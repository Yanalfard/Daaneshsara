using AminWeb.Utilities;
using DataLayer.MetaData;
using DataLayer.Models;
using DataLayer.Services;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            ViewBag.Video = _db.Video.Get().Where(i => i.UserId == SelectUser().UserId && i.PlaylistId == null).ToList();
            return PartialView(_db.Playlist.Get().Where(i => i.UserId == SelectUser().UserId));
            //List<TblVideo> video = _db.Video.Get().Where(i => i.UserId == SelectUser().UserId).OrderByDescending(i=>i.PlaylistId).ToList();
            ////video = video.DistinctBy(i=>i.PlaylistId).ToList();
            //return PartialView(video);
        }
        public ActionResult Create()
        {
            ViewBag.CatagoryId = new SelectList(_db.Cat.Get(i=>i.ParentId==null), "CatagoryId", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MdPlaylist playList, HttpPostedFileBase Link, HttpPostedFileBase CertificateURL)
        {
            if (ModelState.IsValid)
            {
                if (!_db.Playlist.Get().Any(i => i.UserId == SelectUser().UserId && i.Title == playList.Title.Trim()))
                {
                    TblPlaylist addPlaylist = new TblPlaylist();
                    if (Link != null && Link.IsImage())
                    {
                        addPlaylist.Link = Guid.NewGuid().ToString() + Path.GetExtension(Link.FileName);
                        Link.SaveAs(Server.MapPath("/Resources/Classes/Link/" + addPlaylist.Link));
                    }
                    else
                    {
                        addPlaylist.Link = "NoImage.svg";
                    }
                    if (CertificateURL != null && CertificateURL.IsImage())
                    {
                        addPlaylist.CertificateURL = Guid.NewGuid().ToString() + Path.GetExtension(CertificateURL.FileName);
                        CertificateURL.SaveAs(Server.MapPath("/Resources/Classes/CertificateURL/" + addPlaylist.CertificateURL));
                    }
                    else
                    {
                        addPlaylist.CertificateURL = "NoImage.svg";
                    }
                    addPlaylist.Title = playList.Title.Trim();
                    addPlaylist.Description = playList.Description;
                    addPlaylist.IsActive = playList.IsActive;
                    addPlaylist.IsHome = playList.IsHome;
                    addPlaylist.IsCharity = playList.IsCharity;
                    addPlaylist.Price = playList.Price;
                    addPlaylist.CatagoryId = playList.CatagoryId;
                    addPlaylist.UserId = SelectUser().UserId;
                    addPlaylist.ViewCount = 0;
                    addPlaylist.DateSubmited = DateTime.Now.ToShortDateString();
                    _db.Playlist.Add(addPlaylist);
                    _db.Playlist.Save();
                    return Redirect("/User/Video/YourVideos"); ;
                }
                else
                {
                    ModelState.AddModelError("Title", "نام کلاس تکراریست");
                }
            }
            ViewBag.CatagoryId = new SelectList(_db.Cat.Get(), "CatagoryId", "Name", playList.CatagoryId);

            return View(playList);
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
            return View(playList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MdPlaylist playList, HttpPostedFileBase Link, HttpPostedFileBase CertificateURL)
        {
            if (ModelState.IsValid)
            {
                if (!_db.Playlist.Get().Any(i => i.UserId == SelectUser().UserId && i.Title == playList.Title.Trim() && i.PlaylistId != playList.PlaylistId))
                {

                    TblPlaylist UpdatePlaylist = _db.Playlist.GetById(playList.PlaylistId);
                    if (Link != null && Link.IsImage())
                    {
                        if (UpdatePlaylist.Link != null)
                        {
                            string fullPathLogo = Request.MapPath("/Resources/Classes/Link/" + UpdatePlaylist.Link);
                            if (System.IO.File.Exists(fullPathLogo))
                            {
                                System.IO.File.Delete(fullPathLogo);
                            }
                        }
                        playList.Link = Guid.NewGuid().ToString() + Path.GetExtension(Link.FileName);
                        Link.SaveAs(Server.MapPath("/Resources/Classes/Link/" + playList.Link));
                    }
                    if (CertificateURL != null && CertificateURL.IsImage())
                    {
                        if (UpdatePlaylist.CertificateURL != null)
                        {
                            string fullPathLogo = Request.MapPath("/Resources/Classes/CertificateURL/" + UpdatePlaylist.CertificateURL);
                            if (System.IO.File.Exists(fullPathLogo))
                            {
                                System.IO.File.Delete(fullPathLogo);
                            }
                        }
                        playList.CertificateURL = Guid.NewGuid().ToString() + Path.GetExtension(CertificateURL.FileName);
                        CertificateURL.SaveAs(Server.MapPath("/Resources/Classes/CertificateURL/" + playList.CertificateURL));
                    }

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
                    return Redirect("/User/Video/YourVideos");
                    //return JavaScript("showClasses()");
                }
                else
                {
                    ModelState.AddModelError("Title", "نام کلاس تکراریست");
                }
            }
            ViewBag.CatagoryId = new SelectList(_db.Cat.Get(), "CatagoryId", "Name", playList.CatagoryId);
            return View(playList);
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

        public ActionResult Delete(int id)
        {
            TblPlaylist deletePlaylist = _db.Playlist.GetById(id);
            if (deletePlaylist == null)
            {
                return Json(new { success = false, responseText = "کلاس یافت نشد " }, JsonRequestBehavior.AllowGet);
            }
            if (_db.Video.Get(i => i.PlaylistId == id).Any())
            {
                return Json(new { success = false, responseText = "کلاس مورد نظر ویدیو دارد " }, JsonRequestBehavior.AllowGet);
            }
            _db.Playlist.Delete(deletePlaylist);
            if (deletePlaylist.Link != null && deletePlaylist.Link != "NoImage.svg")
            {
                string fullPathLogo = Request.MapPath("/Resources/Classes/Link/" + deletePlaylist.Link);
                if (System.IO.File.Exists(fullPathLogo))
                {
                    System.IO.File.Delete(fullPathLogo);
                }
            }
            if (deletePlaylist.CertificateURL != null && deletePlaylist.CertificateURL != "NoImage.svg")
            {
                string fullPathLogo2 = Request.MapPath("/Resources/Classes/CertificateURL/" + deletePlaylist.CertificateURL);
                if (System.IO.File.Exists(fullPathLogo2))
                {
                    System.IO.File.Delete(fullPathLogo2);
                }
            }

            _db.Playlist.Save();
            return Json(new { success = true, responseText = "کلاس مورد نظر  حذف شد " }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ShowCategory(int? id)
        {
            if (id == null)
            {
                return null;
            }
            List<TblCatagory> playlists = _db.Cat.Get().Where(i => i.ParentId == id).ToList();
            return PartialView(playlists);
        }
    }
}