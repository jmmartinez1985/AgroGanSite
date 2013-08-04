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

    public partial class PRD_ProductosDetalles
    {
        [Display(Name = "Código")]
        public int PRD_Id { get; set; }
        [Required]
        [Display(Name = "Producto")]
        public int PRO_Id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Descripción")]
        public string PRD_Descripcion { get; set; }
        [Display(Name = "Contenido")]
        public string PRD_Html { get; set; }

        [Display(Name = "Imagen del Detalle")]
        [DataType(DataType.ImageUrl)]
        public string PRD_Images { get; set; }
        [Display(Name = "Fecha de Actualización")]
        public Nullable<System.DateTime> PRD_Date { get; set; }
    }
}
