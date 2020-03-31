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

namespace ControlAsistentes.CatalogoUTI.Usuarios
{
	public partial class AdministrarUsuarios : System.Web.UI.Page
	{

		#region variables globales

		private UsuariosServicios usuariosServicios = new UsuariosServicios();

		#region variables globales paginacion tarjetas
		readonly PagedDataSource pgsource = new PagedDataSource();
		int primerIndex, ultimoIndex;
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
		#endregion

		#endregion

		#region page load
		protected void Page_Load(object sender, EventArgs e)
		{
			int[] rolesPermitidos = { 1 };
			Utilidades.escogerMenu(Page, rolesPermitidos);
			Page.Master.FindControl("menu").Visible = false;
			Page.Master.FindControl("MenuControl").Visible = false;

			if (!IsPostBack)
			{
				List<Usuario> usuarios = usuariosServicios.ObtenerUsuarios();
				Session["listaUsuarios"] = usuarios;
				Session["listaUsuariosFiltrada"] = usuarios;
				mostrarUsuarios();
			}
		}

		#endregion

		private void mostrarUsuarios()
		{
			List<Usuario> listaUsuarios = (List<Usuario>)Session["listaUsuariosFiltrada"];
			String filtro = "";
			if (!String.IsNullOrEmpty(txtBuscarUsuario.Text))
			{
				filtro = txtBuscarUsuario.Text;
			}
			List<Usuario> listaUsuariosFiltrada = (List<Usuario>)listaUsuarios.Where(x => x.nombre.ToUpper().Contains(filtro.ToUpper())).ToList();
			Session["listaUsuariosFiltrada"] = listaUsuariosFiltrada;
			var dt = listaUsuariosFiltrada;
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
			rpUsuarios.DataSource = pgsource;
			rpUsuarios.DataBind();
			Paginacion();
		}



		#region paginacion
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
			mostrarUsuarios();
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
			mostrarUsuarios();
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
			mostrarUsuarios();
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
			mostrarUsuarios();
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
			mostrarUsuarios();
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

		#region eventos
		public void btnDevolverse(object sender, EventArgs e)
		{
			String url = Page.ResolveUrl("~/Default.aspx");
			Response.Redirect(url);
		}


		/// <summary>
		/// Jesús Torres
		/// 30/03/2020
		/// Efecto: Activar modal nuevo usuario
		/// Requiere: Presionar boton nuevo usuario
		/// Modifica: Tabla Usuarios
		/// Devuelve: -
		/// </summary>
		protected void btnNuevoUsuario_Click(object sender, EventArgs e)
		{
			txtNuevoUsuario.CssClass = "form-control";
			txtNuevoUsuario.Text = "";
			txtContrasena.CssClass = "form-control";
			txtContrasena.Text = "";
			ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoUsuario();", true);
		}

		/// <summary>
		/// Jesús Torres
		/// 30/03/2020
		/// Efecto: Guarda un nuevo usuario ingresado
		/// Requiere: Presionar boton guardar usuario
		/// Modifica: Tabla Usuarios
		/// Devuelve: -
		/// </summary>
		protected void btnGuardarNuevoUsuario(object sender, EventArgs e)
		{
			
		}

		protected void verContraseña(object sender, EventArgs e)
		{
			if (txtContrasena.Attributes["Type"] == "password")
			{
				txtContrasena.Attributes["Type"] = "text";
				ScriptManager.RegisterStartupScript(this, this.GetType(), "modifica", "$('.icon').removeClass('glyphicon glyphicon-eye-close').addClass('glyphicon glyphicon-eye-open');", true);
				//btnVerContrasena.CssClass = "input-group-addon btn btn-primary fa fa-eye>";
			}
			else
			{
				txtContrasena.Attributes["Type"] = "password";
				ScriptManager.RegisterStartupScript(this, this.GetType(), "modifica", "$('.icon').removeClass('glyphicon glyphicon-eye-open').addClass('glyphicon glyphicon-eye-close');", true);
				//btnVerContrasena.CssClass = "input-group-addon btn btn-primary fa fa-eye-slash>";
			}
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoUsuario", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoUsuario').hide();", true);
			ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoUsuario();", true);

		}

		#endregion

	}
}