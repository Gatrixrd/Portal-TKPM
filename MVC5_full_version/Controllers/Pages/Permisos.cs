using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using MVC5_full_version.Helpers;
using ClickFactura_Entidades.BD.Entidades;

namespace MVC5_full_version.Controllers.Pages
{
    public class PermisosAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string accion = filterContext.ActionDescriptor.ActionName;
            string control = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (!PermisoXUsuario.TienePermiso(accion, control))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Dashboard",
                    action = "Index"
                }));
            }
        }

    }
    public class PermisoXUsuario
    {
        static Desarrollo_CF contexto;

        public static bool TienePermiso(string accion, string control)
        {
            contexto = new Desarrollo_CF();//Models.DatosBafarDataContext();
            var idUsuario = SessionHelper.ObtenerIdUsuario();
            var idPerfil = SessionHelper.ObtenerIdPerfil();
            var miMenu = SessionHelper.ObtenerMenu();
            bool permiso = false;
            if (idUsuario != null && idUsuario > 0)
            {
                var p = contexto.Relacion_Perfil_Menu.Where(x => x.IdPerfil.Equals(idPerfil.ToString()) && x.Activo == true).ToList();
                if (p != null)
                {
                    var siMenu = (from t in miMenu where t.accion.Equals(accion) && t.controlador.Equals(control) select t).FirstOrDefault();
                    if (siMenu == null)
                    {
                        var subM = (from t in miMenu where t.controlador.Equals(control) select t).FirstOrDefault();
                        if (subM != null)
                            siMenu = (from t in subM.SubMenu where t.accion.Equals(accion) select t).FirstOrDefault();
                    }
                    if (siMenu != null)
                    {
                        int id = System.Convert.ToInt32(siMenu.Id);
                        var existe = (from t in p where t.IdMenu == id select t).Any();
                        if (existe)
                        {
                            permiso = true;
                        }
                        else
                            permiso = false;
                    }
                    else
                        permiso = false;

                    return permiso;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }    

}