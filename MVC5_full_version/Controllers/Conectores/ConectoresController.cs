using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using ClickFactura_WebServiceCF.Conectores.Configuracion;
using ClickFactura_Entidades.BD.Entidades;

namespace MVC5_full_version.Controllers
{
    public class ConectoresController : Controller
    {
        ClickFactura_WebServiceCF.Service.Service1 cliente = new ClickFactura_WebServiceCF.Service.Service1();
        csBaseSAPNET claseSAP = new csBaseSAPNET();
        string url = "";
        public ActionResult conexionConectorSAP()
        {
            RespuestasGenericasModel conexion = new RespuestasGenericasModel();
            return View(conexion);
        }

        [HttpPost]
        public JsonResult disparaCargarAlmacenado()
        {
            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
            var resultado = cliente.parametrosAlmacenados();
            foreach (var dato in resultado)
            {
                datos.Add(new KeyValuePair<string, string>(dato.Key, dato.Value));
            }
            return Json(datos);
        }

        [HttpPost]
        public JsonResult disparaCargarProveedorPrincipal(string companyid)
        {
            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
            try
            {
                bool result = false;
                var resultado = cliente.disparaCargarProveedorPrincipal(companyid);
                foreach (var dato in resultado)
                {
                    datos.Add(new KeyValuePair<string, string>(dato.Key, dato.Value));
                }
                Session["numProveedorConector"] = companyid;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.ReadLine();
            }
            return Json(datos);
        }

