//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgroGanSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SER_Servicios
    {
        public int SER_Id { get; set; }
        public string SER_Descripcion { get; set; }
        public string SER_Titulo { get; set; }
        public string SER_HTML { get; set; }
        public int STS_Id { get; set; }
        public string SER_Imagen { get; set; }
        public Nullable<System.DateTime> SER_Date { get; set; }
    
        public virtual STS_Status STS_Status { get; set; }
    }
}
