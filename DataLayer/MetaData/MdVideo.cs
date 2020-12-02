using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    public class MdVideo
    {    
        [Key]
        public int id { get; set; }
        
        [Display(Name = "ویدیو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string VideoUrl { get; set; }
        
        [Display(Name = "دموی ویدیو")]
        [MaxLength(500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        public string VidioDemoUrl { get; set; }
        
        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "طول بیش از 300 کاراکتر مجاز نیست")]
        [DataType(DataType.ImageUrl)]
        public string MainImage { get; set; }
        
        [Display(Name = "تیتر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(256)]
        public string Title { get; set; }
        
        [Display(Name = "توضیحات")]
        [MaxLength(1000, ErrorMessage = "طول بیش از 1000 کاراکتر مجاز نیست")]
        public string Description { get; set; }
        
        [MaxLength(20)]
        [DataType(DataType.DateTime)]
        public string DateSubmited { get; set; }
        
        [Display(Name = "تعداد بازدید")]
        public int ViewCount { get; set; }
        
        [Display(Name = "نشان دادن در خانه؟")]
        public bool IsHome { get; set; }
        
        [Display(Name = "تعداد لایک")]
        public int LikeCount { get; set; }
        
        [Display(Name = "لینک")]
        [MaxLength(500, ErrorMessage = "طول بیش از 500 کاراکتر مجاز نیست")]
        [DataType(DataType.Url)]
        public string Link { get; set; }
        
        public int UserId { get; set; }
        
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }
        
        [Display(Name = "خیریه",Description = "با انتخاب این گزینه 5 درصد از درآمد شما از این مورد به سازمان های خیریه داده میشود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsCharity { get; set; }
        
        public Nullable<int> PlaylistId { get; set; }
        
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(MdVideo))]
    public class TblVideo
    {

    }
}
