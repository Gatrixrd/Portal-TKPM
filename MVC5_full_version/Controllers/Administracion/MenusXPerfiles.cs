using ClickFactura_Entidades.BD.Entidades;
using ClickFactura_Entidades.BD.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5_full_version.Controllers.Administracion
{
    public class MenusXPerfiles
    {
        Desarrollo_CF contexto;
        //wsBafar.Servicio_ClickFacturaClient bafar;

        public MenusXPerfiles()
        {
            contexto = new Desarrollo_CF();//DatosBafarDataContext();
            //bafar = new wsBafar.Servicio_ClickFacturaClient();
        }

        public ClickFactura_Entidades.BD.Modelos.TablaGeneralModel ListaMenusXPerfil(out string mensaje)
        {
            mensaje = "";
            try
            {
                bool[] filtros = { false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false };

                ClickFactura_Entidades.BD.Modelos.TablaGeneralModel tabla = new ClickFactura_Entidades.BD.Modelos.TablaGeneralModel();
                tabla.Nuevo = true;
                tabla.Excel = true;
                tabla.Edicion = true;
                tabla.NombreCreate = "ActualizarMenuPerfil";
                tabla.NombreUpdate = "ActualizarMenuPerfil";
                tabla.Control = "Administracion";
                tabla.NombreExcel = "MenusXPerfil.xlsx";
                tabla.NombreTemplateEdicion = "popup_Edicion";
                tabla.TituloEdicion = "Menu por Perfil";
                tabla._Edicion = "onEdit";
                tabla._Guardar = "onSave";
                tabla.Ancho = 500;
                tabla.Alto = 280;
                List<string> parametros = new List<string>();
                parametros.Add("0");
                parametros.Add("1");
                parametros.Add("0");
                parametros.Add("0");
                parametros.Add("Lista");
                var dTabla = new ClickFactura_Entidades.BD.Modelos.TablasDinamicasModel().EjecutarSP("SP_Relacion_Perfil_Menu", parametros, out mensaje);
                int x = 1;
                foreach (System.Data.DataColumn item in dTabla.Columns)
                {
                    tabla.Columnas.Add(new Columna()
                    {
                        Nombre = item.ColumnName,
                        Tipo = item.DataType,
                        oculto = item.ColumnName.Contains("Id") ? true : false,
                        filtreable = !filtros[x - 1],
                        ClientTemplate = item.DataType.FullName.Contains("System.Boolean") ? "<input type='checkbox' disabled #= R" + x + " == true ? checked='checked' : '' # />" : ""
                    });
                    tabla.NombresModel.Add("R" + x);
                    x += 1;
                }

                foreach (System.Data.DataRow item in dTabla.Rows)
                {
                    List<object> reg = new List<object>();
                    foreach (var celda in item.ItemArray)
                    {
                        reg.Add(celda);
                    }
                    tabla.Registros.Add(new Registros().MapeaDatos(reg));
                }
                return tabla;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return null;
            }
        }
        //public ClickFactura_Entidades.BD.Modelos.ArbolGeneralModel MenuPerfil(out string mensaje, out List<List<Relacion_Perfil_Menu>> listado)
        //{
        //    try
        //    {
        //        mensaje = "";
        //        ArbolGeneralModel arbol = new ArbolGeneralModel();
        //        arbol.DragDrop = false;
        //        arbol.Nombre = "ArbolMenuPerfil";
        //        arbol.Drop = "onDropMP";
        //        arbol.Select = "onSelectMP";
        //        arbol.TemplateId = "PlantillaMenuPerfil";
        //        arbol.isItems = true;
        //        arbol.isDataSource = false;

        //        var perfilesIds = contexto.Relacion_Perfil_Menu.GroupBy(u => u.IdPerfil).Select(grp => grp.ToList()).ToList();
        //        listado = perfilesIds;
        //        var menus = new System.Data.DataTable();// Cambiar 22 Agosto al habilitar la amdinistracion de Menus : new Sistema.Menus().MenuArbol(out mensaje);
        //        int i = 1;
        //        foreach (var item in perfilesIds)
        //        {
        //            var subM = new List<RegistroArbol>();
        //            var lista = item.OrderByDescending(x => x.Cat_Menu.posicionVista).ToList();
        //            foreach (var sub in lista)
        //            {
        //                if (sub.Activo == true)
        //                {
        //                    var menu = new RegistroArbol();
        //                    if (sub.Cat_Menu.Padre != 0)
        //                        menu = (from t in menus.Registros where t.Id == sub.Cat_Menu.Padre.ToString() && sub.Activo == true select t).FirstOrDefault();
        //                    else
        //                        menu = (from t in menus.Registros where t.Id == sub.IdMenu.ToString() && sub.Activo == true select t).FirstOrDefault();
        //                    if (menu.Id != null)
        //                    {
        //                        if (!subM.Where(x => x.Id == menu.Id).Any())
        //                        {
        //                            var listaSubmenu = new List<RegistroArbol>();
        //                            foreach (var ss in menu.SubMenu)
        //                            {
        //                                var s = (from t in item where t.IdMenu.Equals(ss.Id) && t.IdPerfil == sub.IdPerfil && t.Activo == true select t).FirstOrDefault();
        //                                if (s != null)
        //                                {
        //                                    listaSubmenu.Add(ss);
        //                                }
        //                            }
        //                            if (listaSubmenu.Count == 0 && menu.SubMenu.Count > 0)
        //                            {
        //                                listaSubmenu = menu.SubMenu;
        //                            }
        //                            subM.Add(new RegistroArbol()
        //                            {
        //                                Id = menu.Id,
        //                                Accion = menu.Accion,
        //                                Controlador = menu.Controlador,
        //                                Expandir = false,
        //                                Icono = menu.Icono,
        //                                Nombre = menu.Nombre,
        //                                SubMenu = listaSubmenu
        //                            });
        //                        }
        //                    } 
        //                }
        //            }
        //            arbol.Registros.Add(new RegistroArbol()
        //            {
        //                Id = item[0].Cat_Perfil.IdPerfil.ToString(),
        //                Accion = "0",
        //                Controlador = "",
        //                Expandir = false,
        //                Icono = "",
        //                Nombre = item[0].Cat_Perfil.Perfil,
        //                SubMenu = subM
        //            });
        //            i += 1;
        //        }
        //        return arbol;
        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = ex.Message;
        //        listado = new List<List<Relacion_Perfil_Menu>>();
        //        return null;
        //    }
        //}

        public bool GuardarMenusPerfil(List<Relacion_Perfil_Menu> ListaMenusPerfil, out string mensaje)
        {
            mensaje = "";
            try
            {
                foreach (var menuperfil in ListaMenusPerfil)
                {
                    if (menuperfil.IdRelPreMenu == 0)
                    {
                        contexto.Relacion_Perfil_Menu.Add(menuperfil);//InsertOnSubmit(menuperfil);
                    }
                    else
                    {
                        var v = (from t in contexto.Relacion_Perfil_Menu
                                 where t.IdRelPreMenu.Equals(menuperfil.IdRelPreMenu)
                                 select t).FirstOrDefault();
                        v.IdPerfil = menuperfil.IdPerfil;
                        v.IdMenu = menuperfil.IdMenu;
                        v.Activo = menuperfil.Activo;
                    }
                }
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        public List<Cat_Menu> Menus(out string mensaje)
        {
            mensaje = "";
            try
            {
                var menus = (from t in contexto.Cat_Menu
                             select t).ToList();
                return menus;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return null;
            }
        }
        public List<Cat_Perfil> Perfiles(out string mensaje)
        {
            mensaje = "";
            try
            {
                var perfiles = (from t in contexto.Cat_Perfil
                                select t).ToList();
                return perfiles;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return null;
            }
        }

    }

    public class Combos
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
    }
