using ClickFactura_Entidades.BD.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
//using BAFAR.Clases.Genericos;
using System.Web.UI;

namespace MVC5_full_version.Controllers.Monitoreo
{
    public class MonitoreoController : Controller
    {

        //Models.DatosBafarDataContext contexto = new Models.DatosBafarDataContext();
        ClickFactura_WebServiceCF.Service.Service1 cliente = new ClickFactura_WebServiceCF.Service.Service1();
        Desarrollo_CF contexto=new Desarrollo_CF();
        public ActionResult FacturasVerificadas()
        {
            return View();
        }
        public ActionResult EstadoCuenta()
        {
            //string quienEntro = (string)Session["Usuario"];
            //Configuracion con = new Configuracion();
            //Session["NivelAdministradorBafar"] = con.EsAdministradorBafar(quienEntro);
            //Models.EstadodeCuenta estadodecuenta = new Models.EstadodeCuenta();

            //#region Interfas en versión liberada
            //int result = 0;
            //BAFAR.Clases.Genericos.adT_Parametros adp = new BAFAR.Clases.Genericos.adT_Parametros();
            //List<BAFAR.Clases.Genericos.objT_Parametros> objp = new List<BAFAR.Clases.Genericos.objT_Parametros>();
            //objp = adp.mABCT_Parametros(out result, 0, "tipoVersion", "Vacio", true, "ConsultaValor");
            //{
            //    string modo = objp[0].ValorParametro.ToString();
            //    if (modo.Equals("Estable") == false)
            //    {
            //        string actualUrl = Request.RawUrl;
            //        actualUrl = actualUrl.Substring(1, (actualUrl.Length - 1));
            //        System.Data.DataTable t = new System.Data.DataTable();
            //        Dictionary<string, string> parametros = new Dictionary<string, string>();
            //        parametros.Add("interfas", actualUrl);
            //        string tabla = "SP_estatusMenuVersiones";
            //        string P_Opcion = "Estatus";
            //        System.Data.DataTable table = new System.Data.DataTable();
            //        BAFAR.Clases.Genericos.Configuracion c = new BAFAR.Clases.Genericos.Configuracion();
            //        try
            //        {
            //            table = c.executaSP_Generico(parametros, P_Opcion, tabla);
            //            if (table != null)
            //                if (table.Rows.Count > 0)
            //                {
            //                    t = table;
            //                    bool activo = Convert.ToBoolean(t.Rows[0].ItemArray[0]) != null ? Convert.ToBoolean(t.Rows[0].ItemArray[0]) : false;
            //                    if (activo == false)
            //                    {
            //                        return RedirectToAction(@"../Pages/Error404");
            //                    }
            //                }
            //        }
            //        catch
            //        {

            //        }
            //    }
            //}
            //#endregion Interfas en versión liberada

            //return View(estadodecuenta);
            return View();
        }

