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
    
    
    [MetadataType(typeof(TblUser))]
    public partial class TblUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblUser()
        {
            this.TblChats = new HashSet<TblChat>();
            this.TblChats1 = new HashSet<TblChat>();
            this.TblPlaylists = new HashSet<TblPlaylist>();
            this.TblTickets = new HashSet<TblTicket>();
            this.TblUserPlaylistRels = new HashSet<TblUserPlaylistRel>();
            this.TblUserVideoRels = new HashSet<TblUserVideoRel>();
            this.TblWithdraws = new HashSet<TblWithdraw>();
            this.TblVideos = new HashSet<TblVideo>();
        }
    
        public int UserId { get; set; }
        public string Name { get; set; }
        public string TellNo { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; }
        public string Auth { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public Nullable<int> DocsId { get; set; }
        public int RoleId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblChat> TblChats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblChat> TblChats1 { get; set; }
        public virtual TblDoc TblDoc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblPlaylist> TblPlaylists { get; set; }
        public virtual TblRole TblRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblTicket> TblTickets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblUserPlaylistRel> TblUserPlaylistRels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblUserVideoRel> TblUserVideoRels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblWithdraw> TblWithdraws { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblVideo> TblVideos { get; set; }
    }
}
