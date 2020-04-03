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
		/// <returns>List</returns>
		public List<Usuario> ObtenerUsuarios()
		{
			SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
			List<Usuario> usuarios = new List<Usuario>();

			String consulta = @"SELECT u.id_usuario, u.nombre_completo as usuario, u.contrasenia, u.disponible, 
								a.id_asistente, a.nombre_completo, a.carnet, a.telefono, a.cantidad_periodos_nombrado 
								FROM Usuario u LEFT JOIN Asistente a ON u.id_asistente = a.id_asistente;";

			SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
			SqlDataReader reader;
			sqlConnection.Open();
			reader = sqlCommand.ExecuteReader();
			while (reader.Read())
			{
				Asistente asistente = new Asistente();

				if ((reader["id_Asistente"].ToString()) == "")
				{
					asistente = null;
				}
				else
				{
					asistente.idAsistente = Convert.ToInt32(reader["id_Asistente"].ToString());
					asistente.idAsistente = Convert.ToInt32(reader["id_Asistente"].ToString());
					asistente.nombreCompleto = reader["nombre_completo"].ToString();
					asistente.carnet = reader["carnet"].ToString();
					asistente.telefono = reader["telefono"].ToString();
					asistente.cantidadPeriodosNombrado = Convert.ToInt32(reader["cantidad_periodos_nombrado"].ToString());
				}
				
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

		/// <summary>
		/// Jesús Torres R
		/// 27/03/2020
		/// Efecto: iserta un nuevo usuario
		/// Requiere: -
		/// Modifica: -
		/// Devuelve: 
		/// </summary>
		public int insertarUsuarios(Usuario usuario)
		{
			SqlConnection connection = conexion.ConexionControlAsistentes();

			String consulta
				= @"INSERT Usuario (nombre_completo,contrasenia,disponible,id_asistente) 
                    VALUES (@nombre_completo,@contrasenia,@disponible,@id_asistente);
                    SELECT SCOPE_IDENTITY();";

			SqlCommand command = new SqlCommand(consulta, connection);
			command.Parameters.AddWithValue("@nombre_completo", usuario.nombre);
			command.Parameters.AddWithValue("@contrasenia", usuario.contraseña);
			command.Parameters.AddWithValue("@disponible", usuario.disponible);
			if (usuario.asistente == null)
			{
				command.Parameters.AddWithValue("@id_asistente", System.Data.SqlTypes.SqlInt32.Null);

			}
			else
			{
				command.Parameters.AddWithValue("@id_asistente", usuario.asistente.idAsistente);
			}

			connection.Open();
			int idUsuarios = Convert.ToInt32(command.ExecuteScalar());
			connection.Close();

			return idUsuarios;
		}
		#endregion
	}
}
