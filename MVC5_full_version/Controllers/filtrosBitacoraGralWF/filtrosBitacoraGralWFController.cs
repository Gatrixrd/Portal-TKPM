using ClickFactura_Entidades.BD.Entidades;
using ClickFactura_WebServiceCF.AccesoBD.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5_full_version.Controllers.filtrosBitacoraGralWF
{
    public class filtrosBitacoraGralWFController : Controller
    {
        // GET: filtrosBitacoraGralWF
        ClickFactura_WebServiceCF.Service.Service1 cliente = new ClickFactura_WebServiceCF.Service.Service1();
        Desarrollo_CF contexto = new Desarrollo_CF();

        public ActionResult filtrosBitacoraGralWF()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ConfiguraCalendarios()
        {
            //Configuracion con = new Configuracion();
            string ini = cliente.primerDiaMes().Date.ToString();
            string fin = cliente.ultimoDiaMes().Date.ToString();
            List<string> resp = new List<string>();
            resp.Add(ini);
            resp.Add(fin);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSociedades(bool esAdmon, string numproveedor)
        {
            List<KeyValuePair<string, string>> respuesta = new List<KeyValuePair<string, string>>();
            if (esAdmon == true)
            {
                List<Cat_ProcesosWF> procesos = contexto.Cat_ProcesosWF.ToList();
                foreach (Cat_ProcesosWF wf in procesos)
                {
                    string key = wf.idWF.ToString();
                    string value = wf.nombreWF.ToString();
                    respuesta.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            else
            {
                List<Cat_ProcesosWF> procesos = contexto.Cat_ProcesosWF.ToList();
                foreach (Cat_ProcesosWF wf in procesos)
                {
                    string key = wf.idWF.ToString();
                    string value = wf.nombreWF.ToString();
                    respuesta.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult GetTiposUsuario(string idWF)
        {
            List<KeyValuePair<string, string>> respuesta = new List<KeyValuePair<string, string>>();
            List<Cat_TipoUsuarioWF> cat_TipoUsuarioWF = contexto.Cat_TipoUsuarioWF.ToList();
            foreach (Cat_TipoUsuarioWF wf in cat_TipoUsuarioWF)
            {
                if(wf.idWF.Trim().Equals(idWF)==true)
                {
                    string key = wf.idTipoUsuarioWF.ToString();
                    string value = wf.nombreRol.ToString();
                    respuesta.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult GetUsuariosWF(string idTipo)
        {
            List<KeyValuePair<string, string>> respuesta = new List<KeyValuePair<string, string>>();
            List<Cat_UsuariosWF> cat_UsuarioWF = contexto.Cat_UsuariosWF.ToList();
            foreach (Cat_UsuariosWF wf in cat_UsuarioWF)
            {
                if(wf.idTipoUsuarioWF.Equals(idTipo)==true)
                {
                    string key = wf.idUsuarioWF.ToString();
                    string value = wf.nombre.ToString();
                    respuesta.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            return Json(respuesta);
        }

        [HttpPost]
        //'numProveedor': numproveedor, 'finicial': _finicial, 'ffinal': ffinal, 'ordencompra': ordencompra, 'sociedad': sociedad, 'tipo': tipo, 'aplicarfechas': aplicarFechas, 'usuario': usuario, 'uuid': uuid, 'moneda': moneda
        public JsonResult CargaResultados(string numproveedor, string finicial, string ffinal,string ordencompra, string sociedad, string tipo, string aplicarfechas,string usuario, string uuid, string moneda)
        {
            List<KeyValuePair<string, string>> respuesta = new List<KeyValuePair<string, string>>();
            #region Parametros
            List<KeyValuePair<string,string>> parametros = new List<KeyValuePair<string, string>>();
            if(numproveedor!=null)
            {
                if(numproveedor.Length>0)
                    {
                    KeyValuePair<string, string> dato = new KeyValuePair<string, string>("numProveedor", numproveedor);
                    parametros.Add(dato);
                    }
            }
            if (sociedad != null)
            {
                if (sociedad.Length > 0)
                {
                    if(Convert.ToInt32(sociedad)>0)
                    {
                    KeyValuePair<string, string> dato = new KeyValuePair<string, string>("idWF", sociedad);
                    parametros.Add(dato);
                    }
                }
            }
            if (ordencompra != null)
            {
                if (ordencompra.Length > 0)
                {
                    KeyValuePair<string, string> dato = new KeyValuePair<string, string>("OrdenCompra", ordencompra);
                    parametros.Add(dato);
                }
            }
            if (tipo != null)
            {
                if (tipo.Length > 0)
                {
                    if (Convert.ToInt32(tipo) > 0)
                    {
                        KeyValuePair<string, string> dato = new KeyValuePair<string, string>("idTipoUsuarioWF", tipo);
                        parametros.Add(dato);
                    }
                }
            }
            if (usuario != null)
            {
                if (usuario.Length > 0)
                {
                    KeyValuePair<string, string> dato = new KeyValuePair<string, string>("Usuario", usuario);
                    parametros.Add(dato);
                }
            }
            if (uuid != null)
            {
                if (uuid.Length > 0)
                {
                    KeyValuePair<string, string> dato = new KeyValuePair<string, string>("UUID", uuid);
                    parametros.Add(dato);
                }
            }
            if (moneda != null)
            {
                if (moneda.Length > 0)
                {
                    KeyValuePair<string, string> dato = new KeyValuePair<string, string>("moneda", moneda);
                    parametros.Add(dato);
                }
            }
            #endregion Parametros
            string consulta = "";
            DateTime di = (DateTime)Convert.ChangeType(finicial, typeof(DateTime));
            DateTime df = (DateTime)Convert.ChangeType(ffinal, typeof(DateTime));

            string filtro = "";
            string estatus = "";
            string fechaInicial = "";
            string fechaFinal = "";

            #region       Configura formato fechas
            adT_Parametros adp = new adT_Parametros();
                    List<objT_Parametros> objp = new List<objT_Parametros>();
                    int result = 0;
                    string entorno = "";
                    string _Cantidad = "";
                    bool publicado = false;
                    objp = adp.mABCT_Parametros(out result, 0, "Publicado", "Vacio", true, "ConsultaValor");
                    {
                        entorno = objp[0].ValorParametro.ToString();
                        publicado = entorno.Equals("1") == true ? true : false;
                    }
                    if (publicado == false)
                    {
                        fechaInicial = di.Date.ToString("dd-MM-yyyy");
                        fechaFinal = df.Date.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        fechaInicial = di.Date.ToString("yyyy-MM-dd");
                        fechaFinal = df.Date.ToString("yyyy-MM-dd");
                    }
            #endregion Configura formato fechas
            string campoFecha = "fecha";
            filtro = " WHERE  (CONVERT(datetime, "+campoFecha+" , 105) BETWEEN '" + fechaInicial + "' AND '" + fechaFinal + "') ";
            foreach(KeyValuePair<string, string> param in parametros)
            {
                filtro = filtro + " And ";
                if(param.Key.Equals("Usuario")==true || param.Key.Equals("UUID") == true || param.Key.Equals("moneda") == true || param.Key.Equals("numProveedor") == true)
                {
                    filtro = filtro + param.Key + " like '" + param.Value + "' ";
                }
                else
                {
                    filtro = filtro + param.Key + "=" + param.Value + " ";
                }
            }
            consulta = "Select  * from view_todoWorkflow  " + filtro;
            System.Web.HttpContext.Current.Session["queryrptBitacoraWFGral"] = consulta;
            return Json(respuesta);
        }

        public ActionResult vistaPrevia()
        {
            return RedirectToAction("Index", "rptBitacoraWFGral");
        }

    }
}