using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class VmShopCart
    {
        public int VideoId { get; set; }
        public Nullable<int> PlaylistId { get; set; }
        public int UserId { get; set; }
        public int BalanceUser { get; set; }
        public bool IsValidBalance { get; set; }
        public string UserName { get; set; }
        public int Price { get; set; }
        public bool IsPlaylist { get; set; }
    }
}
