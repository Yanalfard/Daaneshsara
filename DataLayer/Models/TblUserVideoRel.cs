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
    
    public partial class TblUserVideoRel
    {
        public int UserVideoRelId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual TblUser TblUser { get; set; }
        public virtual TblVideo TblVideo { get; set; }
    }
}
