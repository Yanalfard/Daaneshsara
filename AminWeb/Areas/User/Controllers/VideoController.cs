using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.User.Controllers
{
    public class VideoController : Controller
    {
        // GET: User/Video
        public ActionResult YourVideos()
        {
            return View();
        }

        public ActionResult UploadVideo()
        {
            //Modal
            return PartialView();
        }

        public ActionResult EditVideo()
        {
            //Modal
            return PartialView();
        }
    }
}