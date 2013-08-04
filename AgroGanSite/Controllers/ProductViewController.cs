using AgroGanSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroGanSite.Extensions;

namespace AgroGanSite.Controllers
{
    public class ProductViewController : Controller
    {
        //
        // GET: /ProductView/

        private Entities db = new Entities();


        public ActionResult GetCategories()
        {
            var categories = new List<CAT_Categorias>();
            categories = db.CAT_Categorias.ToList();
            if (categories == null)
            {
                return HttpNotFound();
            }
            else
                return View(categories);
        }

  
        public ActionResult GetProductsByCategory(int catid)
        {
            var productos = new List<PRO_Productos>();
            productos = db.PRO_Productos.Where(c => c.CAT_Id == catid && c.STS_Id==1).ToList();
            if (productos == null)
            {
                return HttpNotFound();
            }
            else
            {
                string productlist = productos.SerializeToJson();
                return Json(productlist);
            }
        }


        public ActionResult GetProductsBanners()
        {
            try
            {
                var productos = new List<PRO_Productos>();
                productos = db.PRO_Productos.Where(c => c.PRO_IsBanner == true).ToList();
                if (productos == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    string productlist = productos.SerializeToJson();
                    return Json(productlist);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public ActionResult GetInfoPage()
        {
            try
            {
                var detalle = new List<PRC_PantallaInicial>();
                detalle = db.PRC_PantallaInicial.Where(c => c.STS_Id == 1).ToList();
                if (detalle == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    string detallelist = detalle.SerializeToJson();
                    return Json(detallelist);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public ActionResult GetProductsDetails(int prod)
        {
            var productos = new List<PRD_ProductosDetalles>();
            productos = db.PRD_ProductosDetalles.Where(c => c.PRO_Id == prod ).ToList();
            if (productos == null)
            {
                return HttpNotFound();
            }
            else
            {
                string productlist = productos.SerializeToJson();
                return Json(productlist);

            }
        }



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductsDisplay()
        {
            var firtscategory = db.CAT_Categorias.First();
            var allcategories = new SelectList(db.CAT_Categorias, "CAT_Id", "CAT_Descripcion", firtscategory);
            var productsfilter = db.PRO_Productos.Where(c=>c.CAT_Id == firtscategory.CAT_Id);
            var firtsproduct = db.PRO_Productos.First();
            var allproductos = new SelectList(productsfilter, "PRO_Id", "PRO_Descripcion", firtsproduct);
            ViewBag.Products = allproductos;
            ViewBag.Categories = allcategories;
            return View();
        }


    }
}
