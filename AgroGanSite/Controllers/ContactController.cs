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
    public class ContactController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Contact/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.CON_Contactenos.ToList());
        }


        [Authorize]
        public ActionResult GetCurrentContacts()
        {
            var contfiltered = db.CON_Contactenos.
                OrderByDescending(c => c.CON_Fecha).TakeLast(10).ToList();
            if (contfiltered == null)
            {
                return HttpNotFound();
            }
            else
            {
                string contlist = contfiltered.SerializeToJson();
                return Json(contlist);
            }
        }

        //
        // GET: /Contact/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            CON_Contactenos con_contactenos = db.CON_Contactenos.Find(id);
            if (con_contactenos == null)
            {
                return HttpNotFound();
            }
            return View(con_contactenos);
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(CON_Contactenos con_contactenos)
        {
            if (ModelState.IsValid)
            {
                con_contactenos.CON_Fecha = System.DateTime.Now;
                db.CON_Contactenos.Add(con_contactenos);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(con_contactenos);
        }

        //
        // GET: /Contact/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            CON_Contactenos con_contactenos = db.CON_Contactenos.Find(id);
            if (con_contactenos == null)
            {
                return HttpNotFound();
            }
            return View(con_contactenos);
        }

        //
        // POST: /Contact/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(CON_Contactenos con_contactenos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(con_contactenos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(con_contactenos);
        }

        //
        // GET: /Contact/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            CON_Contactenos con_contactenos = db.CON_Contactenos.Find(id);
            if (con_contactenos == null)
            {
                return HttpNotFound();
            }
            return View(con_contactenos);
        }

        //
        // POST: /Contact/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CON_Contactenos con_contactenos = db.CON_Contactenos.Find(id);
            db.CON_Contactenos.Remove(con_contactenos);
            db.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { redirectToUrl = Url.Action("Index", "Contact") });
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