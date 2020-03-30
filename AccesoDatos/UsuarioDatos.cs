using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
	public class UsuarioDatos
	{

		#region variables globales
		private ConexionDatos conexion = new ConexionDatos();
		#endregion

		#region metodos
		/// <summary>
		/// Jesús Torres R
		/// 27/03/2020
		/// Efecto: Regresa la lista de usuarios de la base de datos
		/// Requiere: -
		/// Modifica: -
		/// Devuelve: Lista de usuarios
		/// </summary>
		/// <returns></returns>
		public List<Usuario> ObtenerUsuarios()
		{
			SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
			List<Usuario> usuarios = new List<Usuario>();

			String consulta = @"SELECT u.id_usuario, u.nombre_completo as usuario, u.contrasenia, u.disponible, 
								a.id_asistente, a.nombre_completo, a.carnet, a.telefono, a.cantidad_periodos_nombrado 
								FROM Usuario u JOIN Asistente a ON u.id_asistente = a.id_asistente;";

			SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
			SqlDataReader reader;
			sqlConnection.Open();
			reader = sqlCommand.ExecuteReader();
			while (reader.Read())
			{
				Asistente asistente = new Asistente();
				asistente.idAsistente = Convert.ToInt32(reader["id_Asistente"].ToString());
				asistente.nombreCompleto = reader["nombre_completo"].ToString();
				asistente.carnet = reader["carnet"].ToString();
				asistente.telefono = reader["telefono"].ToString();
				asistente.cantidadPeriodosNombrado = Convert.ToInt32(reader["cantidad_periodos_nombrado"].ToString());
				Usuario usuario = new Usuario();
				usuario.idUsuario = Convert.ToInt32(reader["id_usuario"].ToString());
				usuario.nombre = reader["usuario"].ToString();
				usuario.disponible = Convert.ToBoolean(reader["disponible"]);
				usuario.contraseña = reader["contrasenia"].ToString();
				usuario.asistente = asistente;
				usuarios.Add(usuario);
			}
			sqlConnection.Close();
			return usuarios;
		}
		#endregion
	}
}
