using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClickFactura_Entidades.BD.Entidades;

namespace MVC5_full_version.Controllers.TipoUsuarioWF
{
    public class TipoUsuarioWFController : Controller
    {
        private Desarrollo_CF db = new Desarrollo_CF();

        // GET: TipoUsuarioWF
        public ActionResult Index()
        {
            return View(db.Cat_TipoUsuarioWF.ToList());
        }

        // GET: TipoUsuarioWF/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_TipoUsuarioWF cat_TipoUsuarioWF = db.Cat_TipoUsuarioWF.Find(id);
            if (cat_TipoUsuarioWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_TipoUsuarioWF);
        }

        // GET: TipoUsuarioWF/Create
        public ActionResult Create()
        {
            ViewData["PorcentajeMayor"]= 0;
            return View();
        }


        // POST: TipoUsuarioWF/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTipoUsuarioWF,idWF,nombreRol,idPadre,idHijo,activo,relevanciaJerarquica")] Cat_TipoUsuarioWF cat_TipoUsuarioWF)
        {
            if (ModelState.IsValid)
            {
                if (cat_TipoUsuarioWF.idPadre == null)
                    cat_TipoUsuarioWF.idPadre = 0;
                if (cat_TipoUsuarioWF.idHijo == null)
                    cat_TipoUsuarioWF.idHijo = 0;
                if (cat_TipoUsuarioWF.idWF == null)
                    cat_TipoUsuarioWF.idWF = System.Web.HttpContext.Current.Session["idWF"] as string;
                db.Cat_TipoUsuarioWF.Add(cat_TipoUsuarioWF);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cat_TipoUsuarioWF);
        }

