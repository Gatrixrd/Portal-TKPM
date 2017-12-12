using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Xml;

namespace MVC5_full_version.Genericos
{
    public class cs_Estaticos
    {
        public static string GetCurrentWebsiteRoot()
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
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