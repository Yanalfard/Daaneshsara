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
        public ActionResult Create(int? id)
        {
            return PartialView(new MdCategory()
            {
                ParentId = id,
            });
        }
        [HttpPost]
        public ActionResult Create(MdCategory catagory)
        {
            if (ModelState.IsValid)
            {
                TblCatagory addCat = new TblCatagory()
                {
                    Name=catagory.Name,
                    ParentId= catagory.ParentId,
                };
                _db.Cat.Add(addCat);
                _db.Cat.Save();
                return PartialView("ListCat", _db.Cat.Get().Where(i => i.ParentId == null).ToList());
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
            return PartialView(_db.Cat.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(TblCatagory catagory)
        {
            _db.Cat.Update(catagory);
            _db.Cat.Save();
            return PartialView("ListCat", _db.Cat.Get().Where(i => i.ParentId == null).ToList());
        }
    }
}