using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    class MdTblChat
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("SenderId")]
        public int SenderId { get; set; }

        [ForeignKey("RecieverId")]
        public int RecieverId { get; set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string Message { get; set; }

        [DataType(DataType.DateTime)]
        public System.DateTime TimeSent { get; set; }
    }
}
