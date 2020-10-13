using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MetaData
{
    class MdTblUserPlaylistRel
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [ForeignKey("PlaylistId")]
        public int PlaylistId { get; set; }

        [DataType(DataType.DateTime)]
        public System.DateTime Date { get; set; }
    }
}
