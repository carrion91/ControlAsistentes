using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{  /// <summary>
   /// Mariela Calvo    
   /// marzo/2020
   /// Clase para administrar el CRUD para las Asistentes
   /// </summary>
    public class AsistenteDatos
    {
        private ConexionDatos conexion = new ConexionDatos();




        /// <summary>
        /// Obtiene las Asistentes de la base de datos segun el periodo
        /// </summary>
        /// <returns>Retorna una lista <code>LinkedList<Asistente></code> que contiene las Asistentes</returns>
        public List<Asistente> ObtenerAsistentes()
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Asistente> asistentes = new List<Asistente>();

            String consulta = @"SELECT a.id_asistente, a.nombre_completo,a.carnet,a.telefono,n.aprobado, p.semestre, p.ano_periodo,n.cantidad_horas, a.cantidad_periodos_nombrado, u.nombre as unidad,  e.nombre_completo as nombre_encargado" +
            " FROM Asistente a JOIN Nombramiento n ON a.id_asistente=n.id_asistente JOIN Periodo p ON n.id_periodo=p.id_periodo JOIN Unidad u ON n.id_unidad=u.id_unidad JOIN Encargado_Unidad eu ON u.id_unidad=eu.id_unidad JOIN Encargado e ON e.id_encargado = eu.id_encargado; ";

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
                asistente.nombrado = Convert.ToBoolean(reader["aprobado"].ToString());
                Periodo periodo = new Periodo();
                periodo.semestre =reader["semestre"].ToString();
                periodo.anoPeriodo= Convert.ToInt32(reader["ano_periodo"].ToString());
                asistente.periodo = periodo;
                asistente.cantidadHorasNombrado= Convert.ToInt32(reader["cantidad_horas"].ToString());
                asistente.cantidadPeriodosNombrado= Convert.ToInt32(reader["cantidad_periodos_nombrado"].ToString());
                Unidad unidad = new Unidad();
                unidad.nombre = reader["unidad"].ToString();
                Encargado encargado = new Encargado();
                encargado.nombreCompleto = reader["nombre_encargado"].ToString();
                unidad.encargado = encargado;
                asistente.unidad = unidad;
                asistentes.Add(asistente);
            }

            sqlConnection.Close();

            return asistentes;
        }

        public List<Asistente> ObtenerAsistentesPorUnidad(int idUnidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Asistente> asistentes = new List<Asistente>();

            String consulta = @"SELECT distinct a.id_asistente, a.nombre_completo,a.carnet,a.telefono,n.aprobado, p.semestre, p.ano_periodo,n.cantidad_horas, a.cantidad_periodos_nombrado, u.nombre as unidadA,  e.nombre_completo as nombre_encargado" +
            " FROM Asistente a JOIN Nombramiento n ON a.id_asistente=n.id_asistente JOIN Periodo p ON n.id_periodo=p.id_periodo JOIN Unidad u ON n.id_unidad=u.id_unidad JOIN Encargado_Unidad eu ON u.id_unidad=eu.id_unidad JOIN Encargado e ON e.id_encargado = eu.id_encargado" +
            " WHERE n.id_unidad=@id_unidad and p.habilitado=1; ";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_unidad", idUnidad);

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
                asistente.nombrado = Convert.ToBoolean(reader["aprobado"].ToString());
                Periodo periodo = new Periodo();
                periodo.semestre = reader["semestre"].ToString();
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                asistente.periodo = periodo;
                asistente.cantidadHorasNombrado = Convert.ToInt32(reader["cantidad_horas"].ToString());
                asistente.cantidadPeriodosNombrado = Convert.ToInt32(reader["cantidad_periodos_nombrado"].ToString());
                Unidad unidad = new Unidad();
                unidad.nombre = reader["unidadA"].ToString();
                unidad.idUnidad = idUnidad;
                Encargado encargado = new Encargado();
                encargado.nombreCompleto = reader["nombre_encargado"].ToString();
                unidad.encargado = encargado;
                asistente.unidad = unidad;
                asistentes.Add(asistente);
            }

            sqlConnection.Close();

            return asistentes;
        }

        public int insertarAsistente(Asistente asistente)
        {
            SqlConnection connection = conexion.ConexionControlAsistentes();

            String consulta
                = @"INSERT Asistente (nombre_completo,carnet,telefono,cantidad_periodos_nombrado) 
                    VALUES (@nombre,@carne,@telefono,@cantidadP);
                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@nombre", asistente.nombreCompleto);
            command.Parameters.AddWithValue("@carne", asistente.carnet);
            command.Parameters.AddWithValue("@telefono", asistente.telefono);
            command.Parameters.AddWithValue("@cantidadP", asistente.cantidadPeriodosNombrado);

            connection.Open();
            int idAsistente = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return idAsistente;
        }


    }
}
