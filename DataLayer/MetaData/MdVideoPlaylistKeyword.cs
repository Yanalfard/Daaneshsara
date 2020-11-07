using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    public class MdVideoPlaylistKeyword
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "کلمه ی کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(length: 50, ErrorMessage = "طول بیش از 50 کاراکتر مجاز نیست")]
        public string Name { get; set; }

        [ForeignKey("VideoId")]
        public Nullable<int> VideoId { get; set; }

        [ForeignKey("PlaylistId")]
        public Nullable<int> PlaylistId { get; set; }
    }

    [MetadataType(typeof(MdVideoPlaylistKeyword))]
    public class TblVideoPlaylistKeyword
    {

    }
}
