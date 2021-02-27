using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    public class MdPlaylist
    {
        public int PlaylistId { get; set; }

        [Display(Name = "نام کلاس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(256, ErrorMessage = "طول بیش از 256 کاراکتر مجاز نیست")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        [MaxLength(500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public string DateSubmited { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int ViewCount { get; set; }
        public int CatagoryChildsId { get; set; }

        [Display(Name = "در خانه نشان داده شود؟")]
        public bool IsHome { get; set; }

        [Display(Name = "لینک")]
        public string Link { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }
        [Display(Name = "خیریه")]
        public bool IsCharity { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "مدرک")]
        public string CertificateURL { get; set; }
        [Display(Name = "زیر دسته")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CatagoryId { get; set; }
        public int Catagory { get; set; }
    }

}
