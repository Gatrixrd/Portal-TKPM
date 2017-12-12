using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClickFactura_Entidades.BD.Entidades;

namespace MVC5_full_version.Controllers.Pages
{
    public class RecuperaContrasenia
    {
        Desarrollo_CF contexto;
        public RecuperaContrasenia()
        {
         //using (var ctx = new Desarrollo_CF())
            contexto = new Desarrollo_CF();// DatosBafarDataContext();
        }
        public Cat_Usuario consultarUsuario(string nombreUsuario, out string msg)
        {
            try
            {
                msg = "";
                var usuario = (from t in contexto.Cat_Usuario where t.Usuario.Equals(nombreUsuario) select t).FirstOrDefault();
                return usuario;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        public List<PreguntaRespuesta> preguntasDelUsuario(int idUsuario, out string msg)
        {
            msg = "";
            try
            {
                List<PreguntaRespuesta> lsPreguntas = new List<PreguntaRespuesta>();
                var r = (from t in contexto.Rel_User_Preguntas
                         join p in contexto.Cat_Preguntas on t.IdPregunta equals p.IdPregunta
                         where t.IdUsuario.Equals(idUsuario) && t.Activo == true
                         select new { p.IdPregunta, p.Pregunta, t.Respuesta }).ToList();

                foreach (var item in r)
                {
                    bool res = false;
                    switch (item.Respuesta)
                    {
                        case "Por default":
                            res = false;
                            break;
                        case "Pendiente":
                            res = false;
                            break;
                        case "SI":
                            res = false;
                            break;
                        case "si":
                            res = false;
                            break;
                        case "Si":
                            res = false;
                            break;
                        default:
                            res = true;
                            break;
                    }
                    lsPreguntas.Add(new PreguntaRespuesta()
                    {
                        id = item.IdPregunta,
                        pregunta = item.Pregunta,
                        activo = res
                    });
                }
                return lsPreguntas;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        public bool ValidarRespuesta(int id, string respuesta, Cat_Usuario usuario, out string msg)
        {
            msg = "";
            try
            {
                bool resultado = false;
                var c = (from t in contexto.Rel_User_Preguntas where t.IdPregunta.Equals(id) select t).FirstOrDefault();
                if (c != null)
                {
                    resultado = c.Respuesta.Equals(respuesta);
                    if (resultado)
                    {
                        enviarCorreoNotificacion(usuario.Usuario, "alejandro.ramos@clickfactura.mx", usuario.Password);
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        public struct PreguntaRespuesta
        {
            public int id { get; set; }
            public string pregunta { get; set; }
            public bool activo { get; set; }
        }
        private void enviarCorreoNotificacion(string nombreUsuario, string correo, string contraseña)
        {
            //Genericos.Configuracion c = new Genericos.Configuracion();
            //string respuesta = "";
            //string Error = "";
            //if (correo.Length > 0 && correo.Equals("") != true)
            //{
            //    #region Envia Correo
            //    Genericos.objCorreo info = new Genericos.objCorreo();
            //    info.Asunto = "Reestablecimiento de contraseña de cuenta de Centro de Servicios Compartidos BAFAR";

            //    info.Cuerpo = "Cuenta de Centro de Servicios Compartidos BAFAR \n" +
            //        "La contraseña de la cuenta de Centro de Servicios Compartidos BAFAR solicitada por " + nombreUsuario + " ha sido enviada\n" +
            //        "Este es su contraseña: " + contraseña +
            //        "";

            //    info.De = "arehandoro.ramosu@gmail.com";
            //    info.Usuario = "arehandoro.ramosu@gmail.com";
            //    info.Password = "Rama880801_Alex";
            //    //info.Servidor = "www.clickfactura.com.mx";
            //    info.Servidor = "smtp.gmail.com";
            //    info.Puerto = "465";
            //    info.SSL = true;
            //    info.Para = new string[1] { correo };
            //    bool resultado = false;
            //    Genericos.ad_Correos correos = new Genericos.ad_Correos();
            //    string _correo = correo;
            //    if (_correo.Equals("") == false && _correo.Equals("csc.bafar@gmail.com") == false)
            //        resultado = correos.enviaCorreo(info, Error);
            //    else
            //    {
            //        resultado = false;
            //        Error = "Este usuario no tiene correo!";
            //    }
            //    #endregion Envia Correo
            //}
            //else
            //{
            //    //Notifica no hay correo para enviar info;
            //}
        }

    }
}