        // GET: TipoUsuarioWF/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_TipoUsuarioWF cat_TipoUsuarioWF = db.Cat_TipoUsuarioWF.Find(id);
            if (cat_TipoUsuarioWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_TipoUsuarioWF);
        }

        // POST: TipoUsuarioWF/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTipoUsuarioWF,idWF,nombreRol,idPadre,idHijo,activo,relevanciaJerarquica")] Cat_TipoUsuarioWF cat_TipoUsuarioWF)
        {
            if (ModelState.IsValid)
            {
                Cat_TipoUsuarioWF especifico = db.Cat_TipoUsuarioWF.Find(cat_TipoUsuarioWF.idTipoUsuarioWF);
                especifico.activo = cat_TipoUsuarioWF.activo;
                especifico.idTipoUsuarioWF = cat_TipoUsuarioWF.idTipoUsuarioWF;
                especifico.idWF = cat_TipoUsuarioWF.idWF;
                especifico.nombreRol = cat_TipoUsuarioWF.nombreRol;
                especifico.idHijo = 0;
                especifico.relevanciaJerarquica = cat_TipoUsuarioWF.relevanciaJerarquica;
                db.Entry(especifico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat_TipoUsuarioWF);
        }

        // GET: TipoUsuarioWF/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_TipoUsuarioWF cat_TipoUsuarioWF = db.Cat_TipoUsuarioWF.Find(id);
            if (cat_TipoUsuarioWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_TipoUsuarioWF);
        }

        // POST: TipoUsuarioWF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Cat_TipoUsuarioWF cat_TipoUsuarioWF = db.Cat_TipoUsuarioWF.Find(id);
            db.Cat_TipoUsuarioWF.Remove(cat_TipoUsuarioWF);
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

        #region    obtenComplementos
        // POST: TipoUsuarioWF/Delete/5
        [HttpPost, ActionName("GetWorkFlowList")]
        [ValidateAntiForgeryToken]
        public ActionResult GetWorkFlowList()
        {
            List<KeyValuePair<string, string>> respuesta = new List<KeyValuePair<string, string>>();
            List<Cat_ProcesosWF> cat_TipoUsuarioWF = db.Cat_ProcesosWF.ToList();
            foreach(Cat_ProcesosWF wf in cat_TipoUsuarioWF)
            {
                string key = wf.idWF.ToString();
                string value = wf.nombreWF.ToString();
                respuesta.Add(new KeyValuePair<string, string>(key,value));
            }
            return Json(respuesta);// RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult estableceWF(string id)
        {
            ClickFactura_Entidades.BD.Entidades.Desarrollo_CF db = new ClickFactura_Entidades.BD.Entidades.Desarrollo_CF();
            List<KeyValuePair<string, string>> respuestaP = new List<KeyValuePair<string, string>>();
            List<ClickFactura_Entidades.BD.Entidades.Cat_TipoUsuarioWF> Padres = db.Cat_TipoUsuarioWF.ToList();
            string flujoSeleccionado = id;
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
                if (flujoSeleccionado != null)
                {
                    foreach (ClickFactura_Entidades.BD.Entidades.Cat_TipoUsuarioWF wf in Padres)
                    {
                        if(wf.idWF!=null)
                        {
                            if (wf.idWF.Trim().ToString().Equals(flujoSeleccionado) == true)
                            {
                                keyP = wf.idTipoUsuarioWF.ToString();
                                valueP = wf.nombreRol.ToString();
                                short? _mitipo = wf.idTipoUsuarioWF;
                                var porcentaje = from todos in Padres where todos.idTipoUsuarioWF == _mitipo select todos;
                                if (porcentaje != null)
                                {
                                    if (porcentaje.Count() > 0)
                                    {
                                        var p = porcentaje.ToArray();
                                        ViewData["PorcentajeMayor"] = Convert.ToDouble(p[0].relevanciaJerarquica);
                                    }
                                    respuestaP.Add(new KeyValuePair<string, string>(keyP, valueP));
                                }
                            }
                        }
                        else
                        {
                            keyP = "0";
                            valueP = "Es el primero";
                            respuestaP.Add(new KeyValuePair<string, string>(keyP, valueP));
                        }
                    }
                }
            }
            IEnumerable<SelectListItem> CategorySelectListP = respuestaP.Select(x => new SelectListItem() { Text = x.Value.Trim(), Value = x.Key.Trim() });
            //
            System.Web.HttpContext.Current.Session["idWF"] = id;
            //return JavaScript("javascript:showErrorMessage('Se establecio ya a que proceso o Workflow se agrega el nuevo tipo.'); ");
            return Json(CategorySelectListP);
        }

        [HttpPost]
        public ActionResult actualizaPadre(string id,string nuevo)
        {
            short _id = 0;
            _id = Convert.ToInt16(id);
            short? _nuevo = 0;
            _nuevo = Convert.ToInt16(nuevo);
            List<KeyValuePair<string, string>> respuestaP = new List<KeyValuePair<string, string>>();
            List<ClickFactura_Entidades.BD.Entidades.Cat_TipoUsuarioWF> Padres = db.Cat_TipoUsuarioWF.ToList();
            var existe=from p in Padres where p.idTipoUsuarioWF==_id select p;
            if(existe!=null)
            {
                if(existe.Count()>0)
                {
                    foreach(var _p in existe)
                    {
                        _p.idPadre =_nuevo;
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
                            keyP = wf.idWF.ToString();
                            valueP = wf.nombreWF.ToString();
                            short? _mitipo = wf.idWF;
                            respuestaP.Add(new KeyValuePair<string, string>(keyP, valueP));

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

        [HttpPost]
        public ActionResult actualizaTiposPadres(string idWF)
        {
            List<KeyValuePair<string, string>> mensajes = new List<KeyValuePair<string, string>>();
            IEnumerable<SelectListItem> salida;
            if (idWF.Length > 0)
            {
                List<KeyValuePair<string, string>> respuestaP = new List<KeyValuePair<string, string>>();
                List<ClickFactura_Entidades.BD.Entidades.Cat_TipoUsuarioWF> Padres = db.Cat_TipoUsuarioWF.ToList();
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
                    foreach (ClickFactura_Entidades.BD.Entidades.Cat_TipoUsuarioWF wf in Padres)
                    {
                        keyP =wf.idTipoUsuarioWF.ToString();
                        valueP = wf.nombreRol.ToString();
                        short? _mitipo = wf.idPadre;
                        respuestaP.Add(new KeyValuePair<string, string>(keyP, valueP));

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


        #endregion obtenComplementos





    }
}
