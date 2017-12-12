using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5_full_version.Genericos
{
    public class Login
    {

        public class Menu
        {
            public int nivel { get; set; }
            public string Id { get; set; }
            public string titulo { get; set; }
            public string icono { get; set; }
            public string controlador { get; set; }
            public string accion { get; set; }
            public List<Menu> SubMenu { get; set; }
            public Menu()
            {

            }
            private Menu GeneraNodoMenu(ClickFactura_Entidades.BD.Entidades.SP_Cat_Usuario_Result m, List<IGrouping<int, ClickFactura_Entidades.BD.Entidades.SP_Cat_Usuario_Result>> grupo = null, int nivel = 0)
            {
                try
                {
                    var url = m.Url.Split('/');
                    Menu opcion = new Menu()
                    {
                        Id = m.IdMenu.ToString(),
                        titulo = m.Menu,
                        icono = m.UrlIcon,
                        controlador = url[0],
                        accion = url.Length == 1 ? "" : url[1],
                        SubMenu = new List<Menu>(),
                        nivel = nivel
                    };
                    int key = Convert.ToInt32(opcion.Id);
                    var s = (from t in grupo where t.Key == key select t).FirstOrDefault();
                    if (s != null)
                    {
                        foreach (var subItem in s)
                        {
                            opcion.SubMenu.Add(GeneraNodoMenu(subItem, grupo, nivel + 1));
                        }
                    }
                    return opcion;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            public List<Menu> CrearMenu(List<ClickFactura_Entidades.BD.Entidades.SP_Cat_Usuario_Result> menu, out string mensaje)
            {
                mensaje = "";
                try
                {
                    List<Menu> listaMenu = new List<Menu>();
                    var grupo = (from t in menu.AsEnumerable() orderby t.posicionVista group t by t.Padre into g orderby g.Key select g).ToList();
                    var padres = grupo[0];
                    foreach (var item in padres)
                    {
                        listaMenu.Add(GeneraNodoMenu(item, grupo));
                    }
                    return listaMenu;
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                    return new List<Menu>();
                }
            }
        }

    }
}