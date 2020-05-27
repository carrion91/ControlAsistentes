using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes.Informes
{
    public partial class ReporteAsistentes : System.Web.UI.Page
    {

        #region  variables globales
        private NombramientoServicios nombramientoServicios = new NombramientoServicios();
        private UnidadServicios unidadServicios = new UnidadServicios();
        private PeriodoServicios periodoServicios = new PeriodoServicios();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            int[] rolesPermitidos = { 1 };
            Utilidades.escogerMenu(Page, rolesPermitidos);
            Page.Master.FindControl("MenuControl").Visible = false;
            periodosDDL();
            unidadesDDL();
        
        }

        #region eventos
        /// <summary>
        /// Mariela Calvo
        /// Mayo/2020
        /// Efecto: llean el DropDownList con los periodos que se encuentran en la base de datos
        /// Requiere: - 
        /// Modifica: DropDownList
        /// Devuelve: -
        /// </summary>
        protected void periodosDDL()
        {
            List<Periodo> periodos = new List<Periodo>();
            ddlPeriodo.Items.Clear();
            periodos = periodoServicios.ObtenerPeriodos();
            string valor = "";
            string actual = "";
            foreach (Periodo periodo in periodos)
            {
                if (periodo.habilitado)
                {
                    valor = periodo.semestre + "     - Semestre " + periodo.anoPeriodo;
                   
                }
                else
                {
                    valor = periodo.semestre + "     - Semestre " + periodo.anoPeriodo;
                }
                ListItem itemPeriodo = new ListItem(valor, periodo.idPeriodo + "");
                ddlPeriodo.Items.Add(itemPeriodo);
                

            }
        }

        /// <summary>
        /// Mariela Calvo
        /// Mayo/2020
        /// Efecto: llean el DropDownList con los encargados que se encuentran en la base de datos
        /// Requiere: - 
        /// Modifica: DropDownList
        /// Devuelve: -
        /// </summary>
        protected void unidadesDDL()
        {
            List<Unidad> unidades = new List<Unidad>();
            ddlUnidad.Items.Clear();
            unidades = unidadServicios.ObtenerUnidades();
            foreach (Unidad unidad in unidades)
            {
                ListItem itemEncargado = new ListItem(unidad.nombre, unidad.idUnidad + "");
                ddlUnidad.Items.Add(itemEncargado);
            }
        }
        #endregion
    }
}