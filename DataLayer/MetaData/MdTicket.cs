using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    public class MdTicket
    {
        public int TicketId { get; set; }
        public int SenderId { get; set; }
        [Display(Name = "موضوع ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "طول بیش از 50 کاراکتر مجاز نیست")]
        public string Title { get; set; }
        [Display(Name = "توضیحات ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(950, ErrorMessage = "طول بیش از 950 کاراکتر مجاز نیست")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [Display(Name = "فایل ")]
        public string AttachmentUrl { get; set; }
        public System.DateTime DateSent { get; set; }
    }
}
