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
    
    public partial class auditoriums
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public auditoriums()
        {
            this.shows = new HashSet<shows>();
        }
    
        public long id { get; set; }
        public string name { get; set; }
        public long cinema_id { get; set; }
        public short seats_total { get; set; }
    
        public virtual cinemas cinema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shows> shows { get; set; }
    }
}
