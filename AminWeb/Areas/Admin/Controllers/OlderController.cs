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
    public class OlderController : Controller
    {
        private Heart _db = new Heart();
        // GET: Admin/Older
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Prices()
        {
            try
            {
                TblConfig config = _db.Config.Get().FirstOrDefault();
                return View(config);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult Prices(TblConfig config)
        {
            TblConfig tblConfig = _db.Config.GetById(1);
            tblConfig.DarsadeSud = config.DarsadeSud;
            tblConfig.SaqfeBardasht = config.SaqfeBardasht;
            _db.Config.Save();
            return View(config);
        }
    }
}