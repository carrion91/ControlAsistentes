using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes.Catalogos
{
    public partial class AdministrarProyectos : System.Web.UI.Page
    {
        #region variables globales

        ProyectoServicios proyectoServicios = new ProyectoServicios();
        UnidadServicios unidadServicios = new UnidadServicios();
        Unidad unidad = new Unidad();
        NombramientoServicios nombramientoServicios = new NombramientoServicios();
        Proyecto proyecto = new Proyecto();
        //AsistenteServicios asistenteServicios = new AsistenteServicios();
        //UnidadServicios UnidadServicios = new UnidadServicios();
        //NombramientoServicios nombramientoServicios = new NombramientoServicios();
        //PeriodoServicios periodoServicios = new PeriodoServicios();
        //Nombramiento nombramientoS = new Nombramiento();
        //ArchivoServicios archivoServicios = new ArchivoServicios();
        readonly PagedDataSource pgsource = new PagedDataSource();
        int primerIndex, ultimoIndex, primerIndex2, ultimoIndex2;
        private int elmentosMostrar = 10;

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
        private int paginaActual2
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

        #region carga de datos

        protected void Page_Load(object sender, EventArgs e)
        {
            object[] rolesPermitidos = { 1, 2, 5 };
            Page.Master.FindControl("MenuControl").Visible = false;

            if (!IsPostBack)
            {
                Session["listaProyectos"] = null;
                Session["listaProyectosFiltrada"] = null;

                List<Proyecto> listaProyectos = proyectoServicios.ObtenerProyectos();

                Session["listaProyectos"] = listaProyectos;
                Session["listaProyectosFiltrada"] = listaProyectos;
                // List<Nombramiento> listaNombramiento = nombramientoServicios.ObtenerNombramientos();

                proyectosDDL();

                MostrarProyectos();
            }
        }



        /// <summary>
        /// Karen Guillén
        /// 22/05/2020
        /// Efecto: llena la tabla con los proyectos
        /// Requiere: -
        /// Modifica: datos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MostrarProyectos()
        {

            List<Proyecto> listaProyectos = proyectoServicios.ObtenerProyectos();

            int idProyecto = 0;
            if (!ddlProyecto.SelectedValue.Equals("--Seleccione el Proyecto--"))
            {
                idProyecto = Int32.Parse(ddlProyecto.SelectedValue);
            }

            if (idProyecto!=0)
            {
                listaProyectos = proyectoServicios.ObtenerProyectoPorId(idProyecto);
            }

            String nombreProyecto = "";

            if (!String.IsNullOrEmpty(txtBuscarNombre.Text))
            {
                nombreProyecto = txtBuscarNombre.Text;
            }

            List<Proyecto> listaProyectosFiltrada = listaProyectos.Where(proyecto => proyecto.nombre.ToUpper().Contains(nombreProyecto.ToUpper())).ToList();
            

            Session["listaProyectosFiltrada"] = listaProyectosFiltrada;

            var dt = listaProyectosFiltrada;
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
            rpProyectos.DataSource = pgsource;

            rpProyectos.DataBind();
            Paginacion();
        }

        /// <summary>
        /// Karen Guillén
        /// 23/05/2020
        /// Efecto: llenan DropDownList con los nombres de los proyectos.
        /// Requiere: - 
        /// Modifica: DropDownList
        /// Devuelve: -
        /// </summary>
        protected void proyectosDDL()
        {
            List<Proyecto> proyectos = new List<Proyecto>();
            ddlProyecto.Items.Clear();

            proyectos = proyectoServicios.ObtenerProyectos();
            ddlProyecto.Items.Add("--Seleccione el Proyecto--");
            foreach (Proyecto proyecto in proyectos)
            {
                ListItem itemProyecto = new ListItem(proyecto.nombre, proyecto.idProyecto + "");

                if (!ddlProyecto.Items.Contains(itemProyecto))
                {
                    ddlProyecto.Items.Add(itemProyecto);
                }

            }
        }

        /// <summary>
        /// Karen Guillén
        /// 29/05/2020
        /// Efecto: llenan DropDownList con los nombres de los asistentes.
        /// Requiere: - 
        /// Modifica: DropDownList
        /// Devuelve: -
        /// </summary>
        protected void asistentesDDL()
        {
            //List<Proyecto> proyectos = new List<Proyecto>();
            //ddlProyecto.Items.Clear();

            List<Nombramiento> asistentesNombrados = new List<Nombramiento>();
            ddlAsistentes.Items.Clear();

            //proyectos = proyectoServicios.ObtenerProyectos();
            asistentesNombrados = nombramientoServicios.ObtenerNombramientos();
            ddlAsistentes.Items.Add("--Seleccione un Asistente--");
            
            //ddlProyecto.Items.Add("--Seleccione el Proyecto--");
            foreach (Nombramiento asistNombrado in asistentesNombrados)
            {
                ListItem itemAsistente = new ListItem(asistNombrado.asistente.nombreCompleto, asistNombrado.asistente.idAsistente+"");

                if (!ddlAsistentes.Items.Contains(itemAsistente))
                {
                    ddlAsistentes.Items.Add(itemAsistente);
                }

            }
        }

#endregion

        #region acciones

        /// <summary>
        /// Karen Guillén
        /// 23/05/2020
        /// Efecto: filtra la tabla segun los datos ingresados en el campo de texto
        /// Requiere: dar clic en el boton de flitrar e ingresar criterio de filtro en el campo de texto
        /// Modifica: datos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void filtrarProyectos(object sender, EventArgs e)
        {
            paginaActual = 0;
        
            MostrarProyectos();
        }


        /// <summary>
        /// Karen Guillén
        /// 28/05/2020
        /// Efecto: Guarda el nuevo proyecto en la base de datos
        /// Requiere: 
        /// Modifica: 
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnGuardarProyecto_Click(object sender, EventArgs e)
        {
            Proyecto proyecto = new Proyecto();

            proyecto.nombre = txtNombreProyecto.Text;
            proyecto.fechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
            proyecto.fechaFinalizacion = Convert.ToDateTime(txtFechaFinalización.Text);
            proyecto.disponible = rdProyectoDisponible.Checked;
            proyecto.descripcion = txtDescripción.Text;

            proyectoServicios.insertar(proyecto);

            MostrarProyectos();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoProyecto", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoProyecto').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "closeModalNuevoProyecto();", true);
        }

        public void btnCerrar(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Catalogos/AdministrarProyectos.aspx");
            Response.Redirect(url);
        }


        
        /// <summary>
        /// Karen Guillén
        /// 29/05/2020
        /// Efecto: Asigna un asistente al proyecto seleccionado
        /// Requiere: llenar los campos del formulario
        /// Modifica: la base de datos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnGuardarAsistenteEnProyecto_Click(object sender, EventArgs e)
        {
            int idProyecto = Convert.ToInt32(lblIdProyecto.Text);
            int idAsistente = Int32.Parse(ddlAsistentes.SelectedValue);
            

            proyectoServicios.insertarAsistenteProyecto(idProyecto, idAsistente);

            MostrarProyectos();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalAsignarAsistenteProyecto", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalAsignarAsistenteProyecto').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "closeModalAsignarAsistenteProyecto();", true);
        }


        /// <summary>
        /// Karen Guillén
        /// 29/05/2020
        /// Efecto: Carga la información de proyecto y asistente, en el modal de asignar asistente
        /// Requiere: 
        /// Modifica: 
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnAsignarAsistenteProyecto_Click(object sender, EventArgs e)
        {
            int idProyectoSeleccionado = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            Proyecto proyectoSeleccionado = proyectoServicios.ObtenerProyectoPorId(idProyectoSeleccionado).ElementAt(0);

            lblIdProyecto.Text = proyectoSeleccionado.idProyecto+"";
            txbNombreProyecto.Text = proyectoSeleccionado.nombre;
            txbDescripciónProyecto.Text = proyectoSeleccionado.descripcion;

            asistentesDDL();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalAsignarAsistenteProyecto();", true);
        }

        /// <summary>
        /// Karen Guillén
        /// 23/08/2020
        /// Efecto: Modifica el proyecto
        /// Requiere: seleccionar un proyecto
        /// Modifica: proyecto
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkModificarProyecto_Click(object sender, EventArgs e)
        {
            int idProyectoSeleccionado = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            Proyecto proyectoSeleccionado = proyectoServicios.ObtenerProyectoPorId(idProyectoSeleccionado).ElementAt(0);

            txbNombre.Text = proyectoSeleccionado.nombre;

            DateTime dateI = proyectoSeleccionado.fechaInicio;
            string rnow = "dd/mm/yyyy";
            var Fini = (dateI.ToString(rnow));

            DateTime dateF = proyectoSeleccionado.fechaFinalizacion;
            string rFin = "dd/mm/yyyy";
            var Ffin = (dateF.ToString(rFin));

            txbFInicio.Text = Fini;
            txbFFin.Text = Ffin;
            rdBtnDisponible.Checked = proyectoSeleccionado.disponible;
            txbDescrip.Text = proyectoSeleccionado.descripcion;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalModificarProyecto();", true);

        }

        /// <summary>
        /// Karen Guillén
        /// 29/05/2020
        /// Efecto: Guarda el nuevo proyecto en la base de datos
        /// Requiere: 
        /// Modifica: 
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnEliminarProyecto_Click(object sender, EventArgs e)
        {
            int idProyectoEliminar = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

            proyectoServicios.Eliminar(idProyectoEliminar);

            MostrarProyectos();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoProyecto", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoProyecto').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "closeModalNuevoProyecto();", true);
        }

        ///// <summary>
        ///// Karen Guillén
        ///// 29/05/2020
        ///// Efecto: Guarda el nuevo proyecto en la base de datos
        ///// Requiere: 
        ///// Modifica: 
        ///// Devuelve: -
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public void btnEliminarAsistenteProyecto_Click(object sender, EventArgs e)
        //{
        //    int idProyectoEliminar = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());

        //    Proyecto proyectoEliminar = proyectoServicios.ObtenerProyectoPorId(idProyectoEliminar);

        //    proyectoServicios.Eliminar(idProyectoEliminar);

        //    MostrarProyectos();

        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoProyecto", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoProyecto').hide();", true);
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "closeModalNuevoProyecto();", true);
        //}


        /// <summary>
        /// Karen Guillén
        /// 23/05/2020
        /// Efecto: Selecciona un Proyecto, de acuerdo al Proyecto seleccionada se llenará la tabla con los datos correspondientes
        /// Requiere: 
        /// Modifica: datos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProyecto = 0;

            List<Proyecto> listaProyectos = proyectoServicios.ObtenerProyectos();
            if (!ddlProyecto.SelectedValue.Equals("--Seleccione el Proyecto--"))
            {
                idProyecto = Convert.ToInt32(ddlProyecto.SelectedValue);
                listaProyectos = proyectoServicios.ObtenerProyectoPorId(idProyecto);
            }

            Session["listaProyectos"] = listaProyectos;
            Session["listaProyectosFiltrada"] = listaProyectos;

            MostrarProyectos();
        }

         protected void btnNuevoProyecto_Click(object sender, EventArgs e)
        {

            //MostrarProyectos();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoProyecto();", true);
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
            MostrarProyectos();
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
            MostrarProyectos();
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
            MostrarProyectos();
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
            MostrarProyectos();
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
            MostrarProyectos();
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