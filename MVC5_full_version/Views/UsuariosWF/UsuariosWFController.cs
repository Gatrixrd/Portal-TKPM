using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClickFactura_Entidades.BD.Entidades;

namespace MVC5_full_version.Views.UsuariosWF
{
    public class UsuariosWFController : Controller
    {
        private Desarrollo_CF db = new Desarrollo_CF();

        // GET: UsuariosWF
        public ActionResult Index()
        {
            return View(db.Cat_UsuariosWF.ToList());
        }

        // GET: UsuariosWF/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_UsuariosWF cat_UsuariosWF = db.Cat_UsuariosWF.Find(id);
            if (cat_UsuariosWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_UsuariosWF);
        }

        // GET: UsuariosWF/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosWF/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuarioWF,idTipoUsuarioWF,nombre,correo,centroCostos,idUsuarioPortal")] Cat_UsuariosWF cat_UsuariosWF)
        {
            if (ModelState.IsValid)
            {
                db.Cat_UsuariosWF.Add(cat_UsuariosWF);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cat_UsuariosWF);
        }

        // GET: UsuariosWF/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_UsuariosWF cat_UsuariosWF = db.Cat_UsuariosWF.Find(id);
            if (cat_UsuariosWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_UsuariosWF);
        }

        // POST: UsuariosWF/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuarioWF,idTipoUsuarioWF,nombre,correo,centroCostos,idUsuarioPortal")] Cat_UsuariosWF cat_UsuariosWF)
        {
            if (ModelState.IsValid)
            {
                Cat_UsuariosWF usuario = db.Cat_UsuariosWF.Find(cat_UsuariosWF.idUsuarioWF);
                usuario.idTipoUsuarioWF = cat_UsuariosWF.idTipoUsuarioWF;
                usuario.nombre = cat_UsuariosWF.nombre;
                usuario.correo = cat_UsuariosWF.correo;
                usuario.centroCostos = cat_UsuariosWF.centroCostos;
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat_UsuariosWF);
        }

        // GET: UsuariosWF/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat_UsuariosWF cat_UsuariosWF = db.Cat_UsuariosWF.Find(id);
            if (cat_UsuariosWF == null)
            {
                return HttpNotFound();
            }
            return View(cat_UsuariosWF);
        }

        // POST: UsuariosWF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Cat_UsuariosWF cat_UsuariosWF = db.Cat_UsuariosWF.Find(id);
            db.Cat_UsuariosWF.Remove(cat_UsuariosWF);
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

        #region

        [HttpPost]
        public ActionResult actualizaTipo(string id, string nuevo)
        {
            short _id = 0;
            _id = Convert.ToInt16(id);
            short? _nuevo = 0;
            _nuevo = Convert.ToInt16(nuevo);
            List<KeyValuePair<string, string>> respuestaP = new List<KeyValuePair<string, string>>();
            List<ClickFactura_Entidades.BD.Entidades.Cat_UsuariosWF> Padres = db.Cat_UsuariosWF.ToList();
            var existe = from p in Padres where p.idUsuarioWF == _id select p;
            if (existe != null)
            {
                if (existe.Count() > 0)
                {
                    foreach (var _p in existe)
                    {
                        _p.idTipoUsuarioWF = _nuevo;
                    }
                    db.SaveChanges();
                }
            }
            return null;
        }

        [HttpPost]
        public ActionResult actualizaUsuario(string id, string nuevo)
        {
            short _id = 0;

            if(id.Length>0)
            {
                _id = Convert.ToInt16(id);
                short? _nuevo = 0;
                _nuevo = Convert.ToInt16(nuevo);
                List<KeyValuePair<string, string>> respuestaP = new List<KeyValuePair<string, string>>();
                List<ClickFactura_Entidades.BD.Entidades.Cat_UsuariosWF> Padres = db.Cat_UsuariosWF.ToList();
                var existe = from p in Padres where p.idUsuarioWF == _id select p;
                if (existe != null)
                {
                    if (existe.Count() > 0)
                    {
                        foreach (var _p in existe)
                        {
                            _p.idUsuarioPortal = _nuevo;
                        }
                        db.SaveChanges();
                    }
                }
            }
            return null;
        }

