using ClickFactura_Entidades.BD.Entidades;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5_full_version.Controllers.reportesBitacoraWF
{
    public class reportesBitacoraWFController : Controller
    {
        // GET: reportesBitacoraWF
        ClickFactura_WebServiceCF.Service.Service1 cliente = new ClickFactura_WebServiceCF.Service.Service1();
        Desarrollo_CF contexto = new Desarrollo_CF();

        public string thisConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Desarrollo_CF"].ConnectionString;

        #region EventoAsincrono
        //Definición del evento de Asincronia----------------------------------------------
        //Invocación
        //Opcion = 1; InvocaEventoAsincrono(Opcion);

        public delegate void MiDelegado(Int16 opcion);
        Int16 _opcion = 0;
        public Int16 Opcion
        {
            get { return _opcion; }
            set { _opcion = value; }
        }

        public void TestDelegado(MiDelegado delegado)
        {
            delegado(Opcion);
        }
        public void InvocaEventoAsincrono(Int16 _opcion)
        {
            MiDelegado delegado = new MiDelegado(MetodoDelegado);
            TestDelegado(delegado);
        }

        public void MetodoDelegado(Int16 opcion)
        {
            if (opcion == 0)
            {
                VisorReporte();
            }
        }

        #endregion EventoAsincrono


        public ActionResult Index()
        {
             return View();
        }

        public ActionResult reportesBitacoraWF()
        {
            return View();
        }

        public ActionResult VisorReporte() //FileResult VisorReporte()
    {
        string RptPath = Server.MapPath("~/DataSet/Reportes/BitacorasWF/rptBitacoraWFGral.rdlc");
        Microsoft.Reporting.WebForms.LocalReport rpt = new Microsoft.Reporting.WebForms.LocalReport();

            /* Aplica Report Data Set */
            #region        Trae Información
            Microsoft.Reporting.WebForms.ReportDataSource Fuente = new Microsoft.Reporting.WebForms.ReportDataSource();
            string archivoRDLC = RptPath;// @"wucReportes\Internos\Administrador\rpt_facturasxDia.rdlc";
            string filtro = "";
            string estatus = "";
            //string fechaInicial = rdp_Inicial.SelectedDate.Value.ToString("yyyy-MM-dd");
            //string fechaFinal = rdp_Final.SelectedDate.Value.ToString("yyyy-MM-dd");
            //filtro = " WHERE        (CONVERT(VARCHAR(10), CONVERT(nvarchar, Dia , 106), 103) BETWEEN '" + fechaInicial + "' AND '" + fechaFinal + "') ";
            //if (rbl_Estatus.SelectedValue.Equals("Todas") == true)
            //{
            //    estatus = "";
            //}
            //else
            //{
            //    if (rbl_Estatus.SelectedValue.Equals("Paso") == true)
            //    {
            //        estatus = " And (Resultado='Paso')";
            //    }
            //    else
            //    {
            //        estatus = " And (Resultado='No paso!')";
            //    }
            //}
            string Comando = "Select  * from view_todoWorkflow " + filtro + estatus;
            string nombreDataSource = "DataSetReportes";
            string nombreTabla = "view_facturasxDia";
            //ad_Reportes reportes = new ad_Reportes();

            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = thisConnectionString;
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = Comando;
            DataTable dt = new DataTable();
            dt.TableName = nombreTabla;
            dt.Load(cmd.ExecuteReader());
            ds.DataSetName = nombreDataSource;
            ds.Tables.Add(dt);
            con.Close();
            Fuente.DataMember = nombreTabla;
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableHyperlinks = true;
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.Dispose();
            ReportViewer1.LocalReport.ReportPath = archivoRDLC;
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetReportes", ds.Tables[0]));
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            ReportViewer1.LocalReport.DisplayName = "Detalles sobrefacturas procesadas con BAFAR por Click Factura " + DateTime.Now.ToShortTimeString();
            ReportViewer1.LocalReport.Refresh();
            #endregion Trae Información


            rpt.ReportPath = RptPath;
        string filePath = System.IO.Path.GetTempFileName();
        Exportar(rpt, filePath);
        //Cerrar REPORT OBJECTO           
        rpt.Dispose();
            //return File(filePath, "application/pdf");
        return Json(filePath);//, "application/pdf");
        }

    public string Exportar(LocalReport rpt, string filePath)
    {
        string ack = "";
        try
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = rpt.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            using (System.IO.FileStream stream = System.IO.File.OpenWrite(filePath))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            return ack;
        }
        catch (Exception ex)
        {
            ack = ex.InnerException.Message;
            return ack;
        }
    }

        public string Download(string file)
        {

            string filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["FileManagementPath"]);


            string actualFilePath = System.IO.Path.Combine(filePath, file);
            HttpContext.Response.ContentType = "APPLICATION/OCTET-STREAM";
            string filename = "Descargado";// Path.GetFileName(actualFilePath);
            String Header = "Attachment; Filename=" + filename;
            HttpContext.Response.AppendHeader("Content-Disposition", Header);
            HttpContext.Response.WriteFile(actualFilePath);
            HttpContext.Response.End();
            return "";
        }

        [HttpPost]
        public JsonResult CargaResultados(string numproveedor, string sociedad, string ordencompra, string tipo, string usuario,string uuid, string moneda, string _finicial, string ffinal)
        {
            List<KeyValuePair<string, string>> respuesta = new List<KeyValuePair<string, string>>();

            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult GetSociedades(bool esAdmon,string numproveedor)
        {
            List<KeyValuePair<string, string>> respuesta = new List<KeyValuePair<string, string>>();
            if(esAdmon==true)
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

        [HttpPost, ActionName("GetTiposUsuario")]
        [ValidateAntiForgeryToken]
        public ActionResult GetTiposUsuario()
        {
            List<KeyValuePair<string, string>> respuesta = new List<KeyValuePair<string, string>>();
            List<Cat_TipoUsuarioWF> cat_TipoUsuarioWF = contexto.Cat_TipoUsuarioWF.ToList();
            foreach (Cat_TipoUsuarioWF wf in cat_TipoUsuarioWF)
            {
                string key = wf.idTipoUsuarioWF.ToString();
                string value = wf.nombreRol.ToString();
                respuesta.Add(new KeyValuePair<string, string>(key, value));
            }
            return Json(respuesta);
        }

        [HttpPost, ActionName("GetUsuariosWF")]
        [ValidateAntiForgeryToken]
        public ActionResult GetUsuariosWF()
        {
            List<KeyValuePair<string, string>> respuesta = new List<KeyValuePair<string, string>>();
            List<Cat_UsuariosWF> cat_UsuarioWF = contexto.Cat_UsuariosWF.ToList();
            foreach (Cat_UsuariosWF wf in cat_UsuarioWF)
            {
                string key = wf.idUsuarioWF.ToString();
                string value = wf.nombre.ToString();
                respuesta.Add(new KeyValuePair<string, string>(key, value));
            }
            return Json(respuesta);
        }

    }
}