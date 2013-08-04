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
    public class SponsorController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Sponsor/
        [Authorize]
        public ActionResult Index()
        {
            var ptc_patrocinadores = db.PTC_Patrocinadores.Include(p => p.STS_Status);
            return View(ptc_patrocinadores.ToList());
        }

        public ActionResult GetSponsor()
        {
            var ptc_patrocinadores = db.PTC_Patrocinadores.Where(c => c.STS_Id == 1).ToList();
            if (ptc_patrocinadores == null)
            {
                return HttpNotFound();
            }
            else
            {
                string patronicadoresList = ptc_patrocinadores.SerializeToJson();
                return Json(patronicadoresList);
            }

        }

        //
        // GET: /Sponsor/Details/5

        public ActionResult Details(int id = 0)
        {
            PTC_Patrocinadores ptc_patrocinadores = db.PTC_Patrocinadores.Find(id);
            if (ptc_patrocinadores == null)
            {
                return HttpNotFound();
            }
            return View(ptc_patrocinadores);
        }

        //
        // GET: /Sponsor/Create

        public ActionResult Create()
        {
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion");
            return View();
        }

        //
        // POST: /Sponsor/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PTC_Patrocinadores ptc_patrocinadores)
        {
            if (ModelState.IsValid)
            {
                db.PTC_Patrocinadores.Add(ptc_patrocinadores);
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "Sponsor") });
                }
                return RedirectToAction("Index", "Sponsor");
            }
            else
            {
                throw new Exception("La operación no ha sido completada");
            }

            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", ptc_patrocinadores.STS_Id);
            return View(ptc_patrocinadores);
        }

        //
        // GET: /Sponsor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PTC_Patrocinadores ptc_patrocinadores = db.PTC_Patrocinadores.Find(id);
            if (ptc_patrocinadores == null)
            {
                return HttpNotFound();
            }
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", ptc_patrocinadores.STS_Id);
            return View(ptc_patrocinadores);
        }

        //
        // POST: /Sponsor/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PTC_Patrocinadores ptc_patrocinadores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ptc_patrocinadores).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "Sponsor") });
                }
                return RedirectToAction("Index", "Sponsor");
            }
            else
            {
                throw new Exception("La operación no ha sido completada");
            }
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", ptc_patrocinadores.STS_Id);
            return View(ptc_patrocinadores);
        }

        //
        // GET: /Sponsor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PTC_Patrocinadores ptc_patrocinadores = db.PTC_Patrocinadores.Find(id);
            if (ptc_patrocinadores == null)
            {
                return HttpNotFound();
            }
            return View(ptc_patrocinadores);
        }

        //
        // POST: /Sponsor/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PTC_Patrocinadores ptc_patrocinadores = db.PTC_Patrocinadores.Find(id);
            db.PTC_Patrocinadores.Remove(ptc_patrocinadores);
            db.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { redirectToUrl = Url.Action("Index", "Sponsor") });
            }
            return RedirectToAction("Index", "Sponsor");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}