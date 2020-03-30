using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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
        ArchivoServicios archivoServicios = new ArchivoServicios();
        UnidadServicios unidadServicios = new UnidadServicios();
        Unidad unidadEncargado = new Unidad();

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

            if (Session["nombreCompleto"] != null)
            {
                unidadEncargado = unidadServicios.ObtenerUnidadPorEncargado(Session["nombreCompleto"].ToString());
            }

            if (!IsPostBack)
            {
                Session["listaAsistentes"] = null;
                Session["listaAsistentesFiltrada"] = null;

                List<Asistente> listaAsistentes = asistenteServicios.ObtenerAsistentes();
                Session["listaAsistentes"] = listaAsistentes;
                Session["listaAsistentesFiltrada"] = listaAsistentes;

                //Sesión de Archivos
                Session["archivos"] = null;


                MostrarAsistentes();
                ddlPeriodos();


            }
            else
            {
                if (fuArchivos.HasFiles)
                {
                    Session["archivos"] = fuArchivos;
                    lblArchivos.Text = fuArchivos.PostedFile.FileName;
                }
                if (Session["archivos"] != null)
                {
                    fuArchivos = (FileUpload)Session["archivos"];
                    lblArchivos.Text = fuArchivos.PostedFiles.Count + " Archivo(s) Seleccionado(s)";
                }
            }
        }


        #region eventos
        protected void MostrarAsistentes()
        {
            List<Asistente> listaAsistentes = (List<Asistente>)Session["listaAsistentes"];
            String nombreasistente = "";

            if (!String.IsNullOrEmpty(txtBuscarNombre.Text))
            {
                nombreasistente = txtBuscarNombre.ToString();
            }

            List<Asistente> listaAsistentesFiltrada = (List<Asistente>)listaAsistentes.Where(asistente => asistente.nombreCompleto.ToUpper().Contains(nombreasistente.ToUpper())).ToList();
            Session["listaAsistentesFiltrada"] = listaAsistentesFiltrada;

            var dt = listaAsistentesFiltrada;
            pgsource.DataSource = dt;
            pgsource.AllowPaging = true;
            //numero de items que se Asistenten en el Repeater
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
                ListItem itemPeriodos = new ListItem(periodo.semestre + " Semestre -" + periodo.anoPeriodo, periodo.idPeriodo + "");
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
            if (validarAsistenteNuevo())
            {
                string periodoSemestre = periodosDDL.SelectedValue.ToString();
                int idAsistente = 0;
                /* INSERCIÓN ASISTENTE */
                Asistente asistente = new Asistente();
                asistente.nombreCompleto = txtNombre.Text;
                asistente.carnet = txtCarnet.Text;
                asistente.telefono = txtTelefono.Text;
                asistente.cantidadPeriodosNombrado = 0;
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
                nombramiento.recibeInduccion = Convert.ToBoolean(ChckBxInduccion.Checked);

                nombramiento.cantidadHorasNombrado = Convert.ToInt32(txtHoras.Text);

                //nombramientoServicios.insertarNombramiento(nombramiento);




                /* INSERCIÓN ARCHIVOS ASISTENTE */


                /*int tipo = 1;

                if (fileExpediente.HasFile)
                {
                    List<FileUpload> listaArchivosInsertar = new List<FileUpload>();
                    listaArchivosInsertar.Add(fileExpediente);
                    listaArchivosInsertar.Add(fileInforme);
                    listaArchivosInsertar.Add(fileCV);

                    List<Archivo> listaArchivos = guardarArchivos(listaArchivosInsertar, asistente);

                    Session["listaArchivos"] = listaArchivos;
                    foreach (Archivo archivo in listaArchivos)
                    {
                        archivo.tipoArchivo = tipo;
                        int idArchivo = archivoServicios.insertarArchivo(archivo);
                        archivoServicios.insertarArchivoAsistente(idArchivo, asistente.idAsistente);
                        tipo++;
                    }
                }*/
                if (fuArchivos.FileName != "")
                {
                    List<Archivo> listaArchivos = guardarArchivos1(asistente, fileExpediente, 1);

                    foreach (Archivo archivo in listaArchivos)
                    {
                        archivo.tipoArchivo = 1;
                        archivoServicios.insertarArchivo(archivo);
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoAsistente", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoAsistente').hide();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoAsistente();", true);
            }

        }




        public void guardar(object sender, EventArgs e)
        {
            Asistente asistente = new Asistente();
            asistente.nombreCompleto = "Ma Méndez";
            asistente.carnet = "B41258";
            Session["archivos"] = fuArchivos.PostedFiles;

            if (fuArchivos.HasFile)
            {

                List<Archivo> listaArchivos = guardarArchivos1(asistente, fileExpediente, 1);

                foreach (Archivo archivo in listaArchivos)
                {
                    archivo.tipoArchivo = 1;
                    archivoServicios.insertarArchivo(archivo);
                }
            }

        }





        public List<Archivo> guardarArchivos1(Asistente asistente, FileUpload fuArchivos, int tipoArchivo)
        {
            List<Archivo> listaArchivos = new List<Archivo>();

            String archivosRepetidos = "";

            foreach (HttpPostedFile file in fuArchivos.PostedFiles)
            {
                String nombreArchivo = "";
                if (tipoArchivo == 1)
                {
                    nombreArchivo = Path.GetFileName("Expediente");
                }
                else if (tipoArchivo == 2)
                {
                    nombreArchivo = Path.GetFileName("Informe");
                }
                else if (tipoArchivo == 3)
                {
                    nombreArchivo = Path.GetFileName("CV");
                }
                else
                {
                    nombreArchivo = Path.GetFileName("Cuenta");
                }
                nombreArchivo = nombreArchivo.Replace(' ', '_');
                DateTime fechaHoy = DateTime.Now;
                String carpeta = Convert.ToString(asistente.carnet);

                int guardado = Utilidades.SaveFile(file, fechaHoy.Year, nombreArchivo, carpeta);

                if (guardado == 0)
                {

                    Archivo archivo = new Archivo();
                    Archivo archivoNuevo = new Archivo();
                    archivoNuevo.nombreArchivo = nombreArchivo;
                    archivoNuevo.rutaArchivo = Utilidades.path + fechaHoy.Year + "\\" + carpeta + "\\" + nombreArchivo;
                    archivoNuevo.fechaCreacion = fechaHoy;
                    listaArchivos.Add(archivoNuevo);
                    archivo.creadoPor = "Mariela Calvo";//Session["nombreCompleto"].ToString();

                    listaArchivos.Add(archivo);
                }
                else
                {
                    archivosRepetidos += "* " + nombreArchivo + ", \n";
                }
            }

            if (archivosRepetidos.Trim() != "")
            {
                archivosRepetidos = archivosRepetidos.Remove(archivosRepetidos.Length - 3);
                //(this.Master as SiteMaster).Mensaje("Los archivos " + archivosRepetidos + " no se pudieron guardar porque ya había archivos con ese nombre", "¡Alerta!");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Los archivos " + archivosRepetidos + " no se pudieron guardar porque ya había archivos con ese nombre" + "');", true);
            }

            return listaArchivos;
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

        public List<Archivo> guardarArchivos(List<FileUpload> archivosAsistente, Asistente asistente)
        {
            List<Archivo> listaArchivos = new List<Archivo>();
            string archivosRepetidos = "";


            foreach (FileUpload archivo in archivosAsistente)
            {

                foreach (HttpPostedFile file in archivo.PostedFiles)
                {
                    String nombreArchivo = Path.GetFileName(file.FileName);
                    nombreArchivo = nombreArchivo.Replace(' ', '_');
                    DateTime fechaHoy = DateTime.Now;
                    String carpeta = asistente.carnet + "-" + asistente.periodo.semestre + " " + asistente.periodo.anoPeriodo;

                    int guardado = Utilidades.SaveFile(file, fechaHoy.Year, nombreArchivo, carpeta);

                    if (guardado == 0)
                    {
                        Archivo archivoNuevo = new Archivo();
                        archivoNuevo.nombreArchivo = nombreArchivo;
                        archivoNuevo.rutaArchivo = Utilidades.path + fechaHoy.Year + "\\" + carpeta + "\\" + nombreArchivo;
                        archivoNuevo.fechaCreacion = fechaHoy;
                        listaArchivos.Add(archivoNuevo);
                    }
                    else
                    {
                        archivosRepetidos += "* " + nombreArchivo + ", \n";
                    }
                }

            }
            return listaArchivos;
        }

        public List<Archivo> guardarArchivos(Asistente asistente, FileUpload fuArchivos)
        {
            List<Archivo> listaArchivos = new List<Archivo>();

            String archivosRepetidos = "";

            foreach (HttpPostedFile file in fuArchivos.PostedFiles)
            {
                String nombreArchivo = Path.GetFileName(file.FileName);
                nombreArchivo = nombreArchivo.Replace(' ', '_');
                DateTime fechaHoy = DateTime.Now;
                String carpeta = asistente.carnet + "-" + asistente.periodo.semestre + " " + asistente.periodo.anoPeriodo;

                int guardado = Utilidades.SaveFile(file, fechaHoy.Year, nombreArchivo, carpeta);

                if (guardado == 0)
                {
                    Archivo Archivo = new Archivo();
                    Archivo.nombreArchivo = nombreArchivo;
                    Archivo.rutaArchivo = Utilidades.path + fechaHoy.Year + "\\" + carpeta + "\\" + nombreArchivo;
                    Archivo.fechaCreacion = fechaHoy;
                    Archivo.creadoPor = (String)Session["nombreCompleto"];

                    listaArchivos.Add(Archivo);
                }
                else
                {
                    archivosRepetidos += "* " + nombreArchivo + ", \n";
                }
            }

            if (archivosRepetidos.Trim() != "")
            {
                archivosRepetidos = archivosRepetidos.Remove(archivosRepetidos.Length - 3);
                //(this.Master as SiteMaster).Mensaje("Los archivos " + archivosRepetidos + " no se pudieron guardar porque ya había archivos con ese nombre", "¡Alerta!");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Los archivos " + archivosRepetidos + " no se pudieron guardar porque ya había archivos con ese nombre" + "');", true);
            }

            return listaArchivos;
        }



        /// <summary>
        /// Mariela Calvo
        /// Marzo/2020
        /// Efecto: Validar que los datos del nuevo periodo sean ingresados
        /// Requiere: -
        /// Modifica: Tabla Periodos
        /// Devuelve: -
        /// </summary>
        public Boolean validarAsistenteNuevo()
        {
            Boolean valido = false;

            txtNombre.CssClass = "form-control";
            txtCarnet.CssClass = "form-control";
            txtTelefono.CssClass = "form-control";
            txtHoras.CssClass = "form-control";

            #region nombre
            if (String.IsNullOrEmpty(txtNombre.Text) || txtNombre.Text.Trim() == String.Empty || txtNombre.Text.Length > 255)
            {
                txtNombre.CssClass = "form-control alert-danger";
                valido = false;
            }
            if (String.IsNullOrEmpty(txtCarnet.Text) || txtCarnet.Text.Trim() == String.Empty || txtCarnet.Text.Length > 255)
            {
                txtCarnet.CssClass = "form-control alert-danger";
                valido = false;
            }
            if (String.IsNullOrEmpty(txtTelefono.Text) || txtTelefono.Text.Trim() == String.Empty || txtTelefono.Text.Length > 255)
            {
                txtTelefono.CssClass = "form-control alert-danger";
                valido = false;
            }

            if (String.IsNullOrEmpty(txtHoras.Text) || txtHoras.Text.Trim() == String.Empty || txtHoras.Text.Length > 255)
            {
                txtHoras.CssClass = "form-control alert-danger";
                valido = false;
            }

            #endregion

            return valido;
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
        /// Efecto: se devuelve a la primera pagina y Asistente los datos de la misma
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
        /// Efecto: se devuelve a la pagina anterior y Asistente los datos de la misma
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
        /// Efecto: se devuelve a la pagina siguiente y Asistente los datos de la misma
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
        /// Efecto: se devuelve a la ultima pagina y Asistente los datos de la misma
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
        /// Efecto: actualiza la la pagina actual y Asistente los datos de la misma
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
