using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Controllers
{
    public class ManageEmailsController : Controller
    {
        // GET: ManageEmails
        public ActionResult ActiviationEmail()
        {
            try
            {
                return PartialView();
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }
        public ActionResult RecoveryPassword()
        {
            try
            {
                return PartialView();
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }
    }
}