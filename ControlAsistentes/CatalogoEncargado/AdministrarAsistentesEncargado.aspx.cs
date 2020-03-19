﻿using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes.CatalogoEncargado
{
    public partial class AdministrarAsistentesEncargado : System.Web.UI.Page
    {
        #region variables globales
        AsistenteServicios asistenteServicios = new AsistenteServicios();
        PeriodoServicios periodoServicios = new PeriodoServicios();
        NombramientoServicios nombramientoServicios = new NombramientoServicios();
        readonly PagedDataSource pgsource = new PagedDataSource();
        int primerIndex, ultimoIndex, primerIndex2, ultimoIndex2;
        private int elmentosMostrar = 10;
        #endregion

        #region paginacion
        private int paginaActual
        {
            get
            {
                if (ViewState["paginaActual"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["paginaActual"]);
            }
            set
            {
                ViewState["paginaActual"] = value;
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            object[] rolesPermitidos = { 1, 2, 5 };
            Page.Master.FindControl("menu").Visible = false;
            Page.Master.FindControl("MenuControl").Visible = false;

            if (!IsPostBack)
            {
                Session["listaAsistentes"] = null;
                Session["listaAsistentesFiltrada"] = null;

                List<Asistente> listaAsistentes = asistenteServicios.ObtenerAsistentes();
                Session["listaAsistentes"] = listaAsistentes;
                Session["listaAsistentesFiltrada"] = listaAsistentes;
                MostrarAsistentes();
                ddlPeriodos();


            }
        }


        #region eventos
        protected void MostrarAsistentes()
        {
            List<Asistente> listaAsistentes = (List<Asistente>)Session["listaAsistentes"];
            String nombreasistente = "";

            if (!String.IsNullOrEmpty(""))
            {
                nombreasistente = "".ToString();
            }
            foreach (Asistente asistente in listaAsistentes)
            {
                txBool.Text = asistente.nombrado.ToString();
            }
            List<Asistente> listaAsistentesFiltrada = (List<Asistente>)listaAsistentes.Where(asistente => asistente.nombreCompleto.ToUpper().Contains(nombreasistente.ToUpper())).ToList();
            Session["listaAsistentesFiltrada"] = listaAsistentesFiltrada;

            var dt = listaAsistentesFiltrada;
            pgsource.DataSource = dt;
            pgsource.AllowPaging = true;
            //numero de items que se muestran en el Repeater
            pgsource.PageSize = elmentosMostrar;
            pgsource.CurrentPageIndex = paginaActual;
            //mantiene el total de paginas en View State
            ViewState["TotalPaginas"] = pgsource.PageCount;
            //Ejemplo: "Página 1 al 10"
            lblpagina.Text = "Página " + (paginaActual + 1) + " de " + pgsource.PageCount + " (" + dt.Count + " - elementos)";
            //Habilitar los botones primero, último, anterior y siguiente
            lbAnterior.Enabled = !pgsource.IsFirstPage;
            lbSiguiente.Enabled = !pgsource.IsLastPage;
            lbPrimero.Enabled = !pgsource.IsFirstPage;
            lbUltimo.Enabled = !pgsource.IsLastPage;
            rpAsistentes.DataSource = pgsource;
            rpAsistentes.DataBind();
            Paginacion();
        }
        public void btnDevolverse(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Default.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Mariela Calvo
        /// Marzo20
        /// Efecto: llean el DropDownList con los encargados que se encuentran en la base de datos
        /// Requiere: - 
        /// Modifica: DropDownList
        /// Devuelve: -
        /// </summary>
        protected void ddlPeriodos()
        {
            List<Periodo> periodos = new List<Periodo>();
            periodosDDL.Items.Clear();
            periodos = periodoServicios.ObtenerPeriodos();
            
            foreach (Periodo periodo in periodos)
            {
                ListItem itemPeriodos= new ListItem(periodo.semestre+" Semestre -"+periodo.anoPeriodo,periodo.idPeriodo+"");
                periodosDDL.Items.Add(itemPeriodos);
            }
          
        }


        /// <summary>
        /// Mariela Calvo
        /// Marzo/2020
        /// Efecto: Activar modal nuevo asistente
        /// Requiere: Presionar boton nuevo asistente
        /// Modifica: Tabla Asistentes
        /// Devuelve: -
        /// </summary>
        protected void btnNuevoAsistente_Click(object sender, EventArgs e)
        {
            txtNombre.CssClass = "form-control";
            txtNombre.Text = "";
            txtTelefono.CssClass = "form-control";
            txtTelefono.Text = "";
            txtCarnet.CssClass = "form-control";
            txtCarnet.Text = "";

            txtHoras.CssClass = "form-control";
            txtHoras.Text = "";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoAsistente();", true);
        }

        protected void guardarNuevoAsistente_Click(object sender, EventArgs e)
        {
            string periodoSemestre = periodosDDL.SelectedValue.ToString();
            int idAsistente = 0;
            /* INSERCIÓN ASISTENTE */
            Asistente asistente = new Asistente();
            asistente.nombreCompleto = txtNombre.Text;
            asistente.carnet = txtCarnet.Text;
            asistente.telefono = txtTelefono.Text;
            //idAsistente = asistenteServicios.insertarAsistente(asistente);
            asistente.idAsistente = idAsistente;

            /* INSERCIÓN NOMBRAMIENTO ASISTENTE */
            Nombramiento nombramiento = new Nombramiento();
            nombramiento.asistente = asistente;
            Periodo periodo = new Periodo();
            periodo.idPeriodo = Convert.ToInt32(periodosDDL.SelectedValue);
            nombramiento.periodo = periodo;
            Unidad unidad = new Unidad();
            /* COMO SE CUAL ES LA UNIDAD DEL ENCARGADO */
            unidad.idUnidad = 1;
            nombramiento.unidad = unidad;
            nombramiento.aprobado = false;
            nombramiento.recibeInduccion = ChckBxInduccion.Checked ? true : false;
            nombramiento.cantidadHorasNombrado = Convert.ToInt32(txtHoras.Text.ToString());
            //nombramientoServicios.insertarNombramiento(nombramiento);

            /* INSERCIÓN ARCHIVOS ASISTENTE */

            ArchivoAsistente archivoExpediente = new ArchivoAsistente();
            ArchivoAsistente archivoInforme= new ArchivoAsistente();
            ArchivoAsistente archivoCV = new ArchivoAsistente();

        }


        #endregion


        #region metodos paginacion
        public void Paginacion()
        {
            var dt = new DataTable();
            dt.Columns.Add("IndexPagina"); //Inicia en 0
            dt.Columns.Add("PaginaText"); //Inicia en 1

            primerIndex = paginaActual - 2;
            if (paginaActual > 2)
                ultimoIndex = paginaActual + 2;
            else
                ultimoIndex = 4;

            //se revisa que la ultima pagina sea menor que el total de paginas a mostrar, sino se resta para que muestre bien la paginacion
            if (ultimoIndex > Convert.ToInt32(ViewState["TotalPaginas"]))
            {
                ultimoIndex = Convert.ToInt32(ViewState["TotalPaginas"]);
                primerIndex = ultimoIndex - 4;
            }

            if (primerIndex < 0)
                primerIndex = 0;

            //se crea el numero de paginas basado en la primera y ultima pagina
            for (var i = primerIndex; i < ultimoIndex; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPaginacion.DataSource = dt;
            rptPaginacion.DataBind();
        }

        /// <summary>
        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: se devuelve a la primera pagina y muestra los datos de la misma
        /// Requiere: dar clic al boton de "Primer pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbPrimero_Click(object sender, EventArgs e)
        {
            paginaActual = 0;
            MostrarAsistentes();
        }

        /// <summary>
        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: se devuelve a la pagina anterior y muestra los datos de la misma
        /// Requiere: dar clic al boton de "Anterior pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbAnterior_Click(object sender, EventArgs e)
        {
            paginaActual -= 1;
            MostrarAsistentes();
        }

        /// <summary>
        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: se devuelve a la pagina siguiente y muestra los datos de la misma
        /// Requiere: dar clic al boton de "Siguiente pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbSiguiente_Click(object sender, EventArgs e)
        {
            paginaActual += 1;
            MostrarAsistentes();
        }

        /// <summary>
        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: se devuelve a la ultima pagina y muestra los datos de la misma
        /// Requiere: dar clic al boton de "Ultima pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbUltimo_Click(object sender, EventArgs e)
        {
            paginaActual = (Convert.ToInt32(ViewState["TotalPaginas"]) - 1);
            MostrarAsistentes();
        }

        /// <summary>
        /// Mariela Calvo 
        /// marzo/2020
        /// Efecto: actualiza la la pagina actual y muestra los datos de la misma
        /// Requiere: -
        /// Modifica: elementos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("nuevaPagina")) return;
            paginaActual = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarAsistentes();
        }

        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: marca el boton de la pagina seleccionada
        /// Requiere: dar clic al boton de paginacion
        /// Modifica: color del boton seleccionado
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPagina = (LinkButton)e.Item.FindControl("lbPaginacion");
            if (lnkPagina.CommandArgument != paginaActual.ToString()) return;
            lnkPagina.Enabled = false;
            lnkPagina.BackColor = Color.FromName("#005da4");
            lnkPagina.ForeColor = Color.FromName("#FFFFFF");
        }
        #endregion
    }
}