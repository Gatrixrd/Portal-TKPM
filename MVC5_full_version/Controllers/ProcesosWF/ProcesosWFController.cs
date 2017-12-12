using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClickFactura_Entidades.BD.Entidades;

namespace MVC5_full_version.Controllers.ProcesosWF
{
    public class ProcesosWFController : Controller
    {
        private Desarrollo_CF db = new Desarrollo_CF();

        // GET: Cat_ProcesosWF
        public ActionResult Index()
        {
            return View(db.Cat_ProcesosWF.ToList());
        }

        // GET: Cat_ProcesosWF/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_ProcesosWF cat_ProcesosWF = db.Cat_ProcesosWF.Find(id);
            if (cat_ProcesosWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_ProcesosWF);
        }

        // GET: Cat_ProcesosWF/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cat_ProcesosWF/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idWF,nombreWF,noAprobacionesRequeridas,controlador,minimoRelevanciaJerarquica")] Cat_ProcesosWF cat_ProcesosWF)
        {
            if (ModelState.IsValid)
            {
                db.Cat_ProcesosWF.Add(cat_ProcesosWF);
                db.SaveChanges();
                int newId = cat_ProcesosWF.idWF;
                #region           Agrega al super usuario administrador
                        string admon = "tkp";
                        var usuarioLegeo = from usu in db.Cat_Usuario where usu.Usuario.Equals(admon) == true select usu;
                        if(usuarioLegeo!=null)
                            if(usuarioLegeo.Count()>0)
                            {
                                foreach(var u in usuarioLegeo)
                                {
                                    string insertar= "Insert Into Cat_TipoUsuarioWF (idWF,nombreRol,idPadre,idHijo,activo,relevanciaJerarquica) Values ("+ newId.ToString() + ",'AdmonGral',0,0,1,"+ cat_ProcesosWF.minimoRelevanciaJerarquica+ ")";
                                    ClickFactura_Facturacion.Genericos.Genericos generico = new ClickFactura_Facturacion.Genericos.Genericos();
                                    #region          Ejecuta extracción de todos los datos
                                    var _insertado = generico.genericos_consultaCualquierTabla(insertar);
                                    string recuperaIdTipo = "Select idTipoUsuarioWF From Cat_TipoUsuarioWF Where idWF="+ newId.ToString() + " And  nombreRol='AdmonGral'";
                                    var _idTipo = generico.genericos_consultaCualquierTabla(recuperaIdTipo);
                                    string idTipo = "";
                                    foreach (DataRow _Usuario in _idTipo.Rows)
                                    {
                                        idTipo = _Usuario.Field<Int16>("idTipoUsuarioWF").ToString();
                                        break;
                                    }
                                    #region            Recupera el Id del Usuario Administrador
                                                #region extrae un parametro
                                                            int result = 0;
                                                            ClickFactura_WebServiceCF.AccesoBD.Genericos.adT_Parametros adp = new ClickFactura_WebServiceCF.AccesoBD.Genericos.adT_Parametros();
                                                            List<ClickFactura_WebServiceCF.AccesoBD.Genericos.objT_Parametros> objp = new List<ClickFactura_WebServiceCF.AccesoBD.Genericos.objT_Parametros>();
                                                            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
                                                            string entorno = "";
                                                            objp = adp.mABCT_Parametros(out result, 0, "IddeUsuarioAdministradorBafar", "Vacio", true, "ConsultaValor");
                                                            {
                                                                entorno = objp[0].ValorParametro.ToString();
                                                            }
                                                #endregion extrae un parametro
                                    #endregion     Recupera el Id del Usuario Administrador
                                    string insertarUsuarioAdmon ="Insert Into Cat_UsuariosWF (idTipoUsuarioWF,nombre,correo,centroCostos,idUsuarioPortal) Values ("+idTipo+",'AdmonGral ("+ newId.ToString() + ")','Sin Correo','0',"+entorno+")";
                                    var _AdmonGralCreado= generico.genericos_consultaCualquierTabla(insertarUsuarioAdmon);
                                    #endregion   Ejecuta extracción de todos los datos
                                }
                            }
               #endregion  Agrega al super usuario administrador


                return RedirectToAction("Index");
            }

            return View(cat_ProcesosWF);
        }

        // GET: Cat_ProcesosWF/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_ProcesosWF cat_ProcesosWF = db.Cat_ProcesosWF.Find(id);
            if (cat_ProcesosWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_ProcesosWF);
        }

        // POST: Cat_ProcesosWF/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idWF,nombreWF,noAprobacionesRequeridas,controlador,minimoRelevanciaJerarquica")] Cat_ProcesosWF cat_ProcesosWF)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cat_ProcesosWF).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat_ProcesosWF);
        }

        // GET: Cat_ProcesosWF/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_ProcesosWF cat_ProcesosWF = db.Cat_ProcesosWF.Find(id);
            if (cat_ProcesosWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_ProcesosWF);
        }

        // POST: Cat_ProcesosWF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Cat_ProcesosWF cat_ProcesosWF = db.Cat_ProcesosWF.Find(id);
            db.Cat_ProcesosWF.Remove(cat_ProcesosWF);
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
    }
}
