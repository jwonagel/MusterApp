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
    
    public partial class logging
    {
        public Nullable<sbyte> severity { get; set; }
        public string lognachricht { get; set; }
        public System.DateTime zeitpunkt { get; set; }
        public int device_id_device { get; set; }
        public int id_logging { get; set; }
        public Nullable<sbyte> quittiert { get; set; }
    
        public virtual device device { get; set; }
    }
}
