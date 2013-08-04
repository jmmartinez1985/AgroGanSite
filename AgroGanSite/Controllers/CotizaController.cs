using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroGanSite.Models;
using System.Transactions;
using AgroGanSite.Extensions;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Text;

namespace AgroGanSite.Controllers
{
    public class CotizaController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Cotiza/
        [Authorize]
        public ActionResult Index()
        {
            var firtscategory = db.CAT_Categorias.First();
            var allcategories = new SelectList(db.CAT_Categorias, "CAT_Id", "CAT_Descripcion", firtscategory);
            var productsfilter = db.PRO_Productos.Where(c => c.CAT_Id == firtscategory.CAT_Id);
            var firtsproduct = db.PRO_Productos.First();
            var allproductos = new SelectList(productsfilter, "PRO_Id", "PRO_Descripcion", firtsproduct);
            ViewBag.Products = allproductos;
            ViewBag.Categories = allcategories;
            return View(db.COT_Cotizaciones.ToList());
        }

        [Authorize]
        public ActionResult GetCurrentCotizaciones()
        {
            var cotfiltered = db.COT_Cotizaciones.Where(c => c.STS_Id == 1)
                .OrderByDescending(c => c.COT_Fecha).TakeLast(10).ToList();
            if (cotfiltered == null)
            {
                return HttpNotFound();
            }
            else
            {
                string cotlist = cotfiltered.SerializeToJson();
                return Json(cotlist);
            }
        }

