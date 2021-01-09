using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class VmChangeOldNewPassword
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [MaxLength(30, ErrorMessage = "تعداد کاراکتر بیستر است")]
        public string OldPassword { get; set; }
        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [MaxLength(30, ErrorMessage = "تعداد کاراکتر بیستر است")]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [MaxLength(30, ErrorMessage = "تعداد کاراکتر بیستر است")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }
    public class VmChangePassword
    {
        public int Id { get; set; }
        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [MaxLength(30, ErrorMessage = "تعداد کاراکتر بیستر است")]
        public string Password { get; set; }
    }
    public class VmRecoveryPassword
    {
        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [MaxLength(30, ErrorMessage = "تعداد کاراکتر بیستر است")]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [MaxLength(30, ErrorMessage = "تعداد کاراکتر بیستر است")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }
}
