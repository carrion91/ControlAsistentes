using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ControlAsistentes
{
    internal static class Utilidades
    {
        public static string path = "";
        public static string logs_path = path + "logs";

        public static void SetLogDirectory()
        {
            if (Directory.Exists(logs_path))
            {
                Directory.SetCurrentDirectory(logs_path);
            }
            else
            {
                Directory.CreateDirectory(logs_path);
                Directory.SetCurrentDirectory(logs_path);
            }
            FileStream archivo = new FileStream("Error.log", FileMode.OpenOrCreate);
            archivo.Close();

        }


        /// <summary>
        /// Mariela Calvo
        /// noviembre/2019
        /// Efecto: Permite mostrar los menu de la aplicacion de Control de Asistentes
        /// Requiere: El usuario y su rol 
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        public static void escogerMenu2(Page page, int[] rolesPermitidos)

        {
            int rol = page.Session["rol"] == null ? 0 : Int32.Parse(page.Session["rol"].ToString());

            if (rol == 1)
            {
                page.Master.FindControl("MenuUTI").Visible = true;
                page.Master.FindControl("MenuSecretaria").Visible = false;
                page.Master.FindControl("MenuEncargado").Visible = false;
            }

            else if (rol == 2) {
                page.Master.FindControl("MenuUTI").Visible = false;
                page.Master.FindControl("MenuSecretaria").Visible = true;
                page.Master.FindControl("MenuEncargado").Visible = false;
            }

            else if (rol == 5) {
                page.Master.FindControl("MenuUTI").Visible = false;
                page.Master.FindControl("MenuSecretaria").Visible = false;
                page.Master.FindControl("MenuEncargado").Visible = true;
            }
        }

        /// <summary>
        /// Mariela Calvo
        /// noviembre/2019
        /// Efecto: Permite mostrar los menu de la aplicacion de Control de Asistentes
        /// Requiere: El usuario y su rol 
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        public static void escogerMenu(Page page, int[] rolesPermitidos)
        {
            int rol = page.Session["rol"] == null ? 0 : Int32.Parse(page.Session["rol"].ToString());

            if (rol == 1)
            {
                if (rolesPermitidos.Contains(rol))
                {
                    page.Master.FindControl("MenuUTI").Visible = true;
                    page.Master.FindControl("MenuSecretaria").Visible = false;
                    page.Master.FindControl("MenuEncargado").Visible = false;
                }
                else
                {
                    page.Session.RemoveAll();
                    page.Session.Abandon();
                    page.Session.Clear();
                    String url = page.ResolveUrl("~/login.aspx");
                    page.Response.Redirect(url);
                }
            }
            else if (rol == 2)
            {
                if (rolesPermitidos.Contains(rol))
                {
                    page.Master.FindControl("MenuUTI").Visible = false;
                    page.Master.FindControl("MenuSecretaria").Visible = true;
                    page.Master.FindControl("MenuEncargado").Visible = false;
                }
                else
                {
                    page.Session.RemoveAll();
                    page.Session.Abandon();
                    page.Session.Clear();
                    String url = page.ResolveUrl("~/login.aspx");
                    page.Response.Redirect(url);
                }
            }
            else if (rol == 5)
            {
                if (rolesPermitidos.Contains(rol))
                {
                    page.Master.FindControl("MenuUTI").Visible = false;
                    page.Master.FindControl("MenuSecretaria").Visible = false;
                    page.Master.FindControl("MenuEncargado").Visible = true;
                }
                else
                {
                    page.Session.RemoveAll();
                    page.Session.Abandon();
                    page.Session.Clear();
                    String url = page.ResolveUrl("~/login.aspx");
                    page.Response.Redirect(url);
                }
            }
        }
    }
}

