using MVC5_full_version.Controllers.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using BAFAR.Clases.Sistema;
//using BAFAR.Clases.Login;

namespace MVC5_full_version.Controllers
{
     [Autenticado]
    public class SistemaController: Controller
    {

        //Menus mn;
        //Perfiles per;
        //Paquetes paq;
        //PaquetesXPerfil paqper;
        private static int IdPadre = 0;
        public SistemaController()
        {
            //mn = new Menus();
            //per = new Perfiles();
            //paq = new Paquetes();
            //paqper = new PaquetesXPerfil();
        }
        //[Permisos]
        //// GET: Sistema
        //public ActionResult AcuerdosComerciales()
        //{
        //    return View();
        //}
        //[Permisos]
        //public ActionResult AlmacenamientoInterno()
        //{
        //    return View();
        //}
        //[Permisos]
        //public ActionResult Usuarios()
        //{
        //    return View();
        //}
        //[Permisos]
        //public ActionResult Proveedores()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult GuardarProveedores(List<string> datos)
        //{
        //    return Json(true);
        //}
        //[Permisos]
        //public ActionResult ErroresNotificaciones()
        //{
        //    return View();
        //}
        //[Permisos]
        //public ActionResult AlertasUsuarios()
        //{
        //    return View();
        //}
        //[Permisos]
        //public ActionResult Compañias()
        //{
        //    return View();
        //}
        //#region Paquetes
        //[Permisos]
        //public ActionResult Paquetes()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CargarPaquetes()
        //{
        //    string mensaje;
        //    return PartialView("~/Views/Administracion/Grid.cshtml", paq.ListaPaquetes(out mensaje));
        //}
        //[HttpPost]
        //public ActionResult GuardarPaquete(ClickFactura_Entidades.BD.Entidades.Cat_Paquetes paquete)
        //{
        //    string mensaje;
        //    bool r = paq.GuardarPaquete(paquete, out mensaje);
        //    return Json(r);
        //}
        //#endregion
        //#region Perfiles
        //[Permisos]
        //public ActionResult Perfiles()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CargarPerfiles()
        //{
        //    string mensaje;
        //    return PartialView("~/Views/Administracion/Grid.cshtml", per.ListaPerfiles(out mensaje));
        //}
        //[HttpPost]
        //public ActionResult GuardarPerfil(ClickFactura_Entidades.BD.Entidades.Cat_Perfil perfil)
        //{
        //    string mensaje;
        //    bool r = per.GuardarPerfil(perfil, out mensaje);
        //    return Json(r);
        //}
        //#endregion
        //[Permisos]
        //public ActionResult Historico()
        //{
        //    return View();
        //}
        #region Menus
        //[Permisos]
        //public ActionResult Menus()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CargarMenus()
        //{
        //    string mensaje;
        //    return PartialView("~/Views/Administracion/Grid.cshtml", mn.ListaMenus(out mensaje));
        //}
        //[HttpPost]
        //public ActionResult MenusArbol()
        //{
        //    string mensaje;
        //    var arbol = mn.MenuArbol(out mensaje);
        //    Session["ArbolMenu"] = arbol.Registros;
        //    return PartialView("_Arbol", arbol);
        //}
        //[HttpGet]
        //public ActionResult LeerArbol(int? id)
        //{
        //    var arbol = (List<ClickFactura_Entidades.BD.Modelos.RegistroArbol>)Session["ArbolMenu"];

        //    if (id.HasValue)
        //    {
        //        var q = (from t in arbol where t.Id == id.ToString() select t).FirstOrDefault();
        //        if (q.Id == null)
        //        {
        //            var p = (from t in arbol where t.Id == IdPadre.ToString() select t).FirstOrDefault();
        //            q = (from t in p.SubMenu where t.Id == id.ToString() select t).FirstOrDefault();
        //        }
        //        var sub = (from t in q.SubMenu select new { id = t.Id, text = t.Nombre, spriteCssClasses = t.Icono, hasChildren = (t.SubMenu.Count > 0 ? true : false) }).ToList();
        //        return Json(sub, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        var q = (from t in arbol select new { id = t.Id, text = t.Nombre, spriteCssClasses = t.Icono, hasChildren = (t.SubMenu.Count > 0 ? true : false) }).ToList();
        //        return Json(q, JsonRequestBehavior.AllowGet);
        //    }

