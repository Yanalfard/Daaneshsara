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
    
    public partial class TblPlaylist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblPlaylist()
        {
            this.TblUserPlaylistRel = new HashSet<TblUserPlaylistRel>();
            this.TblVideo = new HashSet<TblVideo>();
            this.TblVideoPlaylistKeyword = new HashSet<TblVideoPlaylistKeyword>();
            this.TblLog = new HashSet<TblLog>();
        }
    
        public int PlaylistId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateSubmited { get; set; }
        public int ViewCount { get; set; }
        public bool IsHome { get; set; }
        public string Link { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public bool IsCharity { get; set; }
        public bool IsActive { get; set; }
        public string CertificateURL { get; set; }
        public int CatagoryId { get; set; }
    
        public virtual TblCatagory TblCatagory { get; set; }
        public virtual TblUser TblUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblUserPlaylistRel> TblUserPlaylistRel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblVideo> TblVideo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblVideoPlaylistKeyword> TblVideoPlaylistKeyword { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblLog> TblLog { get; set; }
    }
}
