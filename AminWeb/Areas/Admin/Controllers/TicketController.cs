using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class TicketController : Controller
    {
        // GET: Admin/Ticket
        public ActionResult Tickets()
        {
            return View();
        }
    }
}