        //}
        //[HttpPost]
        //public ActionResult SeleccionRegistro(int id, int idPadre)
        //{
        //    string mensaje;
        //    IdPadre = idPadre;
        //    var r = mn.ObtenerMenuXId(id, out mensaje);

        //    return Json(r);
        //}
        //[HttpPost]
        //public ActionResult ActualizarNodos(int id, int idPadre, int idAnterior, string posicion)
        //{
        //    string mensaje;
        //    var r = mn.ObtenerMenuXId(id, out mensaje);
        //    if (id == idPadre)
        //        idPadre = 0;
        //    var posiciones = mn.ObtenerListaPosiciones(idAnterior, id, idPadre, posicion);
        //    mn.ActualizarArbol(r, idPadre, posiciones);
        //    return Json(true);
        //}
        //[HttpPost]
        //public ActionResult GuardarMenu(ClickFactura_Entidades.BD.Entidades.Cat_Menu menu)
        //{
        //    string mensaje;
        //    bool r = mn.GuardarMenus(menu, out mensaje);
        //    return Json(r);
        //}
        #endregion
        //#region Paquetes por perfil

        //[Permisos]
        //public ActionResult PerfilesPaquete()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CargarPaquetesPerfilArbol()
        //{
        //    string mensaje;
        //    var listado = new List<List<ClickFactura_Entidades.BD.Entidades.Rel_Paquete_Perfil>>();
        //    var arbol = paqper.PaquetePerfilArbol(out mensaje, out listado);
        //    Session["ArbolPaquetePerfil"] = listado;
        //    return PartialView("_Arbol", arbol);
        //}
        //[HttpPost]
        //public ActionResult CargarPerfilesArbol()
        //{
        //    string mensaje;
        //    var arbol = paqper.PerfilesArbol(out mensaje);
        //    return PartialView("_Arbol", arbol);
        //}
        //[HttpPost]
        //public ActionResult GuardarPaqueteXPerfil(ClickFactura_Entidades.BD.Entidades.Rel_Paquete_Perfil paqueteperfil)
        //{
        //    string mensaje="";

        //    var lista = (List<List<ClickFactura_Entidades.BD.Entidades.Rel_Paquete_Perfil>>)Session["ArbolPaquetePerfil"];
        //    foreach (var item in lista)
        //    {
        //        var v = (from t in item
        //                 where t.IdPaquete.Equals(paqueteperfil.IdPaquete) && t.IdPerfil.Equals(paqueteperfil.IdPerfil)
        //                 select t).FirstOrDefault();
        //        if (v != null)
        //        {
        //            paqueteperfil.Id_Rel_Paquete_Perfil = v.Id_Rel_Paquete_Perfil;
        //            break;
        //        }

        //    }
        //    bool r = paqper.GuardarPaquetePerfil(paqueteperfil, out mensaje);
        //    if (r)
        //    {
        //        if (paqueteperfil.Activo == false)
        //        {
        //            mensaje = "El elemento seleccionado se eliminó correctamente.";
        //        }
        //        else
        //        {
        //            mensaje = "El elemento se agregó correctamente.";
        //        }
        //    }
        //    return Json(new { guardo = r, mensaje = mensaje });
        //}

        //#endregion
        //[Permisos]
        //public ActionResult SociedadesProveedor()
        //{
        //    return View();
        //}
        //[Permisos]
        //public ActionResult OrdenesCompra()
        //{
        //    return View();
        //}
        //[Permisos]
        //public ActionResult AtribAddenda()
        //{
        //    return View();
        //}
    }

    }
