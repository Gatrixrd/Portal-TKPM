using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5_full_version
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            #region Reporte Bitacora WF Gral
            routes.MapRoute(
                name: "rptBitacoraWFGral",
                url: "rptBitacoraWFGral",
                defaults: new { controller = "rptBitacoraWFGral", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "filtrosBitacoraGralWF",
                url: "filtrosBitacoraGralWF",
                defaults: new { controller = "filtrosBitacoraGralWF", action = "Index", id = UrlParameter.Optional }
            );

            #endregion Reporte Bitacora WF Gral

            #region Unidades de Medida WF
            routes.MapRoute(
                name: "UnidadMedidaWF",
                url: "UnidadMedidaWF",
                defaults: new { controller = "Cat_UMedidaWF", action = "Index", id = UrlParameter.Optional }
            );
            #endregion Unidades de Medida WF

            #region TipoUsuariosWF
            routes.MapRoute(
                name: "TipoUsuarioWF",
                url: "TipoUsuarioWF",
                defaults: new { controller = "TipoUsuarioWF", action = "Index", id = UrlParameter.Optional }
            );
            #endregion TipoUsuariosWF

            #region UsuariosWF
            routes.MapRoute(
                name: "UsuariosWF",
                url: "UsuariosWF",
                defaults: new { controller = "UsuariosWF", action = "Index", id = UrlParameter.Optional }
            );
            #endregion UsuariosWF

            #region ProcesosWF
            routes.MapRoute(
                name: "ProcesosWF",
                url: "ProcesosWF",
                defaults: new { controller = "ProcesosWF", action = "Index", id = UrlParameter.Optional }
            );
            #endregion ProcesosWF

            #region       Repositorio de Archivos procesados
            routes.MapRoute(
                name: "repositorioArchivos",
                url: "RepositorioArchivos",
                defaults: new { controller = "repositorioArchivos", action = "repositorioArchivos", id = UrlParameter.Optional }
            );
            #endregion Repositorio de Archivos procesados

            #region       Facturas verificadas
            routes.MapRoute(
                name: "FacturasVerificadas",
                url: "FacturasVerificadas",
                defaults: new { controller = "Monitoreo", action = "FacturasVerificadas", id = UrlParameter.Optional }
            );

            #endregion Facturas verificadas

            #region       MIGO
            routes.MapRoute(
                name: "Migo",
                url: "Migo",
                defaults: new { controller = "Migo", action = "Migo", id = UrlParameter.Optional }
            );
            #endregion MIGO

            #region       MIGO-MIRO
            routes.MapRoute(
                name: "MiroMigo",
                url: "Servicios",
                defaults: new { controller = "MigoMiro", action = "MigoMiro", id = UrlParameter.Optional }
            );
            #endregion MIGO-MIRO

            #region        Administracion
            routes.MapRoute(
                name: "Proveedores",
                url: "Cargas",
                defaults: new { controller = "Administracion", action = "Proveedores", id = UrlParameter.Optional }
            );
            #endregion Administracion

            #region Ordenes de Compra

            routes.MapRoute(
                name: "OrdenesCompraPO",
                url: "OrdenesCompraPO",
                defaults: new { controller = "OrdenesCompra", action = "OrdenCompraPO", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "OrdenesCompraSA",
                url: "OrdenesCompraSA",
                defaults: new { controller = "OrdenesCompraSA", action = "OrdenCompraSA", id = UrlParameter.Optional }
            );

            #endregion Ordenes de Compra

            #region Conectores

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //TKPM/Conectores/Configuracion/
            routes.MapRoute(
                name: "conexionConectorSAP",
                url: "ConexionSAP",
                defaults: new { controller = "Conectores", action = "conexionConectorSAP", id = UrlParameter.Optional }
            );

            #endregion Conectores

            #region Login
            routes.MapRoute(
                name: "Login",
                url: "Ingresar",
                defaults: new { controller = "Pages", action = "SignIn", id = UrlParameter.Optional }
            );
            #endregion

            #region Página por Dashboard
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
            #endregion Página por Dashboard

            #region Página por Arranque
            routes.MapRoute(
                name: "PaquetesOfrecidos",
                url: "PaquetesOfrecidos",
                defaults: new { controller = "PaquetesOfrecidos", action = "PaquetesOfrecidos", id = UrlParameter.Optional }
            );
            #endregion Página por Arranque

        }
    }
}
