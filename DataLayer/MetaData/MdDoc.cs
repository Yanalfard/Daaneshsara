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
        [MaxLength(length: 10, ErrorMessage = "طول بیش از 10 کاراکتر مجاز نیست")]
        public string TellSabet { get; set; }

        [Display(Name = "پروانه آموزشی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string ParvaneAmuzeshgahUrl { get; set; }

        [Display(Name = "مجوز تاسیس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string MojavezTasisUrl { get; set; }

        [Display(Name = "شناسنامه صاحب امتیاز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string ShenasnameSahebEmtiazUrl { get; set; }

        [Display(Name = "کارت ملی صاحب امتیاز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string KarteMeliSahebEmtiazUrl { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string Address { get; set; }
    }

    [MetadataType(typeof(MdDoc))]
    public class TblDoc
    {

    }
}
