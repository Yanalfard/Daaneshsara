using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class VideoController : Controller
    {
        private Heart _db = new Heart();
        // GET: Admin/Video
        public ActionResult Index(string Title = "", int Price = 0, string Catagory = "", string UserName = "", int isActive = -1, int IsHome = -1, int roleId = -1, int PlaylistId = 0, int UserId = 0)
        {
            ViewBag.Name = Title;
            ViewBag.Price = Price;
            ViewBag.Catagory = Catagory;
            ViewBag.UserName = UserName;
            ViewBag.isActive = isActive;
            ViewBag.roleId = roleId;
            ViewBag.IsHome = IsHome;
            return View();
        }
        public ActionResult ListVideo(string Title = "", int Price = 0, string Catagory = "", string UserName = "", int isActive = -1, int IsHome = -1, int roleId = -1, int PlaylistId = 0,int UserId=0)
        {
            List<TblVideo> list = new List<TblVideo>();
            list.AddRange(_db.Video.Get());
            if (Title != "")
            {
                list = list.Where(p => p.Title.Contains(Title)).ToList();
            }
            if (Price != 0)
            {
                list = list.Where(p => p.Price == Price).ToList();
            }
            if (PlaylistId != 0)
            {
                list = list.Where(p => p.PlaylistId == PlaylistId).ToList();
            }
            if (UserId != 0)
            {
                list = list.Where(p => p.UserId == UserId).ToList();
            }
            if (Catagory != "")
            {
                list = list.Where(p => p.TblCatagory.Name.Contains(Catagory)).ToList();
            }
            if (UserName != "")
            {
                list = list.Where(p => p.TblUser.Name.Contains(UserName)).ToList();
            }
            if (isActive > -1)
            {
                if (isActive == 1)
                {
                    list = list.Where(p => p.IsActive == true).ToList();
                }
                else
                {
                    list = list.Where(p => p.IsActive == false).ToList();
                }
            }
            if (IsHome > -1)
            {
                if (IsHome == 1)
                {
                    list = list.Where(p => p.IsHome == true).ToList();
                }
                else
                {
                    list = list.Where(p => p.IsHome == false).ToList();
                }
            }
            if (roleId > -1)
            {
                list = list.Where(p => p.TblUser.RoleId == roleId).ToList();
            }
            return PartialView(list.OrderByDescending(i => i.VideoId));
        }


        public ActionResult ShowHideInHomeVideo(int id)
        {
            TblVideo updateVideo = _db.Video.GetById(id);
            updateVideo.IsHome = !updateVideo.IsHome;
            _db.Video.Update(updateVideo);
            _db.Video.Save();
            return PartialView("ListVideo", _db.Video.Get().OrderByDescending(i => i.VideoId));

        }
        public ActionResult ActiveDisableVideo(int id)
        {
            TblVideo updateVideo = _db.Video.GetById(id);
            updateVideo.IsActive = !updateVideo.IsActive;
            _db.Video.Update(updateVideo);
            _db.Video.Save();
            return PartialView("ListVideo", _db.Video.Get().OrderByDescending(i => i.VideoId));
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

        public ActionResult Report(string name = "", string text = "")
        {
            ViewBag.name = name;
            ViewBag.text = text;
            return View(_db.Report.Get());
        }
        public ActionResult ListReport(string name = "", string text = "")
        {
            List<TblReport> list = new List<TblReport>();
            list.AddRange(_db.Report.Get());
            if (name != "")
            {
                list = list.Where(p => p.TblVideo.Title.Contains(name)).ToList();
            }
            if (text != "")
            {
                list = list.Where(p => p.Text.Contains(text)).ToList();
            }
            return PartialView(list.OrderByDescending(i => i.DateSent));
        }
  
        public ActionResult ViewVideoReport(int id)
        {
            return PartialView(_db.Report.GetById(id));
        }
        public ActionResult DeleteVideoReport(int id)
        {
            TblReport selectUserById = _db.Report.GetById(id);
            bool delete = _db.Report.Delete(selectUserById);
            if (delete)
            {
                _db.Report.Save();
                return Json(new { success = true, responseText = " حذف شد" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "خطا در حذف   لطفا بررسی فرمایید" }, JsonRequestBehavior.AllowGet);
        }
    }
}