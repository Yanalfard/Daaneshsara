using DataLayer.MetaData;
using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class CatagoryController : Controller
    {
        private Heart _db = new Heart();
        // GET: Admin/Catagory
        public ActionResult Index()
        {
            //TblCatagory tbl = new TblCatagory();
            //tbl.ParentId = 1;
            //tbl.Name = "ss";
            //_db.Cat.Add(tbl);
            //_db.Cat.Save();
            return View();
        }

        public ActionResult ListCat(int? id)
        {
            if (id != null)
            {
                ViewBag.ParentId = id;
                List<TblCatagory> list = _db.Cat.Get().Where(i => i.ParentId == id).ToList();
                return PartialView(list);
            }
            return PartialView(_db.Cat.Get().Where(i => i.ParentId == null).ToList());
        }
        public ActionResult SubCategory(int id)
        {
            return PartialView();
        }
        public ActionResult SubCategorys(int id)
        {
            List<TblPlaylist> list = _db.Playlist.Get().Where(i => i.CatagoryId == id).ToList();
            return PartialView(list);
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(MdCategory catagory)
        {
            if (ModelState.IsValid)
            {
                if (_db.Cat.Get().Any(i => i.Name == catagory.Name))
                {
                    ModelState.AddModelError("Name", "نام گروه تکراریست");
                }
                else
                {
                    TblCatagory addCat = new TblCatagory()
                    {
                        Name = catagory.Name,
                    };
                    _db.Cat.Add(addCat);
                    _db.Cat.Save();
                    return JavaScript("doneModal()");
                }
            }
            return PartialView("Create", catagory);
        }
        public ActionResult Delete(int id)
        {
            if (_db.Cat.Get().Any(i => i.ParentId == id))
            {
                return Json(new { success = false, responseText = "خطا در حذف   لطفا اول زیر دسته هارو حذف کنید " }, JsonRequestBehavior.AllowGet);
            }
            TblCatagory deleteCat = _db.Cat.GetById(id);
            bool delete = _db.Cat.Delete(deleteCat);
            if (delete)
            {
                _db.Cat.Save();
                return Json(new { success = true, responseText = "حذف شد" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false, responseText = "خطا در حذف لطفا بررسی کنید" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Edit(int id)
        {
            TblCatagory category = _db.Cat.GetById(id);
            MdCategory mdCategory = new MdCategory()
            {
                CatagoryId = category.CatagoryId,
                Name = category.Name,
            };
            return PartialView(mdCategory);
        }
        [HttpPost]
        public ActionResult Edit(MdCategory catagory)
        {
            if (ModelState.IsValid)
            {
                if (_db.Cat.Get().Any(i => i.Name == catagory.Name && i.CatagoryId != catagory.CatagoryId))
                {
                    ModelState.AddModelError("Name", "نام گروه تکراریست");
                }
                else
                {
                    TblCatagory updateCat = new TblCatagory()
                    {
                        CatagoryId=catagory.CatagoryId,
                        Name = catagory.Name,
                    };
                    _db.Cat.Update(updateCat);
                    _db.Cat.Save();
                    return JavaScript("doneModal()");
                }
            }
            return PartialView("Create", catagory);
        }

        public ActionResult ActiveDisablePlaylist(int id)
        {
            TblPlaylist updateVideo = _db.Playlist.GetById(id);
            updateVideo.IsActive = !updateVideo.IsActive;
            _db.Playlist.Update(updateVideo);
            _db.Video.Save();
            return PartialView("SubCategorys", _db.Playlist.Get().Where(i => i.CatagoryId == updateVideo.CatagoryId).ToList());
        }

        public ActionResult DeletePlayList(int id)
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