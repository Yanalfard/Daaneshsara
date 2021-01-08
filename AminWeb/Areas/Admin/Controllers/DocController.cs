using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class DocController : Controller
    {
        // GET: Admin/Doc
        private Heart _db = new Heart();
        // GET: Admin/Ticket
        public ActionResult Index(string tellNo = "")
        {
            ViewBag.tellNo = tellNo;
            return View();
        }
        public ActionResult ListDoc(string tellNo = "")
        {
            List<TblDoc> list = new List<TblDoc>();
            list.AddRange(_db.Docs.Get());
            if (tellNo != "")
            {
                list = list.Where(p => p.TellSabet.Contains(tellNo)).ToList();
            }
            return PartialView(list.OrderByDescending(i => i.DocId));
        }
        public ActionResult ViewDoc(int id)
        {
            TblUser user = _db.User.Get().Where(i => i.DocsId == id).SingleOrDefault();
            if (user != null)
            {
                ViewBag.UserName = user.Name;
                ViewBag.Tell = user.TellNo;
            }
            return View(_db.Docs.GetById(id));
        }

        public ActionResult DocDone(int id, int isValid, string errorMessage)
        {
            TblDoc selectedBydoc = _db.Docs.GetById(id);
            TblUser user = _db.User.Get().Where(i => i.DocsId == id).SingleOrDefault();
            if (isValid == 2)
            {
                selectedBydoc.IsValid = isValid;
                selectedBydoc.ErrorMessage = errorMessage;
                if (user != null)
                {
                    user.RoleId = 0;
                }
            }
            else if (isValid == 1)
            {
                selectedBydoc.IsValid = isValid;
                selectedBydoc.ErrorMessage = errorMessage;
                if (user != null)
                {
                    user.RoleId = 2;
                }
            }
            _db.Docs.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            TblDoc selectedDocById = _db.Docs.GetById(id);
            bool delete = _db.Docs.Delete(selectedDocById);
            if (delete)
            {
                if (selectedDocById.KarteMeliSahebEmtiazUrl != null)
                {
                    string KarteMeli = Request.MapPath("/Resources/Doc/" + selectedDocById.KarteMeliSahebEmtiazUrl);
                    if (System.IO.File.Exists(KarteMeli))
                    {
                        System.IO.File.Delete(KarteMeli);
                    }
                }
                if (selectedDocById.MojavezTasisUrl != null)
                {
                    string Mojavez = Request.MapPath("/Resources/Doc/" + selectedDocById.MojavezTasisUrl);
                    if (System.IO.File.Exists(Mojavez))
                    {
                        System.IO.File.Delete(Mojavez);
                    }
                }
                if (selectedDocById.ParvaneAmuzeshgahUrl != null)
                {
                    string Parvane = Request.MapPath("/Resources/Doc/" + selectedDocById.ParvaneAmuzeshgahUrl);
                    if (System.IO.File.Exists(Parvane))
                    {
                        System.IO.File.Delete(Parvane);
                    }
                }
                if (selectedDocById.ShenasnameSahebEmtiazUrl != null)
                {
                    string Shenasname = Request.MapPath("/Resources/Doc/" + selectedDocById.ShenasnameSahebEmtiazUrl);
                    if (System.IO.File.Exists(Shenasname))
                    {
                        System.IO.File.Delete(Shenasname);
                    }
                }
                _db.Docs.Save();
                return Json(new { success = true, responseText = " حذف شد" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "خطا در حذف   لطفا بررسی فرمایید" }, JsonRequestBehavior.AllowGet);
        }
    }
}