﻿using DataLayer.Models;
using DataLayer.Services;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                    list.BalanceUser = SelectUser().Balance;
                    list.IsValidBalance = list.Price < list.BalanceUser ? true : false;
                    list.UserId = SelectUser().UserId;
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
        [Authorize(Roles = "user,institution,tutor")]
        public ActionResult VerifyBalance()
        {
            try
            {
                if (Session["ShopCartVideo"] != null)
                {
                    VmShopCart list = Session["ShopCartVideo"] as VmShopCart;
                    TblUser user = _db.User.GetById(list.UserId);
                    if (_db.Log.Get().Where(i => i.UserId == SelectUser().UserId && i.PlayListId == list.PlaylistId).Any())
                    {
                        return Redirect("/");
                    }
                    else if (_db.Log.Get().Where(i => i.UserId == SelectUser().UserId && i.VideoId == list.VideoId).Any())
                    {
                        return Redirect("/");
                    }
                    user.Balance -= list.Price;
                    TblLog log = new TblLog();
                    log.Amount = list.Price;
                    log.Date = DateTime.Now;
                    log.Status = 2;
                    log.UserId = list.UserId;
                    if (list.IsPlaylist)
                    {
                        log.IsVideo = false;
                        log.PlayListId = list.PlaylistId;
                    }
                    else
                    {
                        log.IsVideo = true;
                        log.VideoId = list.VideoId;
                    }
                    _db.Log.Add(log);
                    _db.User.Save();
                    Session["ShopCartVideo"] = null;
                    return View(list);
                }
                return Redirect("/");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(Roles = "user,institution,tutor")]
        public ActionResult Payment()
        {
            VmShopCart list = Session["ShopCartVideo"] as VmShopCart;
            TblLog log = new TblLog();
            log.Amount = list.Price;
            log.Date = DateTime.Now;
            log.Status = 0;
            log.UserId = list.UserId;
            if (list.IsPlaylist)
            {
                log.IsVideo = false;
                log.PlayListId = list.PlaylistId;
            }
            else
            {
                log.IsVideo = true;
                log.VideoId = list.VideoId;
            }
            _db.Log.Add(log);
            _db.User.Save();
            System.Net.ServicePointManager.Expect100Continue = false;
            ZarinPalTest.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPalTest.PaymentGatewayImplementationServicePortTypeClient();
            string Authority;

            int Status = zp.PaymentRequest("5f648351-94a0-4b6d-ab96-3eef0d58a8b5", list.Price, "نیو خرید ", "info@newkharid.com", "09339634557", ConfigurationManager.AppSettings["MyDomain"] + "/ShopCart/Verify/" + log.LogId, out Authority);
            if (Status == 100)
            {
                //Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);

                ////test
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
            }
            else
            {
                ViewBag.Error = "Error : " + Status;
            }
            //TODO : Online Payment
            return null;
        }
        public ActionResult Verify(int id)
        {
            try
            {
                TblLog log = _db.Log.GetById(id);
                if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
                {
                    if (Request.QueryString["Status"].ToString().Equals("OK"))
                    {
                        int Amount = log.Amount;
                        long RefID;
                        System.Net.ServicePointManager.Expect100Continue = false;
                        ZarinPalTest.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPalTest.PaymentGatewayImplementationServicePortTypeClient();

                        int Status = zp.PaymentVerification("a282a431-19d8-43ee-ae50-e3d056519667", Request.QueryString["Authority"].ToString(), Amount, out RefID);
                        if (Status == 100)
                        {
                            log.Status = 1;
                            ViewBag.IsSuccess = true;
                            ViewBag.RefId = RefID;
                            _db.Log.Save();
                            // Response.Write("Success!! RefId: " + RefID);
                            Session["ShopCartVideo"] = null;
                            return View();
                        }
                        else
                        {
                            ViewBag.Status = Status;
                            ViewBag.Error = "تراکنش ناموفق";
                        }
                    }
                    else
                    {
                        ViewBag.Error = Request.QueryString["Authority"].ToString();
                    }
                }
                else
                {
                    ViewBag.Error = "خطا در لود کردن صفحه";
                }
                return View();
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }

        }
        public ActionResult PurchaseResult()
        {
            return View();
        }
    }
}