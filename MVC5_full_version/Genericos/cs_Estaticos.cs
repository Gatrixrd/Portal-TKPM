using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Xml;
using context = System.Web.HttpContext;

namespace MVC5_full_version.Genericos
{
    public class cs_Estaticos
    {
        public static string GetCurrentWebsiteRoot()
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        }

        private static String ErrorlineNo, Errormsg, extype, exurl, hostIp, ErrorLocation, HostAdd;

        public static void grabaError( int tipo,Exception ex,string comentario, string proceso)
        {
            
            if (tipo == 0)
            {
                var line = Environment.NewLine + Environment.NewLine;

                ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                Errormsg = ex.GetType().Name.ToString();
                extype = ex.GetType().ToString();
                exurl = context.Current.Request.Url.ToString();
                ErrorLocation = ex.Message.ToString();
            }
            try
            {
                string filepath = Path.Combine(@"C:\Temp\LogsPortal", proceso);// context.Current.Server.MapPath(@"C:\Temp");// ExceptionDetailsFile/");  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + @"\" + DateTime.Today.ToString("dd-MM-yy") + "_" + proceso  + ".txt"; //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    int line = 0;
                    sw.WriteLine("-----------------------------------------------------------------------------------------");
                    sw.WriteLine("---------------------------      CLICK FACTURA     ----------------------------------");
                    string error = "LOG escrito el día:" + " " + DateTime.Now.ToString() + " para " + proceso;
                    sw.WriteLine(error);
                    sw.WriteLine(" ");
                    sw.WriteLine("Excepciones ocurridas el " + " " + DateTime.Now.ToString());
                    sw.WriteLine(" ");
                    sw.WriteLine("                                         TABLA ERRORES");
                    sw.WriteLine(" ");
                   sw.WriteLine(line.ToString() + " :  " + comentario);
                    sw.WriteLine(ErrorLocation);
                    sw.WriteLine(" ");
                    sw.WriteLine("------------------------------------------------------------------------------------------");
                    sw.WriteLine("--------------------------------*   FIN    *---------------------------------------------");
                    sw.WriteLine("------------------------------------------------------------------------------------------");
                    sw.Flush();
                    sw.Close();
                }

            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public static void SendErrorToText(Exception ex, List<string> errores, int tipo, string origen, string oc, string proceso)
        {
            if (tipo == 0)
            {
                var line = Environment.NewLine + Environment.NewLine;

                ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                Errormsg = ex.GetType().Name.ToString();
                extype = ex.GetType().ToString();
                exurl = context.Current.Request.Url.ToString();
                ErrorLocation = ex.Message.ToString();
            }
            try
            {
                string filepath = Path.Combine(@"C:\Temp\LogsPortal", proceso + @"\" + oc);// context.Current.Server.MapPath(@"C:\Temp");// ExceptionDetailsFile/");  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + @"\" + DateTime.Today.ToString("dd-MM-yy") + "_" + proceso + "_" + oc + ".txt"; //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    int line = 0;
                    sw.WriteLine("---------------------------------------------------------------------------------------");
                    sw.WriteLine("---------------------------      CLICK FACTURA     ----------------------------------");
                    string error = "LOG escrito el día:" + " " + DateTime.Now.ToString() + " para " + origen;// + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line + "User Host IP:" + " " + hostIp + line;
                    sw.WriteLine(error);
                    sw.WriteLine(" ");
                    sw.WriteLine("Excepciones ocurridas el " + " " + DateTime.Now.ToString());
                    sw.WriteLine(" ");
                    sw.WriteLine("                                         TABLA RETURN SAP");
                    sw.WriteLine(" ");
                    foreach (string err in errores)
                    {
                        if (err.Contains("Error") == true)
                        {
                            sw.WriteLine(line.ToString() + " :  " + err);
                            line++;
                        }
                    }
                    sw.WriteLine(" ");
                    sw.WriteLine("                                        Mensajes Portal");
                    foreach (string err in errores)
                    {
                        if (err.Contains("ERROR") == true)
                        {
                            sw.WriteLine(line.ToString() + " :  " + err);
                            line++;
                        }
                    }
                    sw.WriteLine(" ");
                    sw.WriteLine("                                     Informacion procesada de la OC " + oc);
                    foreach (string err in errores)
                    {
                        if (err.Contains("Data") == true)
                        {
                            sw.WriteLine(line.ToString() + " :  " + err);
                            line++;
                        }
                    }
                    sw.WriteLine("---------------------------------------------------------------------------------------");
                    sw.WriteLine("--------------------------------*   FIN    *------------------------------------------");
                    sw.WriteLine("---------------------------------------------------------------------------------------");
                    sw.Flush();
                    sw.Close();
                }

            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        public static void EscribeLOG(List<string> errores, string origenes, string oc, string proceso)
        {
            SendErrorToText(null, errores, 1, origenes, oc, proceso);
        }

        public static void trazaIssue(string proceso,string valor)
        {
                   try
                    {
                        List<string> errores = new List<string>();
                        string origenes = proceso+ "_" + "validando";
                        errores.Add("Trazando " + "- " + valor);
                        EscribeLOG(errores, origenes, proceso, "Trazando_Issue");
                    }
                    catch(Exception)
                    {

                    }
        }


        public static String muestraXML(String XML)
        {
            String Result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.UTF8);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(XML);

                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                String FormattedXML = sReader.ReadToEnd();

                Result = FormattedXML;
            }
            catch (XmlException)
            {
            }

            mStream.Close();
            writer.Close();

            return Result;
        }
    }
}