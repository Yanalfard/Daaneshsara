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
    public class VideoController : Controller
    {
        private Heart _db = new Heart();

        // GET: Video
        [Route("VideoView/{id}/{name}")]
        public ActionResult VideoView(int id, string name)
        {
            ViewBag.Name = name;
            TblVideo video = _db.Video.GetById(id);
            int IpOnline = Convert.ToInt32(Session["IpOnline"]);
            int ViewVideoValid = Convert.ToInt32(Session["ViewVideoValid"]);
            //Session["ViewVideoValid"]=
            if (ViewVideoValid == 0)
            {
                video.ViewCount++;
                if (video.PlaylistId != null)
                {
                    TblPlaylist playlist = _db.Playlist.GetById(video.PlaylistId);
                    if (playlist != null)
                    {
                        playlist.ViewCount++;
                    }
                }
                _db.Video.Save();
                Session["ViewVideoValid"] = 1;
            }
            if (video.PlaylistId != null)
            {
                ViewBag.Playlist = _db.Playlist.Get(i => i.PlaylistId == video.PlaylistId).ToList();
            }
            VmVideoView vmVideo = new VmVideoView();
            vmVideo.VideoId = video.VideoId;
            vmVideo.VideoUrl = video.VideoUrl;
            vmVideo.VidioDemoUrl = video.VidioDemoUrl;
            vmVideo.MainImage = video.MainImage;
            vmVideo.Title = video.Title;
            vmVideo.Description = video.Description;
            vmVideo.DateSubmited = video.DateSubmited;
            vmVideo.ViewCount = video.ViewCount;
            vmVideo.IsHome = video.IsHome;
            vmVideo.UserNameVideo = video.TblUser.Name;
            vmVideo.PlaylistTitle = video.PlaylistId != null ? video.TblPlaylist.Title : "";
            vmVideo.PlaylistPrice = video.PlaylistId != null ? video.TblPlaylist.Price : 0;
            if (User.Identity.IsAuthenticated)
            {
                if (video.UserId == SelectUser().UserId || User.IsInRole("admin"))
                {
                    vmVideo.IsLog = true;
                }
                List<TblLog> log = new List<TblLog>();

                if (video.PlaylistId != null)
                {
                    log = _db.Log.Get().Where(i => i.UserId == SelectUser().UserId && i.PlayListId == video.PlaylistId && (i.Status == 1 || i.Status == 2)).ToList();
                }
                else if (video.PlaylistId == null)
                {
                    log = _db.Log.Get().Where(i => i.UserId == SelectUser().UserId && i.VideoId == video.VideoId && (i.Status == 1 || i.Status == 2)).ToList();
                }
                if (log.Count != 0)
                {
                    vmVideo.IsLog = true;
                    if (video.PlaylistId != null && log.FirstOrDefault().PlayListId != null)
                    {
                        vmVideo.IsPlaylist = true;
                        vmVideo.IsVideo = false;
                    }
                    else
                    {
                        vmVideo.IsPlaylist = false;
                        vmVideo.IsVideo = true;
                    }
                }
            }

            vmVideo.LikeCount = video.LikeCount;
            vmVideo.Link = video.Link;
            vmVideo.UserId = video.UserId;
            vmVideo.Price = video.Price;
            vmVideo.IsCharity = video.IsCharity;
            vmVideo.PlaylistId = video.PlaylistId;
            vmVideo.IsActive = video.IsActive;
            vmVideo.CatagoryId = video.CatagoryId;
            vmVideo.RatingCount = video.RatingCount;

            return View(vmVideo);
        }

        public ActionResult ListVideos()
        {
            List<TblVideo> selectAllVideos = _db.Video.Get().Where(i => i.IsHome && i.IsActive).ToList();
            return PartialView(selectAllVideos.OrderByDescending(i => i.DateSubmited));
        }
        public ActionResult SelectVideos()
        {
            List<TblVideo> selectAllVideos = new List<TblVideo>();
            selectAllVideos.AddRange( _db.Video.Get(i => i.IsHome && i.IsActive && !i.PlaylistId.HasValue).ToList());
            _db.Playlist.Get(i => i.IsHome && i.IsActive).ToList().ForEach(j => selectAllVideos.AddRange(j.TblVideo.ToList()));
            return PartialView("ListVideos", selectAllVideos.OrderByDescending(i => i.DateSubmited));
        }
        public ActionResult SelectVideosByCategory(string name)
        {
            TblCatagory selectCatById = _db.Cat.Get().FirstOrDefault(i => i.Name == name);
            List<TblVideo> selectVideoByCategory = new List<TblVideo>();
            if (selectCatById != null)
            {
                selectVideoByCategory = _db.Video.Get(i => i.CatagoryId == selectCatById.CatagoryId && i.IsActive).ToList();
            }
            else
            {
                selectVideoByCategory = _db.Video.Get(i => i.CatagoryId == selectCatById.CatagoryId && i.IsActive).ToList();
            }
            return PartialView("ListVideos", selectVideoByCategory);
        }
        public ActionResult SelectVideoBySearch(int id)
        {
            List<TblVideo> selectVideoByCategory = new List<TblVideo>();
            if (id == 1)
            {
                selectVideoByCategory = _db.Video.Get(i => i.IsActive).OrderByDescending(i => i.DateSubmited).ToList();
            }
            else if (id == 2)
            {
                selectVideoByCategory = _db.Video.Get(i => i.IsActive).OrderByDescending(i => i.ViewCount).ToList();
            }
            else if (id == 3)
            {
                selectVideoByCategory = _db.Video.Get(i => i.IsActive).OrderByDescending(i => i.LikeCount).ToList();
            }
            return PartialView("ListVideos", selectVideoByCategory);
        }
        [Route("SelectVideoName/{val}")]
        public ActionResult SelectVideoName(string val)
        {
            var list = _db.Video.Get(i => i.Title.Contains(val)).Select(i => i.Title).ToList();
            return Json(new { success = true, responseText = list }, JsonRequestBehavior.AllowGet);
        }
        [Route("SearchVideoByName/{name}")]
        public ActionResult SearchVideoByName(string name)
        {
            List<TblVideo> selectVideoByCategory = new List<TblVideo>();
            selectVideoByCategory = _db.Video.Get(i => i.IsActive && i.Title.Contains(name)).OrderByDescending(i => i.DateSubmited).ToList();
            return PartialView("ListVideos", selectVideoByCategory);
        }
        TblUser SelectUser()
        {
            TblUser selectUser = _db.User.Get().FirstOrDefault(i => i.Email == User.Identity.Name);
            return selectUser;
        }
        public ActionResult CheckUserSaveVideo(int id)
        {
            TblUserVideoRel videoRel = _db.UserVideoRel.Get().FirstOrDefault(i => i.VideoId == id && i.UserId == SelectUser().UserId);
            ViewBag.VideoId = id;
            ViewBag.IsBookMark = false;
            if (videoRel != null)
            {
                ViewBag.IsBookMark = true;
            }
            return PartialView();
        }
        [Authorize]
        public ActionResult CheckbookmarkVideoInUser(int id)
        {
            TblUserVideoRel videoRel = _db.UserVideoRel.Get().FirstOrDefault(i => i.VideoId == id && i.UserId == SelectUser().UserId);
            ViewBag.VideoId = id;
            ViewBag.IsBookMark = false;
            if (videoRel != null && videoRel.IsBookMark)
            {
                _db.UserVideoRel.Delete(videoRel);
                ViewBag.Error = "ویدیو از کتاب خانه شما حذف شد";
            }
            else
            {
                _db.UserVideoRel.Add(new TblUserVideoRel()
                {
                    VideoId = id,
                    UserId = SelectUser().UserId,
                    IsBookMark = true,
                    Date = DateTime.Now
                });
                ViewBag.IsBookMark = true;
                ViewBag.Error = "ویدیو در کتاب خانه شما ذخیره شد";
            }
            _db.UserVideoRel.Save();
            return PartialView("CheckUserSaveVideo", id);
        }

        [HttpPost]
        public ActionResult Report(int id, string Text, string reportRadio)
        {
            _db.Report.Add(new TblReport()
            {
                DateSent = DateTime.Now,
                Text = reportRadio + " توضیحات: " + Text,
                VideoId = id,
            });
            _db.Report.Save();
            return JavaScript("");
        }
        public ActionResult LikeCount(int LikeCount)
        {
            ViewBag.LikeCount = LikeCount / 20;
            return PartialView();
        }
        [HttpPost]
        public ActionResult Raiting(int id, int Raiting)
        {
            int ConverRating = Convert.ToInt32(Raiting) * 20;
            TblVideo product = _db.Video.GetById(id);
            int ViewVideoValid = Convert.ToInt32(Session["ViewVideoRaiting"]);
            if (ViewVideoValid == 0)
            {
                Session["ViewVideoRaiting"] = 1;
                if (product.LikeCount == 0)
                {
                    product.LikeCount = ConverRating;
                    product.RatingCount = 1;
                }
                else
                {
                    var number = product.LikeCount * product.RatingCount;
                    product.RatingCount++;
                    product.LikeCount = (int)((number + ConverRating) / product.RatingCount);
                }
                _db.Report.Save();
            }
            int LikeCount = product.LikeCount;
            ViewBag.LikeCount = product.LikeCount / 20;
            return PartialView("LikeCount", LikeCount);
        }


        #region Chat

        
        public List<TblChat> GetAChat(int user2)
        {
            int user1 = SelectUser().UserId;
            List<TblChat> stp1 = _db.Chat.Get(i => (i.SenderId == user1 && i.RecieverId == user2) || (i.SenderId == user2 && i.RecieverId == user1)).ToList();
            return stp1;
        }
        public ActionResult Chat(int UserId)
        {
            List<VmChatUsers> list = new List<VmChatUsers>();
            ViewBag.SenderId = UserId;
            foreach (var item in GetAChat(UserId))
            {
                VmChatUsers vmChat = new VmChatUsers();
                vmChat.ChatId = item.ChatId;
                vmChat.TimeSent = item.TimeSent;
                vmChat.Message = item.Message;
                vmChat.Name = item.TblUser1.Name;
                if (UserId == item.SenderId)
                {
                    ViewBag.SenderId = item.SenderId;
                    vmChat.SenderId = item.SenderId;
                }
                list.Add(vmChat);
            }
            return PartialView(list);
        }

        public ActionResult SendMessage(int id, string message)
        {
            _db.Chat.Add(new TblChat()
            {
                SenderId = SelectUser().UserId,
                RecieverId = id,
                Message = message,
                TimeSent = DateTime.Now,
            });
            _db.Chat.Save();
            List<VmChatUsers> list = new List<VmChatUsers>();
            ViewBag.SenderId = id;
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
            return PartialView("Chat", list);
        }
        #endregion
    }
}