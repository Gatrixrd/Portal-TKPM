using MVC5_full_version.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5_full_version.Controllers.Pages
{
    public class AutenticadoAttribute : ActionFilterAttribute
    {

        // Si no estamos logeado, regresamos al login
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!SessionHelper.ExisteUsuarioEnSesion())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Pages",
                    action = "SignIn"
                }));
            }
        }
    }

    // Si estamos logeado ya no podemos acceder a la página de Login
    public class NoAutenticadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExisteUsuarioEnSesion())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Dashboard",
                    action = "Index"
                }));
            }
        }
    }

}