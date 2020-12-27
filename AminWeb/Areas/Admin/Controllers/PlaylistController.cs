using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class PlaylistController : Controller
    {
        private Heart _db = new Heart();

        // GET: Admin/Playlist
        public ActionResult Index(string Title = "", int Price = 0, string Catagory = "", string UserName = "", int isActive = -1, int IsHome = -1, int roleId = -1)
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

        public ActionResult ListPlaylist(string Title = "", int Price = 0, string Catagory = "", string UserName = "", int isActive = -1, int IsHome = -1, int roleId = -1)
        {
            List<TblPlaylist> list = new List<TblPlaylist>();
            list.AddRange(_db.Playlist.Get());
            if (Title != "")
            {
                list = list.Where(p => p.Title.Contains(Title)).ToList();
            }
            if (Price != 0)
            {
                list = list.Where(p => p.Price == Price).ToList();
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
            return PartialView(list.OrderByDescending(i => i.DateSubmited));
        }
        public ActionResult ShowHideInHomePlaylist(int id)
        {
            TblPlaylist updatePlaylist = _db.Playlist.GetById(id);
            updatePlaylist.IsHome = !updatePlaylist.IsHome;
            _db.Playlist.Update(updatePlaylist);
            _db.Video.Save();
            return PartialView("ListPlaylist", _db.Playlist.Get());

        }
        public ActionResult ActiveDisablePlaylist(int id)
        {
            TblPlaylist updatePlaylist = _db.Playlist.GetById(id);
            updatePlaylist.IsActive = !updatePlaylist.IsActive;
            _db.Playlist.Update(updatePlaylist);
            _db.Video.Save();
            return PartialView("ListPlaylist", _db.Playlist.Get());
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
    }
}