using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.User.Controllers
{
    public class ChatController : Controller
    {
        // GET: User/Chat
        public ActionResult Index()
        {
            return View();
        }
    }
}