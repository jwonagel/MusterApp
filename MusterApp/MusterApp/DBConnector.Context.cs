﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MusterContext : DbContext
    {
        public MusterContext()
            : base("MusterContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<abrechnung> abrechnung { get; set; }
        public virtual DbSet<administrativ_credentials> administrativ_credentials { get; set; }
        public virtual DbSet<administrativ_credentials_snmp_comunity> administrativ_credentials_snmp_comunity { get; set; }
        public virtual DbSet<adresse> adresse { get; set; }
        public virtual DbSet<device> device { get; set; }
        public virtual DbSet<device_typ> device_typ { get; set; }
        public virtual DbSet<geraetebezeichnung> geraetebezeichnung { get; set; }
        public virtual DbSet<geraetetyp> geraetetyp { get; set; }
        public virtual DbSet<hersteller> hersteller { get; set; }
        public virtual DbSet<kunde> kunde { get; set; }
        public virtual DbSet<lieferant> lieferant { get; set; }
        public virtual DbSet<location> location { get; set; }
        public virtual DbSet<logging> logging { get; set; }
        public virtual DbSet<netzwerkinterface> netzwerkinterface { get; set; }
        public virtual DbSet<person> person { get; set; }
        public virtual DbSet<pod> pod { get; set; }
        public virtual DbSet<position_abrechnung> position_abrechnung { get; set; }
        public virtual DbSet<snmp_comunity> snmp_comunity { get; set; }
        public virtual DbSet<vlan> vlan { get; set; }
        public virtual DbSet<zahlung> zahlung { get; set; }
        public virtual DbSet<pod_person> pod_person { get; set; }
    
        public virtual int LogClear(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LogClear", idParameter);
        }
    }
}
