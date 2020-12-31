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
        public ActionResult VideoView(int id, string name)
        {
            ViewBag.Name = name;
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
            TblCatagory selectCatById = _db.Cat.Get().FirstOrDefault(i => i.Name == name);
            List<TblVideo> selectVideoByCategory = new List<TblVideo>();
            if (selectCatById != null)
            {
                selectVideoByCategory = _db.Video.Get(i => i.CatagoryId == selectCatById.CatagoryId && i.IsActive).ToList();
            }
            else
            {
                selectVideoByCategory = _db.Video.Get(i => i.CatagoryId == selectCatById.CatagoryId && i.IsActive).ToList();
            }
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






        TblUser SelectUser()
        {
            TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Email == User.Identity.Name);
            return selectUser;
        }
        public ActionResult CheckUserSaveVideo(int id)
        {
            TblUserVideoRel videoRel = _db.UserVideoRel.Get().FirstOrDefault(i => i.VideoId == id && i.UserId == SelectUser().UserId);
            ViewBag.VideoId = id;
            ViewBag.IsBookMark = false;
            if (videoRel != null)
            {
                ViewBag.IsBookMark = true;
            }
            return PartialView();
        }
        [Authorize]
        public ActionResult CheckbookmarkVideoInUser(int id)
        {
            TblUserVideoRel videoRel = _db.UserVideoRel.Get().FirstOrDefault(i => i.VideoId == id && i.UserId == SelectUser().UserId);
            ViewBag.VideoId = id;
            ViewBag.IsBookMark = false;
            if (videoRel != null && videoRel.IsBookMark)
            {
                _db.UserVideoRel.Delete(videoRel);
                ViewBag.Error = "ویدیو از کتاب خانه شما حذف شد";
            }
            else
            {
                _db.UserVideoRel.Add(new TblUserVideoRel()
                {
                    VideoId = id,
                    UserId = SelectUser().UserId,
                    IsBookMark = true,
                    Date = DateTime.Now
                });
                ViewBag.IsBookMark = true;
                ViewBag.Error = "ویدیو در کتاب خانه شما ذخیره شد";
            }
            _db.UserVideoRel.Save();
            return PartialView("CheckUserSaveVideo", id);
        }

        [HttpPost]
        public ActionResult Report(int id, string Text, string reportRadio)
        {
            _db.Report.Add(new TblReport()
            {
                DateSent = DateTime.Now,
                Text = reportRadio + " توضیحات: " + Text,
                VideoId = id,
            });
            _db.Report.Save();
            return JavaScript("");
        }
        public ActionResult LikeCount(int LikeCount)
        {
            ViewBag.LikeCount = LikeCount / 20;
            return PartialView();
        }
        [HttpPost]
        public ActionResult Raiting(int id, int Raiting)
        {
            int ConverRating = Convert.ToInt32(Raiting) * 20;
            TblVideo product = _db.Video.GetById(id);
            if (product.LikeCount == 0)
            {
                product.LikeCount = ConverRating;
            }
            else
            {
                product.LikeCount = (ConverRating + product.LikeCount) / 2;
            }
            _db.Report.Save();
            int LikeCount = product.LikeCount;
            ViewBag.LikeCount = product.LikeCount / 20;
            return PartialView("LikeCount", LikeCount);
        }

    }
}