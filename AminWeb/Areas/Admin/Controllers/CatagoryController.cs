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
                List<TblCatagory> list = _db.Cat.Get().Where(i => i.ParentId == id).ToList();
                return PartialView(list);
            }
            return PartialView(_db.Cat.Get().Where(i => i.ParentId == null).ToList());
        }
        public ActionResult Create(int? id)
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(TblCatagory catagory)
        {
            _db.Cat.Add(catagory);
            _db.Cat.Save();
            return PartialView("Index", _db.Cat.Get());
        }
        //public ActionResult SubCategory(int id)
        //{
        //    retu
        //}
    }
}