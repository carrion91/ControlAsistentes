using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class ArchivoDatos
    {

        private ConexionDatos conexion = new ConexionDatos();


        public Archivo obtenerArchivosAsistente(int idAsistente)
        {
            return new Archivo();
        }


        public int insertarArchivo(Archivo archivo)
        {
            SqlConnection connection = conexion.ConexionControlAsistentes();

            String consulta
                = @"INSERT Archivo (fecha_creacion,nombre_archivo,ruta_archivo,tipo_archivo) 
                    VALUES (@fechaCreacion,@nombreArchivo,@rutaArchivo,@tipoArchivo);
                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@fechaCreacion", archivo.fechaCreacion);
            command.Parameters.AddWithValue("@nombreArchivo", archivo.nombreArchivo);
            command.Parameters.AddWithValue("@rutaArchivo", archivo.rutaArchivo);
            command.Parameters.AddWithValue("@tipoArchivo", archivo.tipoArchivo);

            connection.Open();
            int idArchivo = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return idArchivo;
        }


        public int insertarArchivoAsistente(int idArchivo,int idAsistente)
        {
            SqlConnection connection = conexion.ConexionControlAsistentes();

            String consulta
                = @"INSERT Archivo_Asistente(id_archivo,id_asistente) 
                    VALUES (@idArchivo,@idAsistente);
                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@idArchivo", idArchivo);
            command.Parameters.AddWithValue("@idAsistente", idAsistente);
           

            connection.Open();
            connection.Close();

            return idArchivo;
        }
    }
}
