using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5_full_version.Controllers.Migo
{
    public class MigoController : Controller
    {
        ClickFactura_WebServiceCF.Service.Service1 servicio=new ClickFactura_WebServiceCF.Service.Service1();
        public ActionResult Migo()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult cargaOrdenCompra(string ordenCompra)
        {
            List<KeyValuePair<string, string>> mensajes = new List<KeyValuePair<string, string>>();
            List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> listado = new List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>();
            if(ordenCompra.Length>0)
            {
                    List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> _tablas = servicio.Carga_DetalleOrden(ordenCompra,ref mensajes);
                    if(_tablas.Count>0)
                    {
                        System.Web.HttpContext.Current.Session["OrdenCompraMIGO"] = ordenCompra;
                        System.Web.HttpContext.Current.Session["Detalle_OrdenCompraMIGO"] = _tablas; 
                        return Json(_tablas);
                    }
                    else
                    {
                        return Json(mensajes);
                    }
            }
            else
            {
                    var model = listado;
                    return Json(model);
            }
        }

        [HttpPost]
        public ActionResult disparaMIGO(string posicion,string porIngresar,string modo)
        {
            List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> listado = new List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>();
            List<ClickFactura_Entidades.BD.Modelos.Mercancias_Recepcionadas> Recepcionado = new List<ClickFactura_Entidades.BD.Modelos.Mercancias_Recepcionadas>();
            try
            {
                    string ordenCompra = System.Web.HttpContext.Current.Session["OrdenCompraMIGO"] as String; 
                    if (ordenCompra.Length > 0)
                    {
                        List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> Lineas = System.Web.HttpContext.Current.Session["Detalle_OrdenCompraMIGO"] as List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>;
                        if (modo.Equals("Completa") == true)
                        {
                                #region Modo Total
                                 foreach(var linea in Lineas)
                                 {
                                     ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra renglon = (ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra)linea;
                                     ClickFactura_Entidades.BD.Modelos.Mercancias_Recepcionadas recepcion = new ClickFactura_Entidades.BD.Modelos.Mercancias_Recepcionadas();
                                     List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
                                     recepcion.Hora_Inicio = DateTime.Now.ToUniversalTime().ToString();
                                     recepcion.Fecha_Inicio = DateTime.Now.ToShortDateString();
                                     recepcion.Transaccion_Confirmada = false;
                                     recepcion.OrdenCompra = renglon.Orden_Compra;
                                     recepcion.Posicion_OC = renglon.Posicion_OC;
                                     recepcion.Cantidad = Convert.ToDecimal(renglon.Cantidad);
                                     recepcion.Cantidad_Base = Convert.ToDecimal(renglon.Cantidad);
                                     recepcion.Unidad_Medida = renglon.Unidad_Medida;
                                     recepcion.Num_Proveedor = "Pendiente";
                                     recepcion.Usuario = "Pruebas";
                                     recepcion.Almacen = renglon.Almacen;
                                     recepcion.Planta = renglon.Planta;
                                     recepcion.Tipo_OrdenCompra = "PO";
                                     string mensajes = "";
                                     bool generado = servicio.MIGO_creaLineaMIGO(renglon,ref datos,ref mensajes);
                                     if(generado==true)
                                     {
                                     #region        Construye información de lo Recepcionado
                                         recepcion.Numero_Recepcion = datos[0].Value;
                                         recepcion.Año_Recepcion = datos[1].Value;
                                         Recepcionado.Add(recepcion);
                                         bool almacenarenBD_MIGO = true;
                                         if(almacenarenBD_MIGO==true)
                                         {

                                         }
                                     #endregion  Construye información de lo Recepcionado
                                     }
                                     else
                                     {
                                         //La transaccion NO fue Exitosa!!
                                         //System.Web.HttpContext.Current.Session["mensajesMIGO"] = mensajes;
                                         //ViewBag.mensajes = mensajes;
                                         var model = Recepcionado;
                                         return JavaScript("javascript:showErrorMessage('" + mensajes + "');");
                                     }
                                 }
                                #endregion Modo Total
                        }
                        else
                        {
                                #region Modo parcial
                                posicion = posicion.Replace("Posicion: ", "");
                                var unaLinea=from registro in Lineas where registro.Posicion_OC.Equals(posicion)==true select registro;
                               
                                if(unaLinea!=null)
                                {
                                    if(unaLinea.Count()>0)
                                    {
                                        foreach(var _linea in unaLinea.AsEnumerable())
                                        {
                                                   #region Procesa la linea hacia MIGO
                                                        ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra renglon = (ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra)_linea;
                                                        ClickFactura_Entidades.BD.Modelos.Mercancias_Recepcionadas recepcion = new ClickFactura_Entidades.BD.Modelos.Mercancias_Recepcionadas();
                                                        List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
                                                        recepcion.Hora_Inicio = DateTime.Now.ToUniversalTime().ToString();
                                                        recepcion.Fecha_Inicio = DateTime.Now.ToShortDateString();
                                                        recepcion.Transaccion_Confirmada = false;
                                                        recepcion.OrdenCompra = renglon.Orden_Compra;
                                                        recepcion.Posicion_OC = renglon.Posicion_OC;
                                                        recepcion.Cantidad = Convert.ToDecimal(porIngresar);
                                                        recepcion.Cantidad_Base = Convert.ToDecimal(renglon.Cantidad);
                                                        recepcion.Unidad_Medida = renglon.Unidad_Medida;
                                                        recepcion.Num_Proveedor = "Pendiente";
                                                        recepcion.Usuario = "Pruebas";
                                                        recepcion.Almacen = renglon.Almacen;
                                                        recepcion.Planta = renglon.Planta;
                                                        recepcion.Tipo_OrdenCompra = "PO";
                                                        recepcion.Descripcion = renglon.Descripcion;
                                                        recepcion.Importe = renglon.Importe;
                                                        recepcion.Indicador_IVA = renglon.Indicador_IVA;
                                                        recepcion.Numero_Material = renglon.Numero_Material;
                                                        recepcion.Moneda = renglon.Moneda;
                                                       //Colocamos la cantidad que deseamos ingresar
                                                        renglon.Cantidad = porIngresar;
                                                        string mensajes = "";
                                                        //
                                                        bool generado = servicio.MIGO_creaLineaMIGO(renglon, ref datos,ref mensajes);
                                                        if (generado == true)
                                                        {
                                                            #region        Construye información de lo Recepcionado
                                                            recepcion.Numero_Recepcion = datos[0].Value;
                                                            recepcion.Año_Recepcion = datos[1].Value;
                                                            Recepcionado.Add(recepcion);
                                                            #endregion  Construye información de lo Recepcionado
                                                            #region Enviar a almacenar lo generado a base de datos de Portal
                                                            generado=servicio.MIGO_almacenarenBD_MIGO(Recepcionado);
                                                            #endregion Enviar a almacenar lo generado a base de datos de Portal
                                                        }
                                                        else
                                                        {
                                                            //La transaccion NO fue Exitosa!!
                                                            //System.Web.HttpContext.Current.Session["mensajesMIGO"] = null;
                                                            //System.Web.HttpContext.Current.Session["mensajesMIGO"] = mensajes;
                                                            //ViewData["mensajes"] = System.Web.HttpContext.Current.Session["mensajesMIGO"] as String;
                                                            //ViewBag.mensajes = mensajes;
                                                            //System.Web.UI.ScriptManager.RegisterStartupScript("Control", this.GetType(), "ShowStatus", "javascript:showErrorMessage('" + mensajes + "');", true);
                                                            var model = Recepcionado;
                                                            return JavaScript("javascript:showErrorMessage('" + mensajes + "');");
                                                        }
                                                    #endregion Procesa la linea hacia MIGO
                                        }
 
                                    }
                                }
                                #endregion Modo parcial
                        }
                        return Json(Recepcionado);
                    }
                    else
                    {
                        var model = Recepcionado;
                        return Json(model);
                    }
            }
            catch(Exception ex)
            {
                string error = ex.Message;
                var model = Recepcionado;
                return Json(model);
            }

        }

 

    }
}