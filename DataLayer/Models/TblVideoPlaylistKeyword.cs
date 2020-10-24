//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DataLayer.MetaData;
    
    
    [MetadataType(typeof(TblVideoPlaylistKeyword))]
    public partial class TblVideoPlaylistKeyword
    {
        public int VideoPlaylistKeywordId { get; set; }
        public string Name { get; set; }
        public Nullable<int> VideoId { get; set; }
        public Nullable<int> PlaylistId { get; set; }
    
        public virtual TblPlaylist TblPlaylist { get; set; }
        public virtual TblVideo TblVideo { get; set; }
    }
}
