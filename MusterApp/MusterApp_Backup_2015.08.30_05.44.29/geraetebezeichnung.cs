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
    
    public partial class geraetebezeichnung
    {
        public geraetebezeichnung()
        {
            this.device_typ = new HashSet<device_typ>();
        }
    
        public int id_geraetebezeichnung { get; set; }
        public string geraetebezeichnung1 { get; set; }
    
        public virtual ICollection<device_typ> device_typ { get; set; }
    }
}