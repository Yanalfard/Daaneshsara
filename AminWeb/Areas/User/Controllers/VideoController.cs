﻿using AminWeb.Utilities;
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
            return View(_db.Video.Get().Where(i => i.UserId == SelectUser().UserId));
        }

        public ActionResult EditVideo()
        {
            return PartialView();
        }
        public ActionResult Create()
        {
            ViewBag.Catagory = new SelectList(_db.Cat.Get(i => i.ParentId == null), "CatagoryId", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Create(MdVideo video, HttpPostedFileBase MainImage, HttpPostedFileBase VideoDemoUrl, HttpPostedFileBase VideoUrl)
        {
            ViewBag.Catagory = new SelectList(_db.Cat.Get(i => i.ParentId == null), "CatagoryId", "Name", video.CatagoryId);

            if (ModelState.IsValid)
            {
                //ViewBag.PlaylistId = new SelectList(_db.Playlist.Get().Where(i => i.UserId == SelectUser().UserId), "PlaylistId", "Title", video.PlaylistId);
                if (MainImage == null)
                {
                    ModelState.AddModelError("MainImage", "عکس آپلود کنید");
                    return View(video);
                }
                else if (MainImage != null && MainImage.ContentLength > 10485760)
                {
                    ModelState.AddModelError("MainImage", "حجم عکس بیشتر از 10 مگابایات است");
                    return View(video);
                }
                else if (!MainImage.IsImage())
                {
                    ModelState.AddModelError("MainImage", "عکس نامعتبر است");
                    return View(video);
                }
                else if (MainImage != null && MainImage.IsImage())
                {
                    video.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    MainImage.SaveAs(Server.MapPath("/Resources/Video/Image/" + video.MainImage));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Resources/Video/Image/" + video.MainImage),
                        Server.MapPath("/Resources/Video/Image/Thumb/" + video.MainImage));
                }
                string PasVideoUrl = "";
                string PasVideoDemoUrl = "";
                if (VideoUrl != null)
                {
                    PasVideoUrl = Path.GetExtension(VideoUrl.FileName).ToLower();
                }
                if (VideoDemoUrl != null)
                {
                    PasVideoDemoUrl = Path.GetExtension(VideoDemoUrl.FileName).ToLower();
                }

                if (VideoUrl == null)
                {
                    ModelState.AddModelError("VideoUrl", "ویدیو آپلود کنید");
                    return View(video);
                }
                else if (VideoUrl != null && VideoUrl.ContentLength > 104857600)
                {
                    ModelState.AddModelError("VideoUrl", "حجم ویدیو بیشتر از 100 مگابایات است");
                    return View(video);
                }
                else if (VideoUrl != null)
                {
                    if (PasVideoUrl == ".mkv" || PasVideoUrl == ".mp4")
                    {
                        video.VideoUrl = Guid.NewGuid().ToString() + Path.GetExtension(VideoUrl.FileName);
                        VideoUrl.SaveAs(Server.MapPath("/Resources/Video/VideoUrl/" + video.VideoUrl));
                    }
                    else
                    {
                        ModelState.AddModelError("VideoUrl", "ویدیو نامعتبر است");
                        return View(video);
                    }

                }
                if (VideoDemoUrl != null && VideoDemoUrl.ContentLength > 104857600)
                {
                    ModelState.AddModelError("VideoDemoUrl", "حجم دموی ویدیو بیشتر از 100 مگابایات است");
                    return View(video);
                }
                else if (VideoDemoUrl != null)
                {
                    if (PasVideoDemoUrl == ".mkv" || PasVideoDemoUrl == ".mp4")
                    {
                        video.VideoDemoUrl = Guid.NewGuid().ToString() + Path.GetExtension(VideoDemoUrl.FileName);
                        VideoDemoUrl.SaveAs(Server.MapPath("/Resources/Video/VideoDemo/" + video.VideoDemoUrl));
                    }
                    else
                    {
                        ModelState.AddModelError("VideoDemoUrl", "دموی ویدیو نامعتبر است");
                        return View(video);
                    }

                }

                TblVideo addVideo = new TblVideo();
                addVideo.Title = video.Title;
                addVideo.Description = video.Description;
                addVideo.Price = video.Price;
                addVideo.IsCharity = video.IsCharity;
                addVideo.PlaylistId = video.PlaylistId;
                addVideo.IsActive = false;
                addVideo.IsHome = false;
                addVideo.CatagoryId = video.CatagoryId;
                addVideo.MainImage = video.MainImage;
                addVideo.Link = video.MainImage;
                addVideo.VideoUrl = video.VideoUrl;
                addVideo.VidioDemoUrl = video.VideoDemoUrl;
                addVideo.ViewCount = 0;
                addVideo.LikeCount = 0;
                addVideo.UserId = SelectUser().UserId;
                addVideo.DateSubmited = DateTime.Now.ToShortDateString();
                _db.Video.Add(addVideo);
                _db.Video.Save();
                string keywords = RemoveParameters(video.Keywords);
                foreach (var item in keywords.Split('،'))
                {
                    TblVideoPlaylistKeyword tblVideo = new TblVideoPlaylistKeyword();
                    tblVideo.Name = item;
                    tblVideo.VideoId = addVideo.VideoId;
                    _db.VideoPlaylistKeyword.Add(tblVideo);
                }
                _db.VideoPlaylistKeyword.Save();
                return Redirect("/User/Video/YourVideos");
            }

            return PartialView(video);
        }


        public ActionResult Edit(int id)
        {
            TblVideo selectVideoById = _db.Video.GetById(id);
            MdVideo editVideo = new MdVideo();
            editVideo.id = selectVideoById.VideoId;
            editVideo.Title = selectVideoById.Title;
            editVideo.Description = selectVideoById.Description;
            editVideo.Price = selectVideoById.Price;
            editVideo.IsCharity = selectVideoById.IsCharity;
            editVideo.PlaylistId = selectVideoById.PlaylistId;
            editVideo.CatagoryId = selectVideoById.CatagoryId;
            editVideo.MainImage = selectVideoById.MainImage;
            editVideo.VideoUrl = selectVideoById.VideoUrl;
            editVideo.VideoDemoUrl = selectVideoById.VidioDemoUrl;
            List<TblVideoPlaylistKeyword> selectKeywords = _db.VideoPlaylistKeyword.Get().Where(i => i.VideoId == id).ToList();
            editVideo.Keywords = string.Join("،", selectKeywords.Select(t => t.Name));
            TblCatagory selectCatagory = _db.Cat.GetById(selectVideoById.CatagoryId);
            int catagory = 0;
            if (selectCatagory != null)
            {
                if (selectCatagory.ParentId == null)
                {
                    catagory = selectCatagory.CatagoryId;
                }
                else
                {
                    catagory = Convert.ToInt32(selectCatagory.ParentId);
                }
            }
            ViewBag.Catagory = new SelectList(_db.Cat.Get(i => i.ParentId == null), "CatagoryId", "Name", catagory);
            return View(editVideo);
        }

        [HttpPost]
        public ActionResult Edit(MdVideo video, HttpPostedFileBase MainImage, HttpPostedFileBase VideoDemoUrl, HttpPostedFileBase VideoUrl)
        {
            ViewBag.Catagory = new SelectList(_db.Cat.Get(), "CatagoryId", "Name", video.CatagoryId);
            if (ModelState.IsValid)
            {
                if (MainImage != null && MainImage.ContentLength > 10485760)
                {
                    ModelState.AddModelError("MainImage", "حجم عکس بیشتر از 10 مگابایات است");
                    return View(video);
                }
                else if (MainImage != null && !MainImage.IsImage())
                {
                    ModelState.AddModelError("MainImage", " عکس نامعتبر است");
                    return View(video);
                }
                else if (MainImage != null && MainImage.IsImage())
                {
                    string fullPathLogo = Request.MapPath("/Resources/Video/Image/" + video.MainImage);
                    if (System.IO.File.Exists(fullPathLogo))
                    {
                        System.IO.File.Delete(fullPathLogo);
                    }
                    string fullPathLogoMiniSize = Request.MapPath("/Resources/Video/Image/Thumb/" + video.MainImage);
                    if (System.IO.File.Exists(fullPathLogoMiniSize))
                    {
                        System.IO.File.Delete(fullPathLogoMiniSize);
                    }
                    video.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    MainImage.SaveAs(Server.MapPath("/Resources/Video/Image/" + video.MainImage));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Resources/Video/Image/" + video.MainImage),
                        Server.MapPath("/Resources/Video/Image/Thumb/" + video.MainImage));
                }

                string PasVideoUrl = "";
                string PasVideoDemoUrl = "";
                if (VideoUrl != null)
                {
                    PasVideoUrl = Path.GetExtension(VideoUrl.FileName).ToLower();
                }
                if (VideoDemoUrl != null)
                {
                    PasVideoDemoUrl = Path.GetExtension(VideoDemoUrl.FileName).ToLower();
                }

                if (VideoUrl != null && VideoUrl.ContentLength > 104857600)
                {
                    ModelState.AddModelError("VideoUrl", "حجم ویدیو بیشتر از 100 مگابایات است");
                    return View(video);
                }
                else if (VideoUrl != null)
                {
                    if (PasVideoUrl == ".mkv" || PasVideoUrl == ".mp4")
                    {
                        string fullPathLogo = Request.MapPath("/Resources/Video/VideoUrl/" + video.VideoUrl);
                        if (System.IO.File.Exists(fullPathLogo))
                        {
                            System.IO.File.Delete(fullPathLogo);
                        }
                        video.VideoUrl = Guid.NewGuid().ToString() + Path.GetExtension(VideoUrl.FileName);
                        VideoUrl.SaveAs(Server.MapPath("/Resources/Video/VideoUrl/" + video.VideoUrl));
                    }
                    else
                    {
                        ModelState.AddModelError("VideoUrl", "ویدیو نامعتبر است");
                        return View(video);
                    }

                }
                if (VideoDemoUrl != null && VideoDemoUrl.ContentLength > 104857600)
                {
                    ModelState.AddModelError("VideoDemoUrl", "حجم دموی ویدیو بیشتر از 100 مگابایات است");
                    return View(video);
                }
                else if (VideoDemoUrl != null)
                {
                    if (PasVideoDemoUrl == ".mkv" || PasVideoDemoUrl == ".mp4")
                    {
                        string fullPathLogo = Request.MapPath("/Resources/Video/VideoDemo/" + video.VideoDemoUrl);
                        if (System.IO.File.Exists(fullPathLogo))
                        {
                            System.IO.File.Delete(fullPathLogo);
                        }
                        video.VideoDemoUrl = Guid.NewGuid().ToString() + Path.GetExtension(VideoDemoUrl.FileName);
                        VideoDemoUrl.SaveAs(Server.MapPath("/Resources/Video/VideoDemo/" + video.VideoDemoUrl));
                    }
                    else
                    {
                        ModelState.AddModelError("VideoDemoUrl", "دموی ویدیو نامعتبر است");
                        return View(video);
                    }

                }


                TblVideo updateVideoById = _db.Video.GetById(video.id);
                updateVideoById.Title = video.Title;
                updateVideoById.Description = video.Description;
                updateVideoById.Price = video.Price;
                updateVideoById.IsCharity = video.IsCharity;
                updateVideoById.PlaylistId = video.PlaylistId;
                updateVideoById.CatagoryId = video.CatagoryId;
                updateVideoById.MainImage = video.MainImage;
                updateVideoById.VideoUrl = video.VideoUrl;
                updateVideoById.VidioDemoUrl = video.VideoDemoUrl;
                _db.Video.Update(updateVideoById);
                _db.Video.Save();
                _db.VideoPlaylistKeyword.Get().Where(t => t.VideoId == video.id).ToList().ForEach(t => _db.VideoPlaylistKeyword.Delete(t));

                string keywords = RemoveParameters(video.Keywords);
                foreach (var item in keywords.Split('،'))
                {
                    TblVideoPlaylistKeyword tblVideo = new TblVideoPlaylistKeyword();
                    tblVideo.Name = item;
                    tblVideo.VideoId = updateVideoById.VideoId;
                    _db.VideoPlaylistKeyword.Add(tblVideo);
                }
                _db.VideoPlaylistKeyword.Save();
                return Redirect("/User/Video/YourVideos");
            }
            return PartialView(video);
        }

        public string RemoveParameters(string keywords)
        {
            if (keywords[keywords.Length - 1] == '،')
            {
                keywords = keywords.Remove(keywords.Length - 1);
                return RemoveParameters(keywords);
            }
            return keywords;
        }
        public ActionResult Delete(int id)
        {
            TblVideo deletevideo = _db.Video.GetById(id);
            if (deletevideo == null)
            {
                return Json(new { success = false, responseText = "ویدیو یافت نشد " }, JsonRequestBehavior.AllowGet);
            }
            if (deletevideo.MainImage != null)
            {
                string fullPathLogo = Request.MapPath("/Resources/Video/Image/" + deletevideo.MainImage);
                if (System.IO.File.Exists(fullPathLogo))
                {
                    System.IO.File.Delete(fullPathLogo);
                }
            }
            if (deletevideo.VideoUrl != null)
            {
                string fullPathLogo = Request.MapPath("/Resources/Video/VideoUrl/" + deletevideo.VideoUrl);
                if (System.IO.File.Exists(fullPathLogo))
                {
                    System.IO.File.Delete(fullPathLogo);
                }
            }
            if (deletevideo.VidioDemoUrl != null)
            {
                string fullPathLogo = Request.MapPath("/Resources/Video/VideoDemo/" + deletevideo.VidioDemoUrl);
                if (System.IO.File.Exists(fullPathLogo))
                {
                    System.IO.File.Delete(fullPathLogo);
                }
            }
            _db.VideoPlaylistKeyword.Get().Where(t => t.VideoId == id).ToList().ForEach(t => _db.VideoPlaylistKeyword.Delete(t));
            _db.Video.Delete(deletevideo);
            _db.Video.Save();
            return Json(new { success = true, responseText = "ویدیو مورد نظر  حذف شد " }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ShowPlaylist(int? id)
        {
            if (id == null)
            {
                return null;
            }
            ViewBag.PlaylistId = new SelectList(_db.Playlist.Get().Where(i => i.UserId == SelectUser().UserId && i.CatagoryId == id), "PlaylistId", "Title");
            List<TblPlaylist> playlists = _db.Playlist.Get().Where(i => i.UserId == SelectUser().UserId && i.CatagoryId == id).ToList();
            return PartialView(playlists);
        }
    }
}
