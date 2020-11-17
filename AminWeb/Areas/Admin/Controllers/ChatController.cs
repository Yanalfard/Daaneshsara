using DataLayer.Models;
using DataLayer.Services;
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
        public ActionResult Index()
        {
            //TblChat chat = new TblChat();
            //chat.RecieverId = 13;
            //chat.SenderId = 14;
            //chat.TimeSent = DateTime.Now;
            //chat.Message = "salam chetory";


            //TblChat chat2 = new TblChat();
            //chat2.RecieverId = 13;
            //chat2.SenderId = 14;
            //chat2.TimeSent = DateTime.Now;
            //chat2.Message = "salam chetory";

            //TblChat chat3 = new TblChat();
            //chat3.RecieverId = 14;
            //chat3.SenderId = 13;
            //chat3.TimeSent = DateTime.Now;
            //chat3.Message = "salam chetory";


            //TblChat chat4 = new TblChat();
            //chat4.RecieverId = 14;
            //chat4.SenderId = 13;
            //chat4.TimeSent = DateTime.Now;
            //chat4.Message = "salam chetory";


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
                int lengthForSmartedEngin = allChats.DistinctBy(i => i.SenderId).DistinctBy(j => j.RecieverId).Count()+1;
                for (int j = 0; j < lengthForSmartedEngin; j++)
                    for (int i = 1; i < allChats.Count; i++)
                        if (allChats[i].SenderId == helpChat.SenderId && allChats[i].RecieverId == helpChat.RecieverId)
                            allChats.RemoveAt(i);
                        else if (allChats[i].SenderId == helpChat.RecieverId && allChats[i].RecieverId == helpChat.SenderId)
                            allChats.RemoveAt(i);
                        else
                            helpChat = allChats[i];
            }
            return allChats.DistinctBy(i => i.SenderId).ToList();
        }

        public ActionResult ListChat(string name = "", string tellNo = "", string email = "", string message = "")
        {
            List<TblChat> list = new List<TblChat>();
            list.AddRange(GetAllChats());
            if (name != "")
            {
                list = list.Where(p => p.TblUser.Name.Contains(name)).ToList();
            }
            if (tellNo != "")
            {
                list = list.Where(p => p.TblUser.TellNo.Contains(tellNo)).ToList();
            }
            if (email != "")
            {
                list = list.Where(p => p.TblUser.Email.Contains(email)).ToList();
            }
            if (message != "")
            {
                list = list.Where(p => p.Message.Contains(message)).ToList();
            }
            return PartialView(list);
        }

        public ActionResult ViewChat(int user1, int user2)
        {
            return View(GetAChat(user1, user2));
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