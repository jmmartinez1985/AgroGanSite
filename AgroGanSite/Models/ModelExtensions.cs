using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroGanSite.Models
{
    public class ModelExtensions
    {
    }

    [MetadataType(typeof(NEW_NoticiasMetaData))]
    public partial class NEW_Noticias
    {
    }

    [MetadataType(typeof(CAT_CategoriasMetaData))]
    public partial class CAT_Categorias
    {
    }

    [MetadataType(typeof(COD_CotizacionDetalleMetaData))]
    public partial class COD_CotizacionDetalle
    {
    }

    [MetadataType(typeof(COM_CompañiaMetaData))]
    public partial class COM_Compañia
    {
    }

    [MetadataType(typeof(CON_ContactenosMetaData))]
    public partial class CON_Contactenos
    {
    }

    [MetadataType(typeof(COT_CotizacionesMetaData))]
    public partial class COT_Cotizaciones
    {
    }

    [MetadataType(typeof(PRC_PantallaInicialMetaData))]
    public partial class PRC_PantallaInicial
    {
    }

    [MetadataType(typeof(PRD_ProductosDetallesMetaData))]
    public partial class PRD_ProductosDetalles
    {
    }

    [MetadataType(typeof(PRO_ProductosMetaData))]
    public partial class PRO_Productos
    {
    }

    [MetadataType(typeof(PTC_PatrocinadoresMetaData))]
    public partial class PTC_Patrocinadores
    {
    }

    [MetadataType(typeof(STS_StatusMetaData))]
    public partial class STS_Status
    {
    }
    [MetadataType(typeof(SER_ServiciosMetaData))]
    public partial class SER_Servicios
    {
    }

    public partial class NEW_NoticiasMetaData
    {
        [Display(Name = "Noticia Id")]
        public int NEW_Id { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Descripción Noticia")]
        public string NEW_Descripcion { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Titulo Noticia")]
        public string NEW_Titulo { get; set; }
        [Display(Name = "Contenido de la Noticia")]
        public string NEW_HTML { get; set; }
        [Required]
        [Display(Name = "Estatus")]
        public int STS_Id { get; set; }

        [Required]
        [Display(Name = "Imagen Noticia")]
        [DataType(DataType.ImageUrl)]
        public string NEW_Imagen { get; set; }
        [Display(Name = "Fecha Noticia")]
        public Nullable<System.DateTime> NEW_Date { get; set; }

        [Display(Name = "Creada por")]
        public string NEW_CreatedBy { get; set; }

        [JsonIgnore]
        public virtual STS_Status STS_Status { get; set; }

    }

    public partial class CAT_CategoriasMetaData
    {

        [Display(Name = "Categoría Id")]
        public int CAT_Id { get; set; }
        [Display(Name = "Descripción Categoría")]
        public string CAT_Descripcion { get; set; }
        [JsonIgnore]
        public virtual ICollection<PRO_Productos> PRO_Productos { get; set; }

    }

    public partial class COD_CotizacionDetalleMetaData
    {

        [Display(Name = "Detalle Id")]
        public int CED_Id { get; set; }
        [Required]
        [Display(Name = "Cotización Id")]
        public int COD_Id { get; set; }
        [Required]
        [Display(Name = "Producto Id")]
        public int PRO_Id { get; set; }
        [Required]
        [Display(Name = "Cantidad")]
        public int COD_Cantidad { get; set; }
        [JsonIgnore]
        public virtual COT_Cotizaciones COT_Cotizaciones { get; set; }
           [JsonIgnore]
        public virtual PRO_Productos PRO_Productos { get; set; }

    }

    public partial class COM_CompañiaMetaData
    {
        [Display(Name = "Compañia Id")]
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

    public partial class CON_ContactenosMetaData
    {
        [Display(Name = "Contacto Id")]
        public int CON_Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Nombre Contacto")]
        public string CON_Nombre { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Telefóno")]
        public string CON_Telefono { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Celular")]
        public string CON_Celular { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electrónico")]
        public string CON_Email { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Mensaje o Comentario")]
        public string CON_Mensaje { get; set; }

        [Display(Name = "Fecha Contacto")]
        public System.DateTime CON_Fecha { get; set; }
    }

    public partial class COT_CotizacionesMetaData
    {

        [Display(Name = "Código")]
        public int COT_Id { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Mensaje")]
        public string COT_Descripcion { get; set; }
        public Nullable<System.DateTime> COT_Fecha { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Nombre")]
        public string COT_Nombre { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Telefóno")]
        public string COT_Telefono { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Correo Electrónico")]
        public string COT_Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Celular")]
        public string COT_Celular { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Más información")]
        public string COT_Mensaje { get; set; }
        [Required]
        [Display(Name = "Estatus")]
        public int STS_Id { get; set; }
        [JsonIgnore]
        public virtual ICollection<COD_CotizacionDetalle> COD_CotizacionDetalle { get; set; }
        [JsonIgnore]
        public virtual STS_Status STS_Status { get; set; }

    }

    public partial class PRC_PantallaInicialMetaData
    {
        [Display(Name = "Pantalla Id")]
        public int PRC_Id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Titulo Inicial")]
        public string PRC_Text { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Descripción")]
        public string PRC_Descripcion { get; set; }
        [Display(Name = "Contenido Pantalla")]
        public string PRC_Html { get; set; }
        [Required]
        [Display(Name = "Imagen Pantalla Inicial")]
        [DataType(DataType.ImageUrl)]
        public string PRC_Imagen { get; set; }
        [Required]
        [Display(Name = "Estatus")]
        public Nullable<int> STS_Id { get; set; }
        [Display(Name = "Fecha Creación")]
        public Nullable<System.DateTime> PRC_Date { get; set; }
        [JsonIgnore]
        public virtual STS_Status STS_Status { get; set; }

    }

    public partial class PRD_ProductosDetallesMetaData
    {
        [Display(Name = "Detalle Id")]
        public int PRD_Id { get; set; }
        [Required]
        [Display(Name = "Producto Id")]
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

    public partial class PRO_ProductosMetaData
    {

        [Display(Name = "Producto Id")]
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
        [JsonIgnore]
        public virtual CAT_Categorias CAT_Categorias { get; set; }
        [JsonIgnore]
        public virtual ICollection<COD_CotizacionDetalle> COD_CotizacionDetalle { get; set; }
        [JsonIgnore]
        public virtual STS_Status STS_Status { get; set; }

    }

    public partial class PTC_PatrocinadoresMetaData
    {
        [Display(Name = "Patrocinador Id")]
        public int PTC_Id { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Nombre de Patrocinador")]
        public string PTC_Nombre { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Descripción")]
        public string PTC_Descripcion { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Dirección Web del Patrocinador")]
        public string PTC_Url { get; set; }
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Imagen del Patrocinador")]
        public string PTC_Imagen { get; set; }
        [Required]
        [Display(Name = "Estatus")]
        public int STS_Id { get; set; }
        [JsonIgnore]
        public virtual STS_Status STS_Status { get; set; }

    }

    public partial class STS_StatusMetaData
    {

        [Display(Name = "Estatus Id")]
        public int STS_Id { get; set; }
        [Display(Name = "Descripción Estatus")]
        public string STS_Descripcion { get; set; }
        [JsonIgnore]
        public virtual ICollection<BAN_Banner> BAN_Banner { get; set; }
        [JsonIgnore]
        public virtual ICollection<COT_Cotizaciones> COT_Cotizaciones { get; set; }
        [JsonIgnore]
        public virtual ICollection<NEW_Noticias> NEW_Noticias { get; set; }
        [JsonIgnore]
        public virtual ICollection<PRC_PantallaInicial> PRC_PantallaInicial { get; set; }
        [JsonIgnore]
        public virtual ICollection<PRO_Productos> PRO_Productos { get; set; }
        [JsonIgnore]
        public virtual ICollection<PTC_Patrocinadores> PTC_Patrocinadores { get; set; }
        [JsonIgnore]
        public virtual ICollection<SER_Servicios> SER_Servicios { get; set; }

    }

    public partial class SER_ServiciosMetaData
    {

        [Display(Name = "Servicio Id")]
        public int SER_Id { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public string SER_Descripcion { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string SER_Titulo { get; set; }

        [Display(Name = "Contenido")]
        public string SER_HTML { get; set; }


        public int STS_Id { get; set; }

        [Required]
        [Display(Name = "Imagen del Servicio")]
        public string SER_Imagen { get; set; }

        [Display(Name = "Fecha de Creación")]
        public Nullable<System.DateTime> SER_Date { get; set; }

        [JsonIgnore]
        public virtual STS_Status STS_Status { get; set; }
    }
}