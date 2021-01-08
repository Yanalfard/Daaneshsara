using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    public class MdDoc
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "تلفن ثابت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "طول بیش از 11 کاراکتر مجاز نیست")]
        public string TellSabet { get; set; }

        [Display(Name = "پروانه آموزشی")]
        [MaxLength(500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string ParvaneAmuzeshgahUrl { get; set; }

        [Display(Name = "مجوز تاسیس")]
        [MaxLength(500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string MojavezTasisUrl { get; set; }

        [Display(Name = "شناسنامه صاحب امتیاز")]
        [MaxLength(500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string ShenasnameSahebEmtiazUrl { get; set; }

        [Display(Name = "کارت ملی صاحب امتیاز")]
        [MaxLength(500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string KarteMeliSahebEmtiazUrl { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [MaxLength(500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string Address { get; set; }
        public int IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }

}
