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
    
    public partial class kunde
    {
        public kunde()
        {
            this.pod = new HashSet<pod>();
            this.zahlung = new HashSet<zahlung>();
            this.adresse = new HashSet<adresse>();
        }
    
        public int id_kunde { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<pod> pod { get; set; }
        public virtual ICollection<zahlung> zahlung { get; set; }
        public virtual ICollection<adresse> adresse { get; set; }
    }
}