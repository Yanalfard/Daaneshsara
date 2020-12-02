using DataLayer.Models;
using DataLayer.Services;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AminWeb.Areas.User.Controllers
{
    public class AccountController : Controller
    {
        private Heart _db = new Heart();
        // GET: User/Account
        public ActionResult UserData()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        TblUser SelectUser()
        {
            TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Email == User.Identity.Name);
            return selectUser;
        }
        [HttpPost]
        public ActionResult ChangePassword(VmChangeOldNewPassword changePass)
        {
            if (ModelState.IsValid)
            {
                TblUser user = SelectUser();
                if (user != null)
                {
                    string hassPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(changePass.OldPassword,"SHA256");
                    if (user.Password == hassPassword)
                    {
                        user.Password= FormsAuthentication.HashPasswordForStoringInConfigFile(changePass.Password, "SHA256");
                        _db.User.Update(user);
                        _db.User.Save();
                        return View("ChangePassword", changePass);
                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword","رمز قدیمی اشتباه است");
                    }
                }
            }
            return View("ChangePassword",changePass);
        }
        public ActionResult Wallet()
        {
            return View();
        }

        public ActionResult AccountCompletion()
        {
            return View();
        }

        public ActionResult AccountToInstitution()
        {
            return View();
        }

        public ActionResult Info()
        {
            return PartialView(SelectUser());
        }
        public ActionResult Edit()
        {
            VmEditInfo edite = new VmEditInfo();
            edite.Email = SelectUser().Email;
            edite.Name = SelectUser().Name;
            edite.TellNo = SelectUser().TellNo;
            edite.UserId = SelectUser().UserId;
            return PartialView(edite);
        }
        [HttpPost]
        public ActionResult Edit(VmEditInfo user)
        {
            if (ModelState.IsValid)
            {
                user.Email = user.Email.Trim().ToLower().Replace(" ", "");
                user.TellNo = user.TellNo.Trim().ToLower().Replace(" ", "");
                if (_db.User.Get().Any(i => i.TellNo == user.TellNo && i.UserId != user.UserId))
                {
                    ModelState.AddModelError("TelNo", "شماره موبایل تکراریست");
                }
                else if (_db.User.Get().Any(i => i.Email == user.Email && i.UserId != user.UserId))
                {
                    ModelState.AddModelError("Email", "ایمیل تکراریست");
                }
                else
                {
                    TblUser updateUser = _db.User.GetById(user.UserId);
                    updateUser.Email = user.Email;
                    updateUser.Name = user.Name;
                    updateUser.TellNo = user.TellNo;
                    _db.User.Update(updateUser);
                    _db.User.Save();
                    return JavaScript("UIkit.modal(document.getElementById('Modal-Show')).hide();UIkit.notification('اطلاعات ویرایش شد');doneEditInfo()");
                }
            }
            return PartialView("Edit", user);
        }
    }
}