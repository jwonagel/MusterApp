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
    
    public partial class adresse
    {
        public adresse()
        {
            this.abrechnung = new HashSet<abrechnung>();
            this.lieferant = new HashSet<lieferant>();
            this.location = new HashSet<location>();
            this.kunde = new HashSet<kunde>();
        }
    
        public int id_adresse { get; set; }
        public string strasse { get; set; }
        public string hausnummer { get; set; }
        public string plz { get; set; }
        public string ort { get; set; }
        public string land { get; set; }
    
        public virtual ICollection<abrechnung> abrechnung { get; set; }
        public virtual ICollection<lieferant> lieferant { get; set; }
        public virtual ICollection<location> location { get; set; }
        public virtual ICollection<kunde> kunde { get; set; }
    }
}
