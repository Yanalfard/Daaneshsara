using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    public class MdUser
    {
        [Key]
        public int UserId { get; set; }
        
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "طول بیش از 50 کاراکتر مجاز نیست")]
        public string Name { get; set; }
        
        [Display(Name = "شماره تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "طول بیش از 11 کاراکتر مجاز نیست")]
        public string TellNo { get; set; }
        
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "طول بیش از 150 کاراکتر مجاز نیست")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده نامعتبر است")]
        public string Email { get; set; }
        
        [Display(Name = "بالانس مالی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Currency)]
        public int Balance { get; set; }
        
        [MaxLength(256, ErrorMessage = "طول بیش از 256 کاراکتر مجاز نیست")]
        public string Auth { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public bool IsActive { get; set; }
        
        [Display(Name = "رمز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(64, ErrorMessage = "طول بیش از 64 کاراکتر مجاز نیست")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        public string Password { get; set; }
        
        public Nullable<int> DocsId { get; set; }
        [Display(Name = "نقش")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int RoleId { get; set; }
    }


}
