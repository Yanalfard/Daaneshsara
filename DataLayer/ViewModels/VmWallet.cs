using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class VmWallet
    {

        //public int LogId { get; set; }
        //public int UserIdLog { get; set; }
        //public int AmountLog { get; set; }
        //public short StatusLog { get; set; }
        //public DateTime DateLog { get; set; }
        //public Nullable<int> VideoIdLog { get; set; }
        //public Nullable<int> PlayListIdLog { get; set; }
        //public bool IsVideoLog { get; set; }


        //public int WithdrawId { get; set; }
        //public int UserIdWithdraw { get; set; }
        //public int ValueWithdraw { get; set; }
        //public bool IsDoneWithdraw { get; set; }
        //public string DescriptionWithdraw { get; set; }
        //public string CardInfoWithdraw { get; set; }
        //public System.DateTime DateWithdraw { get; set; }


        public int Id { get; set; }
        public int Type { get; set; }
        public bool IsVideo { get; set; }
        public int VideoOrPlaylistId { get; set; }
        public string VideoOrPlaylistName { get; set; }
        public int Fund { get; set; }
        public DateTime Date { get; set; }

    }
}
