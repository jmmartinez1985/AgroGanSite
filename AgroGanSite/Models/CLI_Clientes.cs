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
    
    public partial class CLI_Clientes
    {
        public int CLI_Id { get; set; }
        public int STS_Id { get; set; }
        public string CLI_Nombre { get; set; }
        public string CLI_Contacto { get; set; }
        public string CLI_Direccion { get; set; }
        public string CLI_Telefono1 { get; set; }
        public string CLI_Telefono2 { get; set; }
        public string CLI_Email { get; set; }
        public Nullable<System.DateTime> CLI_Nacimiento { get; set; }
        public string CLI_Cedula { get; set; }
    }
}
