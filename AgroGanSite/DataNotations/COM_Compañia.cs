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
    using System.ComponentModel.DataAnnotations;
    
    public partial class COM_Compañia
    {
        [Display(Name = "Código")]
        public int COM_Id { get; set; }
        [Required]
        [Display(Name = "Nombre Compañia")]
        public string COM_Nombre { get; set; }
        [Required]
        [Display(Name = "Dirección Compañia")]
        public string COM_Direccion { get; set; }
        [Required]
        [Display(Name = "Telefóno")]
        public string COM_Telefono { get; set; }
        [Required]
        [Display(Name = "Correo Electrónico")]
        public string COM_Correo { get; set; }

        [Display(Name = "Sitio Web")]
        public string COM_Web { get; set; }

        [Required]
        [Display(Name = "Nombre de Contacto")]
        public string COM_ContactoNombre { get; set; }
        [Required]
        [Display(Name = "Celular de Contacto")]
        public string COM_ContactoCelular { get; set; }
        [Required]
        [Display(Name = "Correo de Contacto")]
        public string COM_ContactoEmail { get; set; }
        public byte[] COM_Logo { get; set; }
    }
}
