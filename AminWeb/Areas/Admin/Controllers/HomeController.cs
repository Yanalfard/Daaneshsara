using DataLayer.MetaData;
using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private Heart _db = new Heart();

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            try
            {
                TblConfig config = _db.Config.Get().FirstOrDefault();
                MdConfig mdConfig = new MdConfig();
                mdConfig.Rules = config.Rules;
                return View(mdConfig);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Contact(MdConfig config)
        {
            TblConfig tblConfig = _db.Config.GetById(1);
            tblConfig.Rules = config.Rules;
            _db.Config.Save();
            return View(config);
        }
        public ActionResult About()
        {
            try
            {
                TblConfig config = _db.Config.Get().FirstOrDefault();
                MdConfig mdConfig = new MdConfig();
                mdConfig.AboutUsBody = config.AboutUsBody;
                return View(mdConfig);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult About(MdConfig config)
        {
            TblConfig tblConfig = _db.Config.GetById(1);
            tblConfig.AboutUsBody = config.AboutUsBody;
            _db.Config.Save();
            return View(config);
        }
    }
}