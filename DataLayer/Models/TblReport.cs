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
    
    
    [MetadataType(typeof(TblReport))]
    public partial class TblReport
    {
        public int id { get; set; }
        public int VideoId { get; set; }
        public string Text { get; set; }
        public System.DateTime DateSent { get; set; }
    
        public virtual TblVideo TblVideo { get; set; }
    }
}
