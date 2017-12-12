using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using BAFAR.Clases.Genericos;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ClickFactura_Entidades.BD.Entidades;
namespace MVC5_full_version.Controllers.Monitoreo
{
    public class repositorioArchivosController : Controller
    {

        //ClickFactura_Entidades.BD.Modelos.DatosBafarDataContext contexto = new ClickFactura_Entidades.BD.Modelos.DatosBafarDataContext();
        ClickFactura_WebServiceCF.Service.Service1 cliente = new ClickFactura_WebServiceCF.Service.Service1();
        Desarrollo_CF contexto = new Desarrollo_CF();
        public ActionResult repositorioArchivos()
        {
            ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel repositorioarchivos = new ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel();
            return View(repositorioarchivos);
        }

        [HttpPost]
        public JsonResult GetSociedades(string Num_Proveedor)
        {
            //ClickFactura_Entidades.BD.Entidades.Cat_Cia Sociedades = new ClickFactura_Entidades.BD.Entidades.Cat_Cia();
            //var s = from soc in contexto.Cat_Cia select soc;
            //return Json(s);
            bool queEs;
            try
            {
                string usuario = System.Web.HttpContext.Current.Session["Usuario"] as string;
                bool admin = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(usuario);
                queEs = admin;// (bool)Session["NivelAdministradorBafar"];
            }
            catch
            {
                string valor = "True";//17 Agosto obetenr del proceso  (string)Session["NivelAdministradorBafar"];
                queEs = valor.Equals("True") == true ? true : false;
            }

            List<ClickFactura_Entidades.BD.Entidades.Cat_Cia> Sociedades = new List<ClickFactura_Entidades.BD.Entidades.Cat_Cia>();
            var s = from soc in contexto.Cat_Cia select soc;
            //queEs = (bool)Session["NivelAdministradorBafar"];
            if (queEs == false)
            {
                List<ClickFactura_Entidades.BD.Entidades.view_Sociedades_a_las_que_puedo_Facturar> resp = new List<ClickFactura_Entidades.BD.Entidades.view_Sociedades_a_las_que_puedo_Facturar>();
                string usuario = (string)Session["Usuario"];
                resp = ObtenerSociedades(usuario);
                foreach (var todos in s)
                {
                    foreach (var filtrados in resp)
                    {
                        if (todos.Num_Sociedad.Equals(filtrados.Num_Sociedad) && filtrados.RFC.Equals("1") == true)
                        {
                            Sociedades.Add(todos);
                        }
                    }
                }
                s = (from salida in Sociedades where salida.Num_Sociedad != null select salida).AsQueryable();
            }
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
        public JsonResult GetEstatus()
        {
            List<datosEstatus.Datos> Datos = new List<datosEstatus.Datos>();
            for (int i = 0; i <= 2; i++)
            {
                datosEstatus.Datos dato = new datosEstatus.Datos();
                dato.estatus = i.ToString();
                dato.descripcion = i == 0 ? "Verificada" : i == 1 ? "Rechazada" : "Todas";
                dato.valor = i == 0 ? "PasivoOk" : i == 1 ? "PasivoDeny" : "iconTodosDocumentos";
                Datos.Add(dato);
            }
            var s = Datos;
            return Json(s);
        }

        [HttpPost]
        public JsonResult CargaResultadosrepositorioArchivos(string numproveedor, string finicial, string ffinal, string ordencompra, string sociedad, string rfc, bool aplicarFechas, string estatus, string nombrearchivo)
        {
            List<ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel> resp = new List<ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel>();
            SqlCommand comando = new SqlCommand();
            //Clases.Genericos.Configuracion con = new Configuracion();
            string fechas = "";
            DataTable dt = new DataTable();
            dt.TableName = "view_FacturasVerirficadas";
            string query = "";

            #region cargando lo disponibles del mes como inicio
            try
            {
                SqlConnection Conexion = new SqlConnection(ClickFactura_Entidades.BD.Entidades.AccesoBD.CadenaConexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                string mensaje = "";
                string filtros = "";

                #region Fechas
                if (aplicarFechas == true)
                {
                    if (finicial.Length <= 0)
                        finicial = cliente.primerDiaMes().Date.ToString();
                    if (ffinal.Length <= 0)
                        ffinal = DateTime.Now.Date.ToString();
                    cliente.formateaFechasSQL(ref finicial, ref ffinal, ref mensaje, numproveedor, (string)Session["Usuario"]);
                    fechas = "  And (TRY_CONVERT(datetime, fechaAlmacenado, 103)  between TRY_CONVERT(datetime, '" + finicial + "', 103) AND TRY_CONVERT(datetime,  '" + ffinal + "', 103) )  ";
                }
                #endregion Fechas
                if (ordencompra.Length == 0)
                {
                    #region Tipo descarga Uno
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
                        query = query + "  And (Num_Sociedad = '" + sociedad + "') ";
                    }
                    if (rfc.Length > 0)
                    {
                        query = query + "  And (RFCEmisor  like '%" + rfc + "%') ";
                    }

                    #endregion Agregando los demás filtros

                    if (mensaje.Length <= 0)
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Connection = Conexion;
                        comando.CommandText = "select * from view_AlmacenamientoInterno where IdAlmacenado  is not null " + fechas + query;

                        adaptador.SelectCommand = comando;
                        adaptador.Fill(dt);
                    }
                    else
                    {

                    }
                    #endregion Tipodescraga Uno
                }
                else
                {
                    #region Descarga del tipo Dos
                    string sinicial = finicial;
                    string sfinal = ffinal;
                    //Configuracion leeTabla = new Configuracion();
                    string consulta = "Select * from view_AlmacenamientoInterno where " + "contenidoTexto like '%" + ordencompra + "%' Or OrdenCompra like  '%" + ordencompra + "%' Or OC like  '%" + ordencompra + "%'";//OC like  '%" + ordencompra + "%'";
                    dt = cliente.genericos_consultaCualquierTabla(consulta);// consultaCualquierTabla(consulta);
                    filtros = "contenidoTexto like '%" + ordencompra + "%' Or OrdenCompra like  '%" + ordencompra + "%' Or OC like  '%" + ordencompra + "%'";
                    DataRow[] salida = dt.Select(filtros);
                    try
                    {
                        dt = salida.CopyToDataTable();
                    }
                    catch
                    {
                        dt = null;
                    }
                    #endregion Descarga del tipo Dos
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
                    ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel cv = new ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel();
                    cv.R1 = reg.ItemArray[0].ToString();
                    cv.R2 = reg.ItemArray[1].ToString();
                    cv.R3 = reg.ItemArray[2].ToString();
                    cv.R4 = reg.ItemArray[3].ToString(); ;
                    cv.R5 = reg.ItemArray[4].ToString();
                    cv.R6 = reg.ItemArray[5].ToString();
                    cv.R7 = reg.ItemArray[6].ToString();
                    cv.R8 = reg.ItemArray[7].ToString();
                    cv.R9 = reg.ItemArray[8].ToString();
                    cv.R10 = reg.ItemArray[9].ToString();
                    cv.R11 = reg.ItemArray[10].ToString();
                    cv.R12 = reg.ItemArray[11].ToString(); ;
                    cv.R13 = reg.ItemArray[12].ToString();
                    cv.R14 = reg.ItemArray[13].ToString();
                    cv.R15 = reg.ItemArray[14].ToString();
                    cv.R16 = reg.ItemArray[15].ToString();
                    cv.R17 = reg.ItemArray[16].ToString();
                    cv.R18 = reg.ItemArray[17].ToString();
                    cv.R18 = cv.R18.Replace("~/Imagenes/", "");
                    cv.R19 = reg.ItemArray[18].ToString();
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

        //########################### NECESARIO PARA DESCARGAR ARCHIVOS ############################################

        [HttpPost]
        public JsonResult analizaArchivos(string id, string rfc, string ordencompra, string finicial, string ffinal)
        {
            Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile();
            DataTable t = new DataTable();
            string OrdenCompra = ordencompra;// rmt_OrdenCompra.Text;// "0300879112";
            string RFC = rfc;

            if (RFC.Length > 0 || ordencompra.Length > 0)
            {
                #region estructura Fechas
                string sinicial = "";
                string sfinal = "";
                try
                {
                    //string fechaInicial = rdt_Inicial.SelectedDate.Value.ToString("yyyy-MM-dd");
                    //string fechaFinal = rdt_Final.SelectedDate.Value.ToString("yyyy-MM-dd");

                    DateTime inicial = Convert.ToDateTime(finicial);//rdt_Inicial.SelectedDate);
                    sinicial = inicial.ToString("yyyy-MM-dd"); //+" 00:00:00";//("dd/MM/yyyy"); //+" 00:00:00";
                    DateTime final = Convert.ToDateTime(ffinal);//rdt_Final.SelectedDate);
                    sfinal = final.ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                {
                    //rdt_Inicial.SelectedDate = Genericos.cs_Estaticos.primerDiaMes();
                    //rdt_Final.SelectedDate = Genericos.cs_Estaticos.ultimoDiaMes();
                    //string fechaInicial = rdt_Inicial.SelectedDate.Value.ToString("yyyy-MM-dd");
                    //string fechaFinal = rdt_Final.SelectedDate.Value.ToString("yyyy-MM-dd");
                    //DateTime inicial = Convert.ToDateTime(rdt_Inicial.SelectedDate);
                    //sinicial = inicial.ToString("yyyy-dd-MM");
                    //DateTime final = Convert.ToDateTime(rdt_Final.SelectedDate);
                    //sfinal = final.ToString("yyyy-dd-MM");
                }
                #endregion estructura Fechas
                string XMLTexto = "";
                string directorio = @"C:\Temp\";
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Clear();
                string tabla = "Donwload_Facturas_T_BitacoraOperaciones";
                DateTime dtimeIni = DateTime.ParseExact(finicial, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtimeFin = DateTime.ParseExact(ffinal, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //Configuracion con = new Configuracion();
                string mensaje = "";
                string ini = dtimeIni.Date.ToString();
                int pos = ini.IndexOf(" ");
                ini = ini.Substring(0, pos);
                string fin = dtimeFin.Date.ToString();
                pos = fin.IndexOf(" ");
                fin = fin.Substring(0, pos);
                parametros.Add("Inicial", ini);
                parametros.Add("Final", fin);
                OrdenCompra = RFC.Length > 0 == true ? RFC : (ordencompra.Length > 0 ? ordencompra : ordencompra.Length == 9 ? "0" + ordencompra : ordencompra);
                parametros.Add("OrdenCompra", ordencompra.Length > 0 ? ordencompra : RFC);
                string P_Opcion = RFC.Length > 0 == true ? "RFC" : "OC";
                DataTable table = new DataTable();
                //Configuracion c = new Configuracion();
                ClickFactura_Facturacion.Genericos.Genericos c = new ClickFactura_Facturacion.Genericos.Genericos();
                table = c.executaSP_Generico(parametros, P_Opcion, tabla);
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                ordencompra = RFC.Length > 0 == true ? "" : ordencompra;
                if (table != null)
                    if (table.Rows.Count > 0)
                    {
                        int _cuantos = 0;
                        t = table;
                        if (RFC.Length <= 0)
                        {
                            #region Una sola Orden
                            #region Carga el XML
                            string subdirectorio = System.Configuration.ConfigurationManager.AppSettings["pathInvoice"] == null ? @"C:\Temp\" : System.Configuration.ConfigurationManager.AppSettings["pathInvoice"];
                            XMLTexto = table.Rows[0].ItemArray[0].ToString();
                            xdoc.LoadXml(XMLTexto);
                            #endregion Carga el XML
                            #region Crea el directorio temporal
                            System.IO.DirectoryInfo DIR = new System.IO.DirectoryInfo(subdirectorio);
                            if (!DIR.Exists)
                            {
                                System.IO.Directory.CreateDirectory(subdirectorio);
                            }
                            #endregion Crea el directorio temporal
                            #region Guarda el XML en Temporal
                            // guardo el xml fisicamente
                            if (!System.IO.File.Exists(subdirectorio + OrdenCompra + ".xml"))
                            {
                                //subdirectorio + OrdenCompra + ".xml"
                                FileStream stream;
                                stream = new FileStream(subdirectorio + OrdenCompra + ".xml", FileMode.OpenOrCreate, FileAccess.Write);
                                StreamWriter writer = new StreamWriter(stream);
                                writer.Write(XMLTexto);
                                writer.Close();
                            }
                            #endregion Guarda el XML en Temporal
                            #region prepara el PDF
                            string _ruta = subdirectorio + OrdenCompra + ".xml";
                            conviertePDF(xdoc, OrdenCompra, subdirectorio, XMLTexto);
                            #endregion prepara el PDF
                            #region version 2 de descargar archivos
                            string remoteUri = subdirectorio;
                            //string fileName = subdirectorio + OrdenCompra + ".zip", myStringWebResource = null;
                            //using (System.Net.WebClient myWebClient = new System.Net.WebClient())
                            //{
                            //    myStringWebResource = remoteUri + OrdenCompra + ".xml";
                            //    string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            //    myWebClient.DownloadFile(myStringWebResource, escritorio + @"\" + OrdenCompra + ".xml");
                            //}
                            //using (System.Net.WebClient myWebClient = new System.Net.WebClient())
                            //{
                            //    myStringWebResource = remoteUri + OrdenCompra + ".pdf";
                            //    string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            //    myWebClient.DownloadFile(myStringWebResource, escritorio + @"\" + OrdenCompra + ".pdf");
                            //}
                            #endregion version 2 de descargar archivos
                            #region Carga, empaqueta y descarga ZIP
                            zip.AddFile(subdirectorio + OrdenCompra + ".xml");
                            zip.AddFile(subdirectorio + OrdenCompra + ".pdf");
                            zip.Save(subdirectorio + OrdenCompra + ".zip");

                            string fileName = subdirectorio + OrdenCompra + ".zip", myStringWebResource = null;
                            using (System.Net.WebClient myWebClient = new System.Net.WebClient())
                            {
                                myStringWebResource = remoteUri + OrdenCompra + ".xml";
                                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                myWebClient.DownloadFile(myStringWebResource, escritorio + @"\" + OrdenCompra + ".xml");
                            }
                            //Download(subdirectorio + OrdenCompra + ".zip");
                            System.IO.File.Delete(subdirectorio + OrdenCompra + ".xml");
                            System.IO.File.Delete(subdirectorio + OrdenCompra + ".pdf");
                            #endregion Carga, empaqueta y descarga ZIP
                            #endregion Una sola Orden
                        }
                        else
                        {
                            #region un Bloque de XML
                            //directorio = System.Configuration.ConfigurationManager.AppSettings["pathInvoice"];
                            List<string> listaDirectorios = new List<string>();
                            string _tiempo = "";
                            System.IO.DirectoryInfo DIR = new System.IO.DirectoryInfo(directorio);
                            if (!DIR.Exists)
                            {
                                System.IO.Directory.CreateDirectory(directorio);
                            }
                            #region descarga Archivos
                            DataTable dt = new DataTable();
                            string porProveedor = RFC.Length > 0 == true ? " RFC  ='" + RFC + "'" : "";
                            string filtros = porProveedor;
                            DataRow[] salida = table.Select(filtros);
                            string cuantos = table.Rows.Count.ToString();
                            var listaRFCs = salida.GroupBy(test => test["RFC"].ToString()).Select(grp => grp.First()).ToList();
                            string subdirectorio = "";
                            foreach (DataRow rfcs in listaRFCs)
                            {
                                subdirectorio = directorio + rfcs[4].ToString(); ;// @"\" + rfc[4].ToString();
                                System.IO.DirectoryInfo subDIR = new System.IO.DirectoryInfo(subdirectorio);
                                if (!subDIR.Exists)
                                {
                                    System.IO.Directory.CreateDirectory(subdirectorio);
                                }
                                var XMLrelacionados = from subXMLS in salida where subXMLS[4].ToString().Equals(rfcs[4].ToString()) select subXMLS;

                                foreach (DataRow renglon in XMLrelacionados)
                                {
                                    try
                                    {
                                        DateTime tiempo = new DateTime();
                                        if (renglon[1].ToString().Contains("-") == false)
                                            tiempo = DateTime.ParseExact(renglon[1].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                        else
                                            tiempo = DateTime.ParseExact(renglon[1].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                        string _año = tiempo.Year.ToString();
                                        string _mes = tiempo.Month.ToString();
                                        string _ArchivoXML = renglon[0].ToString();
                                        string _Fecha = renglon[1].ToString();
                                        string _OrdenCompra = renglon[2].ToString();
                                        string _NumRecepcion = renglon[3].ToString();
                                        string _RFC = renglon[4].ToString();
                                        string _Serie = renglon[5].ToString();
                                        string _UUID = renglon[6].ToString();
                                        string _Estatus = renglon[7].ToString().Equals("False") == true ? "Rechazada_" : "Aceptada_";
                                        _Fecha = _Fecha.Replace("/", "_");
                                        _Fecha = _Fecha.Replace("_", "_");
                                        #region Calculo de Mes
                                        switch (tiempo.Month)
                                        {
                                            case 1:
                                                _mes = "Ene";
                                                break;
                                            case 2:
                                                _mes = "Feb";
                                                break;
                                            case 3:
                                                _mes = "Mar";
                                                break;
                                            case 4:
                                                _mes = "Abr";
                                                break;
                                            case 5:
                                                _mes = "May";
                                                break;
                                            case 6:
                                                _mes = "Jun";
                                                break;
                                            case 7:
                                                _mes = "Jul";
                                                break;
                                            case 8:
                                                _mes = "Ago";
                                                break;
                                            case 9:
                                                _mes = "Sep";
                                                break;
                                            case 10:
                                                _mes = "Oct";
                                                break;
                                            case 11:
                                                _mes = "Nov";
                                                break;
                                            case 12:
                                                _mes = "Dic";
                                                break;
                                        }
                                        #endregion Calculo de Mes
                                        _tiempo = @"\";// +_año + @"\" + _mes + @"\";
                                        System.IO.DirectoryInfo subDIRTiempo = new System.IO.DirectoryInfo(subdirectorio + _tiempo);
                                        if (!subDIRTiempo.Exists)
                                        {
                                            System.IO.Directory.CreateDirectory(subdirectorio + _tiempo);
                                        }
                                        if (_ArchivoXML.Equals("No proporcionado") == false)
                                        {
                                            string ruta = subdirectorio + _tiempo + _Estatus + _OrdenCompra + "_" + _NumRecepcion + "_" + _Fecha + "_" + _RFC + ".xml";
                                            string _ruta = subdirectorio + _tiempo;
                                            if (!System.IO.File.Exists(ruta))
                                            {
                                                //System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                                                //xdoc.LoadXml(_ArchivoXML);
                                                //xdoc.Save(ruta);
                                                #region Guarda el XML en Temporal
                                                // guardo el xml fisicamente
                                                if (!System.IO.File.Exists(ruta))
                                                {
                                                    try
                                                    {
                                                        //subdirectorio + OrdenCompra + ".xml"
                                                        FileStream stream;
                                                        stream = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.Write);
                                                        StreamWriter writer = new StreamWriter(stream);
                                                        writer.Write(_ArchivoXML);
                                                        writer.Close();
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        string _error = ex.Message;
                                                    }
                                                }
                                                #endregion Guarda el XML en Temporal

                                                //#region Carga, empaqueta y descarga ZIP
                                                //zip.AddFile(ruta);
                                                //#endregion Carga, empaqueta y descarga ZIP
                                                conviertePDF(xdoc, _OrdenCompra, _ruta, _ArchivoXML);
                                                #region Carga, empaqueta y descarga ZIP
                                                zip.AddFile(ruta);
                                                zip.AddFile(_ruta + _OrdenCompra + ".pdf");//subdirectorio + OrdenCompra + ".pdf");
                                                zip.Save(subdirectorio + ".zip");// + OrdenCompra + ".zip");
                                                //Download(subdirectorio + ".zip");//+ OrdenCompra + ".zip");
                                                System.IO.File.Delete(ruta);//subdirectorio + OrdenCompra + ".xml");
                                                System.IO.File.Delete(_ruta + _OrdenCompra + ".pdf");//subdirectorio + OrdenCompra + ".pdf");
                                                #endregion Carga, empaqueta y descarga ZIP
                                            }
                                            _cuantos++;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string _ex = ex.Message;
                                    }
                                }
                                //zip.Save(subdirectorio+ ".zip");
                                listaDirectorios.Add(subdirectorio + ".zip");
                                //Download(subdirectorio + ".zip");
                                Directory.Delete(subdirectorio + _tiempo, true);
                            }
                            try
                            {
                                //lbl_cuantos_descargados.Text = "Se descargaron " + _cuantos + " archivos XML exitosamente en la ruta " + subdirectorio + ". Recuerde que es un archivo .ZIP y debe descomprimirlo antes de consultar sus documentos.";

                                foreach (string _directorio in listaDirectorios)
                                {
                                    //Download(_directorio);
                                    //string remoteUri = subdirectorio;
                                    //string fileName = subdirectorio + ".zip", myStringWebResource = null;
                                    //using (System.Net.WebClient myWebClient = new System.Net.WebClient())
                                    //{
                                    //    myStringWebResource = remoteUri  + ".zip";
                                    //    string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                    //    myWebClient.DownloadFile(myStringWebResource, escritorio + @"\" + RFC + ".zip");
                                    //}
                                    //lbl_cuantos_descargados.Visible = true;
                                    //lbl_cuantos_descargados.Text = "Se descargaron " + _cuantos + " archivos XML exitosamente en su escritorio' ";
                                    //lbl_cuantos_descargados.DataBind();
                                    //string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                    //string[] arreglo = new string[_directorio.Length];
                                    //if (_directorio.Contains(@"\") == false)
                                    //    arreglo = _directorio.Split('\\');
                                    //else
                                    //    arreglo = _directorio.Split('*');
                                    //Response.Clear();
                                    //Response.ContentType = "application/octet-stream";
                                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + arreglo[arreglo.Length - 1].ToString());
                                    //Response.WriteFile(escritorio + @"\" + RFC + ".zip");
                                    //Response.Flush();
                                    //Response.End();
                                }
                            }
                            catch (Exception ex)
                            {
                                string _ex = ex.Message;
                                //Notificacion(ex.Message, "Error", "warning");
                            }
                            //lbl_cuantos_descargados.Text = "Se descargaron " + _cuantos + " archivos XML exitosamente en su escritorio' ";

                            #endregion descarga Archivos
                            #endregion un Bloque de XML
                        }
                        //lbl_cuantos_descargados.Text = "Se descargaron " + _cuantos + @" archivos XML exitosamente en la ruta 'C:\Temp' ";

                    }
            }
            else
            {
                //Notificacion("Especifique un RFC del cual descargar los XML del periodo.", "Falta RFC", "deny");
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult guardaItem(string item)
        {
            Session["pasaIdAlmacenado"] = item;
            return Json(item);
        }

        private static string deRutaFisicaaVirtual(string rutaFisica)
        {
            return @"~\" + rutaFisica.Replace(System.Web.HttpContext.Current.Request.PhysicalApplicationPath, String.Empty);
        }

        /// <summary>
        /// Descarga un XMl en el Escritorio de un renglon del Grid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult pasaIdAlmacenado(string id)
        {
            string paraLog = "";
            //Configuracion con = new Configuracion();
            System.Collections.Generic.List<ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel> respuesta = new System.Collections.Generic.List<ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel>();
            List<ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel> salidaRespuesta = new List<ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel>();
            string ini = cliente.primerDiaMes().Date.ToString();
            string fin = cliente.ultimoDiaMes().Date.ToString();
            List<string> resp = new List<string>();
            resp.Add(ini);
            resp.Add(fin);
            string escritorio = "";
            string directorio = "";
            //escribeLog("Configuracion", "Tratando de ubicar la ruta al directorio Escritorio ");
            //try
            //{
            // escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //}
            //catch(Exception ___ex)
            //{
            //     escribeLog("Configuracion", "Ruta al directorio Escritorio no pudo ser mapeada hacia "+ escritorio+ " por la razon "+___ex.Message);
            //}

            List<ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel> registro = new List<ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel>();
            SqlCommand comando = new SqlCommand();
            string fechas = "";
            DataTable dt = new DataTable();
            dt.TableName = "view_FacturasVerirficadas";
            SqlConnection Conexion = new SqlConnection(ClickFactura_Entidades.BD.Entidades.AccesoBD.CadenaConexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            comando.CommandType = System.Data.CommandType.Text;
            comando.Connection = Conexion;
            comando.CommandText = "select * from view_AlmacenamientoInterno where IdAlmacenado = " + id;

            adaptador.SelectCommand = comando;
            adaptador.Fill(dt);
            //escribeLog("Configuracion", "Tratando de determinar si existe o no el directorio Temporales ");
            //escribeLog("Configuracion", "Va a probar directorio  Temporales ");
            paraLog = paraLog + " 1 Tratando de determinar si existe o no el directorio Temporales ";
            paraLog = paraLog + " 2 Va a probar directorio  Temporales ";
            try
            {
                String RelativePath = deRutaFisicaaVirtual(@"Temp\");// AbsolutePath.Replace(Request.ServerVariables[@"C:\Temp\"], String.Empty);
                //escribeLog("Configuracion", "Se haran pruebas para el path relativo " + RelativePath);
                paraLog = paraLog + "3 Se haran pruebas para el path relativo " + RelativePath;
                bool exists = System.IO.Directory.Exists(RelativePath);
                if (!exists)
                {
                    //System.IO.Directory.CreateDirectory(Server.MapPath("~/Temporales"));
                    //escribeLog("Configuracion",paraLog+ " 4 Aparentemente directorio " + RelativePath+ "ya existe" );
                    paraLog = paraLog + " 4 Aparentemente directorio " + RelativePath + "ya existe";
                    directorio = RelativePath;
                }
                else
                {
                    directorio = RelativePath;
                    //escribeLog("Configuracion", "Se mapeo la ruta relativa y el valor de directorio sera "+RelativePath);
                    paraLog = paraLog + " 4 Se mapeo la ruta relativa y el valor de directorio sera " + RelativePath;
                }
            }
            catch (Exception __ex)
            {
                //escribeLog("Configuracion", paraLog+ "No pudo determinarse si existia el directorio Temporales por "+__ex.Message);
                paraLog = paraLog + "  4 No pudo determinarse si existia el directorio Temporales por " + __ex.Message;
            }
            bool resultado = false;
            //escribeLog("Configuracion", "Determinando numero de documentos ubicados ");
            paraLog = paraLog + " 5 Determinando numero de documentos ubicados ";
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        string RFC = (from DataRow dr in dt.Rows
                                      where (Int32)dr["idAlmacenado"] == Convert.ToInt32(id)
                                      select (string)dr["RFCEmisor"]).FirstOrDefault();
                        string OC = (from DataRow dr in dt.Rows
                                     where (Int32)dr["idAlmacenado"] == Convert.ToInt32(id)
                                     select (string)dr["OC"]).FirstOrDefault();
                        #region llama a descarga
                        //bajaArchivos(id, "", OC,"","",ref resultado);
                        Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile();
                        DataTable t = new DataTable();
                        string ordencompra = OC;
                        string OrdenCompra = ordencompra;// rmt_OrdenCompra.Text;// "0300879112";
                        RFC = "";

                        if (RFC.Length > 0 || ordencompra.Length > 0)
                        {
                            #region estructura Fechas
                            string sinicial = "";
                            string sfinal = "";
                            //try
                            //{
                            //    //string fechaInicial = rdt_Inicial.SelectedDate.Value.ToString("yyyy-MM-dd");
                            //    //string fechaFinal = rdt_Final.SelectedDate.Value.ToString("yyyy-MM-dd");

                            //    DateTime inicial = Convert.ToDateTime(finicial);//rdt_Inicial.SelectedDate);
                            //    sinicial = inicial.ToString("yyyy-MM-dd"); //+" 00:00:00";//("dd/MM/yyyy"); //+" 00:00:00";
                            //    DateTime final = Convert.ToDateTime(ffinal);//rdt_Final.SelectedDate);
                            //    sfinal = final.ToString("yyyy-MM-dd");
                            //}
                            //catch (Exception ex)
                            //{

                            //}
                            #endregion estructura Fechas
                            string XMLTexto = "";
                            //escribeLog("Configuracion", "Tratando de ubicar la ruta al directorio Temporales");
                            //directorio = Server.MapPath("~/Temporales");// @"C:\Temp\";
                            //escribeLog("Configuracion", "Logro ubicar la ruta al directorio Temporales en "+directorio);
                            Dictionary<string, string> parametros = new Dictionary<string, string>();
                            parametros.Clear();
                            string tabla = "Donwload_Facturas_T_BitacoraOperaciones";
                            //try
                            //{
                            //        paraLog = paraLog + " 5.1  formato a fechas";
                            //        DateTime dtimeIni = DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            //        paraLog = paraLog + " 5.2  Fecha  inicial " + dtimeIni.Date.ToString();
                            //        DateTime dtimeFin = DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            //        paraLog = paraLog + " 5.2  Fecha  Final " + dtimeFin.Date.ToString();
                            //}
                            //catch (Exception _mal)
                            //{
                            string text = DateTime.Now.ToShortDateString();

                            string timeFormat = "yyyy-M-d h:mm:ss tt";

                            //DateTime.ParseExact(text, timeFormat, System.Globalization.CultureInfo.InvariantCulture);
                            DateTime dtimeIni = ConvertToDateTime(text); //DateTime.ParseExact(text,timeFormat, System.Globalization.CultureInfo.InvariantCulture);
                            paraLog = paraLog + " 5.2  Fecha  inicial para servidor " + dtimeIni.Date.ToString();
                            DateTime dtimeFin = dtimeIni;// DateTime.ParseExact(text, timeFormat, System.Globalization.CultureInfo.InvariantCulture);
                            paraLog = paraLog + " 5.2  Fecha  Final para servidor " + dtimeFin.Date.ToString();
                            //}
                            //Configuracion con = new Configuracion();
                            string mensaje = "";
                            //string ini = dtimeIni.Date.ToString();
                            int pos = ini.IndexOf(" ");
                            ini = ini.Substring(0, pos);
                            //string fin = dtimeFin.Date.ToString();
                            pos = fin.IndexOf(" ");
                            fin = fin.Substring(0, pos);
                            parametros.Add("Inicial", sinicial.Equals(sfinal) == true ? sinicial : sinicial);
                            parametros.Add("Final", sfinal.Equals(sinicial) == true ? sinicial : sfinal);
                            OrdenCompra = RFC.Length > 0 == true ? RFC : (ordencompra.Length > 0 ? ordencompra : ordencompra.Length == 9 ? "0" + ordencompra : ordencompra);
                            parametros.Add("OrdenCompra", ordencompra.Length > 0 ? ordencompra : RFC);
                            string P_Opcion = RFC.Length > 0 == true ? "RFC" : "OC";
                            DataTable table = new DataTable();
                            //Configuracion c = new Configuracion();
                            ClickFactura_Facturacion.Genericos.Genericos c = new ClickFactura_Facturacion.Genericos.Genericos();
                            paraLog = paraLog + " 5.3 Se va a lanzar consulta SQL ";
                            table = c.executaSP_Generico(parametros, P_Opcion, tabla);
                            paraLog = paraLog + " 5.4 Se volvio ya de consulta SQL ";
                            System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                            ordencompra = RFC.Length > 0 == true ? "" : ordencompra;
                            try
                            {
                                //escribeLog("Operacion", "La tabla de resultados a procesar fue de " + table.Rows.Count.ToString());
                                paraLog = paraLog + " 6 La tabla de resultados a procesar fue de " + table.Rows.Count.ToString();
                                if (table != null)
                                    if (table.Rows.Count > 0)
                                    {
                                        int _cuantos = 0;
                                        t = table;
                                        if (RFC.Length <= 0)
                                        {
                                            for (int indice = 0; indice < (table.Rows.Count); indice++)
                                            {
                                                #region Una sola Orden
                                                #region Carga el XML
                                                string subdirectorio = System.Configuration.ConfigurationManager.AppSettings["pathInvoice"];// System.Configuration.ConfigurationManager.AppSettings["pathInvoice"] == null ? @"C:\Temp\" : System.Configuration.ConfigurationManager.AppSettings["pathInvoice"];
                                                string Filepath = @"~/Temporales/";
                                                //escribeLog("Actividad","Estableciendo la ruta "+subdirectorio);
                                                paraLog = paraLog + " 7 Estableciendo la ruta " + subdirectorio;

                                                XMLTexto = table.Rows[indice].ItemArray[0].ToString();
                                                xdoc.LoadXml(XMLTexto);
                                                paraLog = paraLog + "8 Cargando ya el XML Factura";
                                                //escribeLog("Actividad", "Cargando ya el XML Factura");
                                                #endregion Carga el XML
                                                #region Crea el directorio temporal
                                                System.IO.DirectoryInfo DIR = new System.IO.DirectoryInfo(subdirectorio);
                                                if (!DIR.Exists)
                                                {
                                                    System.IO.Directory.CreateDirectory(subdirectorio);
                                                    //escribeLog("Actividad", "Acaba de crear el Directorio:"+subdirectorio);
                                                    paraLog = paraLog + " 9 Creo el directorio ";
                                                }
                                                else
                                                {
                                                    //escribeLog("Actividad", "Detecto que el Directorio auxiliar si existe");
                                                    paraLog = paraLog + "  9 Detecto que el Directorio auxiliar si existe ";
                                                }
                                                #endregion Crea el directorio temporal
                                                #region Guarda el XML en Temporal
                                                // guardo el xml fisicamente
                                                if (!System.IO.File.Exists(subdirectorio + OrdenCompra + ".xml"))
                                                {
                                                    FileStream stream;
                                                    //Carpeta destino
                                                    stream = new FileStream(subdirectorio + OrdenCompra + ".xml", FileMode.OpenOrCreate, FileAccess.Write);
                                                    StreamWriter writer = new StreamWriter(stream);
                                                    writer.Write(XMLTexto);
                                                    writer.Close();
                                                    //escribeLog("Actividad", "Acaba de crear uno d elos XML Factura en el directorio " + subdirectorio);
                                                    paraLog = paraLog + " 10 Acaba de crear uno d elos XML Factura en el directorio " + subdirectorio;
                                                }
                                                #endregion Guarda el XML en Temporal
                                                #region prepara el PDF
                                                string _ruta = subdirectorio + OrdenCompra + ".xml";
                                                conviertePDF(xdoc, OrdenCompra, subdirectorio, XMLTexto);
                                                //escribeLog("Actividad", "Acaba de crear uno de el PDF Factura en el directorio " + subdirectorio);
                                                paraLog = paraLog + " 11 Acaba de crear uno de el PDF Factura en el directorio " + subdirectorio;
                                                #endregion prepara el PDF
                                                #region version 2 de descargar archivos
                                                string remoteUri = subdirectorio;

                                                #endregion version 2 de descargar archivos
                                                #region Carga, empaqueta y descarga ZIP
                                                zip.AddFile(subdirectorio + OrdenCompra + ".xml");
                                                zip.AddFile(subdirectorio + OrdenCompra + ".pdf");
                                                zip.Save(subdirectorio + OrdenCompra + ".zip");
                                                paraLog = paraLog + " 12 Acaba de adjuntar los archivos " + subdirectorio + OrdenCompra + " al ZIP " + subdirectorio;
                                                //escribeLog("Actividad", "Acaba de adjuntar los archivos "+subdirectorio + OrdenCompra +" al ZIP " + subdirectorio);
                                                //escribeLog("Actividad", "Iniciara descarga del ZIP del directorio " + subdirectorio);
                                                paraLog = paraLog + " 13 Iniciara descarga del ZIP del directorio " + subdirectorio;
                                                //--> oldDownload(subdirectorio + OrdenCompra + ".zip");
                                                //escribeLog("Actividad", "Realizo la  descarga del ZIP." );
                                                //escribeLog("Actividad", "Iniciara descarga del XMl alescritorio " + escritorio);
                                                paraLog = paraLog + "  14 Iniciara descarga del XMl al escritorio <'" + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "'> " + " alternativamente se podria utilizar " + System.Web.HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data") + " alternativamente se podria utilizar tambien " + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                                                byte[] byteZIP = null; ;
                                                #region        Objeto de respuesta
                                                try
                                                {

                                                    #region Descarga el ZIP con todos los archivos adjuntados al ZIP de una vez funciona local GRD 17012017
                                                    //El tamaño se va incrementando hasta tener en la utlima posicion todos los archivos adjuntos
                                                    if (System.IO.File.Exists(subdirectorio + OrdenCompra + ".zip"))
                                                    {
                                                        try
                                                        {
                                                            byteZIP = stream2Byte(subdirectorio + OrdenCompra + ".zip");
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            string _error = ex.Message;
                                                        }
                                                    }
                                                    #endregion Descarga el ZIP con todos los archivos adjuntados al ZIP de una vez

                                                    ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel Respuesta = new ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel();
                                                    Byte[] data = null;
                                                    string contentType = "";
                                                    string _XML = "";
                                                    data = byteZIP;// reg.ContenidoBinario;
                                                    Download(subdirectorio + OrdenCompra + ".zip", "archivo.zip", ref data, ref contentType, ref _XML);
                                                    Respuesta.R1 = "correcto";// reg.IdAlmacenado.ToString();
                                                    Respuesta.R2 = subdirectorio + OrdenCompra + ".zip";
                                                    Respuesta.R3 = "0";
                                                    Respuesta.R4 = "application/octet-stream";
                                                    Respuesta.RByte = data;
                                                    respuesta.Add(Respuesta);
                                                }
                                                catch (Exception ex)
                                                {

                                                }
                                                #endregion Objeto de respuesta

                                                paraLog = paraLog + " 16 Iniciara el borrado los fuentes XML y PDF del ZIP del directorio " + subdirectorio;
                                                System.IO.File.Delete(subdirectorio + OrdenCompra + ".xml");
                                                System.IO.File.Delete(subdirectorio + OrdenCompra + ".pdf");

                                                #region Envia al cliente
                                                ////string fileName = subdirectorio + OrdenCompra + ".zip";
                                                //string fileName = OrdenCompra + ".zip";
                                                //string fullPath = subdirectorio + fileName;
                                                //FileInfo file = new FileInfo(fullPath);
                                                //Response.Clear();
                                                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                                                //Response.ContentType = "application/x-zip-compressed";
                                                //Response.WriteFile(fullPath);
                                                //Response.End();
                                                #endregion Envia al cliente

                                                resultado = true;
                                                ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel miResp = new ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel();
                                                miResp.R1 = "correcto";
                                                //salidaRespuesta.Add(miResp);
                                                escribeLog("Terminado ", paraLog + "Realizo la  descarga del ZIP en el escritorio con ruta " + subdirectorio + OrdenCompra + ".zip");
                                                #endregion Carga, empaqueta y descarga ZIP
                                                #endregion Una sola Orden
                                            }

                                        }
                                        else
                                        {
                                            #region un Bloque de XML
                                            List<string> listaDirectorios = new List<string>();
                                            string _tiempo = "";
                                            System.IO.DirectoryInfo DIR = new System.IO.DirectoryInfo(directorio);
                                            if (!DIR.Exists)
                                            {
                                                System.IO.Directory.CreateDirectory(directorio);
                                            }
                                            #region descarga Archivos
                                            //DataTable dt = new DataTable();
                                            string porProveedor = RFC.Length > 0 == true ? " RFC  ='" + RFC + "'" : "";
                                            string filtros = porProveedor;
                                            DataRow[] salida = table.Select(filtros);
                                            string cuantos = table.Rows.Count.ToString();
                                            var listaRFCs = salida.GroupBy(test => test["RFC"].ToString()).Select(grp => grp.First()).ToList();
                                            string subdirectorio = "";
                                            foreach (DataRow rfcs in listaRFCs)
                                            {
                                                subdirectorio = directorio + rfcs[4].ToString(); ;// @"\" + rfc[4].ToString();
                                                System.IO.DirectoryInfo subDIR = new System.IO.DirectoryInfo(subdirectorio);
                                                if (!subDIR.Exists)
                                                {
                                                    System.IO.Directory.CreateDirectory(subdirectorio);
                                                }
                                                var XMLrelacionados = from subXMLS in salida where subXMLS[4].ToString().Equals(rfcs[4].ToString()) select subXMLS;

                                                foreach (DataRow renglon in XMLrelacionados)
                                                {
                                                    try
                                                    {
                                                        DateTime tiempo = new DateTime();
                                                        if (renglon[1].ToString().Contains("-") == false)
                                                            tiempo = DateTime.ParseExact(renglon[1].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                                        else
                                                            tiempo = DateTime.ParseExact(renglon[1].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                                        string _año = tiempo.Year.ToString();
                                                        string _mes = tiempo.Month.ToString();
                                                        string _ArchivoXML = renglon[0].ToString();
                                                        string _Fecha = renglon[1].ToString();
                                                        string _OrdenCompra = renglon[2].ToString();
                                                        string _NumRecepcion = renglon[3].ToString();
                                                        string _RFC = renglon[4].ToString();
                                                        string _Serie = renglon[5].ToString();
                                                        string _UUID = renglon[6].ToString();
                                                        string _Estatus = renglon[7].ToString().Equals("False") == true ? "Rechazada_" : "Aceptada_";
                                                        _Fecha = _Fecha.Replace("/", "_");
                                                        _Fecha = _Fecha.Replace("_", "_");
                                                        #region Calculo de Mes
                                                        switch (tiempo.Month)
                                                        {
                                                            case 1:
                                                                _mes = "Ene";
                                                                break;
                                                            case 2:
                                                                _mes = "Feb";
                                                                break;
                                                            case 3:
                                                                _mes = "Mar";
                                                                break;
                                                            case 4:
                                                                _mes = "Abr";
                                                                break;
                                                            case 5:
                                                                _mes = "May";
                                                                break;
                                                            case 6:
                                                                _mes = "Jun";
                                                                break;
                                                            case 7:
                                                                _mes = "Jul";
                                                                break;
                                                            case 8:
                                                                _mes = "Ago";
                                                                break;
                                                            case 9:
                                                                _mes = "Sep";
                                                                break;
                                                            case 10:
                                                                _mes = "Oct";
                                                                break;
                                                            case 11:
                                                                _mes = "Nov";
                                                                break;
                                                            case 12:
                                                                _mes = "Dic";
                                                                break;
                                                        }
                                                        #endregion Calculo de Mes
                                                        _tiempo = @"\";// +_año + @"\" + _mes + @"\";
                                                        System.IO.DirectoryInfo subDIRTiempo = new System.IO.DirectoryInfo(subdirectorio + _tiempo);
                                                        if (!subDIRTiempo.Exists)
                                                        {
                                                            System.IO.Directory.CreateDirectory(subdirectorio + _tiempo);
                                                        }
                                                        if (_ArchivoXML.Equals("No proporcionado") == false)
                                                        {
                                                            string ruta = subdirectorio + _tiempo + _Estatus + _OrdenCompra + "_" + _NumRecepcion + "_" + _Fecha + "_" + _RFC + ".xml";
                                                            string _ruta = subdirectorio + _tiempo;
                                                            if (!System.IO.File.Exists(ruta))
                                                            {
                                                                #region Guarda el XML en Temporal
                                                                // guardo el xml fisicamente
                                                                if (!System.IO.File.Exists(ruta))
                                                                {
                                                                    try
                                                                    {
                                                                        //subdirectorio + OrdenCompra + ".xml"
                                                                        FileStream stream;
                                                                        stream = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.Write);
                                                                        StreamWriter writer = new StreamWriter(stream);
                                                                        writer.Write(_ArchivoXML);
                                                                        writer.Close();
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        string _error = ex.Message;
                                                                    }
                                                                }
                                                                #endregion Guarda el XML en Temporal
                                                                conviertePDF(xdoc, _OrdenCompra, _ruta, _ArchivoXML);
                                                                #region Carga, empaqueta y descarga ZIP
                                                                zip.AddFile(ruta);
                                                                zip.AddFile(_ruta + _OrdenCompra + ".pdf");//subdirectorio + OrdenCompra + ".pdf");
                                                                zip.Save(subdirectorio + ".zip");// + OrdenCompra + ".zip");
                                                                //Download(subdirectorio + ".zip");//+ OrdenCompra + ".zip");
                                                                System.IO.File.Delete(ruta);//subdirectorio + OrdenCompra + ".xml");
                                                                System.IO.File.Delete(_ruta + _OrdenCompra + ".pdf");//subdirectorio + OrdenCompra + ".pdf");
                                                                #endregion Carga, empaqueta y descarga ZIP
                                                            }
                                                            _cuantos++;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        string _ex = ex.Message;
                                                        escribeLog("Error", "Ocurrio el error: Excepcion " + ex.InnerException + " con el mensaje " + ex.Message + " y el origen " + ex.Source);
                                                    }
                                                }
                                                //zip.Save(subdirectorio+ ".zip");
                                                listaDirectorios.Add(subdirectorio + ".zip");
                                                //Download(subdirectorio + ".zip");
                                                Directory.Delete(subdirectorio + _tiempo, true);
                                                ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel miResp = new ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel();
                                                miResp.R1 = "correcto";
                                                salidaRespuesta.Add(miResp);
                                                resultado = true;
                                            }
                                            #endregion descarga Archivos
                                            #endregion un Bloque de XML
                                        }
                                    }
                            }
                            catch (Exception _ex)
                            {
                                escribeLog("Error", paraLog + "Ocurrio el error: Excepcion " + _ex.InnerException + " con el mensaje " + _ex.Message + " y el origen " + _ex.Source);
                                resultado = false;
                                ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel miResp = new ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel();
                                miResp.R1 = "error";
                                salidaRespuesta.Add(miResp);
                            }

                        }
                        #endregion
                    }
                    catch (Exception _error)
                    {
                        escribeLog("Error ", paraLog + " Ocurrio el error " + _error.Message);
                    }
                }
                else
                {
                    escribeLog("Fin ", paraLog + " No encontro datos para procesar");
                }
            return Json(respuesta);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        //[HttpPost]
        public ActionResult Download(string ubicacion, string nombreArchivo, ref byte[] data, ref string contentType, ref string _XML)
        {
            System.IO.FileInfo toDownload = new System.IO.FileInfo(ubicacion);
            //Configuracion con = new Configuracion();

            //if (ubicacion.Equals("NA") == false)
            //{
            //    string _ubicacion = @"C:\Temp\" + nombreArchivo;
            //    byte[] fileBytes = System.IO.File.ReadAllBytes(@"" + ubicacion);
            //    data = fileBytes;
            //    contentType = MimeMapping.GetMimeMapping(ubicacion);
            //    String contentXml = null;
            //    contentXml = System.Text.Encoding.UTF8.GetString(data);
            //    System.Xml.XmlDocument doc = StringToXml(contentXml);
            //    _XML = contentXml;
            //    return File(fileBytes, contentType);
            //}
            //else
            //{
            int punto = nombreArchivo.IndexOf(".");
            int largo = nombreArchivo.Length;
            int extension = (largo - punto);
            string _extencion = nombreArchivo.Substring(punto, extension);
            _extencion = _extencion.ToUpper();
            if (_extencion.Contains("PDF") == true)
                contentType = "application/pdf";
            else
                if (_extencion.Contains("OCX") == true)
                    contentType = "application/ms-word";
                else
                    if (_extencion.Contains("LSX") == true)
                        contentType = "application/vnd.xls";
                    else
                        contentType = "application/octet-stream";
            return File(data, contentType);
            //}
        }

        private DateTime ConvertToDateTime(string strDateTime)
        {
            DateTime dtFinaldate; string sDateTime;
            try { dtFinaldate = Convert.ToDateTime(strDateTime); }
            catch (Exception e)
            {
                string[] sDate = strDateTime.Split('/');
                sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
                dtFinaldate = Convert.ToDateTime(sDateTime);
            }
            return dtFinaldate;
        }



        private void escribeLog(string tipo, string descripcion)
        {
            //using (FileStream fs = System.IO.File.Open(@"C:\\Temp\\" + "log_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".xml",
            using (FileStream fs = System.IO.File.Open(@"C:\\Temp\\" + "log.xml",
             FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(fs, System.Text.Encoding.ASCII);
                writer.WriteElementString("Evento", "", tipo + " " + descripcion + " " + DateTime.Now.ToString());
                writer.WriteWhitespace("\n");
                writer.Close();
            }
        }

        [HttpPost]
        public ActionResult recuperaArchivosZip()//List<KeyValuePair<string, byte[]>> recuperaArchivosZip()
        {
            System.Collections.Generic.List<ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel> respuesta = new System.Collections.Generic.List<ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel>();
            List<KeyValuePair<string, byte[]>> archivosZip = (List<KeyValuePair<string, byte[]>>)Session["archivosZip"];
            #region        Objeto de respuesta
            try
            {
                ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel Respuesta = new ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel();
                Byte[] data = null;
                string contentType = "";
                string _XML = "";
                data = archivosZip[archivosZip.Count - 1].Value;// reg.ContenidoBinario;
                //Download(archivosZip[archivosZip.Count - 1].Key, "archivo.zip", ref data, ref contentType, ref _XML);
                Respuesta.R1 = "correcto";// reg.IdAlmacenado.ToString();
                Respuesta.R2 = archivosZip[archivosZip.Count - 1].Key;
                Respuesta.R3 = "0";
                Respuesta.R4 = "application/octet-stream";
                Respuesta.RByte = data;
                //Borrando todo dentro de el directorio
                //System.IO.Directory.Delete(directorio + @"\" + _RFC, true);
                respuesta.Add(Respuesta);
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    wc.DownloadFile(Respuesta.R2, @"C:\Temp\1.zip");
                }
            }
            catch (Exception ex)
            {

            }
            #endregion Objeto de respuesta
            return Json(respuesta);
        }


        private string convierteFecha(string fecha)
        {
            string salida = "";
            int posPunto = 0;
            const string punto = ".";
            string dia = "";
            string año = "";
            string mes = "";
            string original = fecha;

            posPunto = fecha.LastIndexOf(punto) + 1;
            año = fecha.Substring(posPunto, 4);
            posPunto = fecha.IndexOf(punto);
            dia = fecha.Substring(0, posPunto);
            fecha = fecha.Replace("." + año, "");
            fecha = fecha.Replace(dia + ".", "");
            mes = fecha.Replace(".", "");
            salida = año + "/" + mes + "/" + dia;
            return salida;
        }

        /// <summary>
        /// Descarga XML y PDF en bloque por RFC
        /// </summary>
        /// <param name="rfc"></param>
        /// <param name="aplicarfechas"></param>
        /// <param name="finicial"></param>
        /// <param name="ffinal"></param>
        /// <param name="numproveedor"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult bajaTodoRFC(string rfc, bool aplicarfechas, string finicial, string ffinal)//, string numproveedor)
        {
            string paraLog = "";
            System.Collections.Generic.List<ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel> respuesta = new System.Collections.Generic.List<ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel>();
            List<KeyValuePair<string, byte[]>> archivosZip = new List<KeyValuePair<string, byte[]>>();
            //Configuracion con = new Configuracion();
            string ini = cliente.primerDiaMes().Date.ToString();
            string fin = cliente.ultimoDiaMes().Date.ToString();
            string _refini = "";
            string _reffin = "";
            string mensaje = "";
            string query = "";
            string _sinicial = finicial;
            string _sfinal = ffinal;

            #region Fechas
            string fechas = "";
            if (aplicarfechas == true)
            {
                if (finicial.Length <= 0)
                    finicial = cliente.primerDiaMes().Date.ToString();
                if (ffinal.Length <= 0)
                    ffinal = DateTime.Now.Date.ToString();
                cliente.formateaFechasSQL(ref finicial, ref ffinal, ref mensaje, "", (string)Session["Usuario"]);//    numproveedor, (string)Session["Usuario"]);
                fechas = "  And (TRY_CONVERT(datetime, fechaAlmacenado, 103)  between TRY_CONVERT(datetime, '" + finicial + "', 103) AND TRY_CONVERT(datetime,  '" + ffinal + "', 103) )  ";
            }
            ini = finicial;
            fin = ffinal;
            if (rfc.Length > 0)
            {
                query = query + "  And (RFCEmisor  like '%" + rfc + "%') ";
            }
            #endregion Fechas
            string _RFC = "";

            #region Consulta a la Base de Datos
            //bool exists = System.IO.Directory.Exists(Server.MapPath("~/Temporales"));
            //if (!exists)
            //    System.IO.Directory.CreateDirectory(Server.MapPath("~/Temporales"));

            List<string> resp = new List<string>();
            resp.Add(ini);
            resp.Add(fin);
            List<ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel> registro = new List<ClickFactura_Entidades.BD.Modelos.repositorioArchivosModel>();
            SqlCommand comando = new SqlCommand();
            DataTable dt = new DataTable();
            dt.TableName = "view_FacturasVerirficadas";
            SqlConnection Conexion = new SqlConnection(ClickFactura_Entidades.BD.Entidades.AccesoBD.CadenaConexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            comando.CommandType = System.Data.CommandType.Text;
            comando.Connection = Conexion;

            comando.CommandText = "select * from view_AlmacenamientoInterno where IdAlmacenado  is not null " + fechas + query;

            adaptador.SelectCommand = comando;
            adaptador.Fill(dt);
            paraLog = paraLog + " realiza la consulta a la tabla  view_AlmacenamientoInterno ";
            #endregion Consulta a la Base de Datos

            bool resultado = false;
            try
            {
                if (dt != null)
                    if (dt.Rows.Count > 0)
                    {
                        #region llama a descarga
                        Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile();
                        DataTable t = new DataTable();
                        string OrdenCompra = rfc;// rmt_OrdenCompra.Text;// "0300879112";
                        string ordencompra = OrdenCompra;
                        string RFC = rfc;
                        if (ordencompra.Length > 0)
                        {
                            #region estructura Fechas
                            string sinicial = "";
                            string sfinal = "";
                            try
                            {
                                System.Globalization.DateTimeFormatInfo usDtfi = new System.Globalization.CultureInfo("es-MX", false).DateTimeFormat;

                                //DateTime.ParseExact(_sinicial, "yyyy-M-d", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                                //-->sinicial=Convert.ToDateTime(finicial).ToString("yyyy-MM-dd",new System.Globalization.CultureInfo("es-MX", false).DateTimeFormat);

                                //-->sfinal = Convert.ToDateTime(ffinal).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("es-MX", false).DateTimeFormat);
                                var culture = System.Globalization.CultureInfo.CurrentCulture;

                                //sinicial=Convert.ToDateTime(finicial.ToString()).ToString("yyyy-MM-dd HH:mm:ss",culture);
                                //paraLog = paraLog + " Valor para  inicial  " + sinicial;
                                //sfinal = Convert.ToDateTime(ffinal.ToString()).ToString("yyyy-MM-dd HH:mm:ss",culture);
                                //paraLog = paraLog + " Valor para  final  " + sfinal;

                                _refini = _sinicial;
                                _reffin = _sfinal;

                                paraLog = paraLog + " Entonces quedan para inicial  " + sinicial + " ya para final " + sfinal;
                            }
                            catch (Exception ex)
                            {
                                paraLog = paraLog + "Error" + " Convirtiendo fechas! Inicial  " + sinicial + " y la final " + sfinal + " en " + ex.Message;
                            }
                            #endregion estructura Fechas
                            string XMLTexto = "";
                            //string directorio = Server.MapPath("~/Temporales");// @"C:\Temp\";
                            //                    var fileName = Path.Combine(Environment.GetFolderPath(
                            //Environment.SpecialFolder.ApplicationData), "DateLinks.xml")

                            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                            string pathDownload = Path.Combine(pathUser, "Downloads");

                            string directorio = System.Configuration.ConfigurationManager.AppSettings["pathInvoice"];// pathDownload;//Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "");

                            //directorio = System.Configuration.ConfigurationManager.AppSettings["pathInvoice"]; //Server.MapPath("~/Temporales");
                            Dictionary<string, string> parametros = new Dictionary<string, string>();
                            parametros.Clear();
                            string tabla = "Donwload_Facturas_T_BitacoraOperaciones";


                            //DateTime.ParseExact(text, timeFormat, System.Globalization.CultureInfo.InvariantCulture);
                            //DateTime dtimeIni = ConvertToDateTime(text); //DateTime.ParseExact(text,timeFormat, System.Globalization.CultureInfo.InvariantCulture);
                            //paraLog = paraLog + " 5.2  Fecha  inicial para servidor " + dtimeIni.Date.ToString();
                            //DateTime dtimeFin = dtimeIni;// DateTime.ParseExact(text, timeFormat, System.Globalization.CultureInfo.InvariantCulture);
                            //paraLog = paraLog + " 5.2  Fecha  Final para servidor " + dtimeFin.Date.ToString();

                            //DateTime dtimeIni = DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            //DateTime dtimeFin = DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            int pos = ini.IndexOf(" ");
                            if (pos > 0)
                                ini = ini.Substring(0, pos);
                            pos = fin.IndexOf(" ");
                            if (pos > 0)
                                fin = fin.Substring(0, pos);
                            sinicial = sinicial.Equals("") == true ? finicial : sinicial;
                            sfinal = sfinal.Equals("") == true ? ffinal : sfinal;
                            sinicial = convierteFecha(sinicial);
                            sfinal = convierteFecha(sfinal);
                            parametros.Add("Inicial", sinicial.Equals(sfinal) == true ? sinicial : sinicial);
                            parametros.Add("Final", sfinal.Equals(sinicial) == true ? sinicial : sfinal);
                            paraLog = paraLog + " Se va a mandar como paremetros al Store: fecha inicial " + sinicial + " y fecha final " + sfinal;
                            OrdenCompra = RFC.Length > 0 == true ? RFC : (ordencompra.Length > 0 ? ordencompra : ordencompra.Length == 9 ? "0" + ordencompra : ordencompra);
                            parametros.Add("OrdenCompra", ordencompra.Length > 0 && ordencompra != RFC ? ordencompra : RFC);
                            string P_Opcion = RFC.Length > 0 == true ? "RFC" : "OC";
                            DataTable table = new DataTable();
                            //Configuracion c = new Configuracion();
                            ClickFactura_Facturacion.Genericos.Genericos c = new ClickFactura_Facturacion.Genericos.Genericos();
                            paraLog = paraLog + "Se va a lanzar la consulta a SQL Server ";
                            table = c.executaSP_Generico(parametros, P_Opcion, tabla);
                            paraLog = paraLog + "Se acaba de ejecutar la consulta ";
                            if (table == null)
                            {
                                paraLog = paraLog + "La tabla resultante de buscar por RFC regreso nula ";
                                parametros.Clear();
                                parametros.Add("Inicial", _refini.Equals(_reffin) == true ? _refini : _refini);
                                parametros.Add("Final", _reffin.Equals(_refini) == true ? _refini : _reffin);
                                OrdenCompra = RFC.Length > 0 == true ? RFC : (ordencompra.Length > 0 ? ordencompra : ordencompra.Length == 9 ? "0" + ordencompra : ordencompra);
                                parametros.Add("OrdenCompra", ordencompra.Length > 0 ? ordencompra : RFC);
                                P_Opcion = RFC.Length > 0 == true ? "RFC" : "OC";
                                paraLog = paraLog + " Se reenviara a valores por default con fechas  " + _refini + "  " + _reffin;
                                table = c.executaSP_Generico(parametros, P_Opcion, tabla);
                                paraLog = paraLog + " No se encnontraron datos enviando fechas por default pero si regreso de la consulta";
                            }
                            System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                            ordencompra = RFC.Length > 0 == true ? "" : ordencompra;
                            try
                            {
                                if (table != null)
                                    if (table.Rows.Count > 0)
                                    {
                                        int _cuantos = 0;
                                        t = table;
                                        if (RFC.Length <= 0)
                                        {
                                            for (int indice = 0; indice < (table.Rows.Count); indice++)
                                            {
                                                #region Una sola Orden
                                                #region Carga el XML
                                                string subdirectorio = System.Configuration.ConfigurationManager.AppSettings["pathInvoice"] == null ? @"C:\Temp\" : System.Configuration.ConfigurationManager.AppSettings["pathInvoice"];
                                                XMLTexto = table.Rows[indice].ItemArray[0].ToString();
                                                xdoc.LoadXml(XMLTexto);
                                                #endregion Carga el XML
                                                #region Crea el directorio temporal
                                                System.IO.DirectoryInfo DIR = new System.IO.DirectoryInfo(subdirectorio);
                                                if (!DIR.Exists)
                                                {
                                                    System.IO.Directory.CreateDirectory(subdirectorio);
                                                }
                                                #endregion Crea el directorio temporal
                                                #region Guarda el XML en Temporal
                                                // guardo el xml fisicamente
                                                if (!System.IO.File.Exists(subdirectorio + OrdenCompra + ".xml"))
                                                {
                                                    //subdirectorio + OrdenCompra + ".xml"
                                                    FileStream stream;
                                                    stream = new FileStream(subdirectorio + OrdenCompra + ".xml", FileMode.OpenOrCreate, FileAccess.Write);
                                                    StreamWriter writer = new StreamWriter(stream);
                                                    writer.Write(XMLTexto);
                                                    writer.Close();
                                                }
                                                #endregion Guarda el XML en Temporal
                                                #region prepara el PDF
                                                string _ruta = subdirectorio + OrdenCompra + ".xml";
                                                conviertePDF(xdoc, OrdenCompra, subdirectorio, XMLTexto);
                                                #endregion prepara el PDF
                                                #region version 2 de descargar archivos
                                                string remoteUri = subdirectorio;
                                                //string fileName = subdirectorio + OrdenCompra + ".zip", myStringWebResource = null;
                                                //using (System.Net.WebClient myWebClient = new System.Net.WebClient())
                                                //{
                                                //    myStringWebResource = remoteUri + OrdenCompra + ".xml";
                                                //    string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                                //    myWebClient.DownloadFile(myStringWebResource, escritorio + @"\" + OrdenCompra + ".xml");
                                                //}
                                                //using (System.Net.WebClient myWebClient = new System.Net.WebClient())
                                                //{
                                                //    myStringWebResource = remoteUri + OrdenCompra + ".pdf";
                                                //    string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                                //    myWebClient.DownloadFile(myStringWebResource, escritorio + @"\" + OrdenCompra + ".pdf");
                                                //}
                                                #endregion version 2 de descargar archivos
                                                #region Carga, empaqueta y descarga ZIP
                                                zip.AddFile(subdirectorio + OrdenCompra + ".xml");
                                                zip.AddFile(subdirectorio + OrdenCompra + ".pdf");
                                                zip.Save(subdirectorio + OrdenCompra + ".zip");

                                                string fileName = subdirectorio + OrdenCompra + ".zip", myStringWebResource = null;
                                                using (System.Net.WebClient myWebClient = new System.Net.WebClient())
                                                {
                                                    myStringWebResource = remoteUri + OrdenCompra + ".xml";
                                                    string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                                    myWebClient.DownloadFile(myStringWebResource, escritorio + @"\" + OrdenCompra + ".xml");
                                                }
                                                Download(subdirectorio + OrdenCompra + ".zip");
                                                System.IO.File.Delete(subdirectorio + OrdenCompra + ".xml");
                                                System.IO.File.Delete(subdirectorio + OrdenCompra + ".pdf");
                                                resultado = true;
                                                #endregion Carga, empaqueta y descarga ZIP
                                                #endregion Una sola Orden
                                            }

                                        }
                                        else
                                        {
                                            #region un Bloque de XML
                                            List<string> listaDirectorios = new List<string>();
                                            string _tiempo = "";
                                            #region descarga Archivos
                                            //DataTable dt = new DataTable();
                                            string porProveedor = RFC.Length > 0 == true ? " RFC  ='" + RFC + "'" : "";
                                            string filtros = porProveedor;
                                            DataRow[] salida = table.Select(filtros);
                                            string cuantos = table.Rows.Count.ToString();
                                            paraLog = paraLog + " Se filtarran los RFC de la DataTable ";
                                            var listaRFCs = salida.GroupBy(test => test["RFC"].ToString()).Select(grp => grp.First()).ToList();
                                            string subdirectorio = "";
                                            foreach (DataRow rfcs in listaRFCs)
                                            {
                                                paraLog = paraLog + "  RFCs filtrados de la DataTable ";
                                                subdirectorio = directorio + rfcs[4].ToString(); ;// @"\" + rfc[4].ToString();
                                                System.IO.DirectoryInfo subDIR = new System.IO.DirectoryInfo(subdirectorio + @"\");
                                                paraLog = paraLog + "  Probando si existe el directorio " + subDIR;
                                                if (!subDIR.Exists)
                                                {
                                                    System.IO.Directory.CreateDirectory(subdirectorio);
                                                    paraLog = paraLog + " No encontro el subdirectorio " + subDIR + " y lo creo";
                                                }
                                                var XMLrelacionados = from subXMLS in salida where subXMLS[4].ToString().Equals(rfcs[4].ToString()) select subXMLS;

                                                foreach (DataRow renglon in XMLrelacionados)
                                                {
                                                    try
                                                    {
                                                        DateTime tiempo = new DateTime();
                                                        if (renglon[1].ToString().Contains("-") == false)
                                                            tiempo = DateTime.ParseExact(renglon[1].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                                        else
                                                            tiempo = DateTime.ParseExact(renglon[1].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                                        string _año = tiempo.Year.ToString();
                                                        string _mes = tiempo.Month.ToString();
                                                        string _ArchivoXML = renglon[0].ToString();
                                                        string _Fecha = renglon[1].ToString();
                                                        string _OrdenCompra = renglon[2].ToString();
                                                        string _NumRecepcion = renglon[3].ToString();
                                                        _RFC = renglon[4].ToString();
                                                        string _Serie = renglon[5].ToString();
                                                        string _UUID = renglon[6].ToString();
                                                        string _Estatus = renglon[7].ToString().Equals("False") == true ? "Rechazada_" : "Aceptada_";
                                                        _Fecha = _Fecha.Replace("/", "_");
                                                        _Fecha = _Fecha.Replace("_", "_");
                                                        #region Calculo de Mes
                                                        switch (tiempo.Month)
                                                        {
                                                            case 1:
                                                                _mes = "Ene";
                                                                break;
                                                            case 2:
                                                                _mes = "Feb";
                                                                break;
                                                            case 3:
                                                                _mes = "Mar";
                                                                break;
                                                            case 4:
                                                                _mes = "Abr";
                                                                break;
                                                            case 5:
                                                                _mes = "May";
                                                                break;
                                                            case 6:
                                                                _mes = "Jun";
                                                                break;
                                                            case 7:
                                                                _mes = "Jul";
                                                                break;
                                                            case 8:
                                                                _mes = "Ago";
                                                                break;
                                                            case 9:
                                                                _mes = "Sep";
                                                                break;
                                                            case 10:
                                                                _mes = "Oct";
                                                                break;
                                                            case 11:
                                                                _mes = "Nov";
                                                                break;
                                                            case 12:
                                                                _mes = "Dic";
                                                                break;
                                                        }
                                                        #endregion Calculo de Mes
                                                        _tiempo = @"\";// +_año + @"\" + _mes + @"\";
                                                        System.IO.DirectoryInfo subDIRTiempo = new System.IO.DirectoryInfo(subdirectorio + _tiempo);
                                                        paraLog = paraLog + " No encontro el subdirectorio " + subDIR + " y lo creo";
                                                        //if (!subDIRTiempo.Exists)
                                                        //{
                                                        //    System.IO.Directory.CreateDirectory(subdirectorio + _tiempo);
                                                        //}
                                                        if (_ArchivoXML.Equals("No proporcionado") == false)
                                                        {
                                                            string ruta = subdirectorio + _tiempo + _Estatus + _OrdenCompra + "_" + _NumRecepcion + "_" + _Fecha + "_" + _RFC + ".xml";
                                                            string _ruta = subdirectorio + _tiempo;
                                                            byte[] byteZIP = null;
                                                            // if (!System.IO.File.Exists(ruta))
                                                            if (System.IO.Directory.Exists(subdirectorio))
                                                            {
                                                                #region Guarda el XML en Temporal
                                                                // guardo el xml fisicamente
                                                                //if (!System.IO.File.Exists(ruta))
                                                                if (!System.IO.File.Exists(ruta) == true)
                                                                {
                                                                    try
                                                                    {
                                                                        //subdirectorio + OrdenCompra + ".xml"
                                                                        FileStream stream;
                                                                        stream = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.Write);
                                                                        StreamWriter writer = new StreamWriter(stream);
                                                                        writer.Write(_ArchivoXML);
                                                                        writer.Close();
                                                                        paraLog = paraLog + " Se creo un XML en la ruta " + ruta;
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        string _error = ex.Message;
                                                                        escribeLog("Error ", paraLog + " Ocurrio el error creando los XML " + _error);
                                                                    }
                                                                }
                                                                #endregion Guarda el XML en Temporal
                                                                paraLog = paraLog + " iniciaara la creacion del PDF " + _ruta;
                                                                conviertePDF(xdoc, _OrdenCompra, _ruta, _ArchivoXML);
                                                                paraLog = paraLog + " Creo el PDF de la ruta " + _ruta;
                                                                #region Carga, empaqueta y descarga ZIP
                                                                zip.AddFile(ruta);
                                                                zip.AddFile(_ruta + _OrdenCompra + ".pdf");//subdirectorio + OrdenCompra + ".pdf");

                                                                #region Descarga el ZIP con todos los archivos adjuntados al ZIP de una vez funciona local GRD 17012017
                                                                //El tamaño se va incrementando hasta tener en la utlima posicion todos los archivos adjuntos
                                                                paraLog = paraLog + "Va a crear el ZIP en la ruta  " + subdirectorio + @"\" + _RFC + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".zip";
                                                                zip.Save(subdirectorio + @"\" + _RFC + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".zip");

                                                                if (System.IO.File.Exists(subdirectorio + @"\" + _RFC + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".zip"))
                                                                {
                                                                    try
                                                                    {
                                                                        paraLog = paraLog + " Va a crear el flujo de bytes de la ruta " + subdirectorio + @"\" + _RFC + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".zip";
                                                                        byteZIP = stream2Byte(subdirectorio + @"\" + _RFC + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".zip");
                                                                        paraLog = paraLog + " Flujo de bytes creado de la ruta " + subdirectorio + @"\" + _RFC + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".zip";
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        string _error = ex.Message;
                                                                        paraLog = paraLog + " Ocurrio el error  creando el flujo de los bytes " + _error;
                                                                    }
                                                                }
                                                                #endregion Descarga el ZIP con todos los archivos adjuntados al ZIP de una vez

                                                                try
                                                                {
                                                                    archivosZip.Add(new KeyValuePair<string, byte[]>(subdirectorio + @"\" + _RFC + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".zip", byteZIP));

                                                                    paraLog = paraLog + " Se agrego un objeto de bytes del listado que se regresan";
                                                                }
                                                                catch (Exception _issue)
                                                                {
                                                                    string _error = _issue.Message;
                                                                    paraLog = paraLog + " Ocurrio el error agregando las rutas ZIP  " + _error;
                                                                }

                                                                //archivosZip.Add(new KeyValuePair<string, string>(filePDF, _OrdenCompra));
                                                                //Download(subdirectorio + ".zip");//+ OrdenCompra + ".zip");
                                                                //System.IO.File.Delete(ruta);//subdirectorio + OrdenCompra + ".xml");
                                                                //System.IO.File.Delete(_ruta + _OrdenCompra + ".pdf");//subdirectorio + OrdenCompra + ".pdf");

                                                                #endregion Carga, empaqueta y descarga ZIP
                                                            }
                                                            _cuantos++;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        string _ex = ex.Message;
                                                        escribeLog("Error ", paraLog + " Ocurrio el error General " + _ex);
                                                    }
                                                }

                                                listaDirectorios.Add(subdirectorio + ".zip");
                                                resultado = true;
                                            }
                                            #endregion descarga Archivos
                                            #endregion un Bloque de XML
                                        }
                                    }
                            }
                            catch (Exception _ex)
                            {
                                //resultado = false;
                                escribeLog("Error ", paraLog + " Ocurrio el error " + _ex);
                            }

                        }
                        #endregion
                    }
                //return Json(resultado);
                #region Por borrar
                if (resultado == false)
                {
                    //Session["archivosZip"] = archivosZip;
                    #region        Objeto de respuesta
                    //try
                    //{
                    //    BAFAR.Models.RespuestasGenericasModel Respuesta = new BAFAR.Models.RespuestasGenericasModel();
                    //    Byte[] data = null;
                    //    string contentType = "";
                    //    string _XML = "";
                    //    data = archivosZip[0].Value;// reg.ContenidoBinario;
                    //    Respuesta.R1 = _XML;// reg.IdAlmacenado.ToString();
                    //    Respuesta.R2 = "archivo.zip";
                    //    Respuesta.R3 = "0";
                    //    Respuesta.R4 = "application/octet-stream";
                    //    Respuesta.RByte = data;
                    //    respuesta.Add(Respuesta);
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                    #endregion Objeto de respuesta
                }
                else
                {
                    //Session["archivosZip"] = archivosZip;
                    #region        Objeto de respuesta
                    //try
                    //{
                    //    BAFAR.Models.RespuestasGenericasModel Respuesta = new BAFAR.Models.RespuestasGenericasModel();
                    //    Byte[] data = null;
                    //    string contentType = "";
                    //    string _XML = "";
                    //    data = archivosZip[archivosZip.Count - 1].Value;// reg.ContenidoBinario;
                    //    Respuesta.R1 = "correcto";// reg.IdAlmacenado.ToString();
                    //    Respuesta.R2 = "archivo.zip";
                    //    Respuesta.R3 = "0";
                    //    Respuesta.R4 = "application/octet-stream";
                    //    Respuesta.RByte = data;
                    //    respuesta.Add(Respuesta);
                    //}
                    //catch (Exception ex)
                    //{
                    //}
                    #endregion Objeto de respuesta
                }
                #endregion Por borrar
                Session["archivosZip"] = archivosZip;
                ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel Respuesta = new ClickFactura_Entidades.BD.Modelos.RespuestasGenericasModel();
                Byte[] data = null;
                data = archivosZip[archivosZip.Count - 1].Value;// reg.ContenidoBinario;
                Respuesta.R1 = "hecho";// reg.IdAlmacenado.ToString();
                Respuesta.R2 = archivosZip[archivosZip.Count - 1].Key;// "archivo.zip";
                Respuesta.R3 = "0";
                Respuesta.R4 = "application/octet-stream";
                Respuesta.RByte = data;
                respuesta.Add(Respuesta);
                paraLog = paraLog + " Iniciaremos la descarga al cliente";
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    try
                    {
                        wc.DownloadFile(Respuesta.R2, @"C:\Temp\" + _RFC + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".zip");
                        paraLog = paraLog + " Ya se envio y ahora se borraran los archivos creados";
                        #region Borrando archivos auxiliares
                        if (System.IO.Directory.Exists(@"C:\Temp\" + _RFC))
                        {
                            foreach (var item in System.IO.Directory.GetFiles(@"C:\Temp\" + _RFC))
                            {
                                if (item.Contains(".zip") != true)
                                    System.IO.File.Delete(item);
                                paraLog = paraLog + " Se borro el archivo " + item;
                            }
                            //Borrando todo dentro de el directorio
                            System.IO.Directory.Delete(@"C:\Temp\" + _RFC, true);
                            paraLog = paraLog + " Se borro el directorio raiz " + @"C:\Temp\" + _RFC;
                        }
                        #endregion Borrando archivos auxiliares
                    }
                    catch (Exception __ex)
                    {
                        paraLog = paraLog + "Ocurrrio el problema " + __ex.Message;

                    }
                }
                escribeLog("Finalizado  ", paraLog);
            }
            catch (Exception _issue)
            {
                string error = _issue.Message;
                escribeLog("Error general  ", paraLog + error);
            }
            return Json(respuesta);
        }

        public class JsonStringResult : ContentResult
        {
            public JsonStringResult(string json)
            {
                Content = json;
                ContentType = "application/json";
            }
        }
        static public void oldDownload(string patch)
        {
            System.IO.FileInfo toDownload = new System.IO.FileInfo(patch);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.WriteFile(patch);
            System.Web.HttpContext.Current.Response.End();

        }
        public bool ByteArrayaArchivoyenviaEscritorio(string _FileName, byte[] _ByteArray)
        {
            try
            {
                if (System.IO.File.Exists(_FileName) == true)
                {
                    System.IO.File.Delete(_FileName);
                }

                // Open file for reading
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                int largo = _FileName.Length;
                int posicion = _FileName.LastIndexOf(@"\") + 1;
                int Cuantos = largo - posicion;
                string fileName = _FileName.Substring(posicion, Cuantos);

                Download(_FileName + fileName);

                System.IO.File.Copy(_FileName, escritorio + @"/" + fileName, true);

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }
        public void bajaArchivos(string id, string rfc, string ordencompra, string finicial, string ffinal, ref bool resultado)
        {
            Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile();
            DataTable t = new DataTable();
            string OrdenCompra = ordencompra;// rmt_OrdenCompra.Text;// "0300879112";
            string RFC = rfc;

            if (RFC.Length > 0 || ordencompra.Length > 0)
            {
                #region estructura Fechas
                string sinicial = "";
                string sfinal = "";
                try
                {
                    //string fechaInicial = rdt_Inicial.SelectedDate.Value.ToString("yyyy-MM-dd");
                    //string fechaFinal = rdt_Final.SelectedDate.Value.ToString("yyyy-MM-dd");

                    DateTime inicial = Convert.ToDateTime(finicial);//rdt_Inicial.SelectedDate);
                    sinicial = inicial.ToString("yyyy-MM-dd"); //+" 00:00:00";//("dd/MM/yyyy"); //+" 00:00:00";
                    DateTime final = Convert.ToDateTime(ffinal);//rdt_Final.SelectedDate);
                    sfinal = final.ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                {

                }
                #endregion estructura Fechas
                string XMLTexto = "";
                string directorio = @"C:\Temp\";
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Clear();
                string tabla = "Donwload_Facturas_T_BitacoraOperaciones";
                DateTime dtimeIni = DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtimeFin = DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //Configuracion con = new Configuracion();
                string mensaje = "";
                string ini = dtimeIni.Date.ToString();
                int pos = ini.IndexOf(" ");
                ini = ini.Substring(0, pos);
                string fin = dtimeFin.Date.ToString();
                pos = fin.IndexOf(" ");
                fin = fin.Substring(0, pos);
                parametros.Add("Inicial", sinicial.Equals(sfinal) == true ? sinicial : sinicial);
                parametros.Add("Final", sfinal.Equals(sinicial) == true ? sinicial : sfinal);
                OrdenCompra = RFC.Length > 0 == true ? RFC : (ordencompra.Length > 0 ? ordencompra : ordencompra.Length == 9 ? "0" + ordencompra : ordencompra);
                parametros.Add("OrdenCompra", ordencompra.Length > 0 ? ordencompra : RFC);
                string P_Opcion = RFC.Length > 0 == true ? "RFC" : "OC";
                DataTable table = new DataTable();
                //Configuracion c = new Configuracion();
                ClickFactura_Facturacion.Genericos.Genericos c = new ClickFactura_Facturacion.Genericos.Genericos();
                table = c.executaSP_Generico(parametros, P_Opcion, tabla);
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                ordencompra = RFC.Length > 0 == true ? "" : ordencompra;
                try
                {
                    if (table != null)
                        if (table.Rows.Count > 0)
                        {
                            int _cuantos = 0;
                            t = table;
                            if (RFC.Length <= 0)
                            {
                                for (int indice = 0; indice < (table.Rows.Count); indice++)
                                {
                                    #region Una sola Orden
                                    #region Carga el XML
                                    string subdirectorio = System.Configuration.ConfigurationManager.AppSettings["pathInvoice"] == null ? @"C:\Temp\" : System.Configuration.ConfigurationManager.AppSettings["pathInvoice"];
                                    XMLTexto = table.Rows[indice].ItemArray[0].ToString();
                                    xdoc.LoadXml(XMLTexto);
                                    #endregion Carga el XML
                                    #region Crea el directorio temporal
                                    System.IO.DirectoryInfo DIR = new System.IO.DirectoryInfo(subdirectorio);
                                    if (!DIR.Exists)
                                    {
                                        System.IO.Directory.CreateDirectory(subdirectorio);
                                    }
                                    #endregion Crea el directorio temporal
                                    #region Guarda el XML en Temporal
                                    // guardo el xml fisicamente
                                    if (!System.IO.File.Exists(subdirectorio + OrdenCompra + ".xml"))
                                    {
                                        //subdirectorio + OrdenCompra + ".xml"
                                        FileStream stream;
                                        stream = new FileStream(subdirectorio + OrdenCompra + ".xml", FileMode.OpenOrCreate, FileAccess.Write);
                                        StreamWriter writer = new StreamWriter(stream);
                                        writer.Write(XMLTexto);
                                        writer.Close();
                                    }
                                    #endregion Guarda el XML en Temporal
                                    #region prepara el PDF
                                    string _ruta = subdirectorio + OrdenCompra + ".xml";
                                    conviertePDF(xdoc, OrdenCompra, subdirectorio, XMLTexto);
                                    #endregion prepara el PDF
                                    #region version 2 de descargar archivos
                                    string remoteUri = subdirectorio;
                                    //string fileName = subdirectorio + OrdenCompra + ".zip", myStringWebResource = null;
                                    //using (System.Net.WebClient myWebClient = new System.Net.WebClient())
                                    //{
                                    //    myStringWebResource = remoteUri + OrdenCompra + ".xml";
                                    //    string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                    //    myWebClient.DownloadFile(myStringWebResource, escritorio + @"\" + OrdenCompra + ".xml");
                                    //}
                                    //using (System.Net.WebClient myWebClient = new System.Net.WebClient())
                                    //{
                                    //    myStringWebResource = remoteUri + OrdenCompra + ".pdf";
                                    //    string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                    //    myWebClient.DownloadFile(myStringWebResource, escritorio + @"\" + OrdenCompra + ".pdf");
                                    //}
                                    #endregion version 2 de descargar archivos
                                    #region Carga, empaqueta y descarga ZIP
                                    zip.AddFile(subdirectorio + OrdenCompra + ".xml");
                                    zip.AddFile(subdirectorio + OrdenCompra + ".pdf");
                                    zip.Save(subdirectorio + OrdenCompra + ".zip");

                                    string fileName = subdirectorio + OrdenCompra + ".zip", myStringWebResource = null;
                                    using (System.Net.WebClient myWebClient = new System.Net.WebClient())
                                    {
                                        myStringWebResource = remoteUri + OrdenCompra + ".xml";
                                        string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                        try
                                        {
                                            myWebClient.DownloadFile(myStringWebResource, escritorio + @"\" + OrdenCompra + ".xml");
                                        }
                                        catch
                                        {
                                            myWebClient.DownloadFile(myStringWebResource, @"C:\Temp" + @"\" + OrdenCompra + ".xml");
                                        }
                                    }
                                    //Download(subdirectorio + OrdenCompra + ".zip");
                                    System.IO.File.Delete(subdirectorio + OrdenCompra + ".xml");
                                    System.IO.File.Delete(subdirectorio + OrdenCompra + ".pdf");
                                    resultado = true;
                                    #endregion Carga, empaqueta y descarga ZIP
                                    JavaScript(@"javascript:showSuccess('La factura relaciona a la Orden de Compra " + ordencompra + " ha sido descargada en la ubicación de C:\\Temp');");
                                    #endregion Una sola Orden
                                }

                            }
                            else
                            {
                                #region un Bloque de XML
                                List<string> listaDirectorios = new List<string>();
                                string _tiempo = "";
                                System.IO.DirectoryInfo DIR = new System.IO.DirectoryInfo(directorio);
                                if (!DIR.Exists)
                                {
                                    System.IO.Directory.CreateDirectory(directorio);
                                }
                                #region descarga Archivos
                                DataTable dt = new DataTable();
                                string porProveedor = RFC.Length > 0 == true ? " RFC  ='" + RFC + "'" : "";
                                string filtros = porProveedor;
                                DataRow[] salida = table.Select(filtros);
                                string cuantos = table.Rows.Count.ToString();
                                var listaRFCs = salida.GroupBy(test => test["RFC"].ToString()).Select(grp => grp.First()).ToList();
                                string subdirectorio = "";
                                foreach (DataRow rfcs in listaRFCs)
                                {
                                    subdirectorio = directorio + rfcs[4].ToString(); ;// @"\" + rfc[4].ToString();
                                    System.IO.DirectoryInfo subDIR = new System.IO.DirectoryInfo(subdirectorio);
                                    if (!subDIR.Exists)
                                    {
                                        System.IO.Directory.CreateDirectory(subdirectorio);
                                    }
                                    var XMLrelacionados = from subXMLS in salida where subXMLS[4].ToString().Equals(rfcs[4].ToString()) select subXMLS;

                                    foreach (DataRow renglon in XMLrelacionados)
                                    {
                                        try
                                        {
                                            DateTime tiempo = new DateTime();
                                            if (renglon[1].ToString().Contains("-") == false)
                                                tiempo = DateTime.ParseExact(renglon[1].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                            else
                                                tiempo = DateTime.ParseExact(renglon[1].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                            string _año = tiempo.Year.ToString();
                                            string _mes = tiempo.Month.ToString();
                                            string _ArchivoXML = renglon[0].ToString();
                                            string _Fecha = renglon[1].ToString();
                                            string _OrdenCompra = renglon[2].ToString();
                                            string _NumRecepcion = renglon[3].ToString();
                                            string _RFC = renglon[4].ToString();
                                            string _Serie = renglon[5].ToString();
                                            string _UUID = renglon[6].ToString();
                                            string _Estatus = renglon[7].ToString().Equals("False") == true ? "Rechazada_" : "Aceptada_";
                                            _Fecha = _Fecha.Replace("/", "_");
                                            _Fecha = _Fecha.Replace("_", "_");
                                            #region Calculo de Mes
                                            switch (tiempo.Month)
                                            {
                                                case 1:
                                                    _mes = "Ene";
                                                    break;
                                                case 2:
                                                    _mes = "Feb";
                                                    break;
                                                case 3:
                                                    _mes = "Mar";
                                                    break;
                                                case 4:
                                                    _mes = "Abr";
                                                    break;
                                                case 5:
                                                    _mes = "May";
                                                    break;
                                                case 6:
                                                    _mes = "Jun";
                                                    break;
                                                case 7:
                                                    _mes = "Jul";
                                                    break;
                                                case 8:
                                                    _mes = "Ago";
                                                    break;
                                                case 9:
                                                    _mes = "Sep";
                                                    break;
                                                case 10:
                                                    _mes = "Oct";
                                                    break;
                                                case 11:
                                                    _mes = "Nov";
                                                    break;
                                                case 12:
                                                    _mes = "Dic";
                                                    break;
                                            }
                                            #endregion Calculo de Mes
                                            _tiempo = @"\";// +_año + @"\" + _mes + @"\";
                                            System.IO.DirectoryInfo subDIRTiempo = new System.IO.DirectoryInfo(subdirectorio + _tiempo);
                                            if (!subDIRTiempo.Exists)
                                            {
                                                System.IO.Directory.CreateDirectory(subdirectorio + _tiempo);
                                            }
                                            if (_ArchivoXML.Equals("No proporcionado") == false)
                                            {
                                                string ruta = subdirectorio + _tiempo + _Estatus + _OrdenCompra + "_" + _NumRecepcion + "_" + _Fecha + "_" + _RFC + ".xml";
                                                string _ruta = subdirectorio + _tiempo;
                                                if (!System.IO.File.Exists(ruta))
                                                {
                                                    #region Guarda el XML en Temporal
                                                    // guardo el xml fisicamente
                                                    if (!System.IO.File.Exists(ruta))
                                                    {
                                                        try
                                                        {
                                                            //subdirectorio + OrdenCompra + ".xml"
                                                            FileStream stream;
                                                            stream = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.Write);
                                                            StreamWriter writer = new StreamWriter(stream);
                                                            writer.Write(_ArchivoXML);
                                                            writer.Close();
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            string _error = ex.Message;
                                                        }
                                                    }
                                                    #endregion Guarda el XML en Temporal
                                                    conviertePDF(xdoc, _OrdenCompra, _ruta, _ArchivoXML);
                                                    #region Carga, empaqueta y descarga ZIP
                                                    zip.AddFile(ruta);
                                                    zip.AddFile(_ruta + _OrdenCompra + ".pdf");//subdirectorio + OrdenCompra + ".pdf");
                                                    zip.Save(subdirectorio + ".zip");// + OrdenCompra + ".zip");
                                                    //Download(subdirectorio + ".zip");//+ OrdenCompra + ".zip");
                                                    System.IO.File.Delete(ruta);//subdirectorio + OrdenCompra + ".xml");
                                                    System.IO.File.Delete(_ruta + _OrdenCompra + ".pdf");//subdirectorio + OrdenCompra + ".pdf");
                                                    #endregion Carga, empaqueta y descarga ZIP
                                                }
                                                _cuantos++;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            string _ex = ex.Message;
                                        }
                                    }
                                    //zip.Save(subdirectorio+ ".zip");
                                    listaDirectorios.Add(subdirectorio + ".zip");
                                    //Download(subdirectorio + ".zip");
                                    Directory.Delete(subdirectorio + _tiempo, true);
                                    resultado = true;
                                }
                                try
                                {
                                    //lbl_cuantos_descargados.Text = "Se descargaron " + _cuantos + " archivos XML exitosamente en la ruta " + subdirectorio + ". Recuerde que es un archivo .ZIP y debe descomprimirlo antes de consultar sus documentos.";

                                    foreach (string _directorio in listaDirectorios)
                                    {
                                        //Download(_directorio);
                                        //string remoteUri = subdirectorio;
                                        //string fileName = subdirectorio + ".zip", myStringWebResource = null;
                                        //using (System.Net.WebClient myWebClient = new System.Net.WebClient())
                                        //{
                                        //    myStringWebResource = remoteUri  + ".zip";
                                        //    string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                        //    myWebClient.DownloadFile(myStringWebResource, escritorio + @"\" + RFC + ".zip");
                                        //}
                                        //lbl_cuantos_descargados.Visible = true;
                                        //lbl_cuantos_descargados.Text = "Se descargaron " + _cuantos + " archivos XML exitosamente en su escritorio' ";
                                        //lbl_cuantos_descargados.DataBind();
                                        //string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                        //string[] arreglo = new string[_directorio.Length];
                                        //if (_directorio.Contains(@"\") == false)
                                        //    arreglo = _directorio.Split('\\');
                                        //else
                                        //    arreglo = _directorio.Split('*');
                                        //Response.Clear();
                                        //Response.ContentType = "application/octet-stream";
                                        //Response.AddHeader("Content-Disposition", "attachment; filename=" + arreglo[arreglo.Length - 1].ToString());
                                        //Response.WriteFile(escritorio + @"\" + RFC + ".zip");
                                        //Response.Flush();
                                        //Response.End();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string _ex = ex.Message;
                                    //Notificacion(ex.Message, "Error", "warning");
                                }
                                //lbl_cuantos_descargados.Text = "Se descargaron " + _cuantos + " archivos XML exitosamente en su escritorio' ";

                                #endregion descarga Archivos
                                #endregion un Bloque de XML
                            }
                        }
                }
                catch (Exception _ex)
                {
                    resultado = false;
                    JavaScript("javascript:showSuccess('La factura relaciona a la Orden de Compra " + ordencompra + " no fue localizada en el Portal,ocurrió el error "+ _ex.Message +" ');");
                }

            }
            else
            {
                //Notificacion("Especifique un RFC del cual descargar los XML del periodo.", "Falta RFC", "deny");
            }
            //return Json(null, JsonRequestBehavior.AllowGet);
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void conviertePDF(System.Xml.XmlDocument xdoc, string OrdenCompra, string ruta, string XML)
        {
            //string subdirectorio = System.Configuration.ConfigurationManager.AppSettings["pathInvoice"] == null ? @"C:\Temp\" : System.Configuration.ConfigurationManager.AppSettings["pathInvoice"]; //directorio + @"\XML varios\";
            string subdirectorio = ruta;
            //System.IO.DirectoryInfo DIR = new System.IO.DirectoryInfo(subdirectorio);
            //if (!DIR.Exists)
            //{
            //    System.IO.Directory.CreateDirectory(subdirectorio);
            //}
            #region Guarda el XML en Temporal
            #region descarga PDF
            //plantillas.SiteControllerService clientePlantillas = new plantillas.SiteControllerService();
            FileStream streamPDF;
            string pdfData = "";
            string xml64 = Base64Encode(XML);
            try
            {
                //Bajar XML
                //17 agosto hay que restablecer este servicio GRD
                //pdfData = clientePlantillas.generaPlantilla("3615", "NC", "2000", "S", xml64);
                pdfData = "";
            }
            catch (Exception e)
            {
                pdfData = "";
            }

            streamPDF = new FileStream(ruta + OrdenCompra + ".pdf", FileMode.OpenOrCreate, FileAccess.Write);
            System.IO.BinaryWriter writer = new BinaryWriter(streamPDF);

            byte[] bytes = Convert.FromBase64String(pdfData);
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();
            //}
            #endregion descarga PDF
            #endregion Guarda el XML en Temporal
        }

        //########################### NECESARIO PARA DESCARGAR ARCHIVOS #############################################

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        public static byte[] stream2Byte(string input)
        {
            FileStream sourceFile = new FileStream(input, FileMode.Open); //Open streamer
            BinaryReader binReader = new BinaryReader(sourceFile);
            byte[] output = new byte[sourceFile.Length]; //create byte array of size file
            for (long i = 0; i < sourceFile.Length; i++)
                output[i] = binReader.ReadByte(); //read until done
            sourceFile.Close(); //dispose streamer
            binReader.Close(); //dispose reader
            return output;
        }


        [AcceptVerbs(HttpVerbs.Get)]
        static public ActionResult Download(string patch)
        {
            System.IO.FileInfo toDownload = new System.IO.FileInfo(patch);
            //Configuracion con = new Configuracion();
            string rootpath = System.Web.HttpContext.Current.Server.MapPath("~/");
            patch = patch.Replace(rootpath, "");
            patch = patch.Replace("\\", "/");
            patch = "~/" + patch;
            return new MVC5_full_version.Genericos.DownloadResult
            {

                VirtualPath = patch,//GetVirtualPath(patch),//file.Path),
                FileDownloadName = toDownload.Name
            };
        }

        private string GetVirtualPath(string physicalPath)
        {
            string rootpath = System.Web.HttpContext.Current.Server.MapPath("~/");

            physicalPath = physicalPath.Replace(rootpath, "");
            physicalPath = physicalPath.Replace("\\", "/");

            return "~/" + physicalPath;
        }

    }
}
namespace datosEstatus
{
    public class Datos
    {
        public string estatus { get; set; }
        public string valor { get; set; }
        public string descripcion { get; set; }
    }
}