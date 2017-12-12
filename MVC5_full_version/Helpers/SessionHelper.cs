//using BAFAR.Clases.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MVC5_full_version.Helpers
{
    public class SessionHelper
    {
        public static bool ExisteUsuarioEnSesion()
        {
            bool logeado = HttpContext.Current.User.Identity.IsAuthenticated;

            var menu = ObtenerMenu();
            if (menu == null)
            {
                return false;
            }
            else if (menu.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            //return HttpContext.Current.User.Identity.IsAuthenticated;
        }
        public static void CerrarSession()
        {
            FormsAuthentication.SignOut();
        }
        public static string ObtenerUsuario()
        {
            string user_id = "";
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = ticket.UserData;
                }
            }
            return user_id;
        }
        public static int ObtenerIdUsuario()
        {
            int id = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                id = Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
            }
            return id;
        }
        public static string ObtenerNombreProveedor()
        {
            string id = "";
            //if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            //{
                id = Convert.ToString(HttpContext.Current.Session["Nombre_Proveedor"]);
            //}
            return id;
        }
        public static int ObtenerIdPerfil()
        {
            int id = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                id = Convert.ToInt32(HttpContext.Current.Session["IdPerfil"]);
            }
            return id;
        }
        public static List<MVC5_full_version.Genericos.Login.Menu> ObtenerMenu()
        {
            List<MVC5_full_version.Genericos.Login.Menu> menu = new List<MVC5_full_version.Genericos.Login.Menu>();
            //if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            //{

            menu = (List<MVC5_full_version.Genericos.Login.Menu>)HttpContext.Current.Session["miMenu"];
            //}
            return menu;
        }
        public static void AgregarUsuarioASesion(string usuario)
        {
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie("Usuario", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddMonths(3);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, usuario);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}