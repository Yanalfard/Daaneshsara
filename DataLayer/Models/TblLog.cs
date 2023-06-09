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
    
    public partial class TblLog
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public short Status { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> VideoId { get; set; }
        public Nullable<int> PlayListId { get; set; }
        public bool IsVideo { get; set; }
        public Nullable<int> SellerId { get; set; }
        public Nullable<int> SellerAmount { get; set; }
    
        public virtual TblPlaylist TblPlaylist { get; set; }
        public virtual TblUser TblUser { get; set; }
        public virtual TblVideo TblVideo { get; set; }
    }
}
