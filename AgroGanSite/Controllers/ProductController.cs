using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroGanSite.Models;
using AgroGanSite.Extensions;

namespace AgroGanSite.Controllers
{
    public class ProductController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Product/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var pro_productos = db.PRO_Productos.Include(p => p.CAT_Categorias).Include(p => p.STS_Status);


            var firtscategory = db.CAT_Categorias.First();
            var allcategories = new SelectList(db.CAT_Categorias, "CAT_Id", "CAT_Descripcion", firtscategory);
            var productsfilter = db.PRO_Productos.Where(c => c.CAT_Id == firtscategory.CAT_Id);
            var firtsproduct = db.PRO_Productos.First();
            var allproductos = new SelectList(productsfilter, "PRO_Id", "PRO_Descripcion", firtsproduct);
            ViewBag.Products = allproductos;
            ViewBag.Categories = allcategories;

            return View(pro_productos.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetCurrentProducts()
        {
            var prodfiltered = db.PRO_Productos.Where(c => c.STS_Id == 1)
                .OrderByDescending(c => c.PRO_Date).TakeLast(10).ToList();
            if (prodfiltered == null)
            {
                return HttpNotFound();
            }
            else
            {
                string prodlist = prodfiltered.SerializeToJson();
                return Json(prodlist);
            }
        }

        //
        // GET: /Product/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id = 0)
        {
            PRO_Productos pro_productos = db.PRO_Productos.Find(id);
            if (pro_productos == null)
            {
                return HttpNotFound();
            }
            return View(pro_productos);
        }

        //
        // GET: /Product/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CAT_Id = new SelectList(db.CAT_Categorias, "CAT_Id", "CAT_Descripcion");
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion");
            return View();
        }

        //
        // POST: /Product/Create
        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(PRO_Productos pro_productos)
        {
            if (ModelState.IsValid)
            {
                db.PRO_Productos.Add(pro_productos);
                db.SaveChanges();
                ModelState.Clear();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "Product") });
                }
                return RedirectToAction("Index", "Product");
            }
            else
            {
                throw new Exception("La operación no ha sido completada");
            }

            ViewBag.CAT_Id = new SelectList(db.CAT_Categorias, "CAT_Id", "CAT_Descripcion", pro_productos.CAT_Id);
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", pro_productos.STS_Id);
            return View(pro_productos);
        }

        //
        // GET: /Product/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            PRO_Productos pro_productos = db.PRO_Productos.Find(id);
            if (pro_productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CAT_Id = new SelectList(db.CAT_Categorias, "CAT_Id", "CAT_Descripcion", pro_productos.CAT_Id);
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", pro_productos.STS_Id);
            return View(pro_productos);
        }

        //
        // POST: /Product/Edit/5
        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(PRO_Productos pro_productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pro_productos).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "Product") });
                }
                return RedirectToAction("Index", "Product");
            }
            else
            {
                throw new Exception("La operación no ha sido completada");
            }

            ViewBag.CAT_Id = new SelectList(db.CAT_Categorias, "CAT_Id", "CAT_Descripcion", pro_productos.CAT_Id);
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", pro_productos.STS_Id);

            return View(pro_productos);
        }

        //
        // GET: /Product/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            PRO_Productos pro_productos = db.PRO_Productos.Find(id);
            if (pro_productos == null)
            {
                return HttpNotFound();
            }
            return View(pro_productos);
        }

        //
        // POST: /Product/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PRO_Productos pro_productos = db.PRO_Productos.Find(id);
            db.PRO_Productos.Remove(pro_productos);
            db.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { redirectToUrl = Url.Action("Index", "Product") });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}