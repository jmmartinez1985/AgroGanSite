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
    public class PrincipalInfoController : Controller
    {
        private Entities db = new Entities();

        //66303711  
        // GET: /PrincipalInfo/
          [Authorize]
        public ActionResult Index()
        {
            var prc_pantallainicial = db.PRC_PantallaInicial.Include(p => p.STS_Status);
            return View(prc_pantallainicial.ToList());
        }

        //
        // GET: /PrincipalInfo/Details/5
          [Authorize]
        public ActionResult Details(int id = 0)
        {
            PRC_PantallaInicial prc_pantallainicial = db.PRC_PantallaInicial.Find(id);
            if (prc_pantallainicial == null)
            {
                return HttpNotFound();
            }
            return View(prc_pantallainicial);
        }

        //
        // GET: /PrincipalInfo/Create
          [Authorize]
        public ActionResult Create()
        {
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion");
            return View();
        }

        //
        // POST: /PrincipalInfo/Create
        [Authorize]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(PRC_PantallaInicial prc_pantallainicial)
        {
            if (ModelState.IsValid)
            {
                db.PRC_PantallaInicial.Add(prc_pantallainicial);
                db.SaveChanges();

                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "PrincipalInfo") });
                }

            }
            else
            {
                throw new Exception("La operación no ha sido completada");
            }

            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", prc_pantallainicial.STS_Id);
            return View(prc_pantallainicial);
        }

        //
        // GET: /PrincipalInfo/Edit/5
          [Authorize]
        public ActionResult Edit(int id = 0)
        {
            PRC_PantallaInicial prc_pantallainicial = db.PRC_PantallaInicial.Find(id);
            if (prc_pantallainicial == null)
            {
                return HttpNotFound();
            }
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", prc_pantallainicial.STS_Id);
            return View(prc_pantallainicial);
        }

        //
        // POST: /PrincipalInfo/Edit/5
        [Authorize]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(PRC_PantallaInicial prc_pantallainicial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prc_pantallainicial).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "PrincipalInfo") });
                }
            }
            else
            {
                throw new Exception("La operación no ha sido completada");
            }

            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", prc_pantallainicial.STS_Id);
            return View(prc_pantallainicial);
        }

        //
        // GET: /PrincipalInfo/Delete/5
          [Authorize]
        public ActionResult Delete(int id = 0)
        {
            PRC_PantallaInicial prc_pantallainicial = db.PRC_PantallaInicial.Find(id);
            if (prc_pantallainicial == null)
            {
                return HttpNotFound();
            }
            return View(prc_pantallainicial);
        }

        //
        // POST: /PrincipalInfo/Delete/5
        [Authorize]
        [ValidateInput(false)]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PRC_PantallaInicial prc_pantallainicial = db.PRC_PantallaInicial.Find(id);
            db.PRC_PantallaInicial.Remove(prc_pantallainicial);
            db.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { redirectToUrl = Url.Action("Index", "PrincipalInfo") });
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