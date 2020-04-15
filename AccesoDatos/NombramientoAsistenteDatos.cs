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

    }
}