        [HttpPost]
        ///Registro básico del Proveedor
        ///Su objetivo es realizar el registro de los datos mínimos que la BAPI "BAPI_COMPANY_GETDETAIL" hacia la tabla "Cat_Proveedores"
        ///Con esta información se realiza un preregistro al cual le faltan 2 datos importantes y necesarios: Clave de Correo SAP, RFC Fiscal del Proveedor y un Correo para contacto 
        ///La Fase Dos del registro sería contando con el RFC y un correo en registrar al proveedor en la tabla Cat_Usuario
        ///Como parametro se solicita solo el n+umero de Proveedor de SAP
        public bool registroBasicoProveedor(string numproveedor)
        {
            bool _resultado = false;

            #region      Recupera Información
            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
            try
            {
                bool result = false;
                var resultado = cliente.disparaCargarProveedorPrincipal(numproveedor);
                foreach (var dato in resultado)
                {
                    datos.Add(new KeyValuePair<string, string>(dato.Key, dato.Value));
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.ReadLine();
            }
            #endregion Recupera Información

            ClickFactura_Entidades.BD.Entidades.Cat_Proveedor nuevoProveedor = new Cat_Proveedor();

            #region Registro Fase Uno
            nuevoProveedor.Num_Proveedor = numproveedor;
            nuevoProveedor.Nombre = datos[0].Value;
            nuevoProveedor.Direccion = datos[3].Value;
            nuevoProveedor.Poblacion = datos[4].Value;
            nuevoProveedor.Pais = datos[1].Value;
            #endregion Registro Fase Uno

            #region Registro Fase Uno pendientes
            nuevoProveedor.Num_Sociedad = "Pendiente";
            nuevoProveedor.Sociedad = "Pendiente";
            nuevoProveedor.GrupoCA = "Pendiente";
            nuevoProveedor.RFC = "Pendiente";
            nuevoProveedor.CPD = "Pendiente";
            nuevoProveedor.GrupoCCPD = "Pendiente";
            nuevoProveedor.Correo = "Pendiente";
            bool? activado = true;
            nuevoProveedor.Activo = activado;
            nuevoProveedor.P_General = (decimal)0;
            nuevoProveedor.P_Comercial = (decimal)0;
            nuevoProveedor.RFC2 = "Pendiente";
            #endregion Registro Fase Uno pendientes

            string mensaje = "";
            _resultado = cliente.Administracion_cat_Proveedores_registroProveedorBasico(numproveedor, nuevoProveedor, ref mensaje);
            //datos.Add(new KeyValuePair<string, string>("COUNTRY", detail.GetString("COUNTRY")));
            //datos.Add(new KeyValuePair<string, string>("LANGU", detail.GetString("LANGU")));
            //datos.Add(new KeyValuePair<string, string>("STREET", detail.GetString("STREET")));
            //datos.Add(new KeyValuePair<string, string>("CITY", detail.GetString("CITY")));
            //datos.Add(new KeyValuePair<string, string>("CURRENCY", detail.GetString("CURRENCY")));
            //datos.Add(new KeyValuePair<string, string>("PO_BOX", detail.GetString("PO_BOX")));
            //datos.Add(new KeyValuePair<string, string>("POSTL_COD1", detail.GetString("POSTL_COD1")));
            return _resultado;
        }

        [HttpPost]
        public ActionResult _disparaCargarOrdenCompra(string OrdenCompra)
        {

            List<KeyValuePair<string, System.Data.DataTable>> respuesta = new List<KeyValuePair<string, System.Data.DataTable>>();
            List<KeyValuePair<string, List<RespuestasGenericasModel>>> salida = new List<KeyValuePair<string, List<RespuestasGenericasModel>>>();
            bool result = false;

            try
            {
                List<KeyValuePair<string, string>> mensajes = new List<KeyValuePair<string, string>>();
                List<ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra> listadoOrdenes = cliente.Carga_DetalleOrden(OrdenCompra, ref mensajes);// conectores_obtenOrdenCompra(companyid);
                List<RespuestasGenericasModel> listarespGenericas = new List<RespuestasGenericasModel>();
                var men = from _men in mensajes where _men.Key.Contains("Error") == true select _men;
                int registros = 0;
                try
                {
                    registros = men.Count();
                }
                catch
                {
                    registros = 0;
                }

                //Esto indica que aunque hubo errores si se encontro información VÁLIDA  para ser cargada en pantalla
                var poitems = from _pi in mensajes where _pi.Key.Contains("PO_ITEMS") == true select _pi;
                try
                {
                    registros = poitems.Count();
                    if (registros > 0)
                        registros = 0;
                    else
                        registros = 1;
                }
                catch
                {
                    registros = 1;
                }
                bool todosConIndicador = true;
                if (registros == 0)//men==null)
                {

                    foreach (ClickFactura_Entidades.BD.Entidades.Detalle_OrdenCompra detalles in listadoOrdenes)
                    {
                        List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
                        var a = new RespuestasGenericasModel();
                        a.R1 = detalles.Orden_Compra;
                        datos.Add(new KeyValuePair<string, string>("Orden de Compra", a.R1));
                        a.R2 = detalles.Posicion_OC;
                        datos.Add(new KeyValuePair<string, string>("Posición OC", a.R2));
                        a.R3 = detalles.Descripcion;
                        datos.Add(new KeyValuePair<string, string>("Descripción", a.R3));
                        a.R4 = detalles.Cantidad;
                        datos.Add(new KeyValuePair<string, string>("Cantidad", a.R4));
                        a.R5 = detalles.Importe;
                        datos.Add(new KeyValuePair<string, string>("Importe", a.R5));
                        a.R6 = detalles.Unidad_Medida;
                        datos.Add(new KeyValuePair<string, string>("Unidad Medida", a.R6));
                        a.R7 = detalles.Indicador_IVA;
                        if (a.R7.Length <= 0)
                        {
                            todosConIndicador = false;
                            mensajes.Add(new KeyValuePair<string, string>("Error", " La posición " + a.R2.ToString() + " no tiene definido el Indicador de IVA, no se podra recibir la Factura."));
                        }
                        datos.Add(new KeyValuePair<string, string>("Indicador Imp.", a.R7.Length > 0 ? a.R7 : "ERROR"));
                        a.R8 = detalles.Fecha_Contable;
                        datos.Add(new KeyValuePair<string, string>("Fecha contable", a.R8));
                        listarespGenericas.Add(a);
                    }
                    salida.Add(new KeyValuePair<string, List<RespuestasGenericasModel>>("PO_ITEMS", listarespGenericas));
                }
                else
                {
                    if (men.Count() > 0)
                    {
                        string _m = "Errores: ";
                        foreach (KeyValuePair<string, string> m in mensajes)
                        {
                            _m = _m + "  " + m;
                        }
                        //return JavaScript("javascript:showErrorMessage('" + _m + "');");
                        return Json(mensajes, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.ReadLine();
            }
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult guardarConfiguracion(string name, string user, string password, string client, string language, string systemnumber, string appserverhost, string maxpoolsize, string idletimeout, string _iPMSH, string logonGroup, string GateWayHost, string AppServerService, string MessageServerService, string GateWayService)
        {
            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
            //SAP.Middleware.Connector.RfcDestination conector = csBaseSAPNET.GetRfcDestination;
            try
            {
                configuracionConexionSAPModel modelo = new configuracionConexionSAPModel();
                modelo.Name = name;
                modelo.User = user;
                modelo.Password = password;
                modelo.Client = client;
                modelo.Language = language;
                modelo.SystemNumber = systemnumber;
                modelo.AppServerHost = appserverhost;
                modelo.MessageServerHost = _iPMSH;
                modelo.MaxPoolSize = maxpoolsize;
                modelo.IdleTimeOut = idletimeout;
                modelo.LogonGroup = logonGroup;
                modelo.GateWayHost = GateWayHost;
                modelo.AppServerService = AppServerService;
                modelo.MessageServerService = MessageServerService;
                modelo.GateWayService = GateWayService;

                csBaseSAPNET guardaParametros = new csBaseSAPNET();
                //ad_configuracionConexionSAP guardaParametros = new ad_configuracionConexionSAP();
                guardaParametros.actualizaParametros(modelo);
                datos.Add(new KeyValuePair<string, string>("Name", name));
                datos.Add(new KeyValuePair<string, string>("User", user));
                datos.Add(new KeyValuePair<string, string>("Password", password));
                datos.Add(new KeyValuePair<string, string>("Client", client));
                datos.Add(new KeyValuePair<string, string>("Language", language));
                datos.Add(new KeyValuePair<string, string>("SystemNumber", systemnumber));
                datos.Add(new KeyValuePair<string, string>("AppServerHost", _iPMSH));
                datos.Add(new KeyValuePair<string, string>("MaxPoolSize", maxpoolsize));
                datos.Add(new KeyValuePair<string, string>("IdleTimeout", idletimeout));
                datos.Add(new KeyValuePair<string, string>("AppServerHost", appserverhost));
                datos.Add(new KeyValuePair<string, string>("LogonGroup", logonGroup));
                datos.Add(new KeyValuePair<string, string>("GateWayHost", GateWayHost));
                datos.Add(new KeyValuePair<string, string>("AppServerService", AppServerService));
                datos.Add(new KeyValuePair<string, string>("MessageServerService", MessageServerService));
                datos.Add(new KeyValuePair<string, string>("GatewayService", GateWayService));
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.ReadLine();
            }
            return Json(datos);
        }

        [HttpPost]
        public List<RespuestasGenericasModel> CargaTabla(System.Data.DataTable tabla, string _tabla, List<RespuestasGenericasModel> salida)
        {
            // List<BAFAR.Models.RespuestasGenericasModel> salida = new List<BAFAR.Models.RespuestasGenericasModel>();
            var a = new RespuestasGenericasModel();
            if (_tabla.Equals("PO_ITEMS") == true)
            {
                foreach (System.Data.DataRow row in tabla.Rows)
                {
                    a.R1 = row.ItemArray[0].ToString();
                    a.R2 = row.ItemArray[1].ToString();
                    a.R3 = row.ItemArray[2].ToString();
                    a.R4 = row.ItemArray[3].ToString();
                    a.R5 = row.ItemArray[4].ToString();
                    a.R6 = row.ItemArray[5].ToString();
                    a.R7 = row.ItemArray[6].ToString();
                    a.R8 = row.ItemArray[7].ToString();
                    salida.Add(a);
                }
            }

            return (salida);
        }
        #region Pruebas Bapis
        public ActionResult ejecutarBapis()
        {            
            return View();
        }
        public ActionResult ejecutar()
        {
            ClickFactura_WebServiceCF.Service.Service1 cliente = new ClickFactura_WebServiceCF.Service.Service1();
            cliente.EjecutarBapis();
            return Json(true);
        }
        #endregion
    }
}