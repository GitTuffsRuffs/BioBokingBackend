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
    
    public partial class authentication
    {
        public long id { get; set; }
        public string service { get; set; }
        public string identyfier { get; set; }
        public string secret { get; set; }
        public long user_id { get; set; }
    
        public virtual users user { get; set; }
    }
}
