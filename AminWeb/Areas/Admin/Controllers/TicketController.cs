using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class TicketController : Controller
    {
        private Heart _db = new Heart();
        // GET: Admin/Ticket
        public ActionResult Index(string name = "", string tellNo = "", string email = "", string title = "")
        {
            ViewBag.name = name;
            ViewBag.tellNo = tellNo;
            ViewBag.email = email;
            ViewBag.titl = title;
            return View();
        }
        public ActionResult ListTicket(string name = "", string tellNo = "", string email = "", string title = "")
        {

            List<TblTicket> list = new List<TblTicket>();
            list.AddRange(_db.Ticket.Get());
            if (name != "")
            {
                list = list.Where(p => p.TblUser.Name.Contains(name)).ToList();
            }
            if (tellNo != "")
            {
                list = list.Where(p => p.TblUser.TellNo.Contains(tellNo)).ToList();
            }
            if (email != "")
            {
                list = list.Where(p => p.TblUser.Email.Contains(email)).ToList();
            }
            if (title != "")
            {
                list = list.Where(p => p.Title.Contains(title)).ToList();
            }
            return PartialView(list.OrderByDescending(i => i.DateSent));
        }

        public ActionResult ViewTicket(int id)
        {
            return PartialView(_db.Ticket.GetById(id));
        }


        public ActionResult Delete(int id)
        {
            TblTicket selectUserById = _db.Ticket.GetById(id);
            bool delete = _db.Ticket.Delete(selectUserById);
            if (delete)
            {
                string fullPathLogo = Request.MapPath("/Resources/Ticket/" + selectUserById.AttachmentUrl);
                if (System.IO.File.Exists(fullPathLogo))
                {
                    System.IO.File.Delete(fullPathLogo);
                }
                _db.User.Save();
                return Json(new { success = true, responseText = " حذف شد" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "خطا در حذف   لطفا بررسی فرمایید" }, JsonRequestBehavior.AllowGet);
        }
    }
}