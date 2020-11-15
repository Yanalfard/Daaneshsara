using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class ChatController : Controller
    {
        // GET: Admin/Chat
        public ActionResult ChatList()
        {
            return View();
        }

        public ActionResult ChatWithUser()
        {
            return View();
        }

    }
}