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
    
    public partial class administrativ_credentials
    {
        public administrativ_credentials()
        {
            this.administrativ_credentials_snmp_comunity = new HashSet<administrativ_credentials_snmp_comunity>();
        }
    
        public int id_administrative_credentials { get; set; }
        public string benutzer { get; set; }
        public string passwort { get; set; }
    
        public virtual ICollection<administrativ_credentials_snmp_comunity> administrativ_credentials_snmp_comunity { get; set; }
    }
}
