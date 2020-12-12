using DataLayer.Models;
using DataLayer.Services;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminWeb.Areas.User.Controllers
{
    public class ChatController : Controller
    {
        private Heart _db = new Heart();
        // GET: User/Chat
        public ActionResult Index()
        {
            return View();
        }
        TblUser SelectUser()
        {
            TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Email == User.Identity.Name);
            return selectUser;
        }
        public List<TblChat> GetAChat(int user2)
        {
            int user1 = SelectUser().UserId;
            List<TblChat> stp1 = _db.Chat.Get(i => i.SenderId == user1 && i.RecieverId == user2).ToList();
            List<TblChat> stp2 = _db.Chat.Get(i => i.SenderId == user2 && i.RecieverId == user1).ToList();
            stp1.AddRange(stp2);
            stp1.DistinctBy(i => i.ChatId);
            return stp1;
        }

        public List<TblChat> GetAllChats()
        {
            int user = SelectUser().UserId;
            List<TblChat> chatSent = _db.Chat.Get(i => i.SenderId == user).ToList();
            List<TblChat> chatRecieved = _db.Chat.Get(i => i.RecieverId == user).ToList();
            chatSent = chatSent.DistinctBy(i => i.RecieverId).ToList();
            chatRecieved = chatRecieved.DistinctBy(i => i.SenderId).ToList();
            chatSent.AddRange(chatRecieved);
            TblChat chat = chatSent[0];
            for (int i = 0; i < chatSent.Count; i++)
            {
                chat = chatSent[i];
                for (int j = 0; j < chatSent.Count; j++)
                    if (chat.SenderId == chatSent[j].RecieverId && chat.RecieverId == chatSent[j].SenderId)
                        chatSent.RemoveAt(j);
            }
            return chatSent;
        }
        public ActionResult Chats()
        {
            return PartialView(GetAllChats());
        }
        public ActionResult ViewChat(int id)
        {
            return PartialView(GetAChat(id));
        }
        public ActionResult SendMessage(string id)
        {
            return PartialView("ViewChat");
        }
    }
}