        [HttpPost]
        public JsonResult GetSociedades(string Num_Proveedor)
        {
            List<ClickFactura_Entidades.BD.Entidades.Cat_Cia> Sociedades = new List<ClickFactura_Entidades.BD.Entidades.Cat_Cia>();
            //var s = from soc in contexto.Cat_Cia select soc;
            //bool queEs;
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
            //    List<Models.view_Sociedades_a_las_que_puedo_Facturar> resp = new List<Models.view_Sociedades_a_las_que_puedo_Facturar>();
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
            compañia.Num_Sociedad = "6000";
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
        public JsonResult GetDocumentos()
        {
            //Models.Cat_TipoDoc = new Models.Cat_TipoDoc();
            var sd = from td in contexto.Cat_TipoDoc select td;
            return Json(sd);
        }

        //[HttpPost]
        //public JsonResult CargaResultadosCargaCarteraVencida(string numproveedor, string finicial, string ffinal, string tipodoc, string sociedad)
        //{
        //    #region opcion2
        //    // C   A   R   T   E  R  A
        //    wsBafar.Servicio_ClickFacturaClient cliente = new wsBafar.Servicio_ClickFacturaClient();
        //    cliente.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
        //    System.Data.DataSet ds = new System.Data.DataSet();

        //    wsBafar.FiltroGeneric _obj = new wsBafar.FiltroGeneric();
        //    wsBafar.ModelGeneric[] _Obj = new wsBafar.ModelGeneric[0];
        //    _obj.P1 = "PARABI";
        //    //_obj.P2 = "01.06.2015";
        //    //_obj.P3 = "05.06.2015";
        //    _obj.P4 = numproveedor;// "100330";
        //    _obj.P5 = sociedad; //"2000";

        //    #region Tipo de Documento
        //    //List<string> tipos = new List<string>();
        //    //if (Documentos.Items != null)
        //    //{
        //    //    if (Documentos.Items.Count > 0)
        //    //    {
        //    //        if (Documentos.SelectedValue.Equals("0000") == false)
        //    //        {
        //    //            foreach (ListItem item in Documentos.Items)
        //    //            {
        //    //                if (ddl_tipoDocto.SelectedValue == item.Text)
        //    //                    tipos.Add(item.Text);
        //    //            }
        //    //        }
        //    //        else
        //    //        {
        //    //            foreach (ListItem item in Documentos.Items)
        //    //            {
        //    //                if (item.Text.Contains("Seleccione") == false)
        //    //                    tipos.Add(item.Text);
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //string _tipos = "";
        //    //int cuantos = 1;
        //    //int total = tipos.Count();
        //    //if (total > 1)
        //    //{
        //    //    foreach (string l in tipos)
        //    //    {
        //    //        if (cuantos == 1)
        //    //            _tipos = l + ",";
        //    //        else
        //    //        {
        //    //            if (cuantos < total)
        //    //                _tipos = _tipos + l + ",";
        //    //            else
        //    //                _tipos = _tipos + l;
        //    //        }
        //    //        cuantos++;
        //    //    }
        //    //    int pos = _tipos.LastIndexOf(",");
        //    //    int largo = _tipos.Length - 1;
        //    //    if (pos == largo)
        //    //        _tipos = _tipos.Substring(largo, 1);
        //    //}
        //    //else
        //    //{
        //    //    _tipos = (string)tipos[0];
        //    //}
        //    #endregion Tipo de Documento
        //    if (tipodoc.Equals("0") == true)
        //        _obj.C1 = "YR,KZ,RE";
        //    else
        //    {
        //        switch (Convert.ToInt16(tipodoc))
        //        {
        //            case 1: tipodoc = "YR";
        //                break;
        //            case 2: tipodoc = "KZ";
        //                break;
        //            case 3: tipodoc = "KR";
        //                break;
        //        }
        //        _obj.C1 = tipodoc;
        //    }
        //    //_tipos;
        //    //_obj.C1 = "YR,KZ,RE";

        //    wsBafar.Estado_Cuenta_FI datos = new wsBafar.Estado_Cuenta_FI();
        //    Clases.Genericos.Configuracion c = new Clases.Genericos.Configuracion();
        //    System.Data.DataTable respuesta = c.EstructuraDocumentos("Cartera");
        //    var L_Obj = cliente.MetodoGeneric(_obj);
        //    int registros = L_Obj.Count() - 1;
        //    wsBafar.ModelGeneric[] Registros = new wsBafar.ModelGeneric[registros];
        //    List<Models.CarteraVencida> resp = new List<Models.CarteraVencida>();
        //    Registros = L_Obj;
        //    if (Registros.Count() > 0)
        //    {
        //        foreach (wsBafar.ModelGeneric f in Registros)
        //        {
        //            try
        //            {
        //                if (f.R1.Equals("E") == false)
        //                {
        //                    #region Se encontraron datos válidos
        //                    string r1 = f.R1 != null ? f.R1.ToString() : "";
        //                    string r2 = f.R2 != null ? f.R2.ToString() : "";
        //                    string r3 = f.R3 != null ? f.R3.ToString() : "";
        //                    string r4 = f.R4 != null ? f.R4.ToString() : "";
        //                    string r5 = f.R5 != null ? f.R5.ToString() : "";
        //                    string r6 = f.R6 != null ? f.R6.ToString() : "";
        //                    string r7 = f.R7 != null ? f.R7.ToString() : "";
        //                    string r8 = f.R8 != null ? f.R8.ToString() : "";
        //                    string r9 = f.R9 != null ? "$" + f.R9.ToString() : "";
        //                    string r10 = f.R10 != null ? "$" + f.R10.ToString() : "";
        //                    string r11 = f.R11 != null ? f.R11.ToString() : "";
        //                    string r12 = f.R12 != null ? "$" + f.R12.ToString() : "";
        //                    string r13 = f.R13 != null ? f.R13.ToString() : "";
        //                    string r14 = f.R14 != null ? f.R14.ToString() : "";
        //                    string r15 = f.R15 != null ? f.R15.ToString() : "";
        //                    respuesta.Rows.Add(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15);
        //                    Models.CarteraVencida cv = new Models.CarteraVencida();
        //                    cv.R1 = r1;
        //                    cv.R2 = r2;
        //                    cv.R3 = r3;
        //                    cv.R4 = r4;
        //                    cv.R5 = r5;
        //                    cv.R6 = r6;
        //                    cv.R7 = r7;
        //                    cv.R8 = r8;
        //                    cv.R9 = r9;
        //                    cv.R10 = r10;
        //                    cv.R11 = r11;
        //                    cv.R12 = r12;
        //                    cv.R13 = r13;
        //                    cv.R14 = r14;
        //                    cv.R15 = r15;
        //                    resp.Add(cv);
        //                    #endregion Se encontraron datos válidos
        //                }

        //            }
        //            catch
        //            {
        //                //Notificacion("Tenemos un  problema recuperando su información, intente de nuevo por favor.", "Ocurrío un problema", "warning");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //Notificacion("No se encontro información disponible con los filtros proporcionados, verifíque por favor.", "No se encontro información", "info");
        //    }
        //    #endregion opcion2
        //    return Json(resp, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult CargaResultadosCargaEstadoCuenta(string numproveedor, string finicial, string ffinal, string tipodoc, string sociedad, bool aplicarFechas)
        //{
        //    #region opcion2
        //    // E S T A D O  C U E N T A
        //    wsBafar.Servicio_ClickFacturaClient cliente = new wsBafar.Servicio_ClickFacturaClient();
        //    cliente.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
        //    System.Data.DataSet ds = new System.Data.DataSet();

        //    #region Fechas
        //    if (aplicarFechas == false)
        //    {
        //        Configuracion con = new Configuracion();
        //        finicial = con.primerDiaMes().Date.ToString();
        //        ffinal = DateTime.Now.Date.ToString();
        //    }
        //    #endregion Fechas

        //    wsBafar.FiltroGeneric _obj = new wsBafar.FiltroGeneric();
        //    wsBafar.ModelGeneric[] _Obj = new wsBafar.ModelGeneric[0];
        //    _obj.P1 = "PARCOM";
        //    _obj.P2 = convierteFecha(finicial);
        //    _obj.P3 = convierteFecha(ffinal);// "05.06.2015";
        //    _obj.P4 = numproveedor;// "100330";
        //    _obj.P5 = sociedad; //"2000";

        //    #region Tipo de Documento
        //    if (tipodoc.Equals("0") == true)
        //        _obj.C1 = "YR,KZ,RE";
        //    else
        //    {
        //        switch (Convert.ToInt16(tipodoc))
        //        {
        //            case 1: tipodoc = "YR";
        //                break;
        //            case 2: tipodoc = "KZ";
        //                break;
        //            case 3: tipodoc = "KR";
        //                break;
        //        }
        //        _obj.C1 = tipodoc;
        //    }
        //    #endregion Tipo de Documento
        //    //_tipos;
        //    //_obj.C1 = "YR,KZ,RE";

        //    wsBafar.Estado_Cuenta_FI datos = new wsBafar.Estado_Cuenta_FI();
        //    Clases.Genericos.Configuracion c = new Clases.Genericos.Configuracion();
        //    System.Data.DataTable respuesta = c.EstructuraDocumentos("EstadodeCuenta");
        //    var L_Obj = cliente.MetodoGeneric(_obj);
        //    int registros = L_Obj.Count() - 1;
        //    wsBafar.ModelGeneric[] Registros = new wsBafar.ModelGeneric[registros];
        //    List<Models.EstadodeCuenta> resp = new List<Models.EstadodeCuenta>();
        //    Registros = L_Obj;
        //    if (Registros.Count() > 0)
        //    {
        //        foreach (wsBafar.ModelGeneric f in Registros)
        //        {
        //            try
        //            {
        //                if (f.R1.Equals("E") == false)
        //                {
        //                    #region Se encontraron datos válidos
        //                    string r1 = f.R1 != null ? f.R1.ToString() : "";
        //                    string r2 = f.R2 != null ? f.R2.ToString() : "";
        //                    string r3 = f.R3 != null ? f.R3.ToString() : "";
        //                    string r4 = f.R4 != null ? f.R4.ToString() : "";
        //                    string r5 = f.R5 != null ? f.R5.ToString() : "";
        //                    string r6 = f.R6 != null ? f.R6.ToString() : "";
        //                    string r7 = f.R7 != null ? f.R7.ToString() : "";
        //                    string r8 = f.R8 != null ? f.R8.ToString() : "";
        //                    string r9 = f.R9 != null ? "$" + f.R9.ToString() : "";
        //                    string r10 = f.R10 != null ? "$" + f.R10.ToString() : "";
        //                    string r11 = f.R11 != null ? f.R11.ToString() : "";
        //                    string r12 = f.R12 != null ? "$" + f.R12.ToString() : "";
        //                    string r13 = f.R13 != null ? f.R13.ToString() : "";
        //                    string r14 = f.R14 != null ? f.R14.ToString() : "";
        //                    string r15 = f.R15 != null ? f.R15.ToString() : "";
        //                    string r16 = f.R16 != null ? f.R16.ToString() : "";
        //                    string r17 = f.R17 != null ? f.R17.ToString() : "";
        //                    respuesta.Rows.Add(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17);
        //                    Models.EstadodeCuenta cv = new Models.EstadodeCuenta();
        //                    cv.R1 = r1;
        //                    cv.R2 = r2;
        //                    cv.R3 = r3;
        //                    cv.R4 = r4;
        //                    cv.R5 = r5;
        //                    cv.R6 = r6;
        //                    cv.R7 = r7;
        //                    cv.R8 = r8;
        //                    cv.R9 = r9;
        //                    cv.R10 = r10;
        //                    cv.R11 = r11;
        //                    cv.R12 = r12;
        //                    cv.R13 = r13;
        //                    cv.R14 = r14;
        //                    cv.R15 = r15;
        //                    cv.R16 = r16;
        //                    cv.R17 = r17;
        //                    resp.Add(cv);
        //                    #endregion Se encontraron datos válidos
        //                }
        //            }
        //            catch
        //            {
        //                //Notificacion("Tenemos un  problema recuperando su información, intente de nuevo por favor.", "Ocurrío un problema", "warning");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //Notificacion("No se encontro información disponible con los filtros proporcionados, verifíque por favor.", "No se encontro información", "info");
        //    }
        //    #endregion opcion2
        //    return Json(resp, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult cargaTabla(List<Models.CarteraVencida> lista)
        //{
        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult cargaTablaEstadodeCuenta(List<Models.EstadodeCuenta> lista)
        //{
        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult CarteraVencida()
        //{
        //    Models.CarteraVencida carteravencida = new Models.CarteraVencida();
        //    return View(carteravencida);
        //}
        public ActionResult BandejaEntrada()
        {
            return View();
        }

        /// <summary>
        /// Excel_Export_Save: metodo de exportación del grid y contenido hacia Excel
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="base64"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public string GetSession()
        {
            string num = "";
            string usuario = System.Web.HttpContext.Current.Session["Usuario"] as string;
            bool admin = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(usuario);
            if (admin== false)
            {
                num = (string)Session["Num_Proveedor"];
            }
            else
            {
                num = "0";
            }
            return num;
        }
        private string convierteFecha(string fechaoriginal)
        {
            string salida = "";
            string _fechaoriginal = fechaoriginal;
            string dia = "";
            string mes = "";
            string año = "";
            int largo = 0;
            int pos = 0;
            try
            {
                largo = fechaoriginal.Length;
                pos = fechaoriginal.IndexOf("/");
                dia = fechaoriginal.Substring(0, pos);
                fechaoriginal = fechaoriginal.Remove(0, 3);
                pos = fechaoriginal.IndexOf("/");
                mes = fechaoriginal.Substring(0, pos);
                fechaoriginal = fechaoriginal.Remove(0, 3);
                año = fechaoriginal.Substring(0, 4);
                salida = dia + "." + mes + "." + año;
            }
            catch
            {

            }
            //salida = "2015-09-07 12:00:00 AM";
            //if(_fechaoriginal.Contains("12:00:00 AM")==true)
            if (salida.Equals("") == true)
            {
                //2015-09-07 12:00:00 AM
                largo = _fechaoriginal.Length;
                pos = _fechaoriginal.IndexOf("-");
                año = _fechaoriginal.Substring(0, pos);
                _fechaoriginal = _fechaoriginal.Remove(0, 5);
                pos = _fechaoriginal.IndexOf("-");
                mes = _fechaoriginal.Substring(0, pos);
                _fechaoriginal = _fechaoriginal.Remove(0, 3);
                dia = _fechaoriginal.Substring(0, 2);
                salida = dia + "." + mes + "." + año;
            }
            return salida;
        }

        [HttpPost]
        public JsonResult esAdmon()
        {
            bool admin = false;
            string usuario = System.Web.HttpContext.Current.Session["Usuario"] as string;
            admin = ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(usuario);
            //admin = Convert.ToBoolean(Session["NivelAdministradorBafar"]);
            string proveedor = "error";
            if (admin == false)
            {
                proveedor = Session["Num_Proveedor"].ToString();
            }
            return Json(proveedor, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public List<Models.view_Sociedades_a_las_que_puedo_Facturar> ObtenerSociedades(string usuario)
        //{
        //    List<Models.view_Sociedades_a_las_que_puedo_Facturar> resp = new List<Models.view_Sociedades_a_las_que_puedo_Facturar>();
        //    System.Data.DataTable t = new System.Data.DataTable();
        //    System.Data.DataTable table = new System.Data.DataTable();
        //    if (usuario.Length > 0)
        //    {
        //        Dictionary<string, string> parametros = new Dictionary<string, string>();
        //        parametros.Add("Usuario", usuario);
        //        string tabla = "obtenSociedadesxUsuario";
        //        string P_Opcion = "todasSociedadesxUsuario";
        //        BAFAR.Clases.Genericos.Configuracion c = new BAFAR.Clases.Genericos.Configuracion();
        //        try
        //        {
        //            table = c.executaSP_Generico(parametros, P_Opcion, tabla);
        //            if (table != null)
        //            {
        //                if (table.Rows.Count > 0)
        //                {
        //                    #region Preparando respuesta en tabla
        //                    if (table.Rows.Count > 0)
        //                    {
        //                        foreach (System.Data.DataRow reg in table.Rows)
        //                        {
        //                            Models.view_Sociedades_a_las_que_puedo_Facturar cv = new Models.view_Sociedades_a_las_que_puedo_Facturar();
        //                            cv.Num_Sociedad = reg.ItemArray[0].ToString();
        //                            cv.Compania = reg.ItemArray[1].ToString();
        //                            cv.RFC = reg.ItemArray[2].ToString();
        //                            cv.Num_Proveedor = reg.ItemArray[3].ToString(); ;
        //                            resp.Add(cv);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Models.view_Sociedades_a_las_que_puedo_Facturar cv = new Models.view_Sociedades_a_las_que_puedo_Facturar();
        //                        cv.Num_Sociedad = "0";
        //                        cv.Compania = "Empresa/Usuario sin Sociedades";
        //                        cv.RFC = "0";
        //                        cv.Num_Proveedor = "0";
        //                        resp.Add(cv);
        //                    }
        //                    #endregion Preparando respuesta en tabla
        //                }
        //            }
        //            else
        //            {
        //                Models.view_Sociedades_a_las_que_puedo_Facturar cv = new Models.view_Sociedades_a_las_que_puedo_Facturar();
        //                cv.Num_Sociedad = "0";
        //                cv.Compania = "Empresa/Usuario sin Sociedades";
        //                cv.RFC = "0";
        //                cv.Num_Proveedor = "0";
        //                resp.Add(cv);
        //            }
        //        }
        //        catch
        //        {

        //        }
        //    }
        //    return resp;
        //}



    }
}