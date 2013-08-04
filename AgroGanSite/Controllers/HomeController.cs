using AgroGanSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroGanSite.Extensions;
using System.Data;

namespace AgroGanSite.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Ubicacion()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult UnderConstruction()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult NewsView()
        {
            return PartialView("NewsView");
        }

        [Authorize]
        public ActionResult ImagesChooser()
        {
            Entities db = new Entities();
            var allimages = db.IMG_Images.ToList();
            return PartialView("ImagesChooser", allimages);
        }

        public ActionResult Contact()
        {
            COM_Compañia company = db.COM_Compañia.FirstOrDefault();
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Información de Contacto";
            string jsonobject = company.SerializeToJson();
            return Json(jsonobject);

        }

        [Authorize]
        public ActionResult ViewUploader()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetAllStats()
        {
            var c_cont = db.CON_Contactenos.Count();
            var c_cot = db.COT_Cotizaciones.Count();
            var c_prod = db.PRO_Productos.Where(c => c.STS_Id == 1).Count();
            var c_news = db.NEW_Noticias.Where(c => c.STS_Id == 1).Count();
            var statObject = new Stats();
            statObject.Contactos = c_cont;
            statObject.Cotizaciones = c_cot;
            statObject.Productos = c_prod;
            statObject.Noticias = c_news;
            return Json(statObject.SerializeToJson());

        }

        [Authorize]
        public ActionResult EditCompanyInfo()
        {
            COM_Compañia company = db.COM_Compañia.FirstOrDefault();
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // POST: /News/Edit/5
        [Authorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditCompanyInfo(COM_Compañia company)
        {
            if (ModelState.IsValid)
            {

                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("EditCompanyInfo", "Home") });
                }
                return RedirectToAction("EditCompanyInfo");
            }

            return View(company);
        }

        internal class Stats
        {
            public int Contactos { get; set; }
            public int Cotizaciones { get; set; }
            public int Productos { get; set; }
            public int Noticias { get; set; }
        }
    }
}
