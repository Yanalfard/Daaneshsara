﻿using AminWeb.Utilities;
using DataLayer.Models;
using DataLayer.Services;
using DataLayer.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AminWeb.Controllers
{
    public class AccountController : Controller
    {
        private Heart _db = new Heart();
        // GET: Account
        //[Authorize(Order = 0, Roles = "*", Users = "*")]
        //[ChildActionOnly]
        private async Task<bool> IsCaptchaValid(string response)
        {
            try
            {
                var secret = "6Lc-wi4aAAAAAIIdKUypm-CnTOJO18FDbwmyAb_2";
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        {"secret", secret},
                        {"response", response},
                        {"remoteip", Request.UserHostAddress}
                    };

                    var content = new FormUrlEncodedContent(values);
                    var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);

                    //return verify.IsSuccessStatusCode;

                    var captchaResponseJson = await verify.Content.ReadAsStringAsync();
                    var captchaResult = JsonConvert.DeserializeObject<CaptchaResponseViewModel>(captchaResponseJson);
                    return captchaResult.Success
                           && captchaResult.Action == "contact_us"
                           && captchaResult.Score > 0.5;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginModal(string ReturnUrl = "/")
        {
            if (ReturnUrl != "/")
            {
                return PartialView();
            }
            return null;
        }
        public ActionResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<ActionResult> Login(VmLogin user, string GoogleCapcha, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var isCaptchaValid = await IsCaptchaValid(GoogleCapcha);
                if (isCaptchaValid)
                {
                    user.Email = user.Email.Trim().ToLower().Replace(" ", "");
                    string hashPass = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "SHA256");
                    TblUser checkUser = _db.User.Get().FirstOrDefault(i => i.Email == user.Email && i.Password == hashPass);
                    if (checkUser != null)
                    {
                        if (checkUser.IsActive)
                        {
                            FormsAuthentication.SetAuthCookie(user.Email, user.RememberMe);
                            return Redirect("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "کاربری یافت نشد");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "ورود غیر مجاز");
                }
            }
            return PartialView("Login", user);
        }
        [Route("LogOff")]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
        public ActionResult Register()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<ActionResult> Register(VmRegister user, string GoogleCapcha)
        {
            if (ModelState.IsValid)
            {
                var isCaptchaValid = await IsCaptchaValid(GoogleCapcha);
                if (isCaptchaValid)
                {
                    user.Email = user.Email.Trim().ToLower().Replace(" ", "");
                    if (_db.User.Get().Any(i => i.Email == user.Email))
                    {
                        ModelState.AddModelError("Email", "ایمیل تکراریست");
                    }
                    else
                    {
                        TblUser addUser = new TblUser();
                        addUser.Balance = 0;
                        addUser.Auth = Guid.NewGuid().ToString();
                        addUser.IsActive = false;
                        addUser.Email = user.Email;
                        addUser.Name = user.Name;
                        addUser.RoleId = 0;
                        addUser.TellNo = "0";
                        addUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "SHA256");
                        _db.User.Add(addUser);
                        _db.User.Save();
                        string body = PartialToStringClass.RenderPartialView("ManageEmails", "ActiviationEmail", addUser);
                        SendEmail.Send(user.Email, "ایمیل فعالسازی", body);
                        return JavaScript("UIkit.modal(document.getElementById('Modal-Show')).hide();UIkit.notification('لینک فعال سازی به ایمیل شما ارسال شد')");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "ثبت نام غیر مجاز");
                }
            }
            return PartialView("Register", user);
        }
        [Route("ActiveUser/{id}")]
        public ActionResult ActiveUser()
        {
            try
            {
                return View();
            }
            catch
            {
                return Redirect("/fallback.html");
            }
        }
        [HttpPost]
        [Route("ActiveUser/{id}")]
        public ActionResult ActiveUser(VmActive check)
        {
            try
            {
                TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Auth == check.id);
                if (selectUser != null)
                {
                    selectUser.IsActive = true;
                    selectUser.Auth = Guid.NewGuid().ToString();
                    _db.User.Save();
                    return Redirect("/?ReturnUrl=Active");
                }
                else
                {
                    ModelState.AddModelError("id", "حساب کاربری شما یافت نشد");
                    return View(check);
                    //return JavaScript("UIkit.notification('حساب کاربری شما یافت نشد');");
                }
            }
            catch
            {
                return Redirect("/fallback.html");
            }
        }
        public ActionResult ForgetPassword()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ForgetPassword(VmForgetPassword user)
        {
            if (ModelState.IsValid)
            {
                TblUser selectUser = _db.User.Get(i => i.Email == user.Email).FirstOrDefault();
                if (selectUser != null && selectUser.Email == user.Email)
                {
                    if (selectUser.Email == user.Email && selectUser.IsActive)
                    {
                        string body = PartialToStringClass.RenderPartialView("ManageEmails", "RecoveryPassword", selectUser);
                        SendEmail.Send(user.Email, "بازیابی کلمه عبور", body);
                        return JavaScript("UIkit.modal(document.getElementById('Modal-Show')).hide();UIkit.notification('ایمیلی حاوی لینک بازیابی کلمه عبور به ایمیل شما ارسال شد')");
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری یافت نشد");
                }
            }
            return PartialView("ForgetPassword", user);
        }
        [Route("RecoveryPassword/{id}")]
        public ActionResult RecoveryPassword(string id)
        {
            return View();
        }
        [HttpPost]
        [Route("RecoveryPassword/{id}")]
        public ActionResult RecoveryPassword(string id, VmRecoveryPassword recovery)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _db.User.Get(i => i.Auth == id).FirstOrDefault();
                    if (user == null)
                    {
                        ModelState.AddModelError("Password", "حساب کاربری شما یافت نشد");
                    }
                    else
                    {
                        user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(recovery.Password, "MD5");
                        user.Auth = Guid.NewGuid().ToString();
                        _db.User.Save();
                        return Redirect("/?ReturnUrl=RecoveryPassword");
                    }
                }
                return View(recovery);
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }


    }
}