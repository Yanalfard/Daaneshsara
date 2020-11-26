using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.MetaData;
using System.Web.Security;
using DataLayer.ViewModels;

namespace AminWeb.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private Heart _db = new Heart();
        // GET: Admin/User
        public ActionResult Index(string name = "", string tellNo = "", string email = "", string balance = "", int isActive = -1, int roleId = -1)
        {
            #region MyRegion
            
            //TblRole s1 = new TblRole();
            //s1.Name = "user";
            //s1.Title = "کاربر";
            //s1.RoleId = 0;
            //_db.Role.Add(s1);
            //TblRole s2 = new TblRole();
            //s2.Name = "institution";
            //s2.Title = "آموزشگاه";
            //s2.RoleId = 2;
            //_db.Role.Add(s2);
            //TblRole s3 = new TblRole();
            //s3.Name = "admin";
            //s3.Title = "مدیر";
            //s3.RoleId = 3;
            //_db.Role.Add(s3);
            //TblRole s4 = new TblRole();
            //s4.Name = "tutor";
            //s4.Title = "مربی";
            //s4.RoleId = 1;
            //_db.Role.Add(s4);
            ////TblRole t1= _db.Role.GetById(1);
            ////_db.Role.Delete(t1);
            //_db.Role.Save();
            #endregion
            TblRole role = new TblRole();
            ViewBag.name = name;
            ViewBag.tellNo = tellNo;
            ViewBag.email = email;
            ViewBag.balance = balance;
            ViewBag.isActive = isActive;
            ViewBag.roleId = roleId;
            return View();
        }
        //public ActionResult ListUser()
        //{
        //    return PartialView(_db.User.Get());
        //}
        public ActionResult ViewUser()
        {
            return View();
        }



        public ActionResult Create()
        {
            ViewBag.IsActive = new SelectList(new[] { new { Value = "true", Text = "فعال" }, new { Value = "false", Text = "غیرفعال" }, }, "Value", "Text");
            // ViewBag.IsActive = new SelectList(new[] { new { Value = "1", Text = "فعال" }, new { Value = "0", Text = "غیرفعال" }, }, "Value", "Text");
            ViewBag.RoleName = new SelectList(new[]
            { new { Value = "0", Text = "کارآموز" },
             new { Value = "1", Text = "مربی" },
            new { Value = "2", Text = "آموزشگاه" },
            new { Value = "3", Text = "مدیر" }}, "Value", "Text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MdUser user)
        {
            if (ModelState.IsValid)
            {
                user.Email = user.Email.Trim().ToLower().Replace(" ", "");
                user.TellNo = user.TellNo.Trim().ToLower().Replace(" ", "");
                if (_db.User.Get().Any(i => i.TellNo == user.TellNo))
                {
                    ModelState.AddModelError("TelNo", "شماره موبایل تکراریست");
                }
                else if (_db.User.Get().Any(i => i.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "ایمیل تکراریست");
                }
                else
                {
                    TblUser addUser = new TblUser();
                    addUser.Balance = user.Balance;
                    addUser.Auth = Guid.NewGuid().ToString();
                    addUser.IsActive = user.IsActive;
                    addUser.Email = user.Email;
                    addUser.Name = user.Name;
                    addUser.RoleId = user.RoleId;
                    addUser.TellNo = user.TellNo;
                    addUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "SHA256");
                    _db.User.Add(addUser);
                    _db.User.Save();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.IsActive = new SelectList(new[] { new { Value = "true", Text = "فعال" }, new { Value = "false", Text = "غیرفعال" }, }, "Value", "Text", user.IsActive);
            ViewBag.RoleName = new SelectList(new[]
            { new { Value = "0", Text = "کارآموز" },
             new { Value = "1", Text = "مربی" },
            new { Value = "2", Text = "آموزشگاه" },
            new { Value = "3", Text = "مدیر" }}, "Value", "Text", user.RoleId);
            return View(user);
        }

        public ActionResult ActiveDisableUser(int id)
        {
            TblUser updateUser = _db.User.GetById(id);
            updateUser.IsActive = !updateUser.IsActive;
            _db.User.Update(updateUser);
            _db.User.Save();
            return PartialView("ListUser", _db.User.Get());
        }
        public ActionResult ChangePassword(int id)
        {
            @ViewBag.UserName = _db.User.GetById(id).Name;
            return PartialView(new VmChangePassword()
            {
                Id = id,
            });
        }
        [HttpPost]
        public ActionResult ChangePassword(VmChangePassword pass)
        {
            if (ModelState.IsValid)
            {
                TblUser updateUser = _db.User.GetById(pass.Id);
                updateUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pass.Password, "SHA256");
                _db.User.Update(updateUser);
                return JavaScript("$('#myModal').modal('hide');doneEdit();");
                // return PartialView("ListUser", _db.User.Get());
            }
            return PartialView("ChangePassword", pass);
        }

        public ActionResult Delete(int id)
        {
            TblUser selectUserById = _db.User.GetById(id);
            bool delete = _db.User.Delete(selectUserById);
            if (delete)
            {
                _db.User.Save();
                return Json(new { success = true, responseText = " حذف شد" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "خطا در حذف   لطفا بررسی فرمایید" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int id)
        {
            TblUser selectUserById = _db.User.GetById(id);
            MdUser User = new MdUser()
            {
                UserId = selectUserById.UserId,
                Balance = selectUserById.Balance,
                Auth = selectUserById.Auth,
                IsActive = selectUserById.IsActive,
                Email = selectUserById.Email,
                Name = selectUserById.Name,
                RoleId = selectUserById.RoleId,
                TellNo = selectUserById.TellNo,
                DocsId = selectUserById.DocsId,
                Password = selectUserById.Password,
            };
            ViewBag.IsActive = new SelectList(new[] { new { Value = "1", Text = "فعال" },
                new { Value = "0", Text = "غیرفعال" }, },
                "Value", "Text", selectUserById.IsActive);
            ViewBag.RoleName = new SelectList(new[]
            { new { Value = "0", Text = "کارآموز" },
             new { Value = "1", Text = "مربی" },
            new { Value = "2", Text = "آموزشگاه" },
            new { Value = "3", Text = "مدیر" }}, "Value", "Text", selectUserById.RoleId);
            return View(User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MdUser user)
        {
            if (ModelState.IsValid)
            {
                user.Email = user.Email.Trim().ToLower().Replace(" ", "");
                user.TellNo = user.TellNo.Trim().ToLower().Replace(" ", "");
                if (_db.User.Get().Where(i => i.UserId != user.UserId).Any(i => i.TellNo == user.TellNo))
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
                    updateUser.Balance = user.Balance;
                    updateUser.Auth = Guid.NewGuid().ToString();
                    updateUser.IsActive = user.IsActive;
                    updateUser.Email = user.Email;
                    updateUser.Name = user.Name;
                    updateUser.RoleId = user.RoleId;
                    updateUser.TellNo = user.TellNo;
                    updateUser.DocsId = user.DocsId;
                    updateUser.Password = user.Password;
                    _db.User.Update(updateUser);
                    _db.User.Save();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.IsActive = new SelectList(new[] { new { Value = "1", Text = "فعال" }, new { Value = "0", Text = "غیرفعال" }, }, "Value", "Text", user.IsActive);
            ViewBag.RoleName = new SelectList(new[]
            { new { Value = "0", Text = "کارآموز" },
             new { Value = "1", Text = "مربی" },
            new { Value = "2", Text = "آموزشگاه" },
            new { Value = "3", Text = "مدیر" }}, "Value", "Text", user.RoleId);
            return View(user);
        }
        public ActionResult ListUser(string name = "", string tellNo = "", string email = "", string balance = "", int isActive = -1, int roleId = -1)
        {

            List<TblUser> list = new List<TblUser>();
            list.AddRange(_db.User.Get());
            if (name != "")
            {
                list = list.Where(p => p.Name.Contains(name)).ToList();
            }
            if (tellNo != "")
            {
                list = list.Where(p => p.TellNo.Contains(tellNo)).ToList();
            }
            if (email != "")
            {
                list = list.Where(p => p.Email.Contains(email)).ToList();
            }
            if (balance != "")
            {
                list = list.Where(p => p.Balance.ToString().Contains(balance)).ToList();
            }
            if (isActive > -1)
            {
                if (isActive == 1)
                {
                    list = list.Where(p => p.IsActive == true).ToList();
                }
                else
                {
                    list = list.Where(p => p.IsActive == false).ToList();
                }
            }
            if (roleId > -1)
            {
                if (roleId == 0)
                {
                    list = list.Where(p => p.RoleId == 0).ToList();
                }
                else if (roleId == 1)
                {
                    list = list.Where(p => p.RoleId == 1).ToList();
                }
                else if (roleId == 2)
                {
                    list = list.Where(p => p.RoleId == 2).ToList();
                }
                else if (roleId == 3)
                {
                    list = list.Where(p => p.RoleId == 3).ToList();
                }
            }
            return PartialView(list);
        }
    }
}