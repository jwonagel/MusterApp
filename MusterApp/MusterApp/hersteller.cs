//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusterApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class hersteller
    {
        public hersteller()
        {
            this.device_typ = new HashSet<device_typ>();
            this.lieferant = new HashSet<lieferant>();
        }
    
        public int id_hersteller { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<device_typ> device_typ { get; set; }
        public virtual ICollection<lieferant> lieferant { get; set; }
    }
}
