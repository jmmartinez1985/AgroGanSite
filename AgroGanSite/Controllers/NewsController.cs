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
    public class NewsController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /News/
        [Authorize]
        public ActionResult Index()
        {
            var new_noticias = db.NEW_Noticias.Include(n => n.STS_Status);
            return View(new_noticias.ToList());
        }

        public ActionResult GetNewToDisplay()
        {
            var newfiltered = db.NEW_Noticias.Where(c => c.STS_Id == 1)
                .OrderByDescending(c => c.NEW_Date).TakeLast(10).ToList();
            if (newfiltered == null)
            {
                return HttpNotFound();
            }
            else
            {
                string newlist = newfiltered.SerializeToJson();
                return Json(newlist);
            }
        }

        public ActionResult GetNewTop3()
        {
            var news = new List<NEW_Noticias>();
            news = db.NEW_Noticias.Where(c => c.STS_Id == 1).OrderByDescending(d => d.NEW_Date).Take(3).ToList();
            if (news == null)
            {
                return HttpNotFound();
            }
            else
            {
                string newslist = news.SerializeToJson();
                return Json(newslist);
            }
        }

        public ActionResult GetNewsId(int id)
        {
            int newid = id;

            var newidResult = db.NEW_Noticias.FirstOrDefault(c => c.NEW_Id == newid);

            return View(newidResult);
        }

        private bool ValidateNewsAvailable()
        {
            return db.NEW_Noticias.Count(c => c.STS_Id == 1) > 10 ? false : true;
        }

        //
        // GET: /News/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            NEW_Noticias new_noticias = db.NEW_Noticias.Find(id);
            if (new_noticias == null)
            {
                return HttpNotFound();
            }
            return View(new_noticias);
        }

        //
        // GET: /News/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion");
            return View();
        }

        //
        // POST: /News/Create
        [Authorize]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(NEW_Noticias new_noticias)
        {
            if (ModelState.IsValid)
            {
                if (ValidateNewsAvailable())
                    new_noticias.STS_Id = 2;
                db.NEW_Noticias.Add(new_noticias);
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "News") });
                }
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("La operación no ha sido completada");
            }

            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", new_noticias.STS_Id);
            return View(new_noticias);
        }

        //
        // GET: /News/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            NEW_Noticias new_noticias = db.NEW_Noticias.Find(id);
            if (new_noticias == null)
            {
                return HttpNotFound();
            }
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", new_noticias.STS_Id);
            return View(new_noticias);
        }

        //
        // POST: /News/Edit/5
        [Authorize]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(NEW_Noticias new_noticias)
        {
            if (ModelState.IsValid)
            {
                if (ValidateNewsAvailable())
                    new_noticias.STS_Id = 2;
                db.Entry(new_noticias).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "News") });
                }
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("La operación no ha sido completada");
            }

            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", new_noticias.STS_Id);
            return View(new_noticias);
        }

        //
        // GET: /News/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            NEW_Noticias new_noticias = db.NEW_Noticias.Find(id);
            if (new_noticias == null)
            {
                return HttpNotFound();
            }
            return View(new_noticias);
        }

        //
        // POST: /News/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NEW_Noticias new_noticias = db.NEW_Noticias.Find(id);
            db.NEW_Noticias.Remove(new_noticias);
            db.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { redirectToUrl = Url.Action("Index", "News") });
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