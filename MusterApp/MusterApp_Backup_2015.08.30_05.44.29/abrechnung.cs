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
    
    public partial class abrechnung
    {
        public abrechnung()
        {
            this.position_abrechnung = new HashSet<position_abrechnung>();
            this.zahlung = new HashSet<zahlung>();
        }
    
        public int id_abrechnung { get; set; }
        public string referenz { get; set; }
        public int pod_id_pod { get; set; }
        public int adresse_id_adresse { get; set; }
        public System.DateTime fakturierungsdatum { get; set; }
    
        public virtual pod pod { get; set; }
        public virtual adresse adresse { get; set; }
        public virtual ICollection<position_abrechnung> position_abrechnung { get; set; }
        public virtual ICollection<zahlung> zahlung { get; set; }
    }
}
