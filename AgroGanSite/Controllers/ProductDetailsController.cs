using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroGanSite.Models;

namespace AgroGanSite.Controllers
{
    public class ProductDetailsController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /ProductDetails/
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int prod)
        {
            ViewBag.ProdId = prod;
            return View(db.PRD_ProductosDetalles.Where(c => c.PRO_Id == prod).ToList());
        }

        //
        // GET: /ProductDetails/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id = 0)
        {
            PRD_ProductosDetalles prd_productosdetalles = db.PRD_ProductosDetalles.Find(id);
            if (prd_productosdetalles == null)
            {
                return HttpNotFound();
            }
            return View(prd_productosdetalles);
        }

        //
        // GET: /ProductDetails/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int prod)
        {
            ViewBag.ProdId = prod;
            return View();
        }

        //
        // POST: /ProductDetails/Create
        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(PRD_ProductosDetalles prd_productosdetalles)
        {
            if (Session["prodid"] != null)
                prd_productosdetalles.PRO_Id = int.Parse(Session["prodid"].ToString());

            if (ModelState.IsValid)
            {
                db.PRD_ProductosDetalles.Add(prd_productosdetalles);
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "ProductDetails", new { prod = prd_productosdetalles.PRO_Id }) });
                }
                return RedirectToAction("Index", "ProductDetails", new { prod = prd_productosdetalles.PRO_Id });
            }
            else
            {
                throw new Exception("La operación no ha sido completada");
            }


            return View(prd_productosdetalles);
        }

        //
        // GET: /ProductDetails/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            PRD_ProductosDetalles prd_productosdetalles = db.PRD_ProductosDetalles.Find(id);
            if (prd_productosdetalles == null)
            {
                return HttpNotFound();
            }
            return View(prd_productosdetalles);
        }

        //
        // POST: /ProductDetails/Edit/5
        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(PRD_ProductosDetalles prd_productosdetalles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prd_productosdetalles).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index","ProductDetails", new { prod = prd_productosdetalles.PRO_Id }) });
                }
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("La operación no ha sido completada");
            }

            return View(prd_productosdetalles);
        }

        //
        // GET: /ProductDetails/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            PRD_ProductosDetalles prd_productosdetalles = db.PRD_ProductosDetalles.Find(id);
            if (prd_productosdetalles == null)
            {
                return HttpNotFound();
            }
            return View(prd_productosdetalles);
        }

        //
        // POST: /ProductDetails/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PRD_ProductosDetalles prd_productosdetalles = db.PRD_ProductosDetalles.Find(id);
            db.PRD_ProductosDetalles.Remove(prd_productosdetalles);
            db.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { redirectToUrl = Url.Action("Index", "ProductDetails", new { prod = prd_productosdetalles.PRO_Id }) });
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