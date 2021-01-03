using DataLayer.Models;
using DataLayer.Services;
using DataLayer.ViewModels;
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
        Heart _db = new Heart();


        TblUser SelectUser()
        {
            TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Email == User.Identity.Name);
            return selectUser;
        }
        [Authorize(Roles = "user,institution,tutor")]
        public ActionResult Index(int id)
        {
            try
            {
                TblVideo video = _db.Video.GetById(id);
                if (video != null)
                {
                    VmShopCart list = new VmShopCart();
                    list.VideoId = video.VideoId;
                    list.PlaylistId = video.PlaylistId;
                    list.Price = video.PlaylistId != null ? video.TblPlaylist.Price : video.Price;
                    list.IsPlaylist = video.PlaylistId != null ? true : false;
                    list.IsPlaylist = video.PlaylistId != null ? true : false;
                    list.BalanceUser = SelectUser().Balance;
                    list.IsValidBalance = list.Price < list.BalanceUser ? true : false;
                    list.IdUser = SelectUser().UserId;
                    list.UserName = SelectUser().Name;
                    Session["ShopCartVideo"] = list;
                    return View(list);
                }
                return Redirect("/");
            }
            catch (Exception)
            {
                return Redirect("/");
            }

        }
        public ActionResult PurchaseResult()
        {
            return View();
        }
    }
}