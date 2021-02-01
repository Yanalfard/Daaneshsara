using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Web.Mvc;

namespace DataLayer.MetaData
{
    public class MdConfig
    {
        public int ConfigId { get; set; }
      
        public Nullable<short> DarsadeSud { get; set; }
        public Nullable<int> SaqfeBardasht { get; set; }
        [Display(Name = " درباره ما ")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string AboutUsBody { get; set; }
        [Display(Name = " تماس با ما ")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Rules { get; set; }
    }
}
