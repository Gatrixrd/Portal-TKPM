using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC5_full_version.Controllers.PaquetesOfrecidos
{
    public class PaquetesOfrecidosController: Controller
    {
        //DC_ contexto = new Models.DatosBafarDataContext();
        System.Collections.Generic.Dictionary<string, string> parametros = new System.Collections.Generic.Dictionary<string, string>();
        System.Web.UI.HtmlControls.HtmlGenericControl div;
        public ActionResult PaquetesOfrecidos()
        {
            ViewBag.Message = "Seleccione un paquete";
            ClickFactura_Entidades.BD.Modelos.PaquetesOfrecidosModel paquetesOfrecidos = new ClickFactura_Entidades.BD.Modelos.PaquetesOfrecidosModel();
            return View(paquetesOfrecidos);

        }

    }
}