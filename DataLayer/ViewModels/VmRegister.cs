using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class VmRegister
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 50, ErrorMessage = "طول بیش از 50 کاراکتر مجاز نیست")]
        public string Name { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 150, ErrorMessage = "طول بیش از 150 کاراکتر مجاز نیست")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده نامعتبر است")]
        public string Email { get; set; }
        [Display(Name = "رمز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [MaxLength(length: 64, ErrorMessage = "طول بیش از 64 کاراکتر مجاز نیست")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 64, ErrorMessage = "طول بیش از 64 کاراکتر مجاز نیست")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }
}
