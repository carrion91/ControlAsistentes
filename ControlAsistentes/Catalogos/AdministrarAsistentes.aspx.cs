using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes.Catalogos
{
    public partial class AdministrarAsistentes : System.Web.UI.Page
    {
        #region variables globales
        AsistenteServicios asistenteServicios = new AsistenteServicios();
        UnidadServicios UnidadServicios = new UnidadServicios();
        NombramientoServicios nombramientoServicios=new NombramientoServicios();
        ArchivoServicios archivoServicios = new ArchivoServicios();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            object[] rolesPermitidos = { 1, 2, 5 };
            Page.Master.FindControl("menu").Visible = false;
            Page.Master.FindControl("MenuControl").Visible = false;

            if (!IsPostBack)
            {
                Session["listaAsistentes"] = null;
                Session["listaAsistentesFiltrada"] = null;

                List <Asistente> listaAsistentes= asistenteServicios.ObtenerAsistentes();
                Session["listaAsistentes"] = listaAsistentes;
                Session["listaAsistentesFiltrada"] = listaAsistentes;
                unidadesDDL();
                MostrarAsistentes();
            }
        }


        #region eventos
        protected void MostrarAsistentes()
        {
            List<Asistente> listaAsistentes = (List<Asistente>)Session["listaAsistentes"];
            String nombreasistente = "";

            if (!String.IsNullOrEmpty(txtBuscarNombre.Text))
            {
                nombreasistente = txtBuscarNombre.Text;
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
        protected void MostrarAsistentesPendienteAprovacion()
        {
            List<Asistente> listaAsistentes = (List<Asistente>)Session["listaAsistentes"];
            String nombreasistente = "";

             if (!String.IsNullOrEmpty(txtBuscarNombre1.Text))
            {
                nombreasistente = txtBuscarNombre1.Text;
            }

            List<Asistente> listaAsistentesFiltrada = (List<Asistente>)listaAsistentes.Where(asistente => asistente.nombreCompleto.ToUpper().Contains(nombreasistente.ToUpper()) && asistente.nombrado==false).ToList();

            Session["listaAsistentesFiltrada"] = listaAsistentesFiltrada;

            var dt = listaAsistentesFiltrada;
            pgsource.DataSource = dt;
            pgsource.AllowPaging = true;
            //numero de items que se muestran en el Repeater
            pgsource.PageSize = elmentosMostrar;
            pgsource.CurrentPageIndex = paginaActual2;
            //mantiene el total de paginas en View State
            ViewState["TotalPaginas"] = pgsource.PageCount;
            ////Ejemplo: "Página 1 al 10"
            lblpagina2.Text = "Página " + (paginaActual2 + 1) + " de " + pgsource.PageCount + " (" + dt.Count + " - elementos)";
            ////Habilitar los botones primero, último, anterior y siguiente
            lbAnterior2.Enabled = !pgsource.IsFirstPage;
            lbSiguiente2.Enabled = !pgsource.IsLastPage;
            lbPrimero2.Enabled = !pgsource.IsFirstPage;
            lbUltimo2.Enabled = !pgsource.IsLastPage;
            RpAprovaciones.DataSource = pgsource;

            RpAprovaciones.DataBind();
            Paginacion2();
        }

        public void filtrarAsistentesPendintes(object sender, EventArgs e)
        {
            paginaActual2 = 0;
            MostrarAsistentesPendienteAprovacion();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalAsistentesAprobacionesPendientes", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalAsistentesAprobacionesPendientes').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalAsistentesAprobacionesPendientes();", true);

        }
        /// <summary>
        ///Mariela Calvo
        /// marzo/2020
        /// Efecto: filtra la tabla segun los datos ingresados en los filtros
        /// Requiere: dar clic en el boton de flitrar e ingresar datos en los filtros
        /// Modifica: datos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void filtrarAsistentes(object sender, EventArgs e)
        {
            paginaActual = 0;
            MostrarAsistentes();
            
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
        protected void unidadesDDL()
        {
            List<Unidad> unidades = new List<Unidad>();
            ddlUnidad.Items.Clear();
            unidades = UnidadServicios.ObtenerUnidades();
            foreach (Unidad unidad in unidades)
            {
                ListItem itemEncargado = new ListItem(unidad.nombre, unidad.idUnidad + "");
                ddlUnidad.Items.Add(itemEncargado);
            }
        }

        /// <summary>
        /// Mariela Calvo
        ///Marzo/2020
        /// Efecto: Selecciona un Asistentes, de acuerdo al Asistentes seleccionada se llenará la tabla con los datos correspondientes
        /// Requiere: 
        /// Modifica: datos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idUnidad=Convert.ToInt32(ddlUnidad.SelectedValue);
            List<Asistente> listaAsistentes = asistenteServicios.ObtenerAsistentesPorUnidad(idUnidad);

            Session["listaAsistentes"] = listaAsistentes;
            Session["listaAsistentesFiltrada"] = listaAsistentes;

            MostrarAsistentes();
        }
        protected void AsistenteAprobar_OnChanged(object sender, EventArgs e)
        {


            String numeroCarné = ((LinkButton)(sender)).CommandArgument.ToString();
            List<Asistente> listaAsistentes = (List<Asistente>)Session["listaAsistentes"];
            List<Asistente> listaAsistentesFiltrada = (List<Asistente>)listaAsistentes.Where(asistente => asistente.carnet==numeroCarné).ToList();
            foreach (Asistente asistente in listaAsistentesFiltrada)
            {
                txtNumeroCarné.Text = asistente.carnet;
                txtNumeroCarné.Enabled = false;
                txtNombreAsistente.Text = asistente.nombreCompleto;
                txtNombreAsistente.Enabled = false;
                txtCantidadHoras.Text = Convert.ToString(asistente.cantidadHorasNombrado);
                txtCantidadHoras.Enabled = false;
            }
            SeleccionarEstado();


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalObservacionesAsistente", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalObservacionesAsistente').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "observacionesAsistentes();", true);

        }
        protected void AprobarAsistente_OnChanged(object sender, EventArgs e)
        {
            nombramientoServicios.ActualizarNombramientoAsistente(txtNumeroCarné.Text,AsistenteDDL.SelectedValue,txtObservaciones.Text);

        }
        protected void SeleccionarEstado_OnChanged(object sender, EventArgs e)
        {
            SeleccionarEstado();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalObservacionesAsistente", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalObservacionesAsistente').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "observacionesAsistentes();", true);
        }
        protected void SeleccionarEstado()
        {
            AsistenteDDL.Items.Clear();
            
                ListItem i;
                i = new ListItem("Aprobado", "1");
                AsistenteDDL.Items.Add(i);
                i = new ListItem("Rechazado", "0");
                AsistenteDDL.Items.Add(i);
            

        }
        protected void btnPendientes_Click(object sender, EventArgs e)
        {
            MostrarAsistentesPendienteAprovacion();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalAsistentesAprobacionesPendientes", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalAsistentesAprobacionesPendientes').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalAsistentesAprobacionesPendientes();", true);
        }
        /*
       * Kevin Picado
       * 20/03/20
       * Efecto: descarga el archivo para que el usuario lo pueda ver
       * Requiere: -
       * Modifica: -
       * Devuelve: -
       */
        private void descargar(string ruta)
        {

            Process proceso = new Process();
            proceso.StartInfo.FileName = ruta;
            proceso.Start();


        }
        /*Kevin Picado
      * 20/03/20
      * Efecto: descarga el archivo para que el usuario lo pueda ver
      * Requiere: clic en el archivo
      * Modifica: -
      * Devuelve: -
      */
        protected void btnVerArchivo_Click(object sender, EventArgs e)
        {
            //List<ArchivoEjecucion> archivoEjecucion = (List<ArchivoEjecucion>)Session["listaArchivoEjecucion"];
            //String[] infoArchivo = (((LinkButton)(sender)).CommandArgument).ToString().Split(',');
            //String nombreArchivo = infoArchivo[1];
            String carnet = (((LinkButton)(sender)).CommandArgument).ToString();
            List<Asistente> listAsistente = new List<Asistente>();
            listAsistente = asistenteServicios.ObtenerAsistentes();
            List<Asistente> tempAsistente = listAsistente.Where(item => item.carnet.Equals(carnet)).ToList();
            int idPeriodo = tempAsistente.Where(item => item.carnet.Equals(carnet)).ToList().First().periodo.idPeriodo;
            int idAsistente = tempAsistente.Where(item => item.carnet.Equals(carnet)).ToList().First().idAsistente;
            List <Archivo> listArchivosAsistente= archivoServicios.ObtenerArchivoAsistente(Convert.ToInt32(idAsistente),idPeriodo);
            foreach (Archivo archivo in listArchivosAsistente)
            {
                FileStream fileStream = new FileStream(archivo.rutaArchivo, FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream);
                Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                fileStream.Close();
                binaryReader.Close();

                descargar(archivo.rutaArchivo);
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalAsistentesAprobacionesPendientes", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalAsistentesAprobacionesPendientes').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalAsistentesAprobacionesPendientes();", true);
        }
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
        public void Paginacion2()
        {
            var dt = new DataTable();
            dt.Columns.Add("IndexPagina"); //Inicia en 0
            dt.Columns.Add("PaginaText"); //Inicia en 1

            primerIndex = paginaActual2 - 2;
            if (paginaActual2 > 2)
                ultimoIndex2 = paginaActual2 + 2;
            else
                ultimoIndex2 = 4;

            //se revisa que la ultima pagina sea menor que el total de paginas a mostrar, sino se resta para que muestre bien la paginacion
            if (ultimoIndex2 > Convert.ToInt32(ViewState["TotalPaginas"]))
            {
                ultimoIndex2 = Convert.ToInt32(ViewState["TotalPaginas"]);
                primerIndex2 = ultimoIndex2 - 4;
            }

            if (primerIndex2 < 0)
                primerIndex2 = 0;

            //se crea el numero de paginas basado en la primera y ultima pagina
            for (var i = primerIndex2; i < ultimoIndex2; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptAsistenteAprobado.DataSource = dt;
            rptAsistenteAprobado.DataBind();
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
        protected void lbPrimero2_Click(object sender, EventArgs e)
        {
            paginaActual2 = 0;
            MostrarAsistentesPendienteAprovacion();
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
        protected void lbAnterior2_Click(object sender, EventArgs e)
        {
            paginaActual2 -= 1;
            MostrarAsistentesPendienteAprovacion();
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
        protected void lbSiguiente2_Click(object sender, EventArgs e)
        {
            paginaActual2 += 1;
            MostrarAsistentesPendienteAprovacion();
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
        protected void lbUltimo2_Click(object sender, EventArgs e)
        {
            paginaActual2 = (Convert.ToInt32(ViewState["TotalPaginas"]) - 1);
            MostrarAsistentesPendienteAprovacion();
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
        protected void rptPaginacion2_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("nuevaPagina")) return;
            paginaActual2 = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarAsistentesPendienteAprovacion();
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
        protected void rptPaginacion2_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPagina = (LinkButton)e.Item.FindControl("lbPaginacion");
            if (lnkPagina.CommandArgument != paginaActual2.ToString()) return;
            lnkPagina.Enabled = false;
            lnkPagina.BackColor = Color.FromName("#005da4");
            lnkPagina.ForeColor = Color.FromName("#FFFFFF");
        }
        #endregion
        #endregion
    }
}