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
        public int Id { get; set; }
        public int Type { get; set; }
        public bool IsVideo { get; set; }
        public bool IsDoneWithdraw { get; set; }
        public int VideoOrPlaylistId { get; set; }
        public string VideoOrPlaylistName { get; set; }
        public int Fund { get; set; }
        public DateTime Date { get; set; }

    }
}
