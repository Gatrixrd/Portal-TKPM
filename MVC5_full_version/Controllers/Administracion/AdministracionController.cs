using MVC5_full_version.Controllers.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5_full_version.Controllers.Administracion
{
    [MVC5_full_version.Controllers.Pages.Autenticado]
    public class AdministracionController : Controller
    {
        ClickFactura_WebServiceCF.Service.Service1 cliente = new ClickFactura_WebServiceCF.Service.Service1();

        MenusXPerfiles mp;

        public AdministracionController()
        {
            //carga = new ClickFacturaSAP_Cargas();
            //pre = new PreguntasSeguridad();
            mp = new MenusXPerfiles();
            //aps = new AsignarPreguntaSecreta();
            //pp = new PaquetesPerfiles();
            //tip = new TipoDocumentosTMS();
            //kpi = new KPI();
            //pf = new PasivosFinancieros();
        }
        public ActionResult CargaMasiva()
        {
            return View();
        }

        #region        CargaMasiva

        /// <summary>
        /// Listar Proveedores en Portal extraidos desde SAP
        /// 
        /// Metodo que importa la información básica obtenida por Conector de un Proveedor a partir de su número de proveedor SAP
        /// Esta información es incertada en la tabla Cat_Proveedor
        /// GRD 19 Julio 2017
        /// </summary>
        /// <param name="numProveedor"></param>
        /// <param name="fechaInicial"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Proveedores(string numProveedor, string fechaInicial)
        {
            List<string> mensajes = new List<string>();
            if (numProveedor.Length < 10)
            {
                int largo = numProveedor.Length;
                largo = 10 - largo;
                for (int i = 0; i <= largo - 1; i++)
                {
                    numProveedor = "0" + numProveedor;
                }
            }
            bool r = cliente.Administracion_CargaProveedores(numProveedor, fechaInicial.Replace('/', '.'), out mensajes);
            mensajes.Insert(0, r.ToString());
            return Json(mensajes);
        }

        /// <summary>
        /// Importar Proveedor desde SAP
        /// ZMF_CFS_PROVEEDOR_PP
        /// BAPI Z
        /// Metodo que importa la información básica obtenida por Conector de un Proveedor a partir de su número de proveedor SAP
        /// Esta información es incertada en la tabla Cat_Proveedor
        /// GRD 19 Julio 2017
        /// </summary>
        /// <param name="numProveedor"></param>
        /// <param name="fechaInicial"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProveedoresdesdeSAP(string numProveedor,int Tipo)
        {
            //const int Tipo = 1;
            List<string> mensajes = new List<string>();
            if(numProveedor.Length<10)
            {
                int largo = numProveedor.Length;
                for (int i = largo; i < 10;i++ )
                {
                    numProveedor = "0"+numProveedor;
                }
            }
            bool r = cliente.Administracion_CargaProveedoresdesdeSAP(numProveedor, out mensajes,Tipo);
            mensajes.Insert(0, r.ToString());
            return Json(mensajes);
        }

        /// <summary>
        /// Importar todos los Proveedores de una Sociedad
        /// Se considera en el web service como de tipo 3
        /// </summary>
        /// <param name="miSoc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult importaSociedad(string miSoc)
        {
            const int Tipo = 3;
            List<string> mensajes = new List<string>();
            if (miSoc.Length < 10)
            {
                int largo = miSoc.Length;
                for (int i = largo; i < 4; i++)
                {
                    miSoc= "0" + miSoc;
                }
            }
            bool r = cliente.Administracion_CargaProveedoresdesdeSAP(miSoc, out mensajes, Tipo);
            //int pos = 0;
            //foreach(string m in mensajes)
            //{
            //      mensajes.Insert(pos,m);
            //      pos++;
            //}
            return Json(mensajes);
        }

        [HttpPost]
        public ActionResult cargaComplementoProveedores(string idProveedor)
        {
            List<string> mensajes = new List<string>();
            try
            {
                mensajes.Add("Vacio en SAP");
                mensajes.Add("No proporcionado");
                mensajes.Add("No proporcionado");
                ClickFactura_Facturacion.Genericos.Genericos generico = new ClickFactura_Facturacion.Genericos.Genericos();
                string consulta = "SELECT        ISNULL(dbo.Cat_Proveedor.RFC, ISNULL(dbo.Cat_Proveedor.RFC2, 'No localizado')) AS RFC, dbo.Cat_Proveedor.Correo AS correoSAP, ";
                consulta = consulta + " dbo.Cat_Usuario.Correo AS correoCoorporativo ";
                consulta = consulta + " FROM dbo.Cat_Proveedor INNER JOIN ";
                consulta = consulta + " dbo.Cat_Usuario ON dbo.Cat_Proveedor.IdProveedor = dbo.Cat_Usuario.IdProveedor AND dbo.Cat_Proveedor.IdProveedor = dbo.Cat_Usuario.IdProveedor ";
                consulta = consulta + " WHERE(dbo.Cat_Proveedor.IdProveedor = '"+idProveedor +"') ";
                System.Data.DataTable t1 = generico.genericos_consultaCualquierTabla(consulta);
                if(t1.Rows!=null)
                {
                    if(t1.Rows.Count>0)
                    {
                        System.Data.DataRow r = t1.Rows[0];
                        if(r["RFC"].ToString().Length > 0)
                        {
                            mensajes[0] = r["RFC"].ToString();
                        }
                        if (r["correoSAP"].ToString().Length > 0)
                        {
                            mensajes[2] = r["correoSAP"].ToString();
                        }
                        if (r["correoCoorporativo"].ToString().Length > 0)
                        {
                            mensajes[1] = r["correoCoorporativo"].ToString();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                mensajes.Add("Error: "+ex.Message);
                mensajes.Add("Reintente por favor");
                mensajes.Add("Reintente por favor");
            }
            return Json(mensajes);
        }

        [HttpPost]
        public ActionResult enviaComplementoProveedores(string idProveedor,string RFC, string correoCoorporativo, string correoSAP)
        {
            List<string> mensajes = new List<string>();
            try
            {
                ClickFactura_Facturacion.Genericos.Genericos generico = new ClickFactura_Facturacion.Genericos.Genericos();
                string consulta = "Update Cat_Proveedor Set RFC='"+RFC+"',Correo='"+correoCoorporativo+"' Where IdProveedor='"+idProveedor+"' Go";
                consulta = consulta + " Update Cat_Usuario Set Correo='"+correoSAP+"' Where IdProveedor='"+idProveedor+"' Go";
                System.Data.DataTable t1 = generico.genericos_consultaCualquierTabla(consulta);
                if (t1.Rows != null)
                {
                    if (t1.Rows.Count > 0)
                    {
                        //System.Data.DataRow r = t1.Rows[0];
                        //if (r["RFC"].ToString().Length > 0)
                        //{
                        //    mensajes[0] = r["RFC"].ToString();
                        //}
                        //if (r["correoSAP"].ToString().Length > 0)
                        //{
                        //    mensajes[2] = r["correoSAP"].ToString();
                        //}
                        //if (r["correoCoorporativo"].ToString().Length > 0)
                        //{
                        //    mensajes[1] = r["correoCoorporativo"].ToString();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                mensajes.Add("Error: " + ex.Message);
                mensajes.Add("Reintente por favor");
                mensajes.Add("Reintente por favor");
            }
            return Json(mensajes);
        }

        /// <summary>
        /// Listar todos los Proveedores
        /// 
        /// Metodo que lista todos los proveedores ya importados desde SAP y registrados en primera instancia en el Portal
        /// De este listado se puede seleccionar para complementar la información necesaria para activar completamente al Proveedor
        /// GRD 20 Julio 2017
        /// </summary>
        /// <param name="numProveedor"></param>
        /// <param name="fechaInicial"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConsultaProveedores(string numProveedor, string fechaInicial)
        {
            string mensaje;
            if (numProveedor.Length < 10)
            {
                if(numProveedor.Length>0)
                {
                    int largo = numProveedor.Length;
                    largo = 10 - largo;
                    for (int i = 0; i <= largo-1; i++)
                    {
                        numProveedor = "0" + numProveedor;
                    }
                }
            }

            var lista = cliente.Administracion_ObtenerProveedores(numProveedor, "", out mensaje);
            return Json(new { lista, mensaje });
        }

        /// <summary>
        /// Complementar el registro de un Proveedor
        /// 
        /// Metodo para enviar a almacenar los datos restantes que no ingreso la primera importación de datos desde SAP por medio del  Conector.
        /// Adicionalmente se crea en la tabla Cat_Usuario el usurio correspondiente que le permitira ingresar al Portal por el Login.
        /// Estos datos son el RFC, correo de contacto y clave id del correo SAP.
        /// En el caso del RFC es necesario para poder validar los XML emitidos por él.
        /// GRD 25 Julio 2017
        /// </summary>
        /// <param name="rfc"></param>
        /// <param name="correo"></param>
        /// <param name="correoSAP"></param>
        /// <param name="numproveedor"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult  guardarCompletarRegistroProveedor(string rfc, string correo, string correoSAP,string numproveedor)
        {
            string[] mensaje;
            var lista = cliente.Administracion_guardarCompletarRegistroProveedor(numproveedor, rfc, correo, correoSAP, out mensaje);
            return Json(new { lista, mensaje });
        }

        #endregion CargaMasiva

        #region Menus por Perfil

        [Permisos]
        public ActionResult MenusPerfil()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CargarMenusPerfil()
        {
            string mensaje;
            return PartialView("Grid", mp.ListaMenusXPerfil(out mensaje));
        }
        //[HttpPost]
        //public ActionResult CargarMenusPerfilArbol()
        //{
        //    //string mensaje;
        //    //var listado = new List<List<Models.Relacion_Perfil_Menu>>();
        //    //var arbol = mp.MenuPerfil(out mensaje, out listado);
        //    //Session["ArbolMenuPerfil"] = listado;
        //    //return PartialView("~/Views/Sistema/_Arbol.cshtml", arbol);
        //}
        //[HttpPost]
        //public ActionResult GuardarMenuXPerfil(Models.Relacion_Perfil_Menu menuperfil)
        //{
        //    string mensaje;

        //    var lista = (List<List<Models.Relacion_Perfil_Menu>>)Session["ArbolMenuPerfil"];
        //    var listaMenu = (List<Models.RegistroArbol>)Session["ArbolMenu"];
        //    var menus = new List<Models.Relacion_Perfil_Menu>();
        //    foreach (var item in lista)
        //    {
        //        var v = (from t in item
        //                 where t.IdMenu.Equals(menuperfil.IdMenu) && t.IdPerfil.Equals(menuperfil.IdPerfil)
        //                 select t).FirstOrDefault();
        //        if (v != null)
        //        {
        //            menuperfil.IdRelPreMenu = v.IdRelPreMenu;
        //            menus.Add(menuperfil);
        //            if (v.Cat_Menu.Padre == 0)
        //            {
        //                var submenus = (from t in item
        //                                where t.Cat_Menu.Padre == v.IdMenu && t.IdPerfil == v.IdPerfil
        //                                select t).ToList();
        //                foreach (var sub in submenus)
        //                {
        //                    sub.Activo = menuperfil.Activo;
        //                    menus.Add(sub);
        //                }
        //            }
        //            break;
        //        }
        //    }
        //    if (menuperfil.IdRelPreMenu == 0)
        //    {
        //        menus.Add(menuperfil);
        //        var m = (from t in listaMenu where t.Id.Equals(menuperfil.IdMenu.ToString()) select t).FirstOrDefault();
        //        if (m.Id != null)
        //        {
        //            foreach (var sub in m.SubMenu)
        //            {
        //                menus.Add(new Models.Relacion_Perfil_Menu()
        //                {
        //                    Activo = menuperfil.Activo,
        //                    IdMenu = Convert.ToInt32(sub.Id),
        //                    IdPerfil = menuperfil.IdPerfil
        //                });
        //            }
        //        }

        //    }
        //    bool r = mp.GuardarMenusPerfil(menus, out mensaje);
        //    if (r)
        //    {
        //        if (menuperfil.Activo == false)
        //        {
        //            mensaje = "El elemento seleccionado se eliminó correctamente.";
        //        }
        //        else
        //        {
        //            mensaje = "El elemento se agregó correctamente.";
        //        }
        //    }
        //    return Json(new { guardo = r, mensaje = mensaje });
        //}
        [HttpPost]
        public JsonResult Menus()
        {
            string mensaje;
            var lista = mp.Menus(out mensaje);
            List<Combos> combo = new List<Combos>();
            foreach (var item in lista)
            {
                combo.Add(new Combos()
                {
                    Id = item.IdMenu,
                    Descripcion = item.Menu
                });
            }
            return Json(combo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Perfiles()
        {
            string mensaje;
            var lista = mp.Perfiles(out mensaje);
            List<Combos> combo = new List<Combos>();
            foreach (var item in lista)
            {
                combo.Add(new Combos()
                {
                    Id = item.IdPerfil,
                    Descripcion = item.Perfil
                });
            }
            return Json(combo, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Usuarios adicionales del Proveedor

        private List<KeyValuePair<string,string>> ObtenPerfilesDB(string deQuien, string numproveedor)
        {
            //IEnumerable<SelectListItem>
            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
            using (var ctx = new ClickFactura_Entidades.BD.Entidades.Desarrollo_CF())
            {
                if (deQuien.Equals("Todos") == true)
                {
                    var perfiles = from todosPerfiles in ctx.Cat_Perfil where todosPerfiles.Activo!=false select todosPerfiles;
                            var roles = perfiles
                                        .Select(x =>
                                                new SelectListItem
                                                {
                                                    Value = x.IdPerfil.ToString(),
                                                    Text = x.Perfil
                                                });
                    foreach(var se in roles)
                    {
                        datos.Add(new KeyValuePair<string,string>(se.Value,se.Text));
                    }
                    return datos;// new SelectList(roles, "Value", "Text");
                }
                else
                {
                    var IdProveedor = numproveedor;// (from datosProveedor in ctx.Cat_Proveedor where datosProveedor.Num_Proveedor.Equals(numproveedor) == true select datosProveedor).FirstOrDefault();
                    int? _IdUsuario = Convert.ToInt32(deQuien);
                    int? _IdProveedor = Convert.ToInt32(IdProveedor);
                    var elUsuario = from todosUsuarios in ctx.Cat_Usuario where todosUsuarios.IdUsuario==_IdUsuario && todosUsuarios.IdProveedor==_IdProveedor select todosUsuarios;
                    int? suPerfil=0;
                    foreach (ClickFactura_Entidades.BD.Entidades.Cat_Usuario campo in elUsuario)
                    {
                        suPerfil=campo.IdPerfil;
                        break;
                    }
                    var perfiles = from todosPerfiles in ctx.Cat_Perfil where todosPerfiles.IdPerfil==(int)suPerfil select todosPerfiles;
                    var roles = perfiles
                                .Select(x =>
                                        new SelectListItem
                                        {
                                            Value = x.IdPerfil.ToString(),
                                            Text = x.Perfil
                                        });
                    foreach (var se in roles)
                    {
                        datos.Add(new KeyValuePair<string, string>(se.Value, se.Text));
                    }
                    return datos;// new SelectList(roles, "Value", "Text");
                }
            }
        }

        public ActionResult ObtenPerfiles(string deQuien,string numproveedor)
        {
             List<KeyValuePair<string , string>> datos=new List<KeyValuePair<string,string>>();
            datos.Add(new KeyValuePair<string, string>("-1", "Seleccione"));
            if(deQuien.Equals("Todos")==true)
            {
                        var Perfiles = ObtenPerfilesDB(deQuien,numproveedor);
                        foreach (var perfil in Perfiles)
                        {
                            datos.Add(new KeyValuePair<string, string>(perfil.Key, perfil.Value));
                        };
            }
            else
            {
                //De un Proveedores especifico
               //IEnumerable<SelectListItem> 
                List<KeyValuePair<string, string>>  Perfiles = ObtenPerfilesDB(deQuien, numproveedor);
                foreach (KeyValuePair<string, string> perfil in Perfiles)
                {
                    datos.Add(new KeyValuePair<string, string>(perfil.Key, perfil.Value));
                };
            }

            return Json(datos);
        }

        public JsonResult cargaPerfiles(string idUsuario,string idProveedor)
        {
            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();

            datos.Add(new KeyValuePair<string, string>("-1", "Seleccione"));
            if (idUsuario.Equals("Todos") == true)
            {
                var Perfiles = ObtenPerfilesDB(idUsuario, idProveedor);
                foreach (var perfil in Perfiles)
                {
                    datos.Add(new KeyValuePair<string, string>(perfil.Key, perfil.Value));
                };
            }
            else
            {
                //De un Proveedores especifico
                //IEnumerable<SelectListItem> 
                List<KeyValuePair<string, string>> Perfiles = ObtenPerfilesDB(idUsuario, idProveedor);
                foreach (KeyValuePair<string, string> perfil in Perfiles)
                {
                    datos.Add(new KeyValuePair<string, string>(perfil.Key, perfil.Value));
                };
            }
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ABCUsuario(string idusuario,string usuario,string password,string idproveedor, string correo, bool activo,string idperfil)
        {
            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
            using (var ctx = new ClickFactura_Entidades.BD.Entidades.Desarrollo_CF())
            {
                ClickFactura_Entidades.BD.Entidades.Cat_Usuario _usuario = new ClickFactura_Entidades.BD.Entidades.Cat_Usuario();
                _usuario.IdUsuario = Convert.ToInt32(idusuario);
                _usuario.Usuario = usuario;
                _usuario.Password = password;
                _usuario.IdProveedor = Convert.ToInt32(idproveedor);
                _usuario.Correo = correo;
                _usuario.Activo = activo;
                _usuario.IdPerfil = Convert.ToInt32(idperfil);
                datos=cliente.ABCUsuario(_usuario,ref datos);
            }
            return Json(datos);
        }
        public ActionResult obtenUsuario(string idusuario,  string idproveedor)
        {
            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
            int? _idusuario=Convert.ToInt32(idusuario);
            int? _idproveedor=Convert.ToInt32(idproveedor);
            try
            {
                using (var ctx = new ClickFactura_Entidades.BD.Entidades.Desarrollo_CF())
                {
                    var elusuario = from usu in ctx.Cat_Usuario where usu.IdUsuario==_idusuario && usu.IdProveedor==_idproveedor select usu;
                    foreach(ClickFactura_Entidades.BD.Entidades.Cat_Usuario _usuario in elusuario)
                    {
                        datos.Add(new KeyValuePair<string, string>("OK", "Usuario encontrado"));
                        datos.Add(new KeyValuePair<string, string>("IdUsuario",_usuario.IdUsuario.ToString()));
                        datos.Add(new KeyValuePair<string, string>("Usuario", _usuario.Usuario.ToString()));
                        datos.Add(new KeyValuePair<string, string>("Password", _usuario.Password.ToString()));
                        datos.Add(new KeyValuePair<string, string>("Correo", _usuario.Correo.ToString()));
                        datos.Add(new KeyValuePair<string, string>("Activo", _usuario.Activo.ToString()));
                        var elperfil = from p in ctx.Cat_Perfil where p.IdPerfil == _usuario.IdPerfil select p;
                        var _p = elperfil.ToList();
                        datos.Add(new KeyValuePair<string, string>("IdPerfil", _p[0].IdPerfil.ToString()));
                        datos.Add(new KeyValuePair<string, string>("Perfil", _p[0].Perfil.ToString()));
                        break;
                    }

                }
            }
            catch(Exception ex)
            {
                datos.Add(new KeyValuePair<string, string>("Error", "Ocurrío un problema recargando al usuario : "+ex.Message));
            }
            return Json(datos);
        }
        public ActionResult recuperaUsuario()
        {
            string idusuario = System.Web.HttpContext.Current.Session["IdUsuario"] as string;
            string idproveedor = System.Web.HttpContext.Current.Session["IdProveedor"] as string;
            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();
            int? _idusuario = Convert.ToInt32(idusuario);
            int? _idproveedor = Convert.ToInt32(idproveedor);
            string flujos = "";
            try
            {
                using (var ctx = new ClickFactura_Entidades.BD.Entidades.Desarrollo_CF())
                {
                    var elusuario = from usu in ctx.Cat_Usuario where usu.IdUsuario == _idusuario && usu.IdProveedor == _idproveedor select usu;
                    foreach (ClickFactura_Entidades.BD.Entidades.Cat_Usuario _usuario in elusuario)
                    {
                        datos.Add(new KeyValuePair<string, string>("OK", "Usuario encontrado"));
                        datos.Add(new KeyValuePair<string, string>("IdUsuario", _usuario.IdUsuario.ToString()));
                        datos.Add(new KeyValuePair<string, string>("Usuario", _usuario.Usuario.ToString()));
                        datos.Add(new KeyValuePair<string, string>("Password", _usuario.Password.ToString()));
                        datos.Add(new KeyValuePair<string, string>("Correo", _usuario.Correo.ToString()));
                        datos.Add(new KeyValuePair<string, string>("Activo", _usuario.Activo.ToString()));
                        var elperfil = from p in ctx.Cat_Perfil where p.IdPerfil == _usuario.IdPerfil select p;
                        var _p = elperfil.ToList();
                        datos.Add(new KeyValuePair<string, string>("IdPerfil", _p[0].IdPerfil.ToString()));
                        datos.Add(new KeyValuePair<string, string>("Perfil", _p[0].Perfil.ToString()));
                        break;
                    }
                    try
                    {
                        var usuarioPortal = from u in ctx.Cat_UsuariosWF where u.idUsuarioPortal == _idusuario select u;
                        if(usuarioPortal!=null)
                             {
                                    if(usuarioPortal.Count()>0)
                                    {
                                           foreach(var uu in usuarioPortal)
                                             {
                                                            var WF = from u in ctx.T_BitacoraWF where u.idTipoUsuarioWF == uu.idTipoUsuarioWF select u;
                                                            if (WF != null)
                                                            {
                                                                if (WF.Count() > 0)
                                                                {
                                                                    flujos = "Existen "+ WF.Count().ToString() + " aprobaciones que requieren su liberación/rechazo.";
                                                                }
                                                            }
                                                        break;
                                             }
                                           datos.Add(new KeyValuePair<string, string>("Workflow", flujos));
                            }
                             }
                    }
                    catch
                    {

                    }




                }
            }
            catch (Exception ex)
            {
                datos.Add(new KeyValuePair<string, string>("Error", "Ocurrío un problema recargando al usuario : " + ex.Message));
            }
            return Json(datos);
        }

        public JsonResult cargaUsuarios(string idProveedor)
        {
            List<KeyValuePair<string, string>> datos = new List<KeyValuePair<string, string>>();

            using (var ctx = new ClickFactura_Entidades.BD.Entidades.Desarrollo_CF())
            {
                datos = cliente.misUsuarios(idProveedor);
            }
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        #endregion Usuarios adicionales del Proveedor

        [System.Web.Mvc.HttpPost]
        public System.Web.Mvc.ActionResult invocaCerrarSesion()
        {
            List<string> variables = new List<string>();
            variables.Add("IdUsuario");
            variables.Add("IdPerfil");
            variables.Add("TipoUsuario");
            variables.Add("Usuario");
            variables.Add("RFC");
            variables.Add("IdProveedor");
            variables.Add("Num_Proveedor");
            variables.Add("Nombre_Proveedor");
            #region Almacenando la variables de sesión con los datos generales de quien se Logeo
            foreach (string datos in variables)
            {
                System.Web.HttpContext.Current.Session[datos] = null;
            }
            #endregion Almacenando la variables de sesión con los datos generales de quien se Logeo
            System.Web.HttpContext.Current.Session["miMenu"] = null;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            Helpers.SessionHelper.CerrarSession();
            return null;
        }

    }
}