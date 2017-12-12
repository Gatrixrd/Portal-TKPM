//using BAFAR.Clases.Login;
//using BAFAR.Helpers;
using GoogleRecaptcha;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5_full_version.Controllers.Pages
{
      [NoAutenticado]
    public class PagesController : Controller
    {
        RecuperaContrasenia recu;
        public PagesController()
        {
            recu = new RecuperaContrasenia();
        }
        // GET: Pages
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BlankPage()
        {
            return View();
        }
        public ActionResult Profile_()
        {
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult SignIn_()
        {
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        public ActionResult PreRegistroTerminado()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult ReestablecerPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(string usuario, string contraseña)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Login l = new Login(usuario, contraseña);
                    string v =  l.ValidarSesion();

                    if (v != "Acceso correcto")
                        return Json(v);
                    else
                    {
                        //SessionHelper.AgregarUsuarioASesion(l.DatosSesion["Usuario"]);
                        foreach (var item in l.DatosSesion)
                        {
                            Session[item.Key] = item.Value;
                        }
                        Session["misSociedades"] = l.misSociedades;
                        //Session["miMenu"] = l.miMenu;
                        System.Web.HttpContext.Current.Session["miMenu"] = l.miMenu;
                        return Json(new { NewUrl = @Url.Action("Index", "Dashboard") });
                    }

                }
                catch (Exception ex)
                {
                    return Json("Ocurrió una excepción durante el ingreso de sesión. Información: "+ex.Message);
                }
            }
            else
                return Json("Ocurrió un error en la página");
        }

        public ActionResult BuscarUsuario(string nombreUsuario)
        {
            string mensaje = "";
            var usuario = recu.consultarUsuario(nombreUsuario, out mensaje);
            if (usuario == null)
            {
                mensaje += "<br />" + "No se encontró un usuario " + nombreUsuario + " en el sistema. Consulte con el administrador calvarez@bafar.com.mx";
                return Json(mensaje);
            }
            var preguntas = recu.preguntasDelUsuario(usuario.IdUsuario, out mensaje);
            if (preguntas == null)
            {
                mensaje += "<br />" + "No se encontraron preguntas del usuario " + nombreUsuario + " en el sistema. Consulte con el administrador calvarez@bafar.com.mx";
                return Json(mensaje);
            }
            Session["Cat_Usuario"] = usuario;
            return Json(preguntas);
        }
        [HttpPost]
        public ActionResult VerificarRespuestaPregunta(string respuesta, int id)
        {
            string msg = "";
            var usuario = (ClickFactura_Entidades.BD.Entidades.Cat_Usuario)Session["Cat_Usuario"];
            bool r = recu.ValidarRespuesta(id, respuesta, usuario, out msg);
            if (r)
            {
                msg = "<strong>Operación completada</strong><br />La información sera enviada al correo registrado para recibir esta información.";
            }
            else
            {
                string t = "<strong>Operación incorrecta</strong><br />La respuesta a la pregunta seleccionada no es la correspondiente.";
                msg = msg == string.Empty ? t : t + "<br /><br />Error: " + msg;
            }
            return Json(new { resultado = r, texto = msg });
        }
        public ActionResult LockedScreen()
        {
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }
        public ActionResult Error500()
        {
            return View();
        }



    }
}