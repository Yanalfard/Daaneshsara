using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class VmLog
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public int Balance { get; set; }
        public short Status { get; set; }
        public string Name { get; set; }
        public string Tell { get; set; }
        public string Email { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> VideoId { get; set; }
        public Nullable<int> PlayListId { get; set; }
        public bool IsVideo { get; set; }
        public Nullable<int> SellerId { get; set; }
        public Nullable<int> SellerAmount { get; set; }
    }
}
