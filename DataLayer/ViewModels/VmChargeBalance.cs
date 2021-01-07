using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class VmChargeBalance
    {
        [Display(Name = "مبلغ شارژ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "طول بیش از 20 کاراکتر مجاز نیست")]
        [MinLength(4, ErrorMessage = "طول کمتر از 4 کاراکتر مجاز نیست")]
        [DataType(DataType.PhoneNumber)]
        public int ChargeBalance { get; set; }
    }
}
