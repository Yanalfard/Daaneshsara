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
        public int id { get; set; }
        
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 50, ErrorMessage = "طول بیش از 50 کاراکتر مجاز نیست")]
        public string Name { get; set; }
        
        [Display(Name = "شماره تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 10, ErrorMessage = "طول بیش از 10 کاراکتر مجاز نیست")]
        public string TellNo { get; set; }
        
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 150, ErrorMessage = "طول بیش از 150 کاراکتر مجاز نیست")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Display(Name = "بالانس مالی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Currency)]
        public int Balance { get; set; }
        
        [MaxLength(length: 256, ErrorMessage = "طول بیش از 256 کاراکتر مجاز نیست")]
        public string Auth { get; set; }
        
        public bool IsActive { get; set; }
        
        [Display(Name = "رمز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 64, ErrorMessage = "طول بیش از 64 کاراکتر مجاز نیست")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public Nullable<int> DocsId { get; set; }
        
        public int RoleId { get; set; }
    }

    [MetadataType(typeof(MdUser))]
    public class TblUser
    {

    }
}
