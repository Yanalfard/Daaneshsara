using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    public class MdWithdraw
    {
        [Key]
        public int id { get; set; }

        public int UserId { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Value { get; set; }

        [Display(Name = "انجام شده؟")]
        public bool IsDone { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string Description { get; set; }

    }

    [MetadataType(typeof(MdWithdraw))]
    public class TblWithdraw
    {

    }
}
