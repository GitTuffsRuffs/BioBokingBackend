//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BioBokingMSSQLdatabase.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class payments
    {
        public long id { get; set; }
        public long reservation_id { get; set; }
        public byte[] payed_at { get; set; }
        public long amount { get; set; }
        public string refernece { get; set; }
    
        public virtual reservations reservations { get; set; }
    }
}
