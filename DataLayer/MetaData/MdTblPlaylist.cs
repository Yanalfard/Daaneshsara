using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    class MdTblPlaylist
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "تیتر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 256, ErrorMessage = "طول بیش از 256 کاراکتر مجاز نیست")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(length: 500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public string DateSubmited { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int ViewCount { get; set; }

        [Display(Name = "در خانه نشان داده شود؟")]
        public bool IsHome { get; set; }

        [Display(Name = "لینک")]
        [MaxLength(length: 500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        [DataType(DataType.Url)]
        public string Link { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Display(Name = "خیریه",Description = "با انتخاب این گزینه 5 درصد از درآمد شما از این مورد به سازمان های خیریه داده میشود")]
        public bool IsCharity { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "مدرک",Description = "مدرک در انتهای دیدن تمام ویدیو های موجود در لیست به دانشجو داده میشود")]
        [MaxLength(length: 256, ErrorMessage = "طول بیش از 256 کاراکتر مجاز نیست")]
        public string CertificateURL { get; set; }
    }
}
