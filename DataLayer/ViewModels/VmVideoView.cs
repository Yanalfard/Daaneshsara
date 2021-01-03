using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class VmVideoView
    {
        public int VideoId { get; set; }
        public string VideoUrl { get; set; }
        public string VidioDemoUrl { get; set; }
        public string PlaylistTitle { get; set; }
        public int PlaylistPrice { get; set; }
        public string UserNameVideo { get; set; }
        public string MainImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateSubmited { get; set; }
        public int ViewCount { get; set; }
        public bool IsHome { get; set; }
        public bool IsLog { get; set; }
        public bool IsPlaylist { get; set; }
        public bool IsVideo { get; set; }
        public int LikeCount { get; set; }
        public string Link { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public bool IsCharity { get; set; }
        public Nullable<int> PlaylistId { get; set; }
        public bool IsActive { get; set; }
        public int CatagoryId { get; set; }
        public Nullable<int> RatingCount { get; set; }

    }
}
