using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{

    /// <summary>
    /// Mariela Calvo    
    /// marzo/2020
    /// Clase para administrar el CRUD para las Nombramientos
    /// </summary>
    public class NombramientoAsistenteDatos
    {
        private ConexionDatos conexion = new ConexionDatos();



        public int insertarNombramientoAsistente(Nombramiento nombramiento)
        {
            SqlConnection connection = conexion.ConexionControlAsistentes();

            String consulta
                = @"INSERT Nombramiento (id_asistente,id_periodo,id_unidad, aprobado, cantidad_horas,induccion) 
                    VALUES (@asistente,@periodo,@unidad,@aprobado,@horas,@induccion);
                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@asistente", nombramiento.asistente.idAsistente);
            command.Parameters.AddWithValue("@periodo", nombramiento.periodo.idPeriodo);
            command.Parameters.AddWithValue("@unidad", nombramiento.unidad.idUnidad);
            command.Parameters.AddWithValue("@aprobado", nombramiento.aprobado);
            command.Parameters.AddWithValue("@horas", nombramiento.cantidadHorasNombrado);
            command.Parameters.AddWithValue("@induccion", nombramiento.recibeInduccion);

            connection.Open();
            int idNombramiento = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return idNombramiento;
        }

        public void actualizarAsistenteNombramiento(string numeroCarnet,string aprobado,string observaciones)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            String consulta = @"update Nombramiento set aprobado=@aprobado,observaciones=@observaciones,solicitud=@solicitud
                                 from Nombramiento N,Asistente A
                                 where N.id_asistente=A.id_asistente and A.carnet=@numeroCarnet";


            SqlCommand command = new SqlCommand(consulta, sqlConnection);
            command.Parameters.AddWithValue("@aprobado", aprobado);
            command.Parameters.AddWithValue("@numeroCarnet", numeroCarnet);
            command.Parameters.AddWithValue("@observaciones", observaciones);
            command.Parameters.AddWithValue("@solicitud", 1);




            sqlConnection.Open();
            command.ExecuteReader();
            sqlConnection.Close();
        }

        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Obtiene los asistentes de acuerdo a su Unidad
        /// Requiere: - 
        /// Modifica: 
        /// Devuelve: Lista de asistentes 
        /// </summary>
        public List<Nombramiento> ObtenerNombramientosPorUnidad(int idUnidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Nombramiento> nombramientos = new List<Nombramiento>();

            String consulta = @"SELECT n.induccion, n.id_nombramiento, a.id_asistente, a.nombre_completo,a.carnet,a.telefono, n.aprobado, p.semestre, p.ano_periodo,n.cantidad_horas, a.cantidad_periodos_nombrado, u.nombre as unidad,  e.nombre_completo as nombre_encargado " +
                              " FROM Asistente a JOIN Nombramiento n ON a.id_asistente=n.id_asistente JOIN Encargado_Asistente ea ON a.id_asistente=ea.id_asistente " +
                              " JOIN Encargado e ON ea.id_encargado=e.id_encargado JOIN Encargado_Unidad eu ON ea.id_encargado=eu.id_encargado JOIN Unidad u ON eu.id_unidad=u.id_unidad " +
                               " JOIN Periodo p ON n.id_periodo=p.id_periodo WHERE n.id_unidad=@idUnidad AND a.disponible=1 AND n.disponible=1";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idUnidad", idUnidad);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {

                Nombramiento nombramiento = new Nombramiento();
                nombramiento.idNombramiento = Convert.ToInt32(reader["id_nombramiento"].ToString());
                nombramiento.recibeInduccion= Convert.ToBoolean(reader["induccion"].ToString());
                nombramiento.aprobado = Convert.ToBoolean(reader["aprobado"].ToString());
                nombramiento.cantidadHorasNombrado = Convert.ToInt32(reader["cantidad_horas"].ToString());


                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_asistente"].ToString());
                asistente.nombreCompleto = reader["nombre_completo"].ToString();
                asistente.carnet = reader["carnet"].ToString();
                asistente.telefono = reader["telefono"].ToString();
                asistente.cantidadPeriodosNombrado = Convert.ToInt32(reader["cantidad_periodos_nombrado"].ToString());

                nombramiento.asistente = asistente;


                Periodo periodo = new Periodo();
                periodo.semestre = reader["semestre"].ToString();
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                nombramiento.periodo = periodo;


                Unidad unidad = new Unidad();
                unidad.nombre = reader["unidad"].ToString();
                unidad.idUnidad = idUnidad;

                Encargado encargado = new Encargado();
                encargado.nombreCompleto = reader["nombre_encargado"].ToString();
                unidad.encargado = encargado;

                nombramiento.unidad = unidad;
                nombramientos.Add(nombramiento);
            }

            sqlConnection.Close();

            return nombramientos;
        }

        // <summary>
        // Mariela Calvo
        // Noviembre/2019
        // Efecto: Elimina una unidad de la base de datos
        // Requiere: Unidad
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="idUnidad"></param>
        public void eliminarNombramiento(int idNombramiento)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            SqlCommand sqlCommand = new SqlCommand("UPDATE Nombramiento SET disponible=0 where id_nombramiento=@id_nombramiento_;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_nombramiento_", idNombramiento);

            sqlConnection.Open();

            sqlCommand.ExecuteScalar();

            sqlConnection.Close();
        }


        // <summary>
        // Mariela Calvo
        // Abril/2019
        // Efecto: Actualiza un Nombramiento de la base de datos
        // Requiere: Nombramiento
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="Unidad"></param>
        public void EditarNombramiento(Nombramiento nombramiento)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            SqlCommand sqlCommand = new SqlCommand("update Nombramiento set cantidad_horas=@horas_, induccion=@induccion_ where id_nombramiento=@id_nombramiento_;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_nombramiento_", nombramiento.idNombramiento);
            sqlCommand.Parameters.AddWithValue("@induccion_", nombramiento.recibeInduccion);
            sqlCommand.Parameters.AddWithValue("@horas_", nombramiento.cantidadHorasNombrado);

            sqlConnection.Open();

            sqlCommand.ExecuteScalar();

            sqlConnection.Close();
        }
    }

}

