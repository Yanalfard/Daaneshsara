using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Controllers
{
    public class ShopCartController : Controller
    {
        // GET: ShopCart
        public ActionResult Purchase()
        {
            return View();
        }

        public ActionResult PurchaseResult()
        {
            return View();
        }
    }
}