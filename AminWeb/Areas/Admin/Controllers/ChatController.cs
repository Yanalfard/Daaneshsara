using DataLayer.Models;
using DataLayer.Services;
using DataLayer.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.Admin.Controllers
{
    public class ChatController : Controller
    {
        private Heart _db = new Heart();
        // GET: Admin/Chat
        public ActionResult Index(string tellNo = "", string email = "")
        {
            ViewBag.tellNo = tellNo;
            ViewBag.email = email;
            //TblChat chat = new TblChat();
            //chat.RecieverId = 6;
            //chat.SenderId = 7;
            //chat.TimeSent = DateTime.Now;
            //chat.Message = "salam mehdi";


            //TblChat chat2 = new TblChat();
            //chat2.RecieverId = 7;
            //chat2.SenderId = 6;
            //chat2.TimeSent = DateTime.Now;
            //chat2.Message = "salam sadra";

            //TblChat chat3 = new TblChat();
            //chat3.RecieverId = 6;
            //chat3.SenderId = 7;
            //chat3.TimeSent = DateTime.Now;
            //chat3.Message = "khobi mehdi";


            //TblChat chat4 = new TblChat();
            //chat4.RecieverId = 7;
            //chat4.SenderId =6;
            //chat4.TimeSent = DateTime.Now;
            //chat4.Message = "khobi sadra";


            //_db.Chat.Add(chat);
            //_db.Chat.Add(chat2);
            //_db.Chat.Add(chat3);
            //_db.Chat.Add(chat4);


            //_db.Chat.Save();
            return View();
        }

        public List<TblChat> GetAChat(int user1, int user2)
        {
            List<TblChat> stp1 = _db.Chat.Get(i => i.SenderId == user1 && i.RecieverId == user2).ToList();
            List<TblChat> stp2 = _db.Chat.Get(i => i.SenderId == user2 && i.RecieverId == user1).ToList();
            stp1.AddRange(stp2);
            stp1.DistinctBy(i => i.ChatId);
            return stp1;
        }
        public bool GetDeleteChat(int user1, int user2)
        {
            try
            {
                List<TblChat> stp1 = _db.Chat.Get(i => i.SenderId == user1 && i.RecieverId == user2).ToList();
                List<TblChat> stp2 = _db.Chat.Get(i => i.SenderId == user2 && i.RecieverId == user1).ToList();
                stp1.AddRange(stp2);
                stp1.DistinctBy(i => i.ChatId);
                foreach (var item in stp1)
                {
                    _db.Chat.Delete(item);
                }
                _db.Chat.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<TblChat> GetAllChats()
        {
            List<TblChat> allChats = _db.Chat.Get().ToList();
            if (allChats.Count != 0)
            {
                TblChat helpChat = allChats[0];
                int lengthForSmartedEngin = allChats.DistinctBy(i => i.SenderId).DistinctBy(j => j.RecieverId).Count();
                for (int j = 0; j < lengthForSmartedEngin; j++)
                    for (int i = 1; i < allChats.Count; i++)
                        if (allChats[i].SenderId == helpChat.SenderId && allChats[i].RecieverId == helpChat.RecieverId)
                            allChats.RemoveAt(i);
                        else if (allChats[i].SenderId == helpChat.RecieverId && allChats[i].RecieverId == helpChat.SenderId)
                            allChats.RemoveAt(i);
                        else
                            helpChat = allChats[i];
                allChats = allChats.DistinctBy(i => i.SenderId).ToList();
                if (allChats.Count > 1)
                    if (allChats[0].SenderId == allChats[1].RecieverId && allChats[0].RecieverId == allChats[1].SenderId)
                        allChats.RemoveAt(0);
            }
            return allChats;
        }

        public ActionResult ListChat(string tellNo = "", string email = "")
        {
            List<TblChat> list = GetAllChats();

            if (tellNo != "")
            {
                list = list.Where(p => p.TblUser.TellNo.Contains(tellNo)).ToList();
            }
            if (email != "")
            {
                list = list.Where(p => p.TblUser.Email.Contains(email)).ToList();
            }
            return PartialView(list);
        }

        public ActionResult ViewChat(int user1, int user2)
        {
            //return View(GetAChat(user1, user2));


            List<VmChatUsers> list = new List<VmChatUsers>();
            //ViewBag.SenderId = id;
            foreach (var item in GetAChat(user1, user2))
            {
                VmChatUsers vmChat = new VmChatUsers();
                vmChat.ChatId = item.ChatId;
                vmChat.TimeSent = item.TimeSent;
                vmChat.Message = item.Message;
                vmChat.Name = item.TblUser1.Name;
                vmChat.SenderId = item.SenderId;
                vmChat.RecieverId = item.RecieverId;
                //if (id == item.SenderId)
                //{
                //    ViewBag.SenderId = item.SenderId;
                //    vmChat.SenderId = item.SenderId;
                //}
                list.Add(vmChat);
            }
            return PartialView(list);
        }


        public ActionResult Delete(int user1, int user2)
        {
            bool delete = GetDeleteChat(user1, user2);
            if (delete)
            {
                return Json(new { success = true, responseText = " حذف شد" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "خطا در حذف   لطفا بررسی فرمایید" }, JsonRequestBehavior.AllowGet);
        }

    }
}