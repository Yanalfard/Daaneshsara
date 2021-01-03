﻿using DataLayer.Models;
using DataLayer.Services;
using DataLayer.ViewModels;
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
            //chat4.SenderId = 6;
            //chat4.TimeSent = DateTime.Now;
            //chat4.Message = "khobi sadra";


            //_db.Chat.Add(chat);
            //_db.Chat.Add(chat2);
            //_db.Chat.Add(chat3);
            //_db.Chat.Add(chat4);


            //_db.Chat.Save();
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
            List<TblChat> stp1 = _db.Chat.Get(i => (i.SenderId == user1 && i.RecieverId == user2) || (i.SenderId == user2 && i.RecieverId == user1)).ToList();
            //List<TblChat> stp1 = _db.Chat.Get(i => i.SenderId == user1 && i.RecieverId == user2).ToList();
            //List<TblChat> stp2 = _db.Chat.Get(i => i.SenderId == user2 && i.RecieverId == user1).ToList();
            //stp1.AddRange(stp2);
            //stp1.DistinctBy(i => i.ChatId);
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
            List<VmChatUsers> list = new List<VmChatUsers>();
            foreach (var item in GetAChat(id))
            {
                VmChatUsers vmChat = new VmChatUsers();
                vmChat.ChatId = item.ChatId;
                vmChat.TimeSent = item.TimeSent;
                vmChat.Message = item.Message;
                vmChat.Name = item.TblUser1.Name;
                if (id == item.SenderId)
                {
                    ViewBag.SenderId = item.SenderId;
                    vmChat.SenderId = item.SenderId;
                }
                list.Add(vmChat);
            }
            return PartialView(list);
        }
        public ActionResult SendMessage(int id,string message)
        {
            _db.Chat.Add(new TblChat()
            {
                SenderId= SelectUser().UserId,
                RecieverId=id,
                Message=message,
                TimeSent=DateTime.Now,
            });
            _db.Chat.Save();
            List<VmChatUsers> list = new List<VmChatUsers>();
            foreach (var item in GetAChat(id))
            {
                VmChatUsers vmChat = new VmChatUsers();
                vmChat.ChatId = item.ChatId;
                vmChat.TimeSent = item.TimeSent;
                vmChat.Message = item.Message;
                vmChat.Name = item.TblUser1.Name;
                if (id == item.SenderId)
                {
                    ViewBag.SenderId = item.SenderId;
                    vmChat.SenderId = item.SenderId;
                }
                list.Add(vmChat);
            }
            return PartialView("ViewChat", list);
        }
    }
}