using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidades;

namespace Servicios
{
	public class UsuariosServicios
	{

		#region variables globales
		private UsuarioDatos usuarioDatos = new UsuarioDatos();
		#endregion



		#region metodos
		/// <summary>
		/// Jesús Torres R
		/// 27/03/2020
		/// Efecto: Regresa la lista de Usuarios 
		/// Requiere: -
		/// Modifica: -
		/// Devuelve: Lista de Usuarios
		/// </summary>
		/// <returns>list</returns>
		public List<Usuario> ObtenerUsuarios()
		{
			return usuarioDatos.ObtenerUsuarios();
		}
		/// <summary>
		/// Jesús Torres R
		/// 27/03/2020
		/// Efecto: Regresa la lista de Usuarios 
		/// Requiere: -
		/// Modifica: -
		/// Devuelve: Lista de Usuarios
		/// </summary>
		public void insertarUsuarios(Usuario usuario)
		{
			usuarioDatos.insertarUsuarios(usuario);
		}
		#endregion
	}
}
