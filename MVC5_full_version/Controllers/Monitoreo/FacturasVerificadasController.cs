using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using ClickFactura_Entidades.BD.Entidades;

namespace MVC5_full_version.Controllers.Monitoreo
{
    public class FacturasVerificadasController : Controller
    {
        //ClickFactura_Entidades.BD.Modelos.DatosBafarDataContext contexto = new ClickFactura_Entidades.BD.Modelos.DatosBafarDataContext();
        ClickFactura_WebServiceCF.Service.Service1 cliente = new ClickFactura_WebServiceCF.Service.Service1();
        Desarrollo_CF contexto = new Desarrollo_CF();
        public ActionResult FacturasVerificadas()
        {
            ClickFactura_Entidades.BD.Modelos.FacturasVerificadasModel facturasverificadas = new ClickFactura_Entidades.BD.Modelos.FacturasVerificadasModel();

            return View(facturasverificadas);
        }

        [HttpPost]
        public JsonResult GetSociedades(string Num_Proveedor)
        {
            List<ClickFactura_Entidades.BD.Entidades.Cat_Cia> Sociedades = new List<ClickFactura_Entidades.BD.Entidades.Cat_Cia>();
            //bool queEs;
            //var s = from soc in contexto.Cat_Cia select soc;
            //try
            //{
            //    queEs = (bool)Session["NivelAdministradorBafar"];
            //}
            //catch
            //{
            //    string valor = (string)Session["NivelAdministradorBafar"];
            //    queEs = valor.Equals("True") == true ? true : false;
            //}

            //if (queEs == false)
            //{
            //    List<ClickFactura_Entidades.BD.Modelos.view_Sociedades_a_las_que_puedo_Facturar> resp = new List<ClickFactura_Entidades.BD.Modelos.view_Sociedades_a_las_que_puedo_Facturar>();
            //    string usuario = (string)Session["Usuario"];
            //    resp = ObtenerSociedades(usuario);
            //    foreach (var todos in s)
            //    {
            //        foreach (var filtrados in resp)
            //        {
            //            if (todos.Num_Sociedad.Equals(filtrados.Num_Sociedad) && filtrados.RFC.Equals("1") == true)
            //            {
            //                Sociedades.Add(todos);
            //            }
            //        }
            //    }
            //    s = (from salida in Sociedades where salida.Num_Sociedad != null select salida).AsQueryable();
            //}
            ClickFactura_Entidades.BD.Entidades.Cat_Cia compañia = new Cat_Cia();
            compañia.IdCia = 1;
            compañia.Compania = "Thyssenkrupp Presta México";
            compañia.Activo = true;
            compañia.Num_Sociedad = "0000000050";
            compañia.RFC = "";
            compañia.Poblacion = "";
            compañia.Direccion = "";
            compañia.Pais = "";
            compañia.Moneda = "";
            Sociedades.Add(compañia);
            var s = (from salida in Sociedades where salida.Num_Sociedad != null select salida).AsQueryable();
            return Json(s);
        }

        [HttpPost]
        public List<ClickFactura_Entidades.BD.Entidades.view_Sociedades_a_las_que_puedo_Facturar> ObtenerSociedades(string usuario)
        {
            List<ClickFactura_Entidades.BD.Entidades.view_Sociedades_a_las_que_puedo_Facturar> resp = new List<ClickFactura_Entidades.BD.Entidades.view_Sociedades_a_las_que_puedo_Facturar>();
            System.Data.DataTable t = new System.Data.DataTable();
            System.Data.DataTable table = new System.Data.DataTable();
            if (usuario.Length > 0)
            {
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("Usuario", usuario);
                string tabla = "obtenSociedadesxUsuario";
                string P_Opcion = "todasSociedadesxUsuario";
                //BAFAR.Clases.Genericos.Configuracion c = new BAFAR.Clases.Genericos.Configuracion();
                ClickFactura_Facturacion.Genericos.Genericos c = new ClickFactura_Facturacion.Genericos.Genericos();
                try
                {
                    table = c.executaSP_Generico(parametros, P_Opcion, tabla);
                    if (table != null)
                    {
                        if (table.Rows.Count > 0)
                        {
                            #region Preparando respuesta en tabla
                            if (table.Rows.Count > 0)
                            {
                                foreach (System.Data.DataRow reg in table.Rows)
                                {
                                    ClickFactura_Entidades.BD.Entidades.view_Sociedades_a_las_que_puedo_Facturar cv = new ClickFactura_Entidades.BD.Entidades.view_Sociedades_a_las_que_puedo_Facturar();
                                    cv.Num_Sociedad = reg.ItemArray[0].ToString();
                                    cv.Compania = reg.ItemArray[1].ToString();
                                    cv.RFC = reg.ItemArray[2].ToString();
                                    cv.Num_Proveedor = reg.ItemArray[3].ToString(); ;
                                    resp.Add(cv);
                                }
                            }
                            else
                            {
                                ClickFactura_Entidades.BD.Entidades.view_Sociedades_a_las_que_puedo_Facturar cv = new ClickFactura_Entidades.BD.Entidades.view_Sociedades_a_las_que_puedo_Facturar();
                                cv.Num_Sociedad = "0";
                                cv.Compania = "Empresa/Usuario sin Sociedades";
                                cv.RFC = "0";
                                cv.Num_Proveedor = "0";
                                resp.Add(cv);
                            }
                            #endregion Preparando respuesta en tabla
                        }
                    }
                    else
                    {
                        ClickFactura_Entidades.BD.Entidades.view_Sociedades_a_las_que_puedo_Facturar cv = new ClickFactura_Entidades.BD.Entidades.view_Sociedades_a_las_que_puedo_Facturar();
                        cv.Num_Sociedad = "0";
                        cv.Compania = "Empresa/Usuario sin Sociedades";
                        cv.RFC = "0";
                        cv.Num_Proveedor = "0";
                        resp.Add(cv);
                    }
                }
                catch
                {

                }
            }
            return resp;
        }

