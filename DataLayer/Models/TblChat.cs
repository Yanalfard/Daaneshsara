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
    
    public partial class TblChat
    {
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public string Message { get; set; }
        public System.DateTime TimeSent { get; set; }
    
        public virtual TblUser TblUser { get; set; }
        public virtual TblUser TblUser1 { get; set; }
    }
}
