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
    
    public partial class TblDoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblDoc()
        {
            this.TblUser = new HashSet<TblUser>();
        }
    
        public int DocId { get; set; }
        public string TellSabet { get; set; }
        public string ParvaneAmuzeshgahUrl { get; set; }
        public string MojavezTasisUrl { get; set; }
        public string ShenasnameSahebEmtiazUrl { get; set; }
        public string KarteMeliSahebEmtiazUrl { get; set; }
        public string Address { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblUser> TblUser { get; set; }
    }
}
