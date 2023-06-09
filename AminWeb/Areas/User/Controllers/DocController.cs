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
    public class DocController : Controller
    {
        private Heart _db = new Heart();
        TblUser SelectUser()
        {
            TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Email == User.Identity.Name);
            return selectUser;
        }
        // GET: User/Doc
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(MdDoc upload, HttpPostedFileBase KarteMeliSahebEmtiazUrl, HttpPostedFileBase ShenasnameSahebEmtiazUrl, HttpPostedFileBase MojavezTasisUrl, HttpPostedFileBase ParvaneAmuzeshgahUrl)
        {
            if (ModelState.IsValid)
            {
                if (KarteMeliSahebEmtiazUrl != null)
                {
                    if (KarteMeliSahebEmtiazUrl.IsImage())
                    {
                        upload.KarteMeliSahebEmtiazUrl = Guid.NewGuid().ToString() + Path.GetExtension(KarteMeliSahebEmtiazUrl.FileName);
                        KarteMeliSahebEmtiazUrl.SaveAs(Server.MapPath("/Resources/Doc/" + upload.KarteMeliSahebEmtiazUrl));
                    }
                    else
                    {
                        ModelState.AddModelError("KarteMeliSahebEmtiazUrl", "عکس کارت ملی نامعتبر است");
                        return View(upload);
                    }
                }
                if (ShenasnameSahebEmtiazUrl != null)
                {
                    if (ShenasnameSahebEmtiazUrl.IsImage())
                    {
                        upload.ShenasnameSahebEmtiazUrl = Guid.NewGuid().ToString() + Path.GetExtension(ShenasnameSahebEmtiazUrl.FileName);
                        ShenasnameSahebEmtiazUrl.SaveAs(Server.MapPath("/Resources/Doc/" + upload.ShenasnameSahebEmtiazUrl));
                    }
                    else
                    {
                        ModelState.AddModelError("ShenasnameSahebEmtiazUrl", "عکس شناسنامه  نامعتبر است");
                        return View(upload);
                    }
                }
                if (MojavezTasisUrl != null)
                {
                    if (MojavezTasisUrl.IsImage())
                    {
                        upload.MojavezTasisUrl = Guid.NewGuid().ToString() + Path.GetExtension(MojavezTasisUrl.FileName);
                        MojavezTasisUrl.SaveAs(Server.MapPath("/Resources/Doc/" + upload.MojavezTasisUrl));
                    }
                    else
                    {
                        ModelState.AddModelError("MojavezTasisUrl", "عکس مجوز تاسیس  نامعتبر است");
                        return View(upload);
                    }
                }
                if (ParvaneAmuzeshgahUrl != null)
                {
                    if (ParvaneAmuzeshgahUrl.IsImage())
                    {
                        upload.ParvaneAmuzeshgahUrl = Guid.NewGuid().ToString() + Path.GetExtension(ParvaneAmuzeshgahUrl.FileName);
                        ParvaneAmuzeshgahUrl.SaveAs(Server.MapPath("/Resources/Doc/" + upload.ParvaneAmuzeshgahUrl));
                    }
                    else
                    {
                        ModelState.AddModelError("ParvaneAmuzeshgahUrl", "عکس دیگر مدارک  نامعتبر است");
                        return View(upload);
                    }
                }
                TblDoc doc = new TblDoc();
                doc.TellSabet = upload.TellSabet;
                doc.Address = upload.Address;
                doc.KarteMeliSahebEmtiazUrl = upload.KarteMeliSahebEmtiazUrl;
                doc.MojavezTasisUrl = upload.MojavezTasisUrl;
                doc.ParvaneAmuzeshgahUrl = upload.ParvaneAmuzeshgahUrl;
                doc.ShenasnameSahebEmtiazUrl = upload.ShenasnameSahebEmtiazUrl;
                _db.Docs.Add(doc);
                _db.Docs.Save();
                TblUser user = _db.User.GetById(SelectUser().UserId);
                if (user.DocsId != null)
                {
                    TblDoc selectedDocById = _db.Docs.GetById(user.DocsId);
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
                    }
                }
                user.DocsId = doc.DocId;
                _db.User.Save();
                return Redirect("Index");
            }
            return View(upload);
        }
        public ActionResult InformationUpload()
        {
            return PartialView(_db.Docs.GetById(SelectUser().DocsId));
        }
    }
}