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
    
    public partial class person
    {
        public person()
        {
            this.pod_person = new HashSet<pod_person>();
        }
    
        public int id_person { get; set; }
        public string vorname { get; set; }
        public string nachname { get; set; }
        public string telefonnr { get; set; }
        public string email { get; set; }
    
        public virtual ICollection<pod_person> pod_person { get; set; }
    }
}
