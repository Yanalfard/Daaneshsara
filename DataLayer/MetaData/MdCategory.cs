using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    public class MdCategory
    {
        public int CatagoryId { get; set; }
        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "طول بیش از 50 کاراکتر مجاز نیست")]
        [MinLength(3, ErrorMessage = "طول کاراکتر کمتر است")]
        public string Name { get; set; }
    }

}
