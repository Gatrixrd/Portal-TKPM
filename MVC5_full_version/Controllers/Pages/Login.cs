using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClickFactura_Entidades.BD.Entidades;

namespace MVC5_full_version.Controllers.Pages
{
    public class Login
    {
        ClickFactura_WebServiceCF.Service.Service1 cliente = new ClickFactura_WebServiceCF.Service.Service1();
       //DatosBafarDataContext contexto;
        Desarrollo_CF contexto;
        System.Data.DataSet ds;

        public Dictionary<string, string> DatosSesion { get; set; }
        public List<string> misSociedades { get; set; }
        public List<MVC5_full_version.Genericos.Login.Menu> miMenu { get; set; }
        string usuario;
        string contraseña;
        public Login(string usuario, string contraseña)
        {
            //ClickFactura_WebServiceCF.Service.Service1 ser = new ClickFactura_WebServiceCF.Service.Service1();
            //string cantidad = ser.formatearCantidadesImportes("2228.65");
            //contexto = new DatosBafarDataContext();

            #region Testeo
           // cliente.Hardcore_construyeBAPIMIRO();
            #endregion Testeo

                contexto = new Desarrollo_CF();
            ds = new System.Data.DataSet();
            DatosSesion = new Dictionary<string, string>();
            misSociedades = new List<string>();
            this.usuario = usuario;
            this.contraseña = contraseña;
        }

