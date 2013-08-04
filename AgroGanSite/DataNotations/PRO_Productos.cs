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

    public partial class PRO_Productos
    {
        public PRO_Productos()
        {
            this.COD_CotizacionDetalle = new HashSet<COD_CotizacionDetalle>();
        }

        [Display(Name = "Código de Producto")]
        public int PRO_Id { get; set; }
        [Required]
        [Display(Name = "Estatus")]
        public int STS_Id { get; set; }

        [Required]
        [Display(Name = "Categoría")]
        public int CAT_Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Nombre de Producto")]
        public string PRO_Nombre { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Descripción de Producto")]
        public string PRO_Descripcion { get; set; }

        [Display(Name = "Contenido Producto")]
        public string PRO_HTML { get; set; }

        [Required]
        [Display(Name = "Es Banner")]
        public Nullable<bool> PRO_IsBanner { get; set; }

        [Required]
        [Display(Name = "Imagen del Producto")]
        [DataType(DataType.ImageUrl)]
        public string PRO_Image { get; set; }

        [Display(Name = "Fecha de Actualización")]
        public Nullable<System.DateTime> PRO_Date { get; set; }

        public virtual CAT_Categorias CAT_Categorias { get; set; }
        public virtual ICollection<COD_CotizacionDetalle> COD_CotizacionDetalle { get; set; }
        public virtual STS_Status STS_Status { get; set; }
    }
}
