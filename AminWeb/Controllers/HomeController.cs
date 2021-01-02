using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer.Models;
using DataLayer.MetaData;
using DataLayer.Services;
using System.IO;

namespace AminWeb.Controllers
{
    public class HomeController : Controller
    {
        private Heart _db = new Heart();

        public ActionResult Index()
        {
            return View();
        }
        TblUser SelectUser()
        {
            TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Email == User.Identity.Name);
            return selectUser;
        }
        public ActionResult About()
        {
            return View(_db.Config.Get().FirstOrDefault());
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Contact(MdTicket ticket,HttpPostedFileBase AttachmentUrl)
        {
            if (ModelState.IsValid)
            {
                TblTicket addTicket = new TblTicket();
                if (AttachmentUrl != null)
                {
                    addTicket.AttachmentUrl = Guid.NewGuid().ToString() + Path.GetExtension(AttachmentUrl.FileName);
                    AttachmentUrl.SaveAs(Server.MapPath("/Resources/Ticket/" + addTicket.AttachmentUrl));
                }
                addTicket.SenderId = SelectUser().UserId;
                addTicket.DateSent = DateTime.Now;
                addTicket.Text = ticket.Text;
                addTicket.Title = ticket.Title;
                _db.Ticket.Add(addTicket);
                _db.Ticket.Save();
                return Redirect("/");
            }
            return View(ticket);
        }
        public ActionResult Rules()
        {
            return View(_db.Config.Get().FirstOrDefault());

        }
    }
}