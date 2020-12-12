using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    public class MdRole
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "نام سیستمی")]
        [Required]
        [MaxLength(50, ErrorMessage = "طول بیش از 50 کاراکتر مجاز نیست")]
        public string Name { get; set; }

        [Display(Name = "نوع کاربر")]
        [Required]
        [MaxLength(50, ErrorMessage = "طول بیش از 50 کاراکتر مجاز نیست")]
        public string Title { get; set; }
    }

}
