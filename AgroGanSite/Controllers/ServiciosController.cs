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
    public class ServiciosController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Servicios/

        public ActionResult Index()
        {
            var ser_servicios = db.SER_Servicios.Include(s => s.STS_Status);
            return View(ser_servicios.ToList());
        }

        //
        // GET: /Servicios/Details/5

        public ActionResult Details(int id = 0)
        {
            SER_Servicios ser_servicios = db.SER_Servicios.Find(id);
            if (ser_servicios == null)
            {
                return HttpNotFound();
            }
            return View(ser_servicios);
        }

        //
        // GET: /Servicios/Create

        public ActionResult Create()
        {
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion");
            return View();
        }

        //
        // POST: /Servicios/Create

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SER_Servicios ser_servicios)
        {
            if (ModelState.IsValid)
            {
                db.SER_Servicios.Add(ser_servicios);
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "Servicios") });
                }
                return RedirectToAction("Index");
            }

            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", ser_servicios.STS_Id);
            return View(ser_servicios);
        }

        //
        // GET: /Servicios/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SER_Servicios ser_servicios = db.SER_Servicios.Find(id);
            if (ser_servicios == null)
            {
                return HttpNotFound();
            }
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", ser_servicios.STS_Id);
            return View(ser_servicios);
        }

        //
        // POST: /Servicios/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SER_Servicios ser_servicios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ser_servicios).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "Servicios") });
                }
                return RedirectToAction("Index");
            }
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", ser_servicios.STS_Id);
            return View(ser_servicios);
        }

        //
        // GET: /Servicios/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SER_Servicios ser_servicios = db.SER_Servicios.Find(id);
            if (ser_servicios == null)
            {
                return HttpNotFound();
            }
            return View(ser_servicios);
        }

        //
        // POST: /Servicios/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SER_Servicios ser_servicios = db.SER_Servicios.Find(id);
            db.SER_Servicios.Remove(ser_servicios);
            db.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { redirectToUrl = Url.Action("Index", "Servicios") });
            }
            return RedirectToAction("Index", "Servicios");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}