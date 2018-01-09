using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace MVC5_full_version.Controllers.Migo
{
    public class MigoMiroController:Controller
    {
        ClickFactura_WebServiceCF.Service.Service1 servicio = new ClickFactura_WebServiceCF.Service.Service1();
        static List<ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.objLeidoFactura> FacturasLeidas = new List<ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.objLeidoFactura>();
        static Dictionary<string, string> FacturasCargadas = new Dictionary<string, string>();
        static List<string> elementos = new List<string>();
        List<List<string>> lsMensajes;
        public ActionResult MigoMiro()
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
            bool informacionValida = true;
            string mensajesUsuario = "";
            if (ordenCompra.Length > 0)
            {
                        #region Carga información
                        List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> _tablas = servicio.Carga_DetalleOrden(ordenCompra, ref mensajes);
                        if (_tablas.Count > 0)
                                {
                                        foreach (ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra oc in _tablas)
                                        {
                                            #region Revisando prerequisitos como Indicador de IVA o Almacen
                                            if (oc.Indicador_IVA.Length <= 0)
                                            {
                                                informacionValida = false;
                                                oc.Indicador_IVA = "Error: Falta indicador";
                                                //oc.Cantidad = "0.0";
                                                //oc.Importe = "$0.0";
                                                mensajesUsuario = "Al menos 1 posición de esta Orden de Compra NO tiene Indicador de Impuestos.";
                                            }

                                            int result = 0;
                                            ClickFactura_WebServiceCF.AccesoBD.Genericos.adT_Parametros adp = new ClickFactura_WebServiceCF.AccesoBD.Genericos.adT_Parametros();
                                            List<ClickFactura_WebServiceCF.AccesoBD.Genericos.objT_Parametros> objp = new List<ClickFactura_WebServiceCF.AccesoBD.Genericos.objT_Parametros>();
                                            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
                                            string entorno = "";

                                            objp = adp.mABCT_Parametros(out result, 0, "validarAlmacen", "Vacio", true, "ConsultaValor");
                                            {
                                                entorno = objp[0].ValorParametro.ToString();
                                            }
                                            if(entorno!=null)
                                            {
                                                bool validar = entorno.Equals("0") == true ? false : true;
                                                if (oc.Almacen.Length <= 0)
                                                        {
                                                           if (validar == true)
                                                                   informacionValida = false;
                                                            oc.Almacen = "Atención: Sin almacén";
                                                            //oc.Cantidad = "0.0";
                                                            //oc.Importe = "$0.0";
                                                            mensajesUsuario = mensajesUsuario + " Alerta, no se tiene especificado un Almacén destino al invocar el proceso de ingreso de Mercáncias, servicios u otros objetos.";
                                                        }
                                            }
                                        #endregion Revisando prerequisitos como Indicador de IVA o Almacen
                                        }

                                        #region Extrae los Totales
                                        if (informacionValida==true)
                                                {
                                                        #region Calculando valor de la Orden
                                                        System.Web.HttpContext.Current.Session["OrdenCompraServicio"] = ordenCompra;
                                                        System.Web.HttpContext.Current.Session["Detalle_OrdenCompraServicio"] = _tablas;
                                                        System.Web.HttpContext.Current.Session["OrdenCompra"] = ordenCompra;
                                                        Dictionary<int,string> Unidades = new Dictionary<int, string>();
                                                                    try
                                                                    {
                                                                        double sum = 0;
                                                                        int pos = 0;
                                                                        foreach (var item in _tablas)
                                                                        {
                                                                            sum = sum + Convert.ToDouble(item.Cantidad)* Convert.ToDouble(item.Importe);
                                                                            Unidades.Add(pos,item.Unidad_Medida);
                                                                            pos++;
                                                                        }
                                                                        System.Web.HttpContext.Current.Session["SumOrdenCompra"] = sum.ToString();
                                                                    }
                                                                    catch(Exception ex)
                                                                    {

                                                                    }
                                                                    #region Analizando las Unidades de Medida
                                                                        var todasUnidades=Unidades.GroupBy(x=>x.Value).Select(x=>x.FirstOrDefault());
                                                                        if(todasUnidades!=null)
                                                                                    if(todasUnidades.Count()>0)
                                                                                                {
                                                                                                        if(todasUnidades.Count()==1)
                                                                                                            {
                                                                                                                    ClickFactura_Entidades.BD.Entidades.Desarrollo_CF db = new ClickFactura_Entidades.BD.Entidades.Desarrollo_CF();
                                                                                                                    var unidad = todasUnidades.ToArray();
                                                                                                                    string siglasUM=unidad[0].Value;
                                                                                                                    var Unidad = from usu in db.Cat_UMedidaWF where usu.UnidadMedida.Equals(siglasUM) == true select usu;
                                                                                                                    try
                                                                                                                      { 
                                                                                                                         var unidadEncontrada = Unidad.ToArray();
                                                                                                                        string idWFdeUnidad = unidadEncontrada[0].idWF.ToString();
                                                                                                                        string nombreWF = unidadEncontrada[0].DescripcionUM.ToString();
                                                                                                                        System.Web.HttpContext.Current.Session["nombreWF"] = nombreWF;
                                                                                                                        System.Web.HttpContext.Current.Session["idWFxUmedida"] = idWFdeUnidad;
                                                                                                                        System.Web.HttpContext.Current.Session["idUMedida"] = unidadEncontrada[0].idUnidadMedida.ToString();                                                                                                                     
                                                                                                                        }
                                                                                                                    catch
                                                                                                                        {
                                                                                                                            System.Web.HttpContext.Current.Session["nombreWF"] = "Ambiguo!";
                                                                                                                            System.Web.HttpContext.Current.Session["idWFxUmedida"] = "-1";
                                                                                                                            System.Web.HttpContext.Current.Session["idUMedida"] = "-1";
                                                                                                                            return JavaScript("javascript:showErrorMessage('La unidad de Medida " + siglasUM+ " no ha sido especificada en algún flujo previo.Por favor definala en los catálogos correspondientes.');");
                                                                                                                         }
                                                                                                                }
                                                                                                        else
                                                                                                                {
                                                                                                                    System.Web.HttpContext.Current.Session["nombreWF"] = "Ambiguo!";
                                                                                                                    System.Web.HttpContext.Current.Session["idWFxUmedida"] = "-1";
                                                                                                                    System.Web.HttpContext.Current.Session["idUMedida"] = "-1";
                                                                                                                }
                                                                                                }
                                                                                              else
                                                                                                {
                                                                                                        mensajesUsuario = " Al menos una de las líneas en la Orden no tienen especificado la Unidad de Medida ";
                                                                                                        return JavaScript("javascript:showErrorMessage('" + "La orden de compra " + ordenCompra + " tiene problemas para ser procesada. Detalles: " + mensajesUsuario + "'); $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                                                                                                 }
                                                                    #endregion Analizando las Unidades de Medida
                        #endregion Calculando valor de la Orden
                    }
                    else
                                                {
                                                    //La orden de compra no esta correctamente requisitada, se va a informar que NO  se puede continuar
                                                    return JavaScript("javascript:showErrorMessage('" + "La orden de compra " + ordenCompra + " tiene problemas para ser procesada. Detalles: "+ mensajesUsuario + "'); $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                                                }
                                        #endregion Extrae los Totales
                                        return Json(_tablas);
                               }
                      else
                            {
                                return Json(mensajes);
                            }
                        #endregion sin problemas de información
            }
            else
            {
                var model = listado;
                return Json(model);
            }
        }

        [HttpPost]
        public ActionResult cargaVistaPreviaOrdenCompra(string ordenCompra)
        {
            List<KeyValuePair<string, string>> mensajes = new List<KeyValuePair<string, string>>();
            List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> listado = new List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>();
            bool informacionValida = true;
            string mensajesUsuario = "";
            if (ordenCompra.Length > 0)
            {
                #region Carga información

                #region         Recupera lo seleccionado previamente -------------------------------------------------------------------------
                List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> Lineas = System.Web.HttpContext.Current.Session["Detalle_OrdenCompraServicio"] as List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>;
                                #region Determina si es parcial y que posiciones son
                                                    List<KeyValuePair<int, string>> porProcesar = System.Web.HttpContext.Current.Session["posicionesMIGOMIRO"] as List<KeyValuePair<int, string>>;
                                                    if (porProcesar.Count() > 0)
                                                    {
                                                        List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> nuevoListado = new List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>();
                                                        foreach (ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra todos in Lineas)
                                                        {
                                                            foreach (KeyValuePair<int, string> seleccionado in porProcesar)
                                                            {
                                                                if (Convert.ToInt32(todos.Posicion_OC) == seleccionado.Key)
                                                                {
                                                                    nuevoListado.Add(todos);
                                                                }
                                                            }
                                                        }
                                                        Lineas = nuevoListado;
                                                    }
                                #endregion Determina si es parcial y que posiciones son
                #endregion  Recupera lo seleccionado previamente ---------------------------------------------------------------------------
                List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> _tablas =Lineas; //servicio.Carga_DetalleOrden(ordenCompra, ref mensajes);
                if (_tablas.Count > 0)
                {
                    foreach (ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra oc in _tablas)
                    {
                        #region Revisando prerequisitos como Indicador de IVA o Almacen
                                if (oc.Indicador_IVA.Length <= 0)
                                {
                                    informacionValida = false;
                                    oc.Indicador_IVA = "Error: Falta indicador";
                                    mensajesUsuario = "Al menos 1 posición de esta Orden de Compra NO tiene Indicador de Impuestos.";
                                }
                                int result = 0;
                                ClickFactura_WebServiceCF.AccesoBD.Genericos.adT_Parametros adp = new ClickFactura_WebServiceCF.AccesoBD.Genericos.adT_Parametros();
                                List<ClickFactura_WebServiceCF.AccesoBD.Genericos.objT_Parametros> objp = new List<ClickFactura_WebServiceCF.AccesoBD.Genericos.objT_Parametros>();
                                List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
                                string entorno = "";
                                objp = adp.mABCT_Parametros(out result, 0, "validarAlmacen", "Vacio", true, "ConsultaValor");
                                {
                                    entorno = objp[0].ValorParametro.ToString();
                                }
                                if (entorno != null)
                                {
                                    bool validar = entorno.Equals("0") == true ? false : true;
                                    if (oc.Almacen.Length <= 0)
                                    {
                                        if (validar == true)
                                            informacionValida = false;
                                        oc.Almacen = "Atención: Sin almacén";
                                        mensajesUsuario = mensajesUsuario + " Alerta, no se tiene especificado un Almacén destino al invocar el proceso de ingreso de Mercáncias, servicios u otros objetos.";
                                    }
                                }
                        #endregion Revisando prerequisitos como Indicador de IVA o Almacen
                    }
                    #region Extrae los Totales
                    if (informacionValida == true)
                    {
                        #region Calculando valor de la Orden
                        //System.Web.HttpContext.Current.Session["OrdenCompraServicio"] = ordenCompra;
                        //System.Web.HttpContext.Current.Session["Detalle_OrdenCompraServicio"] = _tablas;
                        //System.Web.HttpContext.Current.Session["OrdenCompra"] = ordenCompra;
                        Dictionary<int, string> Unidades = new Dictionary<int, string>();
                        try
                        {
                            double sum = 0;
                            int pos = 0;
                            foreach (var item in _tablas)
                            {
                                sum = sum + Convert.ToDouble(item.Cantidad) * Convert.ToDouble(item.Importe);
                                Unidades.Add(pos, item.Unidad_Medida);
                                pos++;
                            }
                            System.Web.HttpContext.Current.Session["SumOrdenCompra"] = sum.ToString();
                        }
                        catch (Exception ex)
                        {

                        }
                        #region Analizando las Unidades de Medida
                        var todasUnidades = Unidades.GroupBy(x => x.Value).Select(x => x.FirstOrDefault());
                        if (todasUnidades != null)
                            if (todasUnidades.Count() > 0)
                            {
                                if (todasUnidades.Count() == 1)
                                {
                                    ClickFactura_Entidades.BD.Entidades.Desarrollo_CF db = new ClickFactura_Entidades.BD.Entidades.Desarrollo_CF();
                                    var unidad = todasUnidades.ToArray();
                                    string siglasUM = unidad[0].Value;
                                    var Unidad = from usu in db.Cat_UMedidaWF where usu.UnidadMedida.Equals(siglasUM) == true select usu;
                                    var unidadEncontrada = Unidad.ToArray();
                                    string idWFdeUnidad = unidadEncontrada[0].idWF.ToString();
                                    string nombreWF = unidadEncontrada[0].DescripcionUM.ToString();
                                    System.Web.HttpContext.Current.Session["nombreWF"] = nombreWF;
                                    System.Web.HttpContext.Current.Session["idWFxUmedida"] = idWFdeUnidad;
                                    System.Web.HttpContext.Current.Session["idUMedida"] = unidadEncontrada[0].idUnidadMedida.ToString();
                                }
                                else
                                {
                                    System.Web.HttpContext.Current.Session["nombreWF"] = "Ambiguo!";
                                    System.Web.HttpContext.Current.Session["idWFxUmedida"] = "-1";
                                    System.Web.HttpContext.Current.Session["idUMedida"] = "-1";
                                }
                            }
                            else
                            {
                                mensajesUsuario = " Al menos una de las líneas en la Orden no tienen especificado la Unidad de Medida ";
                                return JavaScript("javascript:showErrorMessage('" + "La orden de compra " + ordenCompra + " tiene problemas para ser procesada. Detalles: " + mensajesUsuario + "'); $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                            }
                        #endregion Analizando las Unidades de Medida
                        #endregion Calculando valor de la Orden
                    }
                    else
                    {
                        //La orden de compra no esta correctamente requisitada, se va a informar que NO  se puede continuar
                        return JavaScript("javascript:showErrorMessage('" + "La orden de compra " + ordenCompra + " tiene problemas para ser procesada. Detalles: " + mensajesUsuario + "'); $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                    }
                    #endregion Extrae los Totales
                    return Json(_tablas);
                }
                else
                {
                    return Json(mensajes);
                }
                #endregion sin problemas de información
            }
            else
            {
                var model = listado;
                return Json(model);
            }
        }


        [HttpPost]
        public ActionResult disparaMIGO(string posicion, string porIngresar, string modo)
        {
            List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> listado = new List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>();

            List<ClickFactura_Entidades.BD.Modelos.Mercancias_Recepcionadas> Recepcionado = new List<ClickFactura_Entidades.BD.Modelos.Mercancias_Recepcionadas>();
            try
            {
                string ordenCompra = System.Web.HttpContext.Current.Session["OrdenCompraServicio"] as String;
                if (ordenCompra.Length > 0)
                {
                    ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + ordenCompra] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
                    string _No_Proveedor = encabezado.Proveedor;
                    string _Sociedad = encabezado.Sociedad;
                    List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> Lineas = System.Web.HttpContext.Current.Session["Detalle_OrdenCompraServicio"] as List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>;
                    #region Determina si es parcial y que posiciones son
                    List<KeyValuePair<int, string>> porProcesar = System.Web.HttpContext.Current.Session["posicionesMIGOMIRO"] as List<KeyValuePair<int, string>>;
                    if(porProcesar!=null)
                                if(porProcesar.Count()>0)
                                {
                                    modo = "Parcial";
                                    List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> nuevoListado = new List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>();
                                    foreach(ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra todos in Lineas)
                                    {
                                        foreach(KeyValuePair<int,string> seleccionado in porProcesar)
                                        {
                                            if(Convert.ToInt32(todos.Posicion_OC)==seleccionado.Key)
                                            {
                                                nuevoListado.Add(todos);
                                            }
                                        }
                                    }
                                    Lineas = nuevoListado;
                                }
                    else
                            {
                                List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> nuevoListado = new List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>();
                                foreach (ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra todos in Lineas)
                                {
                                    foreach (KeyValuePair<int, string> seleccionado in porProcesar)
                                    {
                                            nuevoListado.Add(todos);
                                    }
                                }
                                Lineas = nuevoListado;
                            }
                    #endregion Determina si es parcial y que posiciones son
                    if (modo.Equals("Completa") == true)
                    {
                        #region Modo Total
                        foreach (var linea in Lineas)
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
                            recepcion.Num_Proveedor =_No_Proveedor;
                            recepcion.Usuario = "Pruebas";
                            recepcion.Almacen = renglon.Almacen;
                            recepcion.Planta = renglon.Planta;
                            recepcion.Tipo_OrdenCompra = "PO";
                            string mensajes = "";
                            bool generado = servicio.MIGO_creaLineaMIGO(renglon, ref datos, ref mensajes);
                            if (generado == true)
                            {
                                #region        Construye información de lo Recepcionado
                                recepcion.Numero_Recepcion = datos[0].Value;
                                recepcion.Año_Recepcion = datos[1].Value;
                                Recepcionado.Add(recepcion);
                                bool almacenarenBD_MIGO = true;
                                if (almacenarenBD_MIGO == true)
                                {
                                    #region Enviar a almacenar lo generado a base de datos de Portal
                                    generado = servicio.MIGO_almacenarenBD_MIGO(Recepcionado);
                                    #endregion Enviar a almacenar lo generado a base de datos de Portal
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
                        var unaLinea = from registro in Lineas where registro.Posicion_OC.Equals(posicion) == true select registro;

                        if (unaLinea != null)
                        {
                            if (unaLinea.Count() > 0)
                            {
                                foreach (var _linea in unaLinea.AsEnumerable())
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
                                    recepcion.Cantidad = porIngresar==null? Convert.ToDecimal(renglon.Cantidad):Convert.ToDecimal(porIngresar);
                                    recepcion.Cantidad_Base = Convert.ToDecimal(renglon.Cantidad);
                                    recepcion.Unidad_Medida = renglon.Unidad_Medida;
                                    recepcion.Num_Proveedor = _No_Proveedor;
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
                                    renglon.Cantidad = porIngresar == null ? renglon.Cantidad : porIngresar;
                                    string mensajes = "";
                                    //
                                    bool generado = servicio.MIGO_creaLineaMIGO(renglon, ref datos, ref mensajes);
                                    if (generado == true)
                                    {
                                        #region        Construye información de lo Recepcionado
                                        recepcion.Numero_Recepcion = datos[0].Value;
                                        recepcion.Año_Recepcion = datos[1].Value;
                                        Recepcionado.Add(recepcion);
                                        #endregion  Construye información de lo Recepcionado
                                        #region Enviar a almacenar lo generado a base de datos de Portal
                                        generado = servicio.MIGO_almacenarenBD_MIGO(Recepcionado);
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
                                        if(mensajes.Contains("PU Ordered quantity exceeded by") ==true)
                                        {
                                            mensajes = mensajes + " . Esta Orden tiene ya al menos una Recepción relacionada. Intente verificar su Orden por la opción PO Ordenes Cerradas.";
                                        }
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
            catch (Exception ex)
            {
                string error = ex.Message;
                var model = Recepcionado;
                return Json(model);
            }

        }

        [HttpPost]
        public JsonResult SubirFactura(HttpPostedFileBase[] files)
        {
            string oc = "";
            try
            {
                oc = Session["OrdenCompra"].ToString();
            }
            catch
            {
                oc = System.Web.HttpContext.Current.Session["OrdenCompra"] as String;
            }
            string usuario = System.Web.HttpContext.Current.Session["Usuario"] as string;
            bool admin = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(usuario);
            String proveedor = "";
            if (admin == false)
            {
                //proveedor = System.Web.HttpContext.Current.Session["Num_Proveedor"] as string;
                    admin = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsUsuarioInterno(usuario);
                    if (admin == false)
                    {//Es un proveedor!!
                        proveedor = System.Web.HttpContext.Current.Session["Num_Proveedor"] as string;// Session["Num_Proveedor"].ToString();
                        System.Diagnostics.Trace.WriteLine("Es Proveedor");
                }
                    else
                    {
                        //Es un usuario Interno
                        ClickFactura_Facturacion.Genericos.Genericos generico = new ClickFactura_Facturacion.Genericos.Genericos();
                        ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra _encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
                        proveedor = _encabezado.Proveedor;
                        System.Diagnostics.Trace.WriteLine("Es Interno");
                }
            }
            else
            {
                ClickFactura_Facturacion.Genericos.Genericos generico = new ClickFactura_Facturacion.Genericos.Genericos();
                ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra _encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
                proveedor = _encabezado.Proveedor;
                System.Diagnostics.Trace.WriteLine("Es Adminstrador");
            }

            try
            {
                ClickFactura_Facturacion.Clases.paraVerificacionFactura.cs_Factura csfactura = new ClickFactura_Facturacion.Clases.paraVerificacionFactura.cs_Factura();
                bool carga = false;
                string rutaCompleta = string.Empty;
                string nombreArchivo = string.Empty;
                List<string> datos = new List<string>();
                string mensaje = string.Empty;
                string Mes = DateTime.Now.Month.ToString();// System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                String rfc = "";
                rfc = Session["Usuario"] == null ? "AAA010101AAA" : Session["Usuario"].ToString();
                datos.Add(rfc);
                #region Recupera tipo de Orden
                ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
                string _servicio = encabezado.tipoOrdenCompra; ;
                #endregion Recupera tipo de Orden

                datos.Add(proveedor);
                datos.Add(Mes.ToUpper());
                Response.Write("Datos[proveedor]: "+proveedor);
                datos.Add(Session["OrdenCompra"].ToString());
                List<string> mensajes = new List<string>();
                foreach (var file in files)
                {
                    var archivo = file;
                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        var stream = archivo.InputStream;
                        nombreArchivo = archivo.FileName;
                        if (archivo.ContentType.Contains("zip") || archivo.ContentType.Contains("rar"))
                        {
                            string rutaZip = ClickFactura_Facturacion.Clases.cs_Estaticos.GenerarRuta(datos, nombreArchivo);
                            archivo.SaveAs(rutaZip);
                            List<ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.objLeidoFactura> des = servicio.factXML_DescomprimirPaquete(oc, rutaZip, nombreArchivo, datos, out lsMensajes);//oc, rutaZip, nombreArchivo, datos, out lsMensajes);
                            foreach (ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.objLeidoFactura item in des)
                            {
                                var existe = (from t in FacturasCargadas where t.Key.Equals(item.Archivo) select t.Key).FirstOrDefault();
                                if (existe == null)
                                {
                                    FacturasCargadas.Add(item.Archivo, item.Path);
                                }
                            }
                            List<ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.objLeidoFactura> facturas = servicio.factXML_FusionarFacturas(des, FacturasLeidas);
                            FacturasLeidas = facturas;
                            Session["FacturaValidada"] = true;
                            Session["MensajesFactura"] = lsMensajes;
                        }
                        else
                        {
                            datos.Insert(0, archivo.ContentType.Contains("xml") ? "XML" : "PDF");
                            rutaCompleta = ClickFactura_Facturacion.Clases.cs_Estaticos.GenerarRuta(datos, nombreArchivo);
                            archivo.SaveAs(rutaCompleta);
                            carga = true;
                            string rfcF = "";
                            bool Cambiar = false;
                            #region Observar si ya tiene algo almacenado
                            ClickFactura_WebServiceCF.AccesoBD.Genericos.cs_SQL csFactura = new ClickFactura_WebServiceCF.AccesoBD.Genericos.cs_SQL();
                            ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.cs_Factura _csfactura = new ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.cs_Factura();
                            int rv=0;
                            object[] ListaBuscando = { "idFacturasServicios=0", "ordenCompra=" + datos[4].ToString() + "", "numProveedor=" + proveedor + "", "usuario=" + datos[1].ToString() + "", "mes=" + datos[3].ToString() + "", "nombreArchivo=" + nombreArchivo + "", "rutacompleta=" + rutaCompleta.Replace(@"\\", @"\"), "_XML= '", "fechaCarga=" + DateTime.Now.ToString() + "", "procesado=0", "servicio="+_servicio, "P_Opcion=ObtenID" };
                            csFactura.ExecutarSPUnValor<ClickFactura_Entidades.BD.Modelos.T_FacturasServicios>(out rv, "SP_T_FacturasServicios", ListaBuscando);
                            string xmltexto = _csfactura.XMLtoTXT(rutaCompleta);
                            if(rv<=0 && Cambiar==false)
                            {
                                //No existe
                                #region Envia a la Base de Datos el XML a resguardo
                                            System.Data.SqlClient.SqlParameter parametro = new System.Data.SqlClient.SqlParameter();
                                            object[] ListaVariables = { "idFacturasServicios=0", "ordenCompra=" + datos[4].ToString() + "", "numProveedor=" + proveedor + "", "usuario=" + datos[1].ToString() + "", "mes=" + datos[3].ToString() + "", "nombreArchivo=" + nombreArchivo + "", "rutacompleta=" + rutaCompleta.Replace(@"\\", @"\"), "_XML= '", "fechaCarga=" + DateTime.Now.ToString() + "", "procesado=0", "servicio="+_servicio, "P_Opcion=Agrega" };
                                            csFactura.ExecutarSPUnValor<ClickFactura_Entidades.BD.Modelos.T_FacturasServicios>(out rv, "SP_T_FacturasServicios", ListaVariables);
                                            #region Recupera y actualiza el XML
                                                        object[] ListaVariablesID = { "idFacturasServicios=0", "ordenCompra=" + datos[4].ToString() + "", "numProveedor=" + proveedor + "", "usuario=" + datos[1].ToString() + "", "mes=" + datos[3].ToString() + "", "nombreArchivo=" + nombreArchivo + "", "rutacompleta=" + rutaCompleta.Replace(@"\\", @"\"), "_XML= '", "fechaCarga=" + DateTime.Now.ToString() + "", "procesado=0", "servicio=" + _servicio, "P_Opcion=ObtenID" };
                                                        csFactura.ExecutarSPUnValor<ClickFactura_Entidades.BD.Modelos.T_FacturasServicios>(out rv, "SP_T_FacturasServicios", ListaVariablesID);
                                                        ClickFactura_WebServiceCF.Service.Service1 servicio = new ClickFactura_WebServiceCF.Service.Service1();
                                                        string consulta = "Update T_FacturasServicios Set _XML='"+xmltexto+"' Where idFacturasServicios="+rv;
                                                        servicio.genericos_consultaCualquierTabla(consulta);
                                                        System.Web.HttpContext.Current.Session["idFacturasServicios"] = rv.ToString(); //null; //No mostrará nada por que se acaba de insertar.
                                            #endregion
                                #endregion
                            }
                            else
                            {
                                if(rv>=0 && Cambiar==true)
                                {
                                    //Existe pero NO hay permiso de cambiarlo
                                    string consulta = "Update T_FacturasServicios Set _XML='" + xmltexto + "' Where idFacturasServicios=" + rv;
                                    servicio.genericos_consultaCualquierTabla(consulta);
                                    System.Web.HttpContext.Current.Session["idFacturasServicios"] = rv.ToString();
                                }
                            }
                            #endregion
                            List<ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.objLeidoFactura> v = servicio.factXML_ValidarFactura(oc, rutaCompleta, nombreArchivo, datos, out rfcF, out mensajes);
                            List<String> mensajesSalida = new List<string>();
                            mensajesSalida = mensajes;
                            Session["FacturasLeidas"] = v;
                            mensajesSalida.Insert(0, nombreArchivo);
                            List<String> lsMensajesSalida = new List<string>();
                            if (v != null)
                            {
                                List<ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.objLeidoFactura> facturas =servicio.factXML_FusionarFacturas(v, FacturasLeidas);
                                FacturasLeidas = facturas;
                                var existe = (from t in FacturasCargadas where t.Key.Equals(nombreArchivo) select t.Key).FirstOrDefault();
                                if (existe == null)
                                {
                                    FacturasCargadas.Add(nombreArchivo, rutaCompleta);
                                }
                            }
                            Session["FacturaValidada"] = carga;
                            Session["MensajesFactura"] = mensajes;// lsMensajes;
                        }
                    }
                }

                return Json(carga, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Session["FacturaValidada"] = false;
                Session["MensajesFactura"] = new List<string>();
                return Json("Error en la carga del archivo, favor de intentarlo nuevamente.\nError: " + ex.Message);
            }
        }


        [HttpPost]
        public ActionResult addProcesables(string posGrid,string oc)
        {
            bool exitoso=false;
            KeyValuePair<int, string> dato = new KeyValuePair<int, string>(Convert.ToInt32(posGrid),oc);
            try
            {
                if(System.Web.HttpContext.Current.Session["posicionesMIGOMIRO"]!=null)
                {
                   List<KeyValuePair<int, string>> porProcesar=System.Web.HttpContext.Current.Session["posicionesMIGOMIRO"] as List<KeyValuePair<int, string>>;
                   List<KeyValuePair<int, string>> nuevoListado = new List<KeyValuePair<int, string>>();
                    var existe = from previos in porProcesar where previos.Key.Equals(dato.Key) == true select previos;
                    if (existe != null)
                    {
                        if(existe.Count()>0)
                        {
                            // Ya existe asi que solo copiamos todo igual
                            foreach (KeyValuePair<int, string> anteriores in porProcesar)
                            {
                                nuevoListado.Add(dato);
                            }
                            return JavaScript("javascript:showErrorMessage('" + "La línea "+posGrid+" ya habia sido seleccionada previamente!" + "');");
                        }
                        else
                        {
                            //Pasamos todos y agregamos al nuevo
                           foreach (KeyValuePair<int, string> anteriores in porProcesar)
                            {
                                //if(anteriores.Equals(dato)==false)
                                //{
                                    nuevoListado.Add(anteriores);
                                //}
                            }
                            nuevoListado.Add(dato);
                        }
                    }

                   if(nuevoListado.Count()>0)
                    {
                        porProcesar = nuevoListado;
                    }
                     System.Web.HttpContext.Current.Session["posicionesMIGOMIRO"] = porProcesar;
                    exitoso = true;
                    return JavaScript("javascript:showSuccess('" + "La línea " + posGrid + " se agrego correctamente al listado!" + "');");
                }
                else
                {
                    List<KeyValuePair<int, string>> nuevoListado =new List<KeyValuePair<int, string>>();
                    nuevoListado.Add(dato);
                    System.Web.HttpContext.Current.Session["posicionesMIGOMIRO"] = nuevoListado;
                    exitoso = true;
                    return JavaScript("javascript:showSuccess('" + "La línea " + posGrid + " se agrego correctamente al listado!" + "');");
                }
            }
            catch(Exception ex)
            {
                string mensaje = ex.Message;
                return JavaScript("javascript:showErrorMessage('" + "Ocurrío un error agregando la posición " + posGrid + " : "+ mensaje + "');");
            }
            return Json(exitoso);
        }

        [HttpPost]
        public ActionResult ExisteXML(string oc)
        {
            bool resultado = false;
            string OrdenCompra = oc;
            string NumProveedor = "";
            string usuario = System.Web.HttpContext.Current.Session["Usuario"] as string;
            bool admin = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(usuario);
            if (admin==false)
            {
                admin = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsUsuarioInterno(usuario);
                if (admin == false)
                {//Es un proveedor!!
                    NumProveedor =System.Web.HttpContext.Current.Session["Num_Proveedor"] as string;// Session["Num_Proveedor"].ToString();
                   System.Diagnostics.Trace.WriteLine("Es Proveedor");               
                }
                else
                {
                    //Es un usuario Interno
                    ClickFactura_Facturacion.Genericos.Genericos generico = new ClickFactura_Facturacion.Genericos.Genericos();
                    ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra _encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
                    NumProveedor = _encabezado.Proveedor;
                    System.Diagnostics.Trace.WriteLine("Es Interno");
                }
            }
            else
            {
                    ClickFactura_Facturacion.Genericos.Genericos generico = new ClickFactura_Facturacion.Genericos.Genericos();
                    ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra _encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
                    NumProveedor =_encabezado.Proveedor;
                    System.Diagnostics.Trace.WriteLine("Es Administrador");
            }
            string mes = DateTime.Now.Month.ToString();// (System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month)).ToUpper();
            #region Recupera tipo de Orden
            ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
            string servicio = encabezado.tipoOrdenCompra; ;
            #endregion Recupera tipo de Orden
            string procesado = "0";
            ClickFactura_WebServiceCF.AccesoBD.Genericos.cs_SQL csFactura = new ClickFactura_WebServiceCF.AccesoBD.Genericos.cs_SQL();
            int rv = 0;
            object[] ListaBuscando = { "idFacturasServicios=0", "ordenCompra=" + OrdenCompra , "numProveedor=" + NumProveedor , "usuario=" +usuario,"mes=" + mes , "nombreArchivo=" + "ninguno" , "rutacompleta=" + "ninguna" , "_XML='", "fechaCarga=" + DateTime.Now.ToString(), "procesado="+procesado, "servicio="+servicio, "P_Opcion=ExisteXML" };
            csFactura.ExecutarSPUnValor<ClickFactura_Entidades.BD.Modelos.T_FacturasServicios>(out rv, "SP_T_FacturasServicios", ListaBuscando);
                if(rv>0)
                {
                    resultado = true;
                    System.Web.HttpContext.Current.Session["idFacturasServicios"] = rv.ToString(); 
                }
            return Json(resultado);
        }

        [HttpPost]
        public ActionResult soloUsuariosInternos(string oc)
        {
            string usuario = System.Web.HttpContext.Current.Session["Usuario"] as string;
            bool admin = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(usuario);
            bool resultado = false;
            string OrdenCompra = oc;
            //string NumProveedor = System.Web.HttpContext.Current.Session["Num_Proveedor"] as string;
            string NumProveedor = "";
            ClickFactura_Facturacion.Genericos.Genericos generico = new ClickFactura_Facturacion.Genericos.Genericos();
            if (admin == false)
            {
                //NumProveedor = System.Web.HttpContext.Current.Session["Num_Proveedor"] as string;
                admin = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsUsuarioInterno(usuario);
                if (admin == false)
                {//Es un proveedor!!
                    NumProveedor = System.Web.HttpContext.Current.Session["Num_Proveedor"] as string;// Session["Num_Proveedor"].ToString();
                }
                else
                {
                    //Es un usuario Interno
                    ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra _encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
                    NumProveedor = _encabezado.Proveedor;
                }
            }
            else
            {
                ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra _encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
                NumProveedor = _encabezado.Proveedor;
            }
            string mes = (System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month)).ToUpper();
            bool usuariovalido = false;
            string serviciodelUsuario = "Normal";
            #region Recupera tipo de Orden
            ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
            string servicio = encabezado.tipoOrdenCompra; ;
            #endregion Recupera tipo de Orden

            if (admin == false)
            {
                #region es un usuario interno?
                string consulta = "";
                consulta = "SELECT        dbo.Cat_Usuario.IdUsuario, dbo.Cat_Usuario.Usuario, dbo.Cat_Perfil.Perfil, dbo.Cat_Perfil.esUsuarioExterno, dbo.Cat_Perfil.Activo,";
                consulta = consulta + "          dbo.Cat_TipoUsuarioWF.nombreRol, dbo.Cat_ProcesosWF.nombreWF, dbo.Cat_ProcesosWF.idWF";
                consulta = consulta + " FROM            dbo.Cat_Usuario INNER JOIN ";
                consulta = consulta + "        dbo.Cat_Perfil ON dbo.Cat_Usuario.IdPerfil = dbo.Cat_Perfil.IdPerfil INNER JOIN ";
                consulta = consulta + "         dbo.Cat_UsuariosWF ON dbo.Cat_Usuario.IdUsuario = dbo.Cat_UsuariosWF.idUsuarioPortal INNER JOIN ";
                consulta = consulta + "        dbo.Cat_TipoUsuarioWF ON dbo.Cat_UsuariosWF.idTipoUsuarioWF = dbo.Cat_TipoUsuarioWF.idTipoUsuarioWF INNER JOIN ";
                consulta = consulta + "         dbo.Cat_ProcesosWF ON dbo.Cat_TipoUsuarioWF.idWF = dbo.Cat_ProcesosWF.idWF";
                string idusuario = System.Web.HttpContext.Current.Session["IdUsuario"] as string;
                var interno = generico.genericos_consultaCualquierTabla(consulta + " Where idUsuario=" + idusuario);
                if (interno != null)
                {
                    if (interno.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(interno.Rows[0].Field<bool>("esUsuarioExterno").ToString()) == false)
                        {
                            usuariovalido = true;
                            serviciodelUsuario = interno.Rows[0].Field<string>("nombreWF").ToString();
                        }
                    }
                }
                #endregion es un usuario interno?
            }
            else
            {
                usuariovalido = true;
                serviciodelUsuario = servicio;
            }

            if (usuariovalido == true && (servicio.Equals(serviciodelUsuario) == true))
            {
                #region Ejecuta la operación de Eliminado
                //string procesado = "0";
                //ClickFactura_WebServiceCF.AccesoBD.Genericos.cs_SQL csFactura = new ClickFactura_WebServiceCF.AccesoBD.Genericos.cs_SQL();
                //int rv = 0;
                //object[] ListaBuscando = { "idFacturasServicios=0", "ordenCompra=" + OrdenCompra, "numProveedor=" + NumProveedor, "usuario= " + "cualquiera", "mes= " + mes + "", "nombreArchivo= " + "ninguno" + "", "rutacompleta= " + "ninguna", "_XML= '", "fechaCarga=" + DateTime.Now.ToString() + "", "procesado=" + procesado, "servicio=" + servicio, "P_Opcion=ExisteXML" };
                //csFactura.ExecutarSPUnValor<ClickFactura_Entidades.BD.Modelos.T_FacturasServicios>(out rv, "SP_T_FacturasServicios", ListaBuscando);
                //if (rv > 0)
                //{
                //    System.Web.HttpContext.Current.Session["idFacturasServicios"] = rv.ToString();

                //    #region         Ejecuta eliminacion del XML
                //    generico.genericos_consultaCualquierTabla("Delete from T_FacturasServicios Where idFacturasServicios=" + rv.ToString());
                //    resultado = true;
                //    return JavaScript("javascript:showErrorMessage('" + "Factura XML del proveedor " + NumProveedor + " ha sido elimina exitosamente!" + "'); $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                //    #endregion
                //}
                //else
                //{
                //    resultado = false;
                //    return JavaScript("javascript:showErrorMessage('" + "Factura XML del proveedor " + NumProveedor + " NO fue localizada, contacte a Soporte del Portal por favor." + "'); $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                //}
                #endregion Ejecuta la operación de Eliminado
                return Json(new KeyValuePair<bool, string>(true, "Iniciando proceso de recepción de factura!"));
            }
            else
            {
                if (usuariovalido == false)
                {
                    //EL usuario es externo, quizas un Proveedor
                    //return JavaScript("javascript:showErrorMessage('" + "Usted no tiene el permisos necesarios para esta operación" + "'); $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                    return Json(new KeyValuePair<bool,string>(false,"Lo siento, no tiene permisos para continuar. Los siguientes pasos son para personal de ThyssenKrupp y se le notificarán los resultados."));
                }
                else
                {
                    //Quizas el usuario no esta creado para este Proceso
                    return JavaScript("javascript:showErrorMessage('" + "Usted no tiene el Perfil y permisos necesarios para eliminar un archivo XML.Si usted pertencene al Workflow " + servicio + " y considera debería ejecutar esta operación contacte a Soporte del Portal y reportelo." + "');  $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                }
            }
            return Json(resultado);
        }


        [HttpPost]
        public ActionResult EliminarXML(string oc)
        {
            string usuario = System.Web.HttpContext.Current.Session["Usuario"] as string;
            bool admin = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(usuario);
            bool resultado = false;
            string OrdenCompra = oc;
            //string NumProveedor = System.Web.HttpContext.Current.Session["Num_Proveedor"] as string;
            string NumProveedor = "";
            ClickFactura_Facturacion.Genericos.Genericos generico = new ClickFactura_Facturacion.Genericos.Genericos();
            if (admin == false)
            {
                //NumProveedor = System.Web.HttpContext.Current.Session["Num_Proveedor"] as string;
                admin = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsUsuarioInterno(usuario);
                if (admin == false)
                {//Es un proveedor!!
                    NumProveedor = System.Web.HttpContext.Current.Session["Num_Proveedor"] as string;// Session["Num_Proveedor"].ToString();
                    System.Diagnostics.Trace.WriteLine("Es Proveedor");
                }
                else
                {
                    //Es un usuario Interno
                    ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra _encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
                    NumProveedor = _encabezado.Proveedor;
                    System.Diagnostics.Trace.WriteLine("Es Interno");
                }
            }
            else
            {
                ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra _encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
                NumProveedor = _encabezado.Proveedor;
                System.Diagnostics.Trace.WriteLine("Es Administrador");
            }
            string mes = DateTime.Now.Month.ToString();// (System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month)).ToUpper();
            bool usuariovalido = false;
            string serviciodelUsuario = "Normal";
            #region Recupera tipo de Orden
            ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
            string servicio = encabezado.tipoOrdenCompra; ;
            #endregion Recupera tipo de Orden

            if(admin==false)
            {
                #region es un usuario interno?
                string consulta = "";
                consulta = "SELECT        dbo.Cat_Usuario.IdUsuario, dbo.Cat_Usuario.Usuario, dbo.Cat_Perfil.Perfil, dbo.Cat_Perfil.esUsuarioExterno, dbo.Cat_Perfil.Activo,";
                consulta = consulta + "          dbo.Cat_TipoUsuarioWF.nombreRol, dbo.Cat_ProcesosWF.nombreWF, dbo.Cat_ProcesosWF.idWF";
                consulta = consulta + " FROM            dbo.Cat_Usuario INNER JOIN ";
                consulta = consulta + "        dbo.Cat_Perfil ON dbo.Cat_Usuario.IdPerfil = dbo.Cat_Perfil.IdPerfil INNER JOIN ";
                consulta = consulta + "         dbo.Cat_UsuariosWF ON dbo.Cat_Usuario.IdUsuario = dbo.Cat_UsuariosWF.idUsuarioPortal INNER JOIN ";
                consulta = consulta + "        dbo.Cat_TipoUsuarioWF ON dbo.Cat_UsuariosWF.idTipoUsuarioWF = dbo.Cat_TipoUsuarioWF.idTipoUsuarioWF INNER JOIN ";
                consulta = consulta + "         dbo.Cat_ProcesosWF ON dbo.Cat_TipoUsuarioWF.idWF = dbo.Cat_ProcesosWF.idWF";
                string idusuario = System.Web.HttpContext.Current.Session["IdUsuario"] as string;
                var interno=generico.genericos_consultaCualquierTabla(consulta+ " Where idUsuario="+idusuario) ;
                if(interno!=null)
                    {
                        if(interno.Rows.Count>0)
                        {
                                if(Convert.ToBoolean(interno.Rows[0].Field<bool>("esUsuarioExterno").ToString())==false)
                                {
                                      usuariovalido = true;
                                      serviciodelUsuario = interno.Rows[0].Field<string>("nombreWF").ToString();
                                }
                        }
                    }
                #endregion es un usuario interno?
            }
            else
            {
                usuariovalido = true;
                serviciodelUsuario = servicio;
            }

            if(usuariovalido==true && (servicio.Equals(serviciodelUsuario)==true))
            {
                    #region Ejecuta la operación de Eliminado
                    string procesado = "0";
                    ClickFactura_WebServiceCF.AccesoBD.Genericos.cs_SQL csFactura = new ClickFactura_WebServiceCF.AccesoBD.Genericos.cs_SQL();
                    int rv = 0;
                    object[] ListaBuscando = { "idFacturasServicios=0", "ordenCompra=" + OrdenCompra, "numProveedor=" + NumProveedor, "usuario= " + "cualquiera", "mes=" + mes + "", "nombreArchivo= " + "ninguno" + "", "rutacompleta= " + "ninguna", "_XML= '", "fechaCarga=" + DateTime.Now.ToString() + "", "procesado=" + procesado, "servicio=" + servicio, "P_Opcion=ExisteXML" };
                    csFactura.ExecutarSPUnValor<ClickFactura_Entidades.BD.Modelos.T_FacturasServicios>(out rv, "SP_T_FacturasServicios", ListaBuscando);
                    if (rv > 0)
                    {
                        System.Web.HttpContext.Current.Session["idFacturasServicios"] = rv.ToString();

                        #region         Ejecuta eliminacion del XML
                        generico.genericos_consultaCualquierTabla("Delete from T_FacturasServicios Where idFacturasServicios="+ rv.ToString());
                        resultado = true;
                        return JavaScript("javascript:showErrorMessage('" + "Factura XML del proveedor " + NumProveedor+" ha sido elimina exitosamente!"+ "'); $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                    #endregion
                }
                    else
                    {
                        resultado = false;
                    return JavaScript("javascript:showErrorMessage('" + "Factura XML del proveedor " + NumProveedor + " NO fue localizada, contacte a Soporte del Portal por favor." + "'); $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                }
                    #endregion Ejecuta la operación de Eliminado
            }
            else
            {
                if(usuariovalido==false)
                {
                    //EL usuario es externo, quizas un Proveedor
                    return JavaScript("javascript:showErrorMessage('" + "Usted no tiene el permisos necesarios para esta operación" + "'); $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                }
                else
                {
                    //Quizas el usuario no esta creado para este Proceso
                    return JavaScript("javascript:showErrorMessage('" + "Usted no tiene el Perfil y permisos necesarios para eliminar un archivo XML.Si usted pertencene al Workflow " +servicio+" y considera debería ejecutar esta operación contacte a Soporte del Portal y reportelo."+ "');  $('#MyWizard').wizard( 'selectedItem', {step: 1});");
                }
            }

            return Json(resultado);
        }

        [HttpPost]
        public JsonResult ResultadoFactura()
        {
            try
            {
                bool carga = Session["FacturaValidada"] != null ? Convert.ToBoolean(Session["FacturaValidada"]) : false;
                List<List<string>> lsMensajes = new List<List<string>>();
                List<string> _lsMensajes = new List<string>();
                try
                {
                    lsMensajes = Session["MensajesFactura"] != null ? (List<List<string>>)Session["MensajesFactura"] : new List<List<string>>();
                }
                catch (Exception ex)
                {
                    _lsMensajes = Session["MensajesFactura"] != null ? (List<string>)Session["MensajesFactura"] : new List<string>();
                    lsMensajes.Add(_lsMensajes);
                }
                List<List<string>> mensajes = new List<List<string>>();
                elementos = new List<string>();
                if (carga)
                {
                    string elemento = "";
                    //foreach (var item in lsMensajes)
                    var item = lsMensajes[lsMensajes.Count - 1];
                    {
                        string valor = "";
                        for (int i = 1; i < item.Count; i++)
                        {
                            if (item[i].Substring(0, 1).Equals("0"))
                            {
                                valor = "0";
                                if (item[i].Contains("El sello del emisor es invalido."))
                                {
                                    valor = "2";
                                    item[i] = "2" + item[i].Substring(1);
                                    break;
                                }
                            }
                        }
                        var v = (from t in item select t).LastOrDefault();
                        if (valor == "0")
                        {
                            item.Add(false.ToString());
                            mensajes.Add(item);
                            elemento = "<div class=\"alert alert-danger alert-dismissable panel-body\">"
                                            + "<div class=\"col-md-4\">"
                                                + "<img id=\"icono\" class=\"img-circle\" style=\"width:30px;\" src=\"/Images/bafar/iconNoProcedio.png\" />"
                                                + "<strong> Factura: " + item[0] + "</strong>"
                                            + "</div>"
                                            + "<div class=\"col-md-2\">"
                                                + "<button type=\"button\" class=\"btn btn-danger\" onclick=\"AbrirDetallesMensajes('" + item[0] + "')\"><i class=\"fa  fa-times\"></i>  Detalles</button>"
                                            + "</div>"
                                      + "</div>";
                            elementos.Add(elemento);
                        }
                        else if (valor == "2")
                        {
                            item.Add(true.ToString());
                            mensajes.Add(item);
                            elemento = "<div class=\"alert alert-warning alert-dismissable panel-body\">"
                                            + "<div class=\"col-md-4\">"
                                                + "<img id=\"icono\" class=\"img-circle\" style=\"width:30px;\" src=\"/Images/bafar/iconNoProcedio.png\" />"
                                                + "<strong> Factura: " + item[0] + "</strong>"
                                            + "</div>"
                                            + "<div class=\"col-md-2\">"
                                                + "<button type=\"button\" class=\"btn btn-warning\" onclick=\"AbrirDetallesMensajes('" + item[0] + "')\"><i class=\"fa fa-warning\"></i> Detalles</button>"
                                            + "</div>"
                                      + "</div>";
                            elementos.Add(elemento);
                        }
                        else //if (v != "False")
                        {
                            var nom = (from t in FacturasLeidas where t.Archivo == item[0].Trim() select t).FirstOrDefault();
                            elemento = "<div class=\"alert alert-success alert-dismissable\">"
                                        + "<img id=\"icono\" class=\"img-circle\" style=\"width:30px;\" src=\"/Images/bafar/iconProcedio.png\" />"
                                        + "<strong> Factura Correcta: " + nom.Archivo + "</strong>"
                                   + "</div>";
                            var existe = (from t in elementos where t.Contains(elemento) select t).FirstOrDefault();
                            if (existe == null)
                                elementos.Add(elemento);
                        }

                    }
                    return Json(new
                    {
                        elementos = elementos,
                        mensajes = mensajes,
                    });
                }
                else
                {
                    return Json("La carga no se realizó");
                }
            }
            catch
            {
                return Json("La carga no se realizó");
            }

        }


        [HttpPost]
        public ActionResult VerOnlineXML(string oc)
        {
            string Ruta = "";
            string  _rv = "";
            int rv = 0;
            bool existe = false;
            #region Recupera tipo de Orden
            ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra encabezado = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
            string _servicio = encabezado.tipoOrdenCompra; ;
            #endregion Recupera tipo de Orden
            byte[] data=null;
            string suma="0";
            string SumordenCompra = "0";
            string folio = "";
            if(System.Web.HttpContext.Current.Session["idFacturasServicios"]!=null)
            {
                 _rv = System.Web.HttpContext.Current.Session["idFacturasServicios"] as string;
                 ClickFactura_WebServiceCF.Service.Service1 servicio = new ClickFactura_WebServiceCF.Service.Service1();
                 string consulta = "Select _XML,rutaCompleta From  T_FacturasServicios Where idFacturasServicios=" + _rv;
                 System.Data.DataTable t=servicio.genericos_consultaCualquierTabla(consulta);
                 if (Convert.ToInt64(_rv) > 0)
                 {
                     existe= true;
                     if(t.Rows!=null)
                     {
                         string encodedString = t.Rows[0].ItemArray[0].ToString();
                         Ruta = t.Rows[0].ItemArray[1].ToString();
                         data = Convert.FromBase64String(encodedString);
                         string decodedString = Encoding.UTF8.GetString(data);
                          ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.cs_Factura _csfactura = new ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.cs_Factura();
                          suma=_csfactura.recuperaunValor(decodedString, "Total");
                          folio = _csfactura.recuperaunValor(decodedString, "Folio");
                          suma =suma.Equals("Total")==true?_csfactura.recuperaunValor(decodedString, "total"):suma;
                          folio = folio.Equals("Folio") == true ? _csfactura.recuperaunValor(decodedString, "folio"):folio;
                        #region Analiza y extrae datos del XML
                        List<ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.objLeidoFactura> Listafacturas=new List<ClickFactura_Facturacion.version3_3.Clases33.paraVerificacionFactura33.objLeidoFactura>();
                         bool ultimoResultadoEvaluado=false;
                         string RFCEmisor="";
                         string RFCReceptor="";
                         string UUID="";
                         string FolioFactura="";
                         string[] listaErrores={};
                         _csfactura.Lectura_FacturaExpress33("SoloLectura", ref Listafacturas, Ruta, ref ultimoResultadoEvaluado, ref RFCEmisor, ref RFCReceptor, ref UUID, ref FolioFactura, ref listaErrores);
                         string param = "";
                         string fechaFactura = "";
                         string fechaPedimento = "";

                         string folioFactura = "";
                         string importe = "";
                         string moneda = "";
                         string subTotalXml = "";
                         string sociedad = "";
                         bool calculaImpuesto = false;
                         bool cargoPosterior = false;
                         string importeRetencionFlete = "";
                         string proveedor_Diferente = "";
                         string retencionISRRenta = "";
                         string retencionIVARenta = "";
                         string tipoFactura = "";

                         foreach(var factura in Listafacturas )
                         {
                             fechaFactura = factura.Fecha;
                             fechaPedimento = factura.Fecha;

                              folioFactura = factura.Folio;
                              importe = factura.Timporte;
                              moneda = factura.Moneda;

                              subTotalXml =factura.SubTotal;
                              UUID = factura.UUID;

                              sociedad = "";

                             ClickFactura_Entidades.BD.Modelos.objAnalizarImpuestos objImpuestos=new ClickFactura_Entidades.BD.Modelos.objAnalizarImpuestos();
                             objImpuestos.Factura33 = factura.Impuestos33;
                             objImpuestos.version = factura.Version;
                             objImpuestos.subTotalXml = factura.SubTotal;

                             servicio.factXML_IdentificaImpuestos(ref objImpuestos);


                             calculaImpuesto = objImpuestos.calculaImpuesto;
                             //bool cargoPosterior = false;

                             if(objImpuestos.tipoFactura.Equals("Flete")==true)
                             {
                                  importeRetencionFlete = objImpuestos._importeIVARenta;
                             }
                             if(objImpuestos.tipoFactura.Equals("Honorarios")==true)
                             {
                                 retencionISRRenta = objImpuestos._importeISRRenta;
                             }
                             if(objImpuestos.tipoFactura.Equals("Renta")==true)
                             {
                                 if(Convert.ToDecimal(objImpuestos._importeISRRenta)>0)
                                 {
                                    retencionISRRenta = objImpuestos.Importe;
                                 }
                                 if(Convert.ToDecimal(objImpuestos._importeIVARenta)>0)
                                 {
                                    retencionIVARenta = objImpuestos.Importe;                                 
                                 }
                             }
                             tipoFactura = objImpuestos.tipoFactura;
                         }
                         string parametros=" fechaFactura='"+ fechaFactura+"'";
                         parametros = parametros + ", fechaPedimento='" + fechaPedimento+"'";
                         parametros = parametros + ", moneda ='" + moneda + "'";
                         if (retencionISRRenta.Length>0)
                             parametros = parametros + ", retencionISRRenta ='" + retencionISRRenta + "'";
                         if (retencionIVARenta.Length > 0)
                             parametros = parametros + ", retencionIVARenta ='" + retencionIVARenta + "'";
                         parametros = parametros + ", subTotalXml ='" + subTotalXml + "'";
                         parametros = parametros + ", UUID ='" + UUID + "'";
                         string _calculaImpuesto = "0";
                         if (calculaImpuesto == true)
                             _calculaImpuesto = "1";
                         parametros = parametros + ", calculaImpuesto='" + _calculaImpuesto + "'";
                         parametros = parametros + ", folioFactura ='" + folioFactura + "'";
                         parametros = parametros + ", importe ='" + importe + "'";

                         if (tipoFactura.Equals("Flete") == true)
                         {
                             parametros=parametros+ ", importeRetencionFlete"+ importeRetencionFlete;
                         }
                         if (tipoFactura.Equals("Honorarios") == true)
                         {
                              
                         }
                         if (tipoFactura.Equals("Renta") == true)
                         {
                               
                         }
                         consulta = "Update  T_FacturasServicios Set "+parametros+" Where idFacturasServicios=" + _rv;
                         servicio.genericos_consultaCualquierTabla(consulta);
                         #endregion Analiza y extrae datos del XML
                     }
                     //System.Web.HttpContext.Current.Session["idFacturasServicios"] = rv.ToString();
                 }
            }
            if(existe==true)
            {
                    try
                    {
                        System.IO.File.WriteAllBytes(@"C:\Temp\"+oc+"_"+ encabezado.Proveedor + ".xml", data); //Session["Num_Proveedor"].ToString()+".xml", data);
                    }
                    catch(Exception ex)
                    {
                        string mensaje = "Error en la carga y vista del archivo XML, favor de intentarlo nuevamente.\nError: " + ex.Message;
                        return Json(existe);
                    }
            }

            #region Recalculando valor de la Orden
                    List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> Lineas = System.Web.HttpContext.Current.Session["Detalle_OrdenCompraServicio"] as List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>;
                    #region Determina si es parcial y que posiciones son
                            List<KeyValuePair<int, string>> porProcesar = System.Web.HttpContext.Current.Session["posicionesMIGOMIRO"] as List<KeyValuePair<int, string>>;
                            if(porProcesar!=null)
                                    if (porProcesar.Count() > 0)
                                    {
                                                List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> nuevoListado = new List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>();
                                                foreach (ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra todos in Lineas)
                                                {
                                                    foreach (KeyValuePair<int, string> seleccionado in porProcesar)
                                                    {
                                                        if (Convert.ToInt32(todos.Posicion_OC) == seleccionado.Key)
                                                        {
                                                            nuevoListado.Add(todos);
                                                        }
                                                    }
                                                }
                                                Lineas = nuevoListado;
                                      }
                            else
                                    {
                                        List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> nuevoListado = new List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>();
                                        foreach (ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra todos in Lineas)
                                        {
                                            foreach (KeyValuePair<int, string> seleccionado in porProcesar)
                                            {
                                                    nuevoListado.Add(todos);
                                            }
                                        }
                                        Lineas = nuevoListado;
                                     }
                    #endregion Determina si es parcial y que posiciones son
                    try
                    {
                        double sum = 0;
                        foreach (var item in Lineas)
                        {
                            sum = sum + (Convert.ToDouble(item.Cantidad) * Convert.ToDouble(item.Importe))*(item.Indicador_IVA!=null? item.Indicador_IVA.Equals("V3")==true?1.16:1:1);
                        }
                        System.Web.HttpContext.Current.Session["SumOrdenCompra"] = sum.ToString();
                    }
                    catch (Exception ex)
                    {
                    }
            #endregion Recalculando valor de la Orden
            SumordenCompra = System.Web.HttpContext.Current.Session["SumOrdenCompra"] as String;
            SumordenCompra = SumordenCompra != null ? SumordenCompra : "0";
            List<string> datos = new List<string>();
            datos.Add(existe.ToString());
            datos.Add(SumordenCompra);
            datos.Add("$" + suma);
            datos.Add(folio);
            return Json(datos);
        }

        [HttpPost]
        public ActionResult pasandoTab(string oc)
        {
            List<string> datos = new List<string>();
            datos.Add("Pasando");
            return Json(datos);
        }

        [HttpPost]
        public ActionResult procesoWorkflow(string oc, bool aprobacion)
        {
            ClickFactura_WebServiceCF.Service.Service1 servicio = new ClickFactura_WebServiceCF.Service.Service1();
            List<string> datos = new List<string>();
            string observaciones = "";
            string usuario = (string)Session["Usuario"].ToString();
            string idfacturasservicios = System.Web.HttpContext.Current.Session["idFacturasServicios"] as string;
            string idwf = "";
            string idumedida = "";
            string idusuariowf = "";
            string idestadowf = "0";
            string recepcion = idfacturasservicios;

            //ViewData["nombreWF"] = nombreWF;
            idwf = System.Web.HttpContext.Current.Session["idWFxUmedida"] as string;
            idumedida = System.Web.HttpContext.Current.Session["idUMedida"] as string;

            //Recupera a que WorkFlow y que ID de usuario empata con el usuario que se logeo
            #region Recupera quien operará el Workflow
            string consulta = "";
            consulta = consulta + " SELECT dbo.Cat_UsuariosWF.nombre, dbo.Cat_UsuariosWF.idUsuarioWF, dbo.Cat_TipoUsuarioWF.nombreRol AS TipoUsuario, dbo.Cat_ProcesosWF.idWF,  ";
            consulta = consulta + " dbo.Cat_ProcesosWF.nombreWF AS Proceso, dbo.Cat_Usuario.IdUsuario, dbo.Cat_Usuario.IdProveedor, dbo.Cat_Usuario.IdPerfil, dbo.Cat_Usuario.Usuario, dbo.Cat_UMedidaWF.idUnidadMedida  ";
            consulta = consulta + " FROM dbo.Cat_UsuariosWF INNER JOIN  dbo.Cat_ProcesosWF INNER JOIN  dbo.Cat_TipoUsuarioWF ON dbo.Cat_ProcesosWF.idWF = dbo.Cat_TipoUsuarioWF.idWF ON ";
            consulta = consulta + " dbo.Cat_UsuariosWF.idTipoUsuarioWF = dbo.Cat_TipoUsuarioWF.idTipoUsuarioWF INNER JOIN dbo.Cat_Usuario ON dbo.Cat_UsuariosWF.idUsuarioPortal = dbo.Cat_Usuario.IdUsuario INNER JOIN dbo.Cat_UMedidaWF ON dbo.Cat_ProcesosWF.idWF = dbo.Cat_UMedidaWF.idWF ";
            consulta = consulta + " WHERE  (dbo.Cat_Usuario.Usuario = '"+usuario+ "') And  (dbo.Cat_ProcesosWF.idWF="+idwf+ ")  AND (dbo.Cat_UMedidaWF.idUnidadMedida = " + idumedida + ")";
            //consulta = consulta + " WHERE  (dbo.Cat_UsuariosWF.nombre = '" + usuario + "')  ";
            bool EjecutaProceso = false;
            
            ClickFactura_Facturacion.Genericos.Genericos generico = new ClickFactura_Facturacion.Genericos.Genericos();
            #region             Ejecuta extracción de todos los proveedores de la tabla Cat_Proveedor
            var _proveedor = generico.genericos_consultaCualquierTabla(consulta);
            if(_proveedor!=null)
            {
                if(_proveedor.Rows.Count>0)
                {
                    //identificaFlujoyUsuario(usuario,ref idwf,ref idusuariowf);
                    DataRow infoWorkflow = _proveedor.Rows[0];
                    idwf = infoWorkflow.Field<Int16>("idWF").ToString();
                    idusuariowf = infoWorkflow.Field<Int16>("idUsuarioWF").ToString();
                    if(Convert.ToInt16(idwf)>-1)
                    {
                        if (Convert.ToInt16(idusuariowf) > -1)
                        {
                            EjecutaProceso = true;
                        }
                        else
                        {
                            datos.Add("Error");
                            datos.Add("El usuario logeado no fue localizado dentro de los procesos Workflow.");
                        }
                    }
                    else
                    {
                        datos.Add("Error");
                        datos.Add("La unidad de medida al parecer no esta enlazada a un Workflow.");
                    }

                }
                else
                {
                    //El usuario no existe en las tablas de Workflow
                    idwf = "-1";
                    idusuariowf = "-1";
                }
            }
            #endregion      Ejecuta extracción de todos los proveedores de la tabla Cat_Proveedor
            #endregion

            if(EjecutaProceso==true)
            {
                    List<string> parametros = new List<string>();
                    parametros.Add(idwf);
                    parametros.Add(idusuariowf);
                    parametros.Add(idestadowf);
                    parametros.Add(aprobacion==false?"false":"true");
                    parametros.Add(oc);
                    parametros.Add(recepcion);
                    parametros.Add(idfacturasservicios);

                    aprobacion=servicio.MIGO_Administra_WorkFlow(oc,ref observaciones,parametros);
                    if(aprobacion==true)
                    {
                        datos.Add("OK");
                        datos.Add("Su aprobado/rechazo ha sido almacenado.");
                    }
                    else
                    {
                        datos.Add("Error");
                        consulta = "Select mensaje from dbo.T_BitacoraWF Where OrdenCompra='" + oc + "' And   idWF=" + idwf + " And idUsuarioWF=" + idusuariowf + " And Recepcion=''+( Select folioFactura from dbo.T_FacturasServicios Where idFacturasServicios=" + parametros[5].ToString() + " )+''";
                        var mensaje=servicio.genericos_consultaCualquierTabla(consulta);
                        if(mensaje!=null && mensaje.Rows.Count>0)
                        {
                            observaciones=mensaje.Rows[0].ItemArray[0].ToString();
                        }
                        else
                        {
                            observaciones="Ocurrío un problema recuperando información del Portal.";
                        }
                        datos.Add(" "+observaciones);
                    }
            }
            else
            {
                datos.Add("Error");
                datos.Add("El usuario logeado no fue localizado dentro de los procesos Workflow.");
            }
            return Json(datos);
        }

        [HttpPost]
        public ActionResult EntraVerifica(string oc, string norecepcion,string _servicio)
        {
            ClickFactura_WebServiceCF.Service.Service1 servicio = new ClickFactura_WebServiceCF.Service.Service1();
            List<ClickFactura_Entidades.BD.Entidades.T_FacturasServicios> lista_tfacturasservicios = new List<ClickFactura_Entidades.BD.Entidades.T_FacturasServicios>();
            List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> _tablas = System.Web.HttpContext.Current.Session["Detalle_OrdenCompraServicio"] as List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>;
            List<ClickFactura_Entidades.BD.Modelos.CestaMigoMiro> cestaMigoMiro=new List<ClickFactura_Entidades.BD.Modelos.CestaMigoMiro>();
            ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle[] detallesPasivos=new ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle[30];
            string PMNTTRMS = "";
            string _OrdenCompra = "";
            List<string> datos = new List<string>();
            bool porCesta = false;
            int posicionesFactura=0;
            string NumProveedor = System.Web.HttpContext.Current.Session["Num_Proveedor"] as string;// Session["Num_Proveedor"].ToString();

            #region Identifica si es una sola orden o es una cesta de ordenes
            try
            {
                List<ClickFactura_Entidades.BD.Modelos.CestaMigoMiro> _cestaMigoMiro = System.Web.HttpContext.Current.Session["CestaMigoMiro"] as List<ClickFactura_Entidades.BD.Modelos.CestaMigoMiro>;
                if(_cestaMigoMiro!=null)
                {
                    #region       Varias Ordenes
                    if (_cestaMigoMiro.Count()>0)
                    {
                        foreach(var cesta in _cestaMigoMiro)
                        {
                            oc=cesta.OrdenCompra;
                            _servicio=cesta.Servicio;
                            lista_tfacturasservicios=servicio.MIGOMIRO_recuperaXMLServicios(ref _cestaMigoMiro,oc,NumProveedor,_servicio);
                        }
                        porCesta = true;
                        cestaMigoMiro = _cestaMigoMiro;
                    }
                    #endregion Varias Ordenes
                }
                else
                {
                    #region Orden Compra única
                    //List<ClickFactura_Entidades.BD.Modelos.CestaMigoMiro> cestaMigoMiro=new List<ClickFactura_Entidades.BD.Modelos.CestaMigoMiro>();
                    foreach (var ordencompraUnica in _tablas)
                    {
                        ClickFactura_Entidades.BD.Modelos.CestaMigoMiro cestaparcial = new ClickFactura_Entidades.BD.Modelos.CestaMigoMiro();
                        cestaparcial.Servicio = _servicio;
                        cestaparcial.OrdenCompra = oc;
                        cestaparcial.NumProveedor = NumProveedor;
                        cestaparcial.NoRecepcion = norecepcion;
                        cestaparcial.PosicionOC = ordencompraUnica.Posicion_OC;
                        cestaparcial.XMLAdjuntoValido = false;
                        var _impuesto = from imp in _tablas where imp.Orden_Compra.Equals(oc) == true && imp.Posicion_OC.Equals(ordencompraUnica.Posicion_OC) select imp;
                        string indicadorIVA = "";
                        foreach (var i in _impuesto)
                        {
                            indicadorIVA = i.Indicador_IVA;
                            break;
                        }
                        cestaparcial.IndicadorIVA = indicadorIVA;
                        _cestaMigoMiro.Add(cestaparcial);
                    }
                    lista_tfacturasservicios = servicio.MIGOMIRO_recuperaXMLServicios(ref _cestaMigoMiro, oc, NumProveedor, _servicio);
                    #endregion Orden Compra única
                }
            }
            catch(Exception ex)
            {
                 //List<ClickFactura_Entidades.BD.Modelos.CestaMigoMiro> cestaMigoMiro=new List<ClickFactura_Entidades.BD.Modelos.CestaMigoMiro>();
                foreach(var ordencompraUnica in _tablas)
                {
                     ClickFactura_Entidades.BD.Modelos.CestaMigoMiro cestaparcial = new ClickFactura_Entidades.BD.Modelos.CestaMigoMiro();
                     cestaparcial.Servicio = _servicio;
                     cestaparcial.OrdenCompra = oc;
                     cestaparcial.NumProveedor = NumProveedor;
                     cestaparcial.NoRecepcion = norecepcion;
                     cestaparcial.PosicionOC = ordencompraUnica.Posicion_OC;
                     cestaparcial.XMLAdjuntoValido = false;
                     var _impuesto = from imp in _tablas where imp.Orden_Compra.Equals(oc) == true && imp.Posicion_OC.Equals(ordencompraUnica.Posicion_OC) select imp;
                     string indicadorIVA = "";
                     foreach (var i in _impuesto)
                     {
                         indicadorIVA = i.Indicador_IVA;
                         break;
                     }
                     cestaparcial.IndicadorIVA = indicadorIVA;
                     cestaMigoMiro.Add(cestaparcial);
                }
                 lista_tfacturasservicios=servicio.MIGOMIRO_recuperaXMLServicios(ref cestaMigoMiro,oc,NumProveedor,_servicio);
            }
            #endregion Identifica si es una sola orden o es una cesta de ordenes

            #region       Declarando los Tablas que recibiran la información de SAP
            List<KeyValuePair<string, DataTable>> estructurasRecepciones = new List<KeyValuePair<string, DataTable>>();
            foreach(var cesta in cestaMigoMiro)
                {
                    oc = cesta.OrdenCompra;
                    KeyValuePair<string, DataTable> estructuraRecepcion = servicio.MIGOMIRO_recuperaRecepcionesServicios(oc);
                    estructurasRecepciones.Add(estructuraRecepcion);
                }
           #endregion Declarando los Tablas que recibiran la información de SAP


            ClickFactura_Entidades.BD.Modelos.Pasivo_Encabezado HPasivo = new ClickFactura_Entidades.BD.Modelos.Pasivo_Encabezado();
            List<ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle> DPasivo = new List<ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle>();
            #region        Analizar que tipo de factura es: Mercancias, Flete, Rentas, Honorarios, etc
            bool calcularImpuesto=false;
            bool cargoPosterior=false;

            #endregion Analizar que tipo de factura es: Mercancias, Flete, Rentas, Honorarios, etc

            foreach(var cesta in cestaMigoMiro)
            {
                    DataTable RecepcionadosfromSAP=new DataTable();
                    DataTable listaRecepcionesAnalizadas=servicio.MIGOMIRO_obtenmisRecepciones(cesta.OrdenCompra,estructurasRecepciones);
                    //############################################################################################################################################################
                    #region Construyendo Detalles
                    //listaRecepcionesAnalizadas Registros con las lineas por facturar
                    var RecepcionessinQs = (from conQ in listaRecepcionesAnalizadas.AsEnumerable() where conQ.Field<string>("Tipo_Movimiento").Equals("Q") == false select conQ).ToList();
                    RecepcionadosfromSAP = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.ToDataTable(RecepcionessinQs);
                    int NoregistrosRecepcionados = RecepcionadosfromSAP.Rows.Count;
                    foreach (DataRow ren in RecepcionessinQs)//RecepcionessinQs.ToList())
                    {
                        ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle _DPasivo = new ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle();
                        _DPasivo.CantidadConcepto =ren.Field<string>("Cantidad").ToString();
                        _DPasivo.ClaseCondicion = "";
                        _DPasivo.ImporteConcepto = ren.Field<string>("Importe_Documento").ToString();
                        _DPasivo.Impuesto = cesta.IndicadorIVA;
                        _DPasivo.NumeroRecepcion = ren.Field<string>("Documento_Referencia").ToString();
                        _DPasivo.PosicionRecepcion = ren.Field<string>("Posicion_DocumentoRef").ToString();
                        _DPasivo.YearWE = ren.Field<string>("YearDocumentoRef").ToString();
                        _DPasivo.OrdenCompra = cesta.OrdenCompra;
                        _DPasivo.PosicionOrden = ren.Field<string>("Posicion_OrdenCompra").ToString();
                        _DPasivo.PosicionFactura = (posicionesFactura++).ToString();
                        DPasivo.Add(_DPasivo);
                    }
                    //RecepcionadosfromSAP = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.ToDataTable(RecepcionessinQs);
                    #endregion Construyendo Detalles
                    //############################################################################################################################################################
            }

            if (DPasivo.Count()>0)
            {
                foreach (var facturas in lista_tfacturasservicios)
                {
                    string nombreFactura = facturas.nombreArchivo;
                    string ordencompra = facturas.ordenCompra;
                    bool procesado = (bool)facturas.procesado;
                    List<ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle> DPasivo_filtradosxFactura = new List<ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle>();
                    var _DPasivo_filtradosxFactura = from dpasivo in DPasivo where dpasivo.OrdenCompra.Equals(ordencompra) == true select dpasivo;
                    detallesPasivos = new ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle[_DPasivo_filtradosxFactura.Count()];
                    int pos = 0;
                    foreach (var detalle in _DPasivo_filtradosxFactura)
                    {
                        ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle _detalle = new ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle();
                        _detalle.CantidadConcepto = detalle.CantidadConcepto;
                        _detalle.ClaseCondicion = detalle.ClaseCondicion;
                        _detalle.ImporteConcepto = detalle.ImporteConcepto;
                        _detalle.Impuesto = detalle.Impuesto;
                        _detalle.NumeroRecepcion = detalle.NumeroRecepcion;
                        _detalle.OrdenCompra = detalle.OrdenCompra;
                        _detalle.PosicionFactura = detalle.PosicionFactura;
                        _detalle.PosicionOrden = detalle.PosicionOrden;
                        _detalle.PosicionRecepcion = detalle.PosicionRecepcion;
                        _detalle.YearWE = detalle.YearWE;
                        _detalle.UnidadMedida = "SER";
                        detallesPasivos[pos] = _detalle;
                        pos++;
                    }
                }

                #region        Incrusta la Unidad de Medida desde Orden de Compra ///////////////////////////////////
                List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> Lineas = System.Web.HttpContext.Current.Session["Detalle_OrdenCompraServicio"] as List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra>;
                if (detallesPasivos.Count() > 0)
                {
                    #region Extrae e ingresa la unidad de medida

                    foreach (ClickFactura_Entidades.BD.Modelos.Pasivo_Detalle renRecepcion in detallesPasivos.ToArray())
                    {
                                foreach (var linea in Lineas)
                                {
                                    #region Pasa Unidad
                                    ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra renglon = (ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra)linea;
                                    ClickFactura_Entidades.BD.Modelos.Mercancias_Recepcionadas recepcion = new ClickFactura_Entidades.BD.Modelos.Mercancias_Recepcionadas();
                                    if (renglon.Orden_Compra.Equals(oc) == true)
                                    {
                                        if (renRecepcion.PosicionOrden.Equals(renRecepcion.PosicionOrden) == true)
                                        {
                                            //recepcion.Unidad_Medida = renglon.Unidad_Medida;
                                            renRecepcion.UnidadMedida = renglon.Unidad_Medida;
                                            //PMNTTRMS=renRecepcion.
                                        }
                                    }
                                    #region resto Datos
                                    //List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
                                    //recepcion.Hora_Inicio = DateTime.Now.ToUniversalTime().ToString();
                                    //recepcion.Fecha_Inicio = DateTime.Now.ToShortDateString();
                                    //recepcion.Transaccion_Confirmada = false;
                                    //recepcion.OrdenCompra = renglon.Orden_Compra;
                                    //recepcion.Posicion_OC = renglon.Posicion_OC;
                                    //recepcion.Cantidad = Convert.ToDecimal(renglon.Cantidad);
                                    //recepcion.Cantidad_Base = Convert.ToDecimal(renglon.Cantidad);
                                    //recepcion.Num_Proveedor = "Pendiente";
                                    //recepcion.Usuario = "Pruebas";
                                    //recepcion.Almacen = renglon.Almacen;
                                    //recepcion.Planta = renglon.Planta;
                                    //recepcion.Tipo_OrdenCompra = "PO";
                                    #endregion resto Datos
                                    string mensajes = "";
                                    #endregion Pasa Unidad
                                }
                    }
                    #endregion Extrae e ingresa la unidad de medida
                }
                #endregion  Incrusta la Unidad de Medida desde Orden de Compra ///////////////////////////////////
               
                #region Construyendo Encabezado
                   string idfacturaservicio = System.Web.HttpContext.Current.Session["idFacturasServicios"] as string;
                   string consulta = "Select * from dbo.T_FacturasServicios Where idFacturasServicios="+idfacturaservicio;
                   var paraCabecera = servicio.genericos_consultaCualquierTabla(consulta);
                    if(paraCabecera!=null)
                    {
                        if(paraCabecera.Rows.Count>0)
                        {
                            #region Nueva recuperacion de datos del Proveedor
                                        ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra datosOrdenCompra = System.Web.HttpContext.Current.Session["Encabezado_" + oc] as ClickFactura_Entidades.BD.Modelos.EncabezadoOrdenCompra;
                                        HPasivo.Sociedad = datosOrdenCompra.Sociedad;
                                        HPasivo.Moneda = datosOrdenCompra.Moneda;
                                        HPasivo.Proveedor_Diferente = datosOrdenCompra.Proveedor;
                                        HPasivo.PMNTTRMS = datosOrdenCompra.PMNTTRMS;
                            #endregion Nueva recuperacion de datos del Proveedor
                            foreach(DataRow campo in paraCabecera.Rows)
                            {
                                HPasivo.Factura = false;//valido;
                                string fechafactura = servicio.formatearfechaparaSAP(campo.ItemArray[12].ToString());
                                HPasivo.FechaFactura = fechafactura;
                                string fechapedimento = servicio.formatearfechaparaSAP(campo.ItemArray[13].ToString());
                                HPasivo.FechaPedimento = fechapedimento; 
                                if(campo.ItemArray[14].ToString()=="true")
                                {
                                    calcularImpuesto = true;
                                }
                                if (campo.ItemArray[15].ToString()!=null)
                                {
                                    if (campo.ItemArray[15].ToString()=="true")
                                            cargoPosterior = true;
                                }
                                HPasivo.ClaseDoc = "RE";
                                HPasivo.CalculaImpuesto = calcularImpuesto; 
                                HPasivo.CargoPosterior = cargoPosterior;
                                HPasivo.FolioFactura = campo.ItemArray[16].ToString();
                                HPasivo.Importe = campo.ItemArray[17].ToString();
                                HPasivo.ImporteRetencionFlete = campo.ItemArray[18].ToString()!=null?campo.ItemArray[18].ToString():"";
                                //HPasivo.Moneda = campo.ItemArray[19].ToString() != null ? campo.ItemArray[19].ToString() : "";
                                //HPasivo.Proveedor_Diferente = campo.ItemArray[20].ToString() != null ? campo.ItemArray[20].ToString() : "";
                                HPasivo.RetencionISRRenta = campo.ItemArray[21].ToString() != null ? campo.ItemArray[21].ToString() : "";
                                HPasivo.RetencionIVARenta = campo.ItemArray[22].ToString() != null ? campo.ItemArray[22].ToString() : "";
                                //HPasivo.Sociedad = campo.ItemArray[23].ToString() != null ? campo.ItemArray[23].ToString() : "";
                                HPasivo.SubTotalXml = campo.ItemArray[24].ToString() != null ? campo.ItemArray[24].ToString() : "";
                                HPasivo.UUID = campo.ItemArray[25].ToString() != null ? campo.ItemArray[25].ToString() : "";
                            }
                        }
                        else
                        {
                            HPasivo.Factura = false;//valido;
                            HPasivo.FechaFactura = "";
                            HPasivo.FechaPedimento = "";
                            HPasivo.CalculaImpuesto = calcularImpuesto;
                            HPasivo.CargoPosterior = cargoPosterior;
                            HPasivo.FolioFactura = "";
                            HPasivo.Importe = "";
                            HPasivo.ImporteRetencionFlete = "";
                            HPasivo.Moneda = "";
                            HPasivo.Proveedor_Diferente = "";
                            HPasivo.RetencionISRRenta = "";
                            HPasivo.RetencionIVARenta = "";
                            HPasivo.SubTotalXml = "";
                            HPasivo.UUID = "";
                        }
                    }

                     #region  datos base
                        //HPasivo.Factura=false;//valido;
                        //HPasivo.FechaFactura="";
                        //HPasivo.FechaPedimento="";
                        //HPasivo.CalculaImpuesto=calcularImpuesto;
                        //HPasivo.CargoPosterior=cargoPosterior;
                        //HPasivo.FolioFactura="";
                        //HPasivo.Importe="";
                        //HPasivo.ImporteRetencionFlete="";
                        //HPasivo.Moneda="";
                        //HPasivo.Proveedor_Diferente="";
                        //HPasivo.RetencionISRRenta="";
                        //HPasivo.RetencionIVARenta="";
                        //HPasivo.Sociedad="";
                        //HPasivo.SubTotalXml="";
                        //HPasivo.UUID = "";
                    #endregion  datos base

                    #region ########################### B A P I      M   I    R    0     #############################
                    bool hubodisparo = false;
                    List<KeyValuePair<string, string>> _datos = new List<KeyValuePair<string, string>>();
                    ClickFactura_Entidades.BD.Modelos.Pasivo_Generado[] GPasivo = new ClickFactura_Entidades.BD.Modelos.Pasivo_Generado[30];
                    _datos = servicio.factXML_construyeBAPIMIRO(HPasivo, detallesPasivos);
                    _OrdenCompra = detallesPasivos[0].OrdenCompra;
                    GPasivo = new ClickFactura_Entidades.BD.Modelos.Pasivo_Generado[1];
                    hubodisparo = true;
                    #endregion ########################### B A P I      M   I    R    0     #############################
                #region
                    if (hubodisparo == true)
                    {

                        string _mensajes = "";
                        try
                        {
                            foreach (var gPasivo in GPasivo)
                            {
                                if (gPasivo.Tipo_Error != null)
                                    if (gPasivo.Tipo_Error.Equals("") == false)
                                        _mensajes = _mensajes + "No. Error: " + gPasivo.Tipo_Error + "- Descripción:" + gPasivo.Mensaje_Error;
                            }
                        }
                        catch (Exception ex)
                        {
                            ClickFactura_Entidades.BD.Modelos.Pasivo_Generado[] pasivo = new ClickFactura_Entidades.BD.Modelos.Pasivo_Generado[1];
                            pasivo[0] = new ClickFactura_Entidades.BD.Modelos.Pasivo_Generado();
                            pasivo[0].Tipo_Error = "false";
                            pasivo[0].FolioFactura = "";
                            pasivo[0].Mensaje_Error = "";
                            pasivo[0].NumeroOrden = "";
                            pasivo[0].NumeroPasivo = "";
                            pasivo[0].YearFiscal = "0000";
                            string _mensajeNoHuboErrores = ex.Message;
                            if (_datos.Count > 0)
                            {
                                string mensa = "";
                                bool fueErroneo = false;
                                foreach (KeyValuePair<string, string> resultado in _datos)
                                {
                                    if (resultado.Key.Contains("INVOICEDOCNUMBER") == false)
                                    {
                                            mensa = mensa + " " + resultado.Value;
                                            if (resultado.Key.Equals("E") == true || resultado.Key.Equals("Error") == true)
                                            {
                                                pasivo[0].Tipo_Error = "E";
                                                pasivo[0].Mensaje_Error = mensa;
                                                fueErroneo = true;
                                            }
                                            if (resultado.Key.Equals("OrdenCompra") == true)
                                            {
                                                pasivo[0].NumeroOrden = resultado.Value;
                                            }
                                    }
                                    else
                                    {
                                        if(Convert.ToInt64(resultado.Value)>0)
                                        {
                                            fueErroneo = false;
                                            break;
                                        }
                                    }
                                }
                                if (fueErroneo == false)
                                {
                                    datos.Add("OK");
                                    foreach (KeyValuePair<string, string> resultado in _datos)
                                    {
                                        if (resultado.Key.Equals("INVOICEDOCNUMBER") == true)
                                        {
                                            pasivo[0].Tipo_Error = null;
                                            pasivo[0].Mensaje_Error = null;
                                            pasivo[0].NumeroPasivo = resultado.Value;
                                            datos.Add(_OrdenCompra);
                                            datos.Add("Factura FI : " + resultado.Value);
                                        }
                                        else
                                        {
                                            if (resultado.Key.Equals("FISCALYEAR") == true)
                                            {
                                                pasivo[0].Tipo_Error = null;
                                                pasivo[0].Mensaje_Error = null;
                                                pasivo[0].YearFiscal = resultado.Value;
                                            }
                                        }
                                    }
                                }
                            }
                            if (pasivo != null)
                            {
                                GPasivo[0] = new ClickFactura_Entidades.BD.Modelos.Pasivo_Generado();
                                GPasivo[0].FolioFactura = pasivo[0].FolioFactura;
                                GPasivo[0].Mensaje_Error = pasivo[0].Mensaje_Error;
                                GPasivo[0].NumeroOrden = pasivo[0].NumeroOrden;
                                GPasivo[0].NumeroPasivo = pasivo[0].NumeroPasivo;
                                GPasivo[0].Tipo_Error = pasivo[0].Tipo_Error;
                                GPasivo[0].YearFiscal = pasivo[0].YearFiscal;
                            }

                        }

                        #region Disparo realizado y Bitacorar
                        try
                        {
                            //if ((GPasivo[0].Tipo_Error != null && GPasivo[0].Tipo_Error.Equals("E") == true) || GPasivo[0].NumeroPasivo == null)
                            //{
                            //    resultadosPasivos = new List<KeyValuePair<string, string>>();
                            //    #region Reporta resultados Erroneos
                            //    intentosFallidos++;
                            //    _agregar = new KeyValuePair<string, string>(OrdenCompra[0].OrdenCompra, " Recepción " + OrdenCompra[0].NoRecepcion + " - Orden de Compra " + OrdenCompra[0].OrdenCompra + " fue fallida.");
                            //    resultadosPasivos.Add(_agregar);
                            //    string msj = "";
                            //    if (!esExterno)
                            //        msj = "Configuración SAP respondío: " + _mensajes;////GPasivo.Mensaje_Error;
                            //    else
                            //        msj = "Los datos ingresados no son suficientes o son incorrectos para aceptar la actual operación, por favor verifique o contacte a su Comprador para más información.";
                            //    _agregar = new KeyValuePair<string, string>("E", msj);
                            //    resultadosPasivos.Add(_agregar);
                            //    #region BitacoraOperaciones
                            //    ad_T_BitacoraOperaciones bitacorar = new ad_T_BitacoraOperaciones();
                            //    objT_BitacoraOperaciones obj = new objT_BitacoraOperaciones();
                            //    obj.IdUsuario = idUsuario;
                            //    obj.Num_Proveedor = _Num_Proveedor;// (string)Session["Num_Proveedor"];
                            //    obj.Sociedad = _Sociedad;// (string)Session["Num_Sociedad"];
                            //    obj.IdProveedor = _idproveedor;// Convert.ToInt32((string)Session["IdProveedor"]);
                            //    obj.OrdenCompra = NoOrdenCompra;
                            //    obj.NumRecepcion = NoItem;
                            //    obj.Posicion = OrdenCompra[0].Posicion;
                            //    obj.operacionExitosa = false;
                            //    obj.FacturaValida = true;
                            //    obj.Error = "Configuración SAP respondío: " + OrdenCompra[0].TipoFactura + " " + _mensajes;// GPasivo.Mensaje_Error;//+". Portal CSC documentó: "+ informacionComplementaria;
                            //    obj.Flujo = "BAPI_MIRO";
                            //    obj.Archivo_Factura = OrdenCompra[0].TextoXML;//prefijo;
                            //    obj.FolioFactura = OrdenCompra[0].FolioFactura;
                            //    obj.Mensaje = "Configuración SAP respondío : Para la orden " + NoOrdenCompra + " y la recepcion " + NoItem + " ocurrío un problema bajo el concepto de " + _mensajes + ". Portal CSC  documento la siguiente información : " + informacionComplementaria + " Emisor " + usuario.Usuario;//GPasivo.Mensaje_Error + ". Portal CSC  documento la siguiente información : " + (lblTituloFlujo.InnerHtml.ToString().Length > 0 == true ? lblTituloFlujo.InnerHtml.ToString() : "Carga Manual") + informacionComplementaria  + " Emisor " + (string)Session["Usuario"];
                            //    obj.FechaFactura = OrdenCompra[0].FechaFactura;
                            //    obj.UUID = OrdenCompra[0].UUID;
                            //    obj.Fecha = DateTime.Now.ToString();
                            //    obj.ImporteFactura = Convert.ToString((((importe + trasladado) - retenidos) - descuentos) - Convert.ToDecimal(Math.Abs(ImporteNotasdeCredito))); //OrdenCompra[0].ImporteDocumento.ToString();
                            //    obj.ImportePasivo = importeEnviadoaPasivo;
                            //    obj.ImporteRecepcion = importeCalculadoRecepcion;
                            //    obj.RFCEmisor = OrdenCompra[0].RFC;
                            //    obj.RFC_Recepctor = RFC_del_Receptor;
                            //    obj.Serie = Serie;
                            //    obj.Moneda = OrdenCompra[0].Moneda;
                            //    obj.PasivoGenerado = "No se genero";
                            //    bitacorar.registrarEvento(obj, "Insertar");
                            //    #endregion BitacoraOperaciones
                            //    #endregion Reporta resultados Erroneos
                            //    correcto = false;
                            //}
                            //else
                            //{
                            //    #region Pasivo Generado
                            //    intentosExitosos++;
                            //    resultadosPasivos = new List<KeyValuePair<string, string>>();
                            //    //resultadosPasivos.Add(OrdenCompra[0].OrdenCompra, " Recepción " + OrdenCompra[0].NoRecepcion + " - Orden de Compra " + OrdenCompra[0].OrdenCompra + " fue exitoso.");
                            //    _agregar = new KeyValuePair<string, string>(OrdenCompra[0].OrdenCompra, " Recepción " + OrdenCompra[0].NoRecepcion + " - Orden de Compra " + OrdenCompra[0].OrdenCompra + " fue exitosa.</br>Se genero el pasivo con el número: " + GPasivo[0].NumeroPasivo);
                            //    resultadosPasivos.Add(_agregar);
                            //    //PASIVO GENERADO
                            //    #region BitacoraOperaciones
                            //    ad_T_BitacoraOperaciones bitacorar = new ad_T_BitacoraOperaciones();
                            //    objT_BitacoraOperaciones obj = new objT_BitacoraOperaciones();
                            //    obj.IdUsuario = idUsuario;
                            //    obj.Num_Proveedor = _Num_Proveedor;// (string)Session["Num_Proveedor"];
                            //    obj.Sociedad = _Sociedad;//(string)Session["Num_Sociedad"];
                            //    obj.IdProveedor = _idproveedor; //Convert.ToInt32((string)Session["IdProveedor"]);
                            //    obj.OrdenCompra = OrdenCompra[0].OrdenCompra;
                            //    obj.NumRecepcion = OrdenCompra[0].NoRecepcion;
                            //    obj.Posicion = OrdenCompra[0].Posicion;
                            //    obj.operacionExitosa = true;
                            //    obj.Error = "Sin errores y se genero un pasivo con documento de referencia " + GPasivo[0].NumeroPasivo + (cuadrarconSAP == true ? " pero se forzo el empate enviando con  base a los datos como se tenian en Recepcion " + NoItem : "." + informacionComplementaria);// GPasivo.Mensaje_Error;
                            //    obj.Mensaje = "Portal: Para la orden " + NoOrdenCompra + " y la recepcion " + NoItem + " (" + OrdenCompra[0].TipoFactura + ") " + " si fue generado un pasivo correctamente. Generación  Pasivo - " + GPasivo[0].NumeroPasivo + " " + informacionComplementaria + " Emisor  ";// 15 Agosto Pendiente identificar al  +usuario.Usuario;
                            //    obj.Flujo = "BAPI_MIRO";
                            //    obj.Archivo_Factura = OrdenCompra[0].TextoXML;//prefijo;
                            //    obj.FolioFactura = OrdenCompra[0].FolioFactura;
                            //    obj.FacturaValida = true;
                            //    obj.FechaFactura = OrdenCompra[0].FechaFactura;
                            //    obj.Fecha = DateTime.Now.ToString();
                            //    obj.ImporteFactura = Convert.ToString((((importe + trasladado) - retenidos) - descuentos) - Convert.ToDecimal(Math.Abs(ImporteNotasdeCredito))); //OrdenCompra[0].ImporteDocumento.ToString();
                            //    obj.ImportePasivo = importeEnviadoaPasivo;
                            //    obj.ImporteRecepcion = importeCalculadoRecepcion;
                            //    obj.RFCEmisor = OrdenCompra[0].RFC;
                            //    obj.UUID = OrdenCompra[0].UUID;
                            //    obj.RFC_Recepctor = RFC_del_Receptor;
                            //    obj.Serie = Serie;
                            //    obj.Moneda = OrdenCompra[0].Moneda;
                            //    obj.PasivoGenerado = GPasivo[0].NumeroPasivo;
                            //    NumeroPasivoGenerado = GPasivo[0].NumeroPasivo;
                            //    bitacorar.registrarEvento(obj, "Insertar");
                            //    #endregion BitacoraOperaciones
                            //    try
                            //    {
                            //        #region Envia correo Proveedor
                            //        //var prov = (from t in contexto.Cat_Proveedor where t.IdProveedor.Equals(_idproveedor) select t).FirstOrDefault();
                            //        ////enviaCorreogeneraPasivo(string _IdProveedor,string _IdUsuario, string _Usuario, string _Proveedor,string resultadoVerificacion,string _OrdenCompra,string _Recepcion, string _Cuerpo)
                            //        //string _IdProveedor = _idproveedor.ToString();// (string)Session["IdProveedor"];
                            //        ////string _IdUsuario = (string)Session["idUsuario"];
                            //        ////string _Usuario =  (string)Session["Usuario"];
                            //        //string _Proveedor = prov.Nombre;
                            //        //var us = prov.Cat_Usuario;

                            //        //string resultadoVerificacion = obj.operacionExitosa == true ? " exitosa " : " fallida ";
                            //        //string _OrdenCompra = OrdenCompra[0].OrdenCompra;
                            //        //string _Recepcion = OrdenCompra[0].NoRecepcion;
                            //        //string _Cuerpo = "Estimado " + obj.RFCEmisor + "  su Orden de Compra " + _OrdenCompra + " y la Recepción " + _Recepcion + " fue verificada " + resultadoVerificacion + " el " + DateTime.Now.ToLongDateString() + " para la Empresa " + _Proveedor + ". Su factura fue verificada y aceptada con el no. de referencia " + GPasivo[0].NumeroPasivo + ".  Nota: Este no. de referencia indica que su factura fue recepcionada correctamente.";
                            //        //enviaCorreogeneraPasivo(_IdProveedor, usuario.IdUsuario.ToString(), usuario.Usuario, _Proveedor, resultadoVerificacion, _OrdenCompra, _Recepcion, _Cuerpo);
                            //        #endregion Envia correo Proveedor
                            //    }
                            //    catch
                            //    {

                            //    }

                            //    #endregion Pasivo Generado
                            //    correcto = true;
                            //}
                        }
                        catch (Exception)
                        {
                            //resultadosPasivos = new List<KeyValuePair<string, string>>();
                            //if (GPasivo[0].Tipo_Error != null)
                            //{
                            //    #region Pasivo con Error
                            //    intentosFallidos++;
                            //    //resultadosPasivos.Add(OrdenCompra[0].OrdenCompra, "Recepción " + OrdenCompra[0].NoRecepcion + " - Orden de Compra " + OrdenCompra[0].OrdenCompra + " fue fallida. ");
                            //    _agregar = new KeyValuePair<string, string>(OrdenCompra[0].OrdenCompra, " Recepción " + OrdenCompra[0].NoRecepcion + " - Orden de Compra " + OrdenCompra[0].OrdenCompra + " fue fallida.");
                            //    resultadosPasivos.Add(_agregar);
                            //    string msj = "";
                            //    if (!esExterno)
                            //        msj = "Configuración SAP respondío: " + _mensajes;//GPasivo.Mensaje_Error;
                            //    else
                            //        msj = "Los datos ingresados no son suficientes o son incorrectos para aceptar la actual operación, por favor verifique o contacte a su Comprador para más información.";

                            //    //Mi version de bitacorar
                            //    #region BitacoraOperaciones
                            //    ad_T_BitacoraOperaciones bitacorar = new ad_T_BitacoraOperaciones();
                            //    objT_BitacoraOperaciones obj = new objT_BitacoraOperaciones();
                            //    obj.IdUsuario = idUsuario;
                            //    obj.Num_Proveedor = _Num_Proveedor;// (string)Session["Num_Proveedor"];
                            //    obj.Sociedad = _Sociedad;// (string)Session["Num_Sociedad"];
                            //    obj.IdProveedor = _idproveedor;// Convert.ToInt32((string)Session["IdProveedor"]);
                            //    obj.OrdenCompra = OrdenCompra[0].OrdenCompra;
                            //    obj.NumRecepcion = OrdenCompra[0].NoRecepcion;
                            //    obj.Posicion = OrdenCompra[0].Posicion;
                            //    obj.operacionExitosa = false;
                            //    obj.Error = "Intento fallido configuración SAP respondío: " + OrdenCompra[0].TipoFactura + " " + GPasivo[0].Mensaje_Error;//(GPasivo.Mensaje_Error!=null?" configuración SAP respondío: " + GPasivo.Mensaje_Error+". ":"" )+  " Portal CSC documentó:  " + informacionComplementaria;
                            //    obj.Mensaje = "Portal CSC documentó : Intento fallido de carga de factura " + OrdenCompra[0].NombreArchivoXML + " realizada por el Proveedor " + OrdenCompra[0].RFC + " para compesar su Orden de Compra  " + NoOrdenCompra + " y la recepción " + NoItem + informacionComplementaria;
                            //    obj.Flujo = "BAPI_MIRO";
                            //    obj.FacturaValida = true;
                            //    obj.Archivo_Factura = OrdenCompra[0].TextoXML;//prefijo;
                            //    obj.FolioFactura = OrdenCompra[0].FolioFactura;
                            //    obj.FechaFactura = OrdenCompra[0].FechaFactura;
                            //    obj.Fecha = DateTime.Now.ToString();
                            //    obj.UUID = OrdenCompra[0].UUID;
                            //    obj.ImporteFactura = Convert.ToString((((importe + trasladado) - retenidos) - descuentos) - Convert.ToDecimal(Math.Abs(ImporteNotasdeCredito)));// OrdenCompra[0].ImporteDocumento.ToString();
                            //    obj.ImportePasivo = importeEnviadoaPasivo;
                            //    obj.ImporteRecepcion = importeCalculadoRecepcion;
                            //    obj.RFCEmisor = OrdenCompra[0].RFC;
                            //    obj.RFC_Recepctor = RFC_del_Receptor;
                            //    obj.Serie = Serie;
                            //    obj.Moneda = OrdenCompra[0].Moneda;
                            //    obj.PasivoGenerado = "No se genero";
                            //    bitacorar.registrarEvento(obj, "Insertar");
                            //    #endregion BitacoraOperaciones
                            //    #endregion Pasivo con Error
                            //    correcto = false;
                            //}
                            //else
                            //{
                            //    #region Pasivo Generado
                            //    //PASIVO GENERADO
                            //    intentosExitosos++;
                            //    //resultadosPasivos.Add(OrdenCompra[0].OrdenCompra, "Recepción " + OrdenCompra[0].NoRecepcion + " - Orden de Compra " + OrdenCompra[0].OrdenCompra + " fue exitosa. ");
                            //    _agregar = new KeyValuePair<string, string>(OrdenCompra[0].OrdenCompra, " Recepción " + OrdenCompra[0].NoRecepcion + " - Orden de Compra " + OrdenCompra[0].OrdenCompra + " fue exitosa.</br>Se genero el pasivo con el número: " + GPasivo[0].NumeroPasivo);
                            //    resultadosPasivos.Add(_agregar);
                            //    #region Bitacorar
                            //    ad_T_BitacoraOperaciones bitacorar = new ad_T_BitacoraOperaciones();
                            //    objT_BitacoraOperaciones obj = new objT_BitacoraOperaciones();
                            //    obj.IdUsuario = idUsuario;
                            //    obj.Num_Proveedor = _Num_Proveedor;// (string)Session["Num_Proveedor"];
                            //    obj.Sociedad = _Sociedad;// (string)Session["Num_Sociedad"];
                            //    obj.IdProveedor = _idproveedor;// Convert.ToInt32((string)Session["IdProveedor"]);
                            //    obj.OrdenCompra = OrdenCompra[0].OrdenCompra;
                            //    obj.NumRecepcion = OrdenCompra[0].NoRecepcion;
                            //    obj.Posicion = OrdenCompra[0].Posicion;
                            //    obj.operacionExitosa = true;
                            //    obj.Error = "Portal: Se Genero un Pasivo con referencia " + GPasivo[0].NumeroPasivo + (cuadrarconSAP == true ? " pero se forzo el empate enviando con  base a los datos como se tenian en Recepcion " + NoItem : "." + informacionComplementaria);// +pasivogenerado.NumeroOrden;// GPasivo.Mensaje_Error;
                            //    obj.Mensaje = "Para la orden " + NoOrdenCompra + " y la recepcion " + NoItem + " (" + OrdenCompra[0].TipoFactura + ") " + " si fue generado un pasivo correctamente. Generación  Pasivo -" + GPasivo[0].NumeroPasivo + " " + informacionComplementaria + " Emisor ";// 15 Agosto Pendiente identificar al  +usuario.Usuario;
                            //    obj.Flujo = "BAPI_MIRO";
                            //    obj.FacturaValida = true;
                            //    obj.Archivo_Factura = Convert.ToString((((importe + trasladado) - retenidos) - descuentos) - Convert.ToDecimal(Math.Abs(ImporteNotasdeCredito))); //OrdenCompra[0].TextoXML;//prefijo;
                            //    obj.FolioFactura = OrdenCompra[0].FolioFactura;
                            //    obj.FechaFactura = OrdenCompra[0].FechaFactura;
                            //    obj.UUID = OrdenCompra[0].UUID;
                            //    obj.Fecha = DateTime.Now.ToString();
                            //    obj.ImportePasivo = HPasivo.Importe;
                            //    obj.ImporteFactura = OrdenCompra[0].ImporteDocumento.ToString();
                            //    obj.ImportePasivo = importeEnviadoaPasivo;
                            //    obj.ImporteRecepcion = importeCalculadoRecepcion;
                            //    obj.RFCEmisor = OrdenCompra[0].RFC;
                            //    obj.RFC_Recepctor = RFC_del_Receptor;
                            //    obj.Serie = Serie;
                            //    obj.Moneda = OrdenCompra[0].Moneda;
                            //    obj.PasivoGenerado = GPasivo[0].NumeroPasivo;
                            //    NumeroPasivoGenerado = GPasivo[0].NumeroPasivo;
                            //    bitacorar.registrarEvento(obj, "Insertar");
                            //    #endregion Bitacorar
                            //    try
                            //    {
                            //        #region Envia correo Proveedor
                            //        ////enviaCorreogeneraPasivo(string _IdProveedor,string _IdUsuario, string _Usuario, string _Proveedor,string resultadoVerificacion,string _OrdenCompra,string _Recepcion, string _Cuerpo)
                            //        //string _IdProveedor = _idproveedor.ToString();// (string)Session["IdProveedor"];
                            //        //var prov = (from t in contexto.Cat_Proveedor where t.IdProveedor.Equals(_idproveedor) select t).FirstOrDefault();
                            //        //string resultadoVerificacion = obj.operacionExitosa == true ? " exitosa " : " fallida ";
                            //        //string _OrdenCompra = OrdenCompra[0].OrdenCompra;
                            //        //string _Recepcion = OrdenCompra[0].NoRecepcion;
                            //        //string _Cuerpo = "Estimado " + obj.RFCEmisor + " su Orden de Compra " + _OrdenCompra + " y la Recepción " + _Recepcion + " fue verificada " + resultadoVerificacion + " el " + DateTime.Now.ToLongDateString() + " para la Empresa " + prov.Nombre + ". Su factura fue verificada y aceptada con el no. de referencia " + GPasivo[0].NumeroPasivo + ".  Nota: Este no. de referencia indica que su factura fue recepcionada correctamente.";
                            //        //enviaCorreogeneraPasivo(_IdProveedor, usuario.IdUsuario.ToString(), usuario.Usuario, prov.Nombre, resultadoVerificacion, _OrdenCompra, _Recepcion, _Cuerpo);
                            //        #endregion Envia correo Proveedor
                            //    }
                            //    catch
                            //    {

                            //    }
                            //    #endregion Pasivo Generado
                            //    correcto = true;
                            //}
                        }
                        #endregion Disparo realizado y Bitacorar

                    }
                #endregion
                #endregion Construyendo Encabezado
            }
            else
            {
                //Probablemente la orden ya fue compensada totalmente por sus recepciones previamente
                string mensaje = "";
                mensaje = "La Orden de Compra tenia " + DPasivo.Count() + "recepciones disponibles para verificación";
                return JavaScript("javascript:showErrorMessage('" + mensaje + "');");
            }
            return Json(datos);
        }

        [HttpPost]
        public ActionResult registraEventoT_BitacoraWF(string idusuariowf, string desicion, string servicio,string ordencompra)
        {
            //idusuariowf --> Es quien esta enviando
            //desicion --> Si aprobó o rechazo


            List<string> datos = new List<string>();
            datos.Add("true");
            return Json(datos);
        }


    }
}