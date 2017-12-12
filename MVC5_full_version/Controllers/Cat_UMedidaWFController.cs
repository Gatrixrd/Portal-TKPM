using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClickFactura_Entidades.BD.Entidades;

namespace MVC5_full_version.Controllers
{
    public class Cat_UMedidaWFController : Controller
    {
        private Desarrollo_CF db = new Desarrollo_CF();

        // GET: Cat_UMedidaWF
        public ActionResult Index()
        {
            return View(db.Cat_UMedidaWF.ToList());
        }

        // GET: Cat_UMedidaWF/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_UMedidaWF cat_UMedidaWF = db.Cat_UMedidaWF.Find(id);
            if (cat_UMedidaWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_UMedidaWF);
        }

        // GET: Cat_UMedidaWF/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cat_UMedidaWF/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUnidadMedida,UnidadMedida,DescripcionUM,activo,idWF")] Cat_UMedidaWF cat_UMedidaWF)
        {
            if (ModelState.IsValid)
            {
                db.Cat_UMedidaWF.Add(cat_UMedidaWF);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cat_UMedidaWF);
        }

        // GET: Cat_UMedidaWF/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_UMedidaWF cat_UMedidaWF = db.Cat_UMedidaWF.Find(id);
            if (cat_UMedidaWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_UMedidaWF);
        }

        // POST: Cat_UMedidaWF/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUnidadMedida,UnidadMedida,DescripcionUM,activo,idWF")] Cat_UMedidaWF cat_UMedidaWF)
        {
            if (ModelState.IsValid)
            {
                Cat_UMedidaWF unidad = db.Cat_UMedidaWF.Find(cat_UMedidaWF.idUnidadMedida);
                unidad.activo = cat_UMedidaWF.activo;
                unidad.DescripcionUM = cat_UMedidaWF.DescripcionUM;
                unidad.UnidadMedida = cat_UMedidaWF.UnidadMedida;
                db.Entry(unidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat_UMedidaWF);
        }

        // GET: Cat_UMedidaWF/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_UMedidaWF cat_UMedidaWF = db.Cat_UMedidaWF.Find(id);
            if (cat_UMedidaWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_UMedidaWF);
        }

        // POST: Cat_UMedidaWF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Cat_UMedidaWF cat_UMedidaWF = db.Cat_UMedidaWF.Find(id);
            db.Cat_UMedidaWF.Remove(cat_UMedidaWF);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult actualizaPadre(string id, string nuevo)
        {
            short _id = 0;
            _id = Convert.ToInt16(id);
            short? _nuevo = 0;
            _nuevo = Convert.ToInt16(nuevo);
            List<KeyValuePair<string, string>> respuestaP = new List<KeyValuePair<string, string>>();
            List<ClickFactura_Entidades.BD.Entidades.Cat_UMedidaWF> Padres = db.Cat_UMedidaWF.ToList();
            var existe = from p in Padres where p.idUnidadMedida == _id select p;
            if (existe != null)
            {
                if (existe.Count() > 0)
                {
                    foreach (var _p in existe)
                    {
                        _p.idWF = _nuevo;
                    }
                    db.SaveChanges();
                }
            }
            return null;
        }

        [HttpPost]
        public ActionResult actualizaTipos(string idWF)
        {
            List<KeyValuePair<string, string>> mensajes = new List<KeyValuePair<string, string>>();
            IEnumerable<SelectListItem> salida;
            if (idWF.Length > 0)
            {
                List<KeyValuePair<string, string>> respuestaP = new List<KeyValuePair<string, string>>();
                List<ClickFactura_Entidades.BD.Entidades.Cat_ProcesosWF> Padres = db.Cat_ProcesosWF.ToList();
                string keyP = "0";
                string valueP = "Ninguno";
                if (Padres.Count() == 0)
                {
                    keyP = "0";
                    valueP = "Es el primero";
                    respuestaP.Add(new KeyValuePair<string, string>(keyP, valueP));
                }
                else
                {
                    foreach (ClickFactura_Entidades.BD.Entidades.Cat_ProcesosWF wf in Padres)
                    {
                        //if (wf.idWF.ToString().Trim().Equals(idWF) == true)
                        //{
                            keyP = wf.idWF.ToString();
                            valueP = wf.nombreWF.ToString();
                            short? _mitipo = wf.idWF;
                            respuestaP.Add(new KeyValuePair<string, string>(keyP, valueP));
                        //}
                    }
                }
                IEnumerable<SelectListItem> _c = respuestaP.Select(x => new SelectListItem() { Text = x.Value.Trim(), Value = x.Key.Trim() });

                if (_c.Count() > 0)
                {
                    salida = _c;
                    return Json(salida);
                }
                else
                {
                    return Json(mensajes);
                }
            }
            else
            {
                //var model = listado;
                return null;
            }
        }
    }
}
