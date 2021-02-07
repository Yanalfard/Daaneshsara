using DataLayer.MetaData;
using DataLayer.Models;
using DataLayer.Services;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private Heart _db = new Heart();
        // GET: Admin/Order
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
            TblConfig tblConfig = _db.Config.Get().Single();
            tblConfig.DarsadeSud = config.DarsadeSud;
            tblConfig.SaqfeBardasht = config.SaqfeBardasht;
            _db.Config.Save();
            return View(config);
        }
        public ActionResult Sail(string nameVideo = "", string namePlaylist = "", string user = "", string seller = "")
        {
            ViewBag.nameVideo = nameVideo;
            ViewBag.namePlaylist = namePlaylist;
            ViewBag.user = user;
            ViewBag.seller = seller;
            return View();
        }
        public ActionResult ListSail(string nameVideo = "", string namePlaylist = "", string user = "", string seller = "")
        {
            List<VmOrderVideos> list = new List<VmOrderVideos>();
            List<TblLog> log = _db.Log.Get().Where(i => (i.Status == 1 || i.Status == 2) && (i.PlayListId != null || i.IsVideo)).ToList();
            foreach (var item in log)
            {
                string userName = "";
                string sellerName = "";
                if (item.SellerId != null)
                {
                    sellerName = _db.User.GetById(Convert.ToInt32(item.SellerId)).Name;
                }
                userName = _db.User.GetById(item.UserId).Name;
                int videoid = 0;
                string videoName = "";
                string playListName = "";
                if (item.IsVideo)
                {
                    videoid = (int)item.VideoId;
                    videoName = item.TblVideo.Title;
                }
                else if (item.PlayListId != null)
                {
                    videoid = _db.Video.Get().FirstOrDefault(i => i.PlaylistId == item.PlayListId).VideoId;
                    videoName = _db.Video.Get().FirstOrDefault(i => i.PlaylistId == item.PlayListId).Title;
                    playListName = _db.Playlist.GetById(item.PlayListId).Title;
                }
                VmOrderVideos vmOrder = new VmOrderVideos();
                vmOrder.Amount = item.Amount;
                vmOrder.AmountAdmin = item.Amount - (int)item.SellerAmount;
                vmOrder.Date = item.Date;
                vmOrder.LogId = item.LogId;
                vmOrder.PlayListId = item.PlayListId;
                vmOrder.PlayListName = playListName;
                vmOrder.SellerAmount = item.SellerAmount;
                vmOrder.SellerName = sellerName;
                vmOrder.UserName = userName;
                vmOrder.SellerId = item.SellerId;
                vmOrder.Status = item.Status;
                vmOrder.UserId = item.UserId;
                vmOrder.VideoId = videoid;
                vmOrder.IsVideo = item.IsVideo;
                vmOrder.VideoOrPlaylistName = videoName;
                list.Add(vmOrder);
            }
            if (nameVideo != "")
            {
                list = list.Where(p => p.VideoOrPlaylistName.Contains(nameVideo)).ToList();
            }
            if (namePlaylist != "")
            {
                list = list.Where(p => p.PlayListName.Contains(namePlaylist)).ToList();
            }
            if (user != "")
            {
                list = list.Where(p => p.UserName.Contains(user)).ToList();
            }
            if (seller != "")
            {
                list = list.Where(p => p.SellerName.Contains(seller)).ToList();
            }
            return PartialView(list.OrderByDescending(i => i.Date));
        }
        public List<VmWallet> GetTransAction(int id)
        {
            //Sharj  Type=1
            var selectedBalance = _db.Log.Get().Where(i => i.UserId == id && i.Status == 3 && i.PlayListId == null && i.VideoId == null && i.IsVideo == false);
            //kharid video Type=2 
            var selectedLog = _db.Log.Get().Where(i => i.UserId == id && (i.Status == 1 || i.Status == 2));
            //bardasht Type=3 
            var selectedWithdraw = _db.Withdraw.Get().Where(i => i.UserId == id);
            //forosh video or Playlist Type=4
            var selectSeller = _db.Log.Get().Where(i => i.SellerId == id && (i.Status == 1 || i.Status == 2));
            List<VmWallet> list = new List<VmWallet>();
            foreach (var charge in selectedBalance)
            {
                list.Add(new VmWallet() { Date = charge.Date, Fund = Convert.ToInt32(charge.Amount), Type = 1 });
            }
            foreach (var buy in selectedLog)
            {
                // class or video

                int videoid = 0;
                string videoName = "";
                if (buy.IsVideo)
                {
                    videoid = (int)buy.VideoId;
                    videoName = buy.TblVideo.Title;
                }
                else if (buy.PlayListId != null)
                {
                    videoid = _db.Video.Get().FirstOrDefault(i => i.PlaylistId == buy.PlayListId).VideoId;
                    videoName = _db.Video.Get().FirstOrDefault(i => i.PlaylistId == buy.PlayListId).Title;
                }

                list.Add(new VmWallet() { Date = buy.Date, Fund = buy.Amount, Type = 2, IsVideo = buy.IsVideo, VideoOrPlaylistId = videoid, VideoOrPlaylistName = videoName });
            }


            foreach (var sell in selectedWithdraw)
            {
                list.Add(new VmWallet() { Date = sell.Date, Fund = sell.Value, IsDoneWithdraw = sell.IsDone, Type = 3 });
            }


            foreach (var sell in selectSeller)
            {
                // class or video
                int videoid = 0;
                string videoName = "";
                if (sell.IsVideo)
                {
                    videoid = (int)sell.VideoId;
                    videoName = sell.TblVideo.Title;
                }
                else if (sell.PlayListId != null)
                {
                    videoid = _db.Video.Get().FirstOrDefault(i => i.PlaylistId == sell.PlayListId).VideoId;
                    videoName = _db.Video.Get().FirstOrDefault(i => i.PlaylistId == sell.PlayListId).Title;
                }
                list.Add(new VmWallet() { Date = sell.Date, Fund = Convert.ToInt32(sell.SellerAmount), Type = 4, IsVideo = sell.IsVideo, VideoOrPlaylistId = videoid, VideoOrPlaylistName = videoName });
            }
            list.OrderByDescending(i => i.Date);
            return list;
        }
        public ActionResult Turnover(int id)
        {
            return View(GetTransAction(id));
        }
        public ActionResult Withdraw()
        {
            List<TblWithdraw> withdraws = _db.Withdraw.Get().ToList();
            List<MdWithdraw> list = new List<MdWithdraw>();
            foreach (var item in withdraws)
            {
                MdWithdraw withdraw = new MdWithdraw();
                withdraw.id = item.WithdrawId;
                withdraw.Balance = item.TblUser.Balance;
                withdraw.Description = item.Description;
                withdraw.UserName = item.TblUser.Name;
                withdraw.IsDone = item.IsDone;
                withdraw.UserId = item.UserId;
                withdraw.Value = item.Value;
                withdraw.CardInfo = item.CardInfo;
                withdraw.Date = item.Date;
                list.Add(withdraw);
            }
            return View(list.OrderByDescending(i => i.Date));
        }
        public ActionResult viewWithdraw(int id)
        {
            TblWithdraw withdraws = _db.Withdraw.GetById(id);
            MdWithdraw item = new MdWithdraw();
            item.id = withdraws.WithdrawId;
            item.Balance = withdraws.TblUser.Balance;
            item.Description = withdraws.Description;
            item.UserName = withdraws.TblUser.Name;
            item.IsDone = withdraws.IsDone;
            item.UserId = withdraws.UserId;
            item.Value = withdraws.Value;
            item.CardInfo = withdraws.CardInfo;
            item.Date = withdraws.Date;
            return View(item);
        }
        //        DoneWithdraw
        //DeleteWithdraw
        public ActionResult DoneWithdraw(int id)
        {
            TblWithdraw withdraws = _db.Withdraw.GetById(id);
            withdraws.IsDone = true;
            TblUser user = _db.User.GetById(withdraws.UserId);
            user.Balance -= withdraws.Value;
            _db.Withdraw.Save();
            return RedirectToAction("Withdraw");
        }

        public ActionResult DeleteWithdraw(int id)
        {
            _db.Withdraw.DeleteById(id);
            _db.Withdraw.Save();
            return RedirectToAction("Withdraw");
        }
        public ActionResult DeleteWithdrawJson(int id)
        {
            TblWithdraw selectUserById = _db.Withdraw.GetById(id);
            bool delete = _db.Withdraw.Delete(selectUserById);
            if (delete)
            {
                _db.Ticket.Save();
                return Json(new { success = true, responseText = " حذف شد" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "خطا در حذف   لطفا بررسی فرمایید" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Charging()
        {
            List<TblLog> log = _db.Log.Get().Where(i => i.Status == 3).ToList();
            List<VmLog> list = new List<VmLog>();
            foreach (var item in log)
            {
                TblUser user = _db.User.GetById(item.UserId);
                VmLog vmLog = new VmLog();
                vmLog.LogId = item.LogId;
                vmLog.UserId = item.UserId;
                vmLog.Name = user.Name;
                vmLog.Amount = item.Amount;
                vmLog.Balance = user.Balance;
                vmLog.Date = item.Date;
                vmLog.Tell = user.TellNo;
                vmLog.Email = user.Email;
                list.Add(vmLog);
            }
            return View(list);
        }

    }
}