        [HttpPost]
        public JsonResult CargaResultadosFacturasVerificadas(string numproveedor, string finicial, string ffinal, string ordencompra, string sociedad, string rfc, bool aplicarFechas)
        {
            List<ClickFactura_Entidades.BD.Modelos.FacturasVerificadasModel> resp = new List<ClickFactura_Entidades.BD.Modelos.FacturasVerificadasModel>();
            SqlCommand comando = new SqlCommand();
           //Clases.Genericos.Configuracion con = new Configuracion();
            string fechas = "";
            DataTable dt = new DataTable();
            dt.TableName = "view_FacturasVerirficadas";
            string query = "";
            //numproveedor="0000100263";

            #region cargando lo disponibles del mes como inicio
            try
            {
                SqlConnection Conexion = new SqlConnection(ClickFactura_Entidades.BD.Entidades.AccesoBD.CadenaConexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                string mensaje = "";

                #region Fechas
                if (aplicarFechas == true)
                {
                    if (finicial.Length <= 0)
                        finicial = cliente.primerDiaMes().Date.ToString();
                    if (ffinal.Length <= 0)
                        ffinal = DateTime.Now.Date.ToString();
                    cliente.formateaFechasSQL(ref finicial, ref ffinal, ref mensaje, numproveedor, (string)Session["Usuario"]);
                    fechas = "  And (TRY_CONVERT(datetime, Fecha, 103)  between TRY_CONVERT(datetime, '" + finicial + "', 103) AND TRY_CONVERT(datetime,  '" + ffinal + "', 103) )  ";
                }
                #endregion Fechas

                #region Agregando los demás filtros
                if (numproveedor.Length > 0)
                {
                    query = "  And (Num_Proveedor like '%" + numproveedor + "%')";
                }
                if (ordencompra.Length > 0)
                {
                    query = query + "  And (OrdenCompra like '%" + ordencompra + "%') ";
                }
                if (sociedad.Equals("0") == false)
                {
                    query = query + "  And (Sociedad = " + sociedad + ") ";
                }
                if (rfc.Length > 0)
                {
                    query = query + "  And (RFC  like '%" + rfc + "%') ";
                }
                #endregion Agregando los demás filtros

                ///Se cambia vista en QAs quizas requieera cambio en PRD
                ///CONVERT (VARCHAR(10), CONVERT (nvarchar, T_BitacoraOperaciones1.Fecha, 106), 103)
                ///GRD 02/03/2017
                if (mensaje.Length <= 0)
                {
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.Connection = Conexion;
                    comando.CommandText = "select * from view_FacturasVerirficadas where IdOperaciones  is not null AND (TRY_CONVERT(datetime, dbo.view_FacturasVerirficadas.Fecha, 103)  Is not Null) " + fechas + query;

                    adaptador.SelectCommand = comando;
                    adaptador.Fill(dt);
                }
                else
                {

                }
            }
            catch (SqlException sEx)
            {
                throw new Exception(sEx.Number.ToString() + ": " + sEx.Message);
            }
            #endregion cargando lo disponibles del mes como inicio

            #region Preparando respuesta en tabla
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow reg in dt.Rows)
                {
                    ClickFactura_Entidades.BD.Modelos.FacturasVerificadasModel cv = new ClickFactura_Entidades.BD.Modelos.FacturasVerificadasModel();
                    cv.R1 = reg.ItemArray[0].ToString();
                    cv.R2 = reg.ItemArray[10].ToString();
                    cv.R3 = reg.ItemArray[11].ToString();
                    cv.R4 = reg.ItemArray[12].ToString(); ;
                    cv.R5 = reg.ItemArray[6].ToString();
                    cv.R6 = reg.ItemArray[13].ToString();
                    cv.R7 = reg.ItemArray[1].ToString();
                    cv.R8 = reg.ItemArray[15].ToString();
                    cv.R9 = reg.ItemArray[2].ToString();
                    cv.R10 = reg.ItemArray[3].ToString();
                    cv.R11 = reg.ItemArray[4].ToString();
                    cv.R12 = reg.ItemArray[5].ToString();
                    cv.R13 = reg.ItemArray[7].ToString();
                    cv.R14 = reg.ItemArray[8].ToString();
                    cv.R15 = reg.ItemArray[14].ToString();
                    cv.R16 = reg.ItemArray[9].ToString();
                    resp.Add(cv);
                }
            }
            #endregion Preparando respuesta en tabla

            return Json(resp, JsonRequestBehavior.AllowGet);
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
        public JsonResult esAdmon()
        {
            bool admin = false;
            admin = Convert.ToBoolean(ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(System.Web.HttpContext.Current.Session["Usuario"] as string).ToString());// Session["NivelAdministradorBafar"]);
            string proveedor = "error";
            if (admin == false)
            {
                proveedor = System.Web.HttpContext.Current.Session["Num_Proveedor"] as string;
            }
            else
            {
                proveedor = "";
            }
            return Json(proveedor, JsonRequestBehavior.AllowGet);
        }


    }
}