        //
        // GET: /Cotiza/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            COT_Cotizaciones cot_cotizaciones = db.COT_Cotizaciones.Find(id);
            if (cot_cotizaciones == null)
            {
                return HttpNotFound();
            }
            return View(cot_cotizaciones);
        }

        //
        // GET: /Cotiza/Create

        public ActionResult Create()
        {
            var firtscategory = db.CAT_Categorias.First();
            var allcategories = new SelectList(db.CAT_Categorias, "CAT_Id", "CAT_Descripcion", firtscategory);
            var productsfilter = db.PRO_Productos.Where(c => c.CAT_Id == firtscategory.CAT_Id);
            var firtsproduct = db.PRO_Productos.First();
            var allproductos = new SelectList(productsfilter, "PRO_Id", "PRO_Descripcion", firtsproduct);
            ViewBag.Products = allproductos;
            ViewBag.Categories = allcategories;
            return View();
        }

        //
        // POST: /Cotiza/Create

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                CotizacionMessage cotMessage = new CotizacionMessage(); ;
                if (ModelState.IsValid)
                {
                    var detalle = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelCotiza>(form["datainfo"]);

                    var cotDet = new List<COD_CotizacionDetalle>();

                    COT_Cotizaciones cot = new COT_Cotizaciones()
                    {
                        COT_Celular = detalle.celular,
                        COT_Email = detalle.correo,
                        COT_Descripcion = detalle.descripcion,
                        COT_Mensaje = detalle.mensaje,
                        COT_Telefono = detalle.telefono,
                        COT_Nombre = detalle.nombre,
                        COT_Fecha = System.DateTime.Now,
                        STS_Id = 1,
                    };
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        db.COT_Cotizaciones.Add(cot);

                        var savecot = db.SaveChanges<COT_Cotizaciones>(cot);
                        detalle.cotiza.ForEach(c => cotDet.Add(new COD_CotizacionDetalle
                        {
                            COD_Cantidad = int.Parse(c.prodqty),
                            PRO_Id = int.Parse(c.prodid),
                            COD_Id = savecot.COT_Id
                        }));
                        cotDet.ForEach(c => db.SaveChanges<COD_CotizacionDetalle>(c));
                        transaction.Complete();
                        cotMessage = new CotizacionMessage { Cotizacion = detalle, Id = savecot.COT_Id };
                    }
                    Task.Factory.StartNew(() =>
                    {
                        SendMessage(cotMessage);
                    });
                    return Json(new { success = true });
                }
                return null;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                return Json(new { success = "validation" });
            }

            catch (Exception ex)
            {
                return Json(new { success = false });
            }


        }
        [NonAction]
        public async void SendMessage(CotizacionMessage cotDet)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            if (cotDet != null)
            {
                var correos = System.Configuration.ConfigurationManager.AppSettings["Correos"].Split(';').ToList();
                var from = System.Configuration.ConfigurationManager.AppSettings["From"];

                correos.ForEach(c => mailMessage.To.Add(new MailAddress(c)));

                mailMessage.From = new MailAddress(from);

                mailMessage.Subject = "Nueva cotización creada en Sitio Web";
                mailMessage.Body = GetHtmlMessage(cotDet);
                mailMessage.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
            }

            await smtpClient.SendMailAsync(mailMessage);
        }
        [NonAction]
        private string GetHtmlMessage(CotizacionMessage cotDet)
        {
            var table = "";
            cotDet.Cotizacion.cotiza.ForEach(c =>
            {
                table += string.Format("<tr><td>{0}</td> <td>{1}</td></tr>", c.prodid, c.prodqty);
            });
            var Cotiza = "";
            Cotiza = @" <html>
	                    
	                    <body>
		                    <h1>
			                    <span style='color:#ff0000;'><span style='font-family: 'trebuchet ms', helvetica, sans-serif;'><strong><u>Se ha recibido una nueva cotizaci&oacute;n</u></strong></span></span></h1>
		                    <p>
			                    <span style='font-family:trebuchet ms,helvetica,sans-serif;'>El codigo de cotizaci&oacute;n es: {0}</span></p>
		                    <p>
			                    <span style='color:#0000ff;'><strong><span style='font-family: 'trebuchet ms', helvetica, sans-serif;'>Informaci&oacute;n de la persona interesada</span></strong></span></p>
		                    <p style='margin-left: 40px;'>
			                    <span style='font-family:trebuchet ms,helvetica,sans-serif;'>Nombre:{1}</span></p>
		                    <p style='margin-left: 40px;'>
			                    <span style='font-family:trebuchet ms,helvetica,sans-serif;'>Email:{2}</span></p>
		                    <p style='margin-left: 40px;'>
			                    <span style='font-family:trebuchet ms,helvetica,sans-serif;'>T&eacute;lefono:{3}</span></p>
		                    <p style='margin-left: 40px;'>
			                    <span style='font-family:trebuchet ms,helvetica,sans-serif;'>Celular:{4}</span></p>
		                    <p style='margin-left: 40px;'>
			                    <span style='font-family:trebuchet ms,helvetica,sans-serif;'>Mensaje:{5}</span></p>
		                    <p>
			                    <span style='font-family:trebuchet ms,helvetica,sans-serif;'>La misma cuenta con el siguiente detalle de productos a cotizar:</span></p>
		                    <table border='1' cellpadding='1' cellspacing='1' style='width: 500px;'>
			                    <thead>
				                    <tr>
					                    <th scope='col'>
						                    <span style='font-family:trebuchet ms,helvetica,sans-serif;'>Producto</span></th>
					                    <th scope='col'>
						                    <span style='font-family:trebuchet ms,helvetica,sans-serif;'>Cantidad</span></th>
				                    </tr>
			                    </thead>
			                    <tbody>
				                    {6}
			                    </tbody>
		                    </table></body>
                    </html>";

            return string.Format(Cotiza, cotDet.Id, cotDet.Cotizacion.nombre, cotDet.Cotizacion.correo, cotDet.Cotizacion.telefono, cotDet.Cotizacion.celular, cotDet.Cotizacion.mensaje, table);
        }

        public ActionResult Edit(int id = 0)
        {
            COT_Cotizaciones cot_cotizaciones = db.COT_Cotizaciones.Find(id);
            if (cot_cotizaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", cot_cotizaciones.STS_Id);
            return View(cot_cotizaciones);
        }

        //
        // POST: /Cotiza/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(COT_Cotizaciones cot_cotizaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cot_cotizaciones).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "Cotiza") });
                }
                return RedirectToAction("Index");
            }
            ViewBag.STS_Id = new SelectList(db.STS_Status, "STS_Id", "STS_Descripcion", cot_cotizaciones.STS_Id);
            return View(cot_cotizaciones);
        }

        // GET: /Cotiza/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            COT_Cotizaciones cot_cotizaciones = db.COT_Cotizaciones.Find(id);
            if (cot_cotizaciones == null)
            {
                return HttpNotFound();
            }
            return View(cot_cotizaciones);
        }

        //
        // POST: /Cotiza/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            COT_Cotizaciones cot_cotizaciones = db.COT_Cotizaciones.Find(id);
            db.COT_Cotizaciones.Remove(cot_cotizaciones);
            db.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { redirectToUrl = Url.Action("Index", "Cotiza") });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

    public class cotiza
    {
        public string prodid { get; set; }
        public string prodqty { get; set; }
    }

    public class ModelCotiza
    {
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string descripcion { get; set; }
        public string mensaje { get; set; }
        public List<cotiza> cotiza { get; set; }
    }

    public class CotizacionMessage
    {
        public int Id { get; set; }
        public ModelCotiza Cotizacion { get; set; }

    }
}