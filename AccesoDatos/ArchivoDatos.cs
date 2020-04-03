﻿using Entidades;
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
                = @"INSERT Archivo (fecha_creacion,nombre_archivo,ruta_archivo,tipo_archivo,creado_por) 
                    VALUES (GETDATE(),@nombreArchivo,@rutaArchivo,@tipoArchivo,@creado_por);
                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(consulta, connection);
            //command.Parameters.AddWithValue("@fechaCreacion",archivo.fechaCreacion);
            command.Parameters.AddWithValue("@nombreArchivo", archivo.nombreArchivo);
            command.Parameters.AddWithValue("@rutaArchivo", archivo.rutaArchivo);
            command.Parameters.AddWithValue("@tipoArchivo", archivo.tipoArchivo);
            command.Parameters.AddWithValue("@creado_por", archivo.creadoPor);

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
                    VALUES (@idArchivo,@idAsistente);";

            SqlCommand command = new SqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@idArchivo", idArchivo);
            command.Parameters.AddWithValue("@idAsistente", idAsistente);
            connection.Open();
            command.ExecuteScalar();
            
            connection.Close();

            return idArchivo;
        }
        /*
      * Kevin Picado
      * 20/03/20
      *recupera todos los archivos de muestras de la base de datos
      *retorna una lista de archivos
      */
        public List<Archivo> getArchivosAsistente(int idAsistente, int idPeriodo)
        {

            List<Archivo> listaArchivosAsistente = new List<Archivo>();

            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            String consulta = @"select nombre_archivo,ruta_archivo,tipo_archivo,creado_por
                                   from Asistente A,Nombramiento N,Archivo Ar,Archivo_Nombramiento AN,Periodo P
                                   where A.id_asistente=N.id_asistente and AN.id_nombramiento=N.id_nombramiento and N.id_periodo=P.id_periodo and 
                                   AN.id_archivo=Ar.id_archivo and A.id_asistente=@idAsistente and P.id_periodo=@idPeriodo";

            SqlCommand command = new SqlCommand(consulta, sqlConnection);

            command.Parameters.AddWithValue("@idAsistente", Convert.ToInt32(idAsistente));
            command.Parameters.AddWithValue("@idPeriodo", Convert.ToInt32(idPeriodo));


            SqlDataReader reader;
            sqlConnection.Open();
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Archivo archivo = new Archivo();

                
                archivo.rutaArchivo = reader["ruta_archivo"].ToString();
                archivo.nombreArchivo = reader["nombre_archivo"].ToString();
                archivo.creadoPor = reader["creado_por"].ToString();
                listaArchivosAsistente.Add(archivo);
            }

            sqlConnection.Close();

            return listaArchivosAsistente;
        }
    }
}