        /// <summary>
        /// Permite el acceso al Sistema
        /// El proveedor ya debe de haber sido importado de SAP al Portal
        /// Tambien debería estar activado
        /// </summary>
        /// <returns>
        /// Los valores recuperados y almacenados para el sistema (Session) son:
        /// IdUsuario
        /// IdPerfil
        /// TipoUsuario
        /// Usuario
        /// RFC
        /// IdProveedor
        /// Num_Proveedor
        /// NombreProveedor
        /// </returns>
        public string ValidarSesion()
        {
            try
            {
                string quienEntro = "";
                bool activado = false;
                bool tienePaquete = false;
                bool tienePreguntas = false;
                bool pregutasRespondidas = false;
                string IdProveedor = "0";
                bool existeenSAP = true;

                string mensaje = "";

                try
                {
                    ds = Valida_Menu(usuario,contraseña); ;//bafar.Valida_Menu(usuario, contraseña);
                    var tab = contexto.SP_Cat_Usuario(0, true, usuario, contraseña, "", 0, 0, "Carga_Menu2").ToList();
                    MVC5_full_version.Genericos.Login.Menu m = new MVC5_full_version.Genericos.Login.Menu();
                    miMenu = m.CrearMenu(tab, out mensaje);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        bool esUsuarioparaPasar = true; //= > 15 Agosto
                        if (ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(usuario)==true)//= > 15 Agosto Reactivar:  Genericos.cs_Estaticos.EsAdministradorBafar(usuario))
                        {
                            #region Es Administrador
                            foreach (System.Data.DataRow r in ds.Tables[0].Rows)
                            {
                                DatosSesion.Add("IdUsuario", r["IdUsuario"].ToString());
                                DatosSesion.Add("IdPerfil", r["IdPerfil"].ToString());
                                DatosSesion.Add("TipoUsuario", tipoUsuario(r["IdPerfil"].ToString()));
                                break;
                            }
                            foreach (System.Data.DataRow r in ds.Tables[1].Rows)
                            {
                                DatosSesion.Add("Usuario", r["Usuario"].ToString());
                                DatosSesion.Add("RFC", r["RFC"].ToString());

                                DatosSesion.Add("IdProveedor", r["IdProveedor"].ToString());
                                DatosSesion.Add("Num_Proveedor", r["Num_Proveedor"].ToString());
                                DatosSesion.Add("Nombre_Proveedor", r["Nombre"].ToString());
                                break;
                            }
                            foreach (System.Data.DataRow r in ds.Tables[2].Rows)
                            {
                                DatosSesion.Add("nombreProveedor", r["Compania"].ToString());
                                DatosSesion.Add("Num_Sociedad", r["Num_Sociedad"].ToString());
                                break;
                            }
                            string miNum_Proveedor = DatosSesion["Num_Proveedor"].ToString();

                            quienEntro = DatosSesion["Usuario"].ToString();
                            DatosSesion.Add("NivelAdministradorBafar", ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(quienEntro).ToString());
                            if (ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(quienEntro) == false)
                            {
                                var q = from s in contexto.view_queSociedadesmeTocan.AsEnumerable() where s.Num_Proveedor.Equals(miNum_Proveedor) == true select s;
                                if (q != null)
                                    if (q.Count() > 0)
                                    {
                                        foreach (var socie in q)
                                        {
                                            bool agregar = true;
                                            string soci = socie.Num_Sociedad;
                                            if (soci.Length > 0)
                                            {
                                                foreach (string s in misSociedades)
                                                {
                                                    if (soci.Equals(s) == true)
                                                    {
                                                        agregar = false;
                                                        break;
                                                    }
                                                }
                                                if (agregar == true)
                                                    misSociedades.Add(soci);
                                            }
                                        }
                                    }
                            }
                            else
                            {
                                var result = contexto.Relacion_Proveedores.Where(p => p.Num_Sociedad != null).GroupBy(p => p.Num_Sociedad).Select(grp => grp.FirstOrDefault());
                                if (result != null)
                                    if (result.Count() > 0)
                                    {
                                        foreach (var socie in result)
                                        {
                                            bool agregar = true;
                                            string soci = socie.Num_Sociedad;
                                            if (soci.Length > 0)
                                            {
                                                foreach (string s in misSociedades)
                                                {
                                                    if (soci.Equals(s) == true)
                                                    {
                                                        agregar = false;
                                                        break;
                                                    }
                                                }
                                                if (agregar == true)
                                                    misSociedades.Add(soci);
                                            }
                                        }
                                    }
                            }
                            //=> Genericos.cs_Estaticos.creaAcuerdoComercial(DatosSesion["RFC"]);
                            #endregion Es Administrador
                        }
                        else
                        {
                            #region Averigua si ya completo registro
                            //ACCEDIENDO AL SISTEMA!!
                            #region Obteniedo datos y configurando entorno
                            foreach (System.Data.DataRow r in ds.Tables[0].Rows)
                            {
                                DatosSesion.Add("IdUsuario", r["IdUsuario"].ToString());
                                DatosSesion.Add("IdPerfil", r["IdPerfil"].ToString());
                                //Session["IdUsuario"] = r["IdUsuario"].ToString();
                                //Session["IdPerfil"] = r["IdPerfil"].ToString();
                                DatosSesion.Add("TipoUsuario", tipoUsuario(r["IdPerfil"].ToString()));
                                break;
                            }
                            foreach (System.Data.DataRow r in ds.Tables[1].Rows)
                            {
                                DatosSesion.Add("Usuario", r["Usuario"].ToString());
                                DatosSesion.Add("RFC", r["RFC"].ToString());

                                DatosSesion.Add("IdProveedor", r["IdProveedor"].ToString());
                                DatosSesion.Add("Num_Proveedor", r["Num_Proveedor"].ToString());
                                DatosSesion.Add("Nombre_Proveedor", r["Nombre"].ToString());

                                #region       Procesos de actualización 22 Noviembre 2016

                                //ClickFacturaSAP.ClickFacturaSAPClient clienteSAP = new ClickFacturaSAP.ClickFacturaSAPClient();
                                string fini = "01.01." + DateTime.Now.Year.ToString();
                                string ffin = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
                                string numProv = r["Num_Proveedor"].ToString();
                                string[] mensajes;

                                #region        DESCARGA DE NOTAS DE CARGO
                                           bool realizado = true;// clienteSAP.CargaNotasCargo(fini, ffin, numProv, "", "YR", out mensajes);
                                #endregion DESCARGA DE NOTAS DE CARGO

                                #endregion Procesos de actualización 22 de Noviembre 2016

                                break;
                            }
                            foreach (System.Data.DataRow r in ds.Tables[2].Rows)
                            {
                                DatosSesion.Add("nombreProveedor", r["Compania"].ToString());
                                DatosSesion.Add("Num_Sociedad", r["Num_Sociedad"].ToString());
                                break;
                            }
                            string miNum_Proveedor = DatosSesion["Num_Proveedor"].ToString();
                            quienEntro = DatosSesion["Usuario"];

                            checatienePaquete(usuario, ref activado, ref tienePaquete, ref IdProveedor, ref tienePreguntas, ref pregutasRespondidas, ref existeenSAP);
                            if (tienePaquete == false)
                            {
                                //Response.Redirect("~/WebForms/wform_escogePaquetes.aspx?user=" + user + "&pass=" + pass);
                            }
                            else
                            {
                                if (activado == false)
                                {
                                    //Response.Redirect("~/WebForms/wform_peticionActivarPaquete.aspx?user=" + user + "&Proveedor=" + IdProveedor);
                                }
                                else
                                {
                                    if (tienePreguntas == false)
                                    {
                                        //Decirle que se enviara un recordatorio de esta asignacion de preguntas secretas
                                        //string titulo = "Aún esta en proceso de activación";
                                        //mensaje = "Su administrador aún no le ha asignado preguntas de recuperación de password, le será enviado un recordatorio, este mensaje continuara hasta que le sean asignadas estas preguntas.";
                                        //Response.Redirect("~/WebForms/wform_accionRealizada.aspx?titulo=" + titulo + "&mensaje=" + mensaje + "&return=" + "wform_Login.aspx");
                                    }
                                    else
                                    {
                                        if (pregutasRespondidas == false)
                                        {
                                            //Response.Redirect("~/WebForms/wform_respondePreguntas.aspx?user=" + user + "&ssap=" + pass);
                                        }
                                        else
                                        {
                                            if (ClickFactura_WebServiceCF.Service.Clases.cs_Estaticos.EsAdministradorBafar(quienEntro) == false)
                                            {
                                                var q = from s in contexto.view_queSociedadesmeTocan.AsEnumerable() where s.Num_Proveedor.Equals(miNum_Proveedor) == true select s;
                                                if (q != null)
                                                    if (q.Count() > 0)
                                                    {
                                                        foreach (var socie in q)
                                                        {
                                                            bool agregar = true;
                                                            string soci = socie.Num_Sociedad;
                                                            if (soci.Length > 0)
                                                            {
                                                                foreach (string s in misSociedades)
                                                                {
                                                                    if (soci.Equals(s) == true)
                                                                    {
                                                                        agregar = false;
                                                                        break;
                                                                    }
                                                                }
                                                                if (agregar == true)
                                                                    misSociedades.Add(soci);
                                                            }
                                                        }
                                                    }
                                            }
                                            else
                                            {
                                                var result = contexto.Relacion_Proveedores.Where(p => p.Num_Sociedad != null).GroupBy(p => p.Num_Sociedad).Select(grp => grp.FirstOrDefault());
                                                if (result != null)
                                                    if (result.Count() > 0)
                                                    {
                                                        foreach (var socie in result)
                                                        {
                                                            bool agregar = true;
                                                            string soci = socie.Num_Sociedad;
                                                            if (soci.Length > 0)
                                                            {
                                                                foreach (string s in misSociedades)
                                                                {
                                                                    if (soci.Equals(s) == true)
                                                                    {
                                                                        agregar = false;
                                                                        break;
                                                                    }
                                                                }
                                                                if (agregar == true)
                                                                    misSociedades.Add(soci);
                                                            }
                                                        }
                                                    }
                                            }
                                            // => Genericos.cs_Estaticos.creaAcuerdoComercial(DatosSesion["RFC"]);

                                        }
                                    }
                                }
                            }
                            #endregion de averiguaciones
                            #endregion Obteniedo datos y configurando entorno
                        }
                        #region Almacenando la variables de sesión con los datos generales de quien se Logeo
                                    foreach(KeyValuePair<string,string> datos in DatosSesion)
                                    {
                                        System.Web.HttpContext.Current.Session[datos.Key] = datos.Value;
                                    }
                        #endregion Almacenando la variables de sesión con los datos generales de quien se Logeo

                        return "Acceso correcto";
                    }
                    else
                    {
                        return "No puede iniciar sesión. Verifique su usuario y/o contraseña.";
                    }
                }
                catch (Exception ex)
                {
                    return "Existe un problema de comunicación con la aplicación para validar sus credenciales. Error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private string tipoUsuario(string id)
        {
            string tipo = "";
            var qq = (from u in contexto.Cat_Perfil.AsEnumerable() where (u.IdPerfil.Equals(id)) select u.esUsuarioExterno).FirstOrDefault();
            {
                tipo = qq == true ? "Externo" : "Interno";
            }
            return tipo;
        }

        private System.Data.DataSet Valida_Menu(string usuario,string password)
        {
                       ClickFactura_Facturacion.Genericos.Genericos generico = new ClickFactura_Facturacion.Genericos.Genericos();
                       System.Data.DataSet Ds = new System.Data.DataSet();
            			string consulta="Select Distinct CM.IdMenu,CM.Padre,CM.Hijo,CM.Menu,CM.Descripcion,CM.Url,CU.IdPerfil,CU.IdUsuario,CM.UrlIcon,ISNull(CM.posicionVista,1000) As posicionVista ";
						consulta=consulta+"From Cat_Usuario CU				";
						consulta=consulta+"Inner Join Cat_Perfil CP ";
						consulta=consulta+"on (CP.IdPerfil=CU.IdPerfil) ";
						consulta=consulta+"Inner Join Relacion_Perfil_Menu RPM ";
						consulta=consulta+"on(RPM.IdPerfil=CP.IdPerfil) ";
						consulta=consulta+"Inner Join Cat_Menu2 CM ";
						consulta=consulta+"on(CM.IdMenu=RPM.IdMenu) ";
						consulta=consulta+"Where Usuario='"+usuario + "' and Password='"+ password +"' and rpm.activo=1 ";
                        System.Data.DataTable t1=generico.genericos_consultaCualquierTabla(consulta);
                        Ds.Tables.Add(t1);
                        consulta="";
            			consulta=consulta+@"Select Distinct CPR.Num_Proveedor,CPR.Nombre,CPR.RFC, Usuario,CPR.IdProveedor, '\Documentos\Facturas\'+CPR.RFC 'Ruta'  ";
						consulta=consulta+" From Cat_Usuario CU  ";
						consulta=consulta+" Inner Join Cat_Proveedor CPR  ";
						consulta=consulta+" on(CPR.IdProveedor=CU.IdProveedor)  ";
                        consulta = consulta + " Where Usuario='" + usuario + "' and Password='" + password + "'  ";
						consulta=consulta+" AND (CU.Activo = 1) --cambio para dejar pasar a un Proveedor que aparace con mas de un numero de proveedor  ";
                        System.Data.DataTable t2=generico.genericos_consultaCualquierTabla(consulta);
                        Ds.Tables.Add(t2);
                        consulta="";
						consulta=consulta+"Select Distinct CC.Compania,CC.Num_Sociedad  ";
						consulta=consulta+" From Cat_Usuario CU  ";
						consulta=consulta+" Inner Join Cat_Proveedor CPR  ";
						consulta=consulta+" on(CPR.IdProveedor=CU.IdProveedor)	  ";	 
						consulta=consulta+" Inner Join Relacion_Proveedores RP  ";
						consulta=consulta+" on(RP.IdProveedor=CPR.IdProveedor)  ";
						consulta=consulta+" Inner Join Cat_Cia CC  ";
						consulta=consulta+" on(CC.IdCia=RP.IdCia)	   ";
                        consulta = consulta + " Where Usuario='" + usuario + "' and Password='" + password + "'  ";
						consulta=consulta+" Order by 1	  ";
                        System.Data.DataTable t3=generico.genericos_consultaCualquierTabla(consulta);
                        Ds.Tables.Add(t3);
                        consulta="";                      
						consulta=consulta+"Select Distinct CA.* ";
						consulta=consulta+" From Cat_Usuario CU ";
						consulta=consulta+" Inner Join Rel_Adenda_Prov RAP ";
						consulta=consulta+" on(RAP.IdProveedor=CU.IdProveedor)	 ";
						consulta=consulta+" Inner Join Cat_Adendas CA ";
						consulta=consulta+" on(CA.IdAdenda=RAP.IdAdenda) ";
                        consulta = consulta + " Where Usuario='" + usuario + "' and Password='" + password + "'	 ";
                        System.Data.DataTable t4=generico.genericos_consultaCualquierTabla(consulta);
                        Ds.Tables.Add(t4);
                        return Ds; 
        }
        private void checatienePaquete(string user, ref bool activado, ref bool tienePaquete, ref string IdProveedor, ref bool tienePreguntas, ref bool preguntasRespondidas, ref bool existeenSAP)
        {
            string mitipo = DatosSesion["TipoUsuario"];
            if (mitipo != null)
            {
                string P_Opcion = mitipo.Equals("Externo") == true ? "tienePaquete" : "esInterno";
                //Reactiva lo de abajo
                //var tab = contexto.SP_Cat_Paquete_Proveedor(0, 0, 0, false, Convert.ToDateTime("01/01/2000"), false, false, user, P_Opcion);
                //foreach (var item in tab)
                //{
                //    tienePaquete = item.IdPaquete > 0 ? true : false;
                //    DatosSesion.Add("IdPaquete", item.IdPaquete.ToString());
                //    activado = Convert.ToBoolean(item.paqueteActivo);
                //    DatosSesion.Add("tienePreguntas", item.tienePreguntas.ToString());
                //    tienePreguntas = Convert.ToBoolean(item.tienePreguntas);
                //    IdProveedor = item.IdProveedor.ToString();
                //    preguntasRespondidas = Convert.ToBoolean(item.preguntasRespondidas);
                //    existeenSAP = true;
                //}
            }
            else
            {
                tienePaquete = false;
                preguntasRespondidas = false;
                tienePreguntas = false;
                activado = false;
                existeenSAP = true;
            }
        }

    }
}