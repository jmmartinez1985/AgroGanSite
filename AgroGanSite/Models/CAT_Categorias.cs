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
    
    public partial class CAT_Categorias
    {
        public CAT_Categorias()
        {
            this.PRO_Productos = new HashSet<PRO_Productos>();
        }
    
        public int CAT_Id { get; set; }
        public string CAT_Descripcion { get; set; }
    
        public virtual ICollection<PRO_Productos> PRO_Productos { get; set; }
    }
}