[HttpPost]
public ActionResult actualizaUsuariosPortal(string idTipo)
{
    List<KeyValuePair<string, string>> mensajes = new List<KeyValuePair<string, string>>();
    IEnumerable<SelectListItem> salida;
    //if (idTipo.Length > 0)
    //{
        List<KeyValuePair<string, string>> respuesta = new List<KeyValuePair<string, string>>();
        List<ClickFactura_Entidades.BD.Entidades.Cat_Usuario> Padres = db.Cat_Usuario.ToList();
        List<ClickFactura_Entidades.BD.Entidades.Cat_Usuario> Filtrados = new List<Cat_Usuario>();
        string keyP = "0";
        string valueP = "Ninguno";
        if (Padres.Count() == 0)
        {
            keyP = "0";
            valueP = "Es el primero";
            respuesta.Add(new KeyValuePair<string, string>(keyP, valueP));
        }
        else
        {
          #region Perfiles activos
                                    int result = 0;
                                    ClickFactura_WebServiceCF.AccesoBD.Genericos.adT_Parametros adp = new ClickFactura_WebServiceCF.AccesoBD.Genericos.adT_Parametros();
                                    List<ClickFactura_WebServiceCF.AccesoBD.Genericos.objT_Parametros> objp = new List<ClickFactura_WebServiceCF.AccesoBD.Genericos.objT_Parametros>();
                                    List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
                                    string entorno = null;
                                    string[] perfiles = { };
                                    string[] perfilesAux = { };
                                    List<string> listaPerfiles = new List<string>();

                                    objp = adp.mABCT_Parametros(out result, 0, "perfilesInternos", "Vacio", true, "ConsultaValor");
                                    {
                                        entorno = objp[0].ValorParametro.ToString();
                                    }

                                    if (entorno.Length <= 0)
                                    {
                                        entorno = "0";
                                    }
                                    else
                                    {
                                        char[] split = { ',' };
                                        perfiles = entorno.Split(split);
                                        foreach (string p in perfiles)
                                        {
                                            listaPerfiles.Add(p);
                                        }
                                    }
                    #endregion Perfiles activos
          #region por Revisar
                    //IEnumerable<Cat_Usuario> Original;
                    //IEnumerable<Cat_Usuario> Acumulado=null;
                    //if (listaPerfiles.Count() == 1)
                    //{
                    //    Original = from originales in Padres where originales.IdPerfil.Equals(listaPerfiles[0]) == true select originales;
                    //    Acumulado = Original;
                    //}
                    //else
                    //{
                    //    listaPerfiles.RemoveAt(0);

                    //    foreach (string _p in listaPerfiles)
                    //    {
                    //        IEnumerable<Cat_Usuario> dePaso;
                    //        dePaso = from ll in Padres where ll.IdPerfil.Equals(_p) == true select ll;
                    //        if (dePaso != null)
                    //            if (dePaso.Count() > 0)
                    //            {
                    //                Acumulado.Concat(dePaso);
                    //                //foreach (ClickFactura_Entidades.BD.Entidades.Cat_Usuario wf in Padres)
                    //                //{
                    //                //    #region Carga solo los permitidos
                    //                //    if (wf.Activo == true)
                    //                //    {

                    //                //        if (_p.Equals(wf.IdPerfil) == true)
                    //                //        {
                    //                //            string key = wf.IdUsuario.ToString();
                    //                //            string value = wf.Usuario.ToString();
                    //                //            respuesta.Add(new KeyValuePair<string, string>(key, value));
                    //                //            break;
                    //                //        }

                    //                //    }
                    //                //    #endregion Carga solo los permitidos
                    //                //}
                    //                //IEnumerable<SelectListItem> CategorySelectListPortal = respuesta.Select(x => new SelectListItem() { Text = x.Value.Trim(), Value = x.Key.Trim() });
                    //            }
                    //    }
                    //}
                    #endregion Por revisar

                    foreach (ClickFactura_Entidades.BD.Entidades.Cat_Usuario wf in Padres)
                    {
                        #region Carga solo los permitidos
                                string key = wf.IdUsuario.ToString();
                                string value = wf.Usuario.ToString();
                                respuesta.Add(new KeyValuePair<string, string>(key, value));
                        #endregion Carga solo los permitidos
                    }
                }
                IEnumerable<SelectListItem> _c = respuesta.Select(x => new SelectListItem() { Text = x.Value.Trim(), Value = x.Key.Trim() });

        if (_c.Count() > 0)
        {
            salida = _c;
            return Json(salida);
        }
        else
        {
            return Json(mensajes);
        }
    //}
    //else
    //{
    //    //var model = listado;
    //    return null;
    //}
}

        [HttpPost]
        public ActionResult actualizaTiposUsuarios(string idTipo)
        {
            List<KeyValuePair<string, string>> mensajes = new List<KeyValuePair<string, string>>();
            IEnumerable<SelectListItem> salida;
            //if (idTipo.Length > 0)
            //{
            List<KeyValuePair<string, string>> respuesta = new List<KeyValuePair<string, string>>();
            List<ClickFactura_Entidades.BD.Entidades.Cat_TipoUsuarioWF> Padres = db.Cat_TipoUsuarioWF.ToList();
            List<ClickFactura_Entidades.BD.Entidades.Cat_TipoUsuarioWF> Filtrados = new List<Cat_TipoUsuarioWF>();
            string keyP = "0";
            string valueP = "Ninguno";
            if (Padres.Count() == 0)
            {
                keyP = "0";
                valueP = "Es el primero";
                respuesta.Add(new KeyValuePair<string, string>(keyP, valueP));
            }
            else
            {
                #region Perfiles activos
                //int result = 0;
                //ClickFactura_WebServiceCF.AccesoBD.Genericos.adT_Parametros adp = new ClickFactura_WebServiceCF.AccesoBD.Genericos.adT_Parametros();
                //List<ClickFactura_WebServiceCF.AccesoBD.Genericos.objT_Parametros> objp = new List<ClickFactura_WebServiceCF.AccesoBD.Genericos.objT_Parametros>();
                //List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
                //string entorno = null;
                //string[] perfiles = { };
                //string[] perfilesAux = { };
                //List<string> listaPerfiles = new List<string>();

                //objp = adp.mABCT_Parametros(out result, 0, "perfilesInternos", "Vacio", true, "ConsultaValor");
                //{
                //    entorno = objp[0].ValorParametro.ToString();
                //}

                //if (entorno.Length <= 0)
                //{
                //    entorno = "0";
                //}
                //else
                //{
                //    char[] split = { ',' };
                //    perfiles = entorno.Split(split);
                //    foreach (string p in perfiles)
                //    {
                //        listaPerfiles.Add(p);
                //    }
                //}
                #endregion Perfiles activos

                foreach (ClickFactura_Entidades.BD.Entidades.Cat_TipoUsuarioWF wf in Padres)
                {
                    #region Carga solo los permitidos
                    string key = wf.idTipoUsuarioWF.ToString();
                    string value = wf.nombreRol.ToString();
                    respuesta.Add(new KeyValuePair<string, string>(key, value));
                    #endregion Carga solo los permitidos
                }
            }
            IEnumerable<SelectListItem> _c = respuesta.Select(x => new SelectListItem() { Text = x.Value.Trim(), Value = x.Key.Trim() });

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

        [HttpPost]
        public ActionResult actualizaTipos(string idTipo)
        {
            List<KeyValuePair<string, string>> mensajes = new List<KeyValuePair<string, string>>();
            IEnumerable<SelectListItem> salida;
            if (idTipo.Length > 0)
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
                        if (wf.idWF.ToString().Trim().Equals(idTipo) == true)
                        {
                            keyP = wf.idTipoUsuarioWF.ToString();
                            valueP = wf.nombreRol.ToString();
                            short? _mitipo = wf.idTipoUsuarioWF;
                            //var porcentaje = from todos in Padres where todos.idTipoUsuarioWF == _mitipo select todos;
                            //if (porcentaje != null)
                            //{
                            //    if (porcentaje.Count() > 0)
                            //    {
                            //        var p = porcentaje.ToArray();
                            //        ViewData["PorcentajeMayor"] = Convert.ToDouble(p[0].relevanciaJerarquica);
                            //    }
                            //}
                            respuestaP.Add(new KeyValuePair<string, string>(keyP, valueP));
                        }
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

        #endregion
    }
}
