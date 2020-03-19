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
    }
}
