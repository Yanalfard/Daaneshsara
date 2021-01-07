using DataLayer.Models;
using DataLayer.Services;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.User.Controllers
{
    public class WalletController : Controller
    {
        private Heart _db = new Heart();
        TblUser SelectUser()
        {
            TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Email == User.Identity.Name);
            return selectUser;
        }
        // GET: User/Wallet
        public ActionResult Index()
        {
            return View();
        }
        public int Balance()
        {
            return SelectUser().Balance;
        }
        public List<VmWallet> GetTransAction()
        {
            //Sharj  Type=1
            var selectedBalance = _db.Log.Get().Where(i => i.UserId == SelectUser().UserId && i.Status == 3 && i.PlayListId == null && i.VideoId == null && i.IsVideo == false);
            //kharid video Type=2 
            var selectedLog = _db.Log.Get().Where(i => i.UserId == SelectUser().UserId && (i.Status == 1 || i.Status == 2));
            //bardasht Type=3 
            var selectedWithdraw = _db.Withdraw.Get().Where(i => i.UserId == SelectUser().UserId);
            //forosh video or Playlist Type=4
            var selectSeller = _db.Log.Get().Where(i => i.SellerId == SelectUser().UserId && (i.Status == 1 || i.Status == 2));
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

        public ActionResult Transaction()
        {
            return PartialView(GetTransAction());
        }

        public string Withdraw(string cardWithdraw, string priceWithdraw)
        {
            TblWithdraw withdraw = new TblWithdraw();
            if (SelectUser().Balance < Convert.ToInt32(priceWithdraw))
            {
                return "مبلغ مورد نظر بیشتر از کیف پول شماست";
            }
            withdraw.CardInfo = cardWithdraw.ToString();
            withdraw.IsDone = false;
            withdraw.Value = Convert.ToInt32(priceWithdraw);
            withdraw.Date = DateTime.Now;
            withdraw.UserId = SelectUser().UserId;
            _db.Withdraw.Add(withdraw);
            _db.Withdraw.Save();
            return "درخواست شما ثبت شد و بعد از بررسی به کارت شما واریز خواهد شد";
        }

    }
}