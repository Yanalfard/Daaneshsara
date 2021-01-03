using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class VmChatUsers
    {
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public System.DateTime TimeSent { get; set; }
    }
}
