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
    /// Karen Guillén A
    /// 13/05/2020
    /// Clase para administrar el CRUD de los Proyectos
    /// </summary>
    public class ProyectoAsistenteDatos
    {
        private ConexionDatos conexion = new ConexionDatos();
        private NombramientoAsistenteDatos nombramientoasistenteDatos = new NombramientoAsistenteDatos();

        public List<Proyecto> ObtenerProyectos()
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Proyecto> proyectos = new List<Proyecto>();
            SqlCommand sqlCommand = new SqlCommand(@"select p.id_proyecto, p.nombre, p.descripcion, p.fecha_inicio, p.fecha_finalizacion, p.disponible, p.finalizado, ap.id_asistente "
                                                    +"from Proyecto p join Asistente_Proyecto ap on p.id_proyecto = ap.id_proyecto " +
                                                    "where p.disponible=1", sqlConnection);

            List<Nombramiento> nombramientos = new List<Nombramiento>();
            nombramientos = nombramientoasistenteDatos.ObtenerNombramientos();

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Proyecto proyecto = new Proyecto();
                proyecto.idProyecto = Convert.ToInt32(reader["id_proyecto"].ToString());
                proyecto.nombre = reader["nombre"].ToString();
                proyecto.descripcion = reader["descripcion"].ToString();
                proyecto.fechaInicio = Convert.ToDateTime(reader["fecha_inicio"].ToString());
                proyecto.fechaFinalizacion = Convert.ToDateTime(reader["fecha_finalizacion"].ToString());
                proyecto.disponible = Convert.ToBoolean(reader["disponible"].ToString());
                proyecto.finalizado = Convert.ToBoolean(reader["finalizado"].ToString());
                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_asistente"].ToString());

                //Aquí no utilio el ObtenerAsistentesPorId, por el @id_unidad
                foreach(Nombramiento asist in nombramientos)
                {
                    if (asist.asistente.idAsistente==asistente.idAsistente)
                    {
                      
                        asistente = asist.asistente;
                        asistente.unidad = asist.unidad;
                    }
                }         
                


                proyecto.asistente = asistente;

                proyectos.Add(proyecto);
            }
            sqlConnection.Close();

            return proyectos;
        }

        /// <summary>
        /// Karen Guillén A
        /// 13/05/2020
        /// Metodo para insertar proyectos
        /// </summary>
        public void Insertar(Proyecto proyecto)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            SqlCommand sqlCommand = new SqlCommand("insert into Proyecto (nombre, descripcion, fecha_inicio, fecha_finalizacion, disponible, finalizado) " +
                "VALUES (@nombre, @descripcion, @fecha_inicio, @fecha_finalizacion, @disponible, @finalizado );", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@nombre", proyecto.nombre);
            sqlCommand.Parameters.AddWithValue("@descripcion", proyecto.descripcion);
            sqlCommand.Parameters.AddWithValue("@fecha_inicio", proyecto.fechaInicio);
            sqlCommand.Parameters.AddWithValue("@fecha_finalizacion", proyecto.fechaFinalizacion);
            sqlCommand.Parameters.AddWithValue("@disponible", proyecto.disponible);
            sqlCommand.Parameters.AddWithValue("@finalizado", proyecto.finalizado);

            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlConnection.Open();
                sqlCommand.ExecuteReader();
            sqlConnection.Close();

            //int insertaAsistente = InsertarAsistenteProyecto(proyecto.asistente.idAsistente, proyecto.idProyecto);

        }


        // <summary>
        // Karen Guillén A.
        // 13/05/2020
        // Efecto: Retorna el Proyecto de acuerdo a su id
        // Requiere: Proyecto
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="idProyecto"></param>
        public List<Proyecto> ObtenerProyectoPorId(int idProyecto)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            SqlCommand sqlCommand = new SqlCommand(@"select p.id_proyecto, p.nombre, p.descripcion, p.fecha_inicio, p.fecha_finalizacion, p.disponible, p.finalizado, ap.id_asistente "
                                                    + "from Proyecto p  join Asistente_Proyecto ap on p.id_proyecto = ap.id_proyecto " +
                                                    "where p.disponible=1 and p.id_proyecto = @idProyecto_; ", sqlConnection);

            List<Proyecto> proyectos = new List<Proyecto>();
            List<Nombramiento> nombramientos = new List<Nombramiento>();
            nombramientos = nombramientoasistenteDatos.ObtenerNombramientos();

            sqlCommand.Parameters.AddWithValue("@idProyecto_", idProyecto);
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {

                Proyecto proyecto = new Proyecto();
                proyecto.idProyecto = Convert.ToInt32(reader["id_proyecto"].ToString());
                proyecto.nombre = reader["nombre"].ToString();
                proyecto.descripcion = reader["descripcion"].ToString();
                proyecto.fechaInicio = Convert.ToDateTime(reader["fecha_inicio"].ToString());
                proyecto.fechaFinalizacion = Convert.ToDateTime(reader["fecha_finalizacion"].ToString());
                proyecto.disponible = Convert.ToBoolean(reader["disponible"].ToString());
                proyecto.finalizado = Convert.ToBoolean(reader["finalizado"].ToString());
                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_asistente"].ToString());

                //Aquí no utilizo el ObtenerAsistentesPorId, por el @id_unidad
                foreach (Nombramiento asist in nombramientos)
                {
                    if (asist.asistente.idAsistente == asistente.idAsistente)
                    {

                        asistente = asist.asistente;
                        asistente.unidad = asist.unidad;
                    }
                }


                proyecto.asistente = asistente;

                proyectos.Add(proyecto);
            }
            sqlConnection.Close();

            return proyectos;
        }

        // <summary>
        // Karen Guillén A.
        // 29/05/2020
        // Efecto: Asigna un asistente al proyecto asignado
        // Requiere: idProyecto y idAsistente
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="idProyecto", name="idAsistente"></param>
        public void AsignarAsistenteProyecto(int idProyecto, int idAsistente)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            String consulta = "INSERT INTO  Asistente_Proyecto (id_proyecto, id_asistente)" 
                                +"VALUES(@idProyecto, @idAsistente); ";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idProyecto", idProyecto);
            sqlCommand.Parameters.AddWithValue("@idAsistente", idAsistente);
           

            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlConnection.Open();
            sqlCommand.ExecuteReader();
            sqlConnection.Close();
        }


        // <summary>
        // Karen Guillén A.
        // 13/05/2020
        // Efecto: Elimina una proyecto de la base de datos
        // Requiere: Proyecto
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="idProyecto"></param>
        public void EliminarProyecto(int idProyecto)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            SqlCommand sqlCommand = new SqlCommand("UPDATE Proyecto SET disponible=0 where id_proyecto=@id_proyecto_;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_proyecto_", idProyecto);

            sqlCommand.CommandType = System.Data.CommandType.Text;

            sqlConnection.Open();
                sqlCommand.ExecuteReader();
            sqlConnection.Close();
        }

        // <summary>
        // Karen Guillén A.
        // 29/05/2020
        // Efecto: Elimina el asistente asociado al proyecto
        // Requiere: id del asistente y id del proyecto
        // Modifica: tabla de proyectos
        // Devuelve: -
        // </summary>
        // <param name="Proyecto"></param>
        public void EliminarAsistenteProyecto(int idAsistente, int idProyecto)
        {

           string consulta = "delete from Asistente_Proyecto where id_proyecto = @idProyecto and id_asistente = @idAsistente";

            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idAsistente", idAsistente);
            sqlCommand.Parameters.AddWithValue("@id_proyecto_", idProyecto);

            sqlCommand.CommandType = System.Data.CommandType.Text;

            sqlConnection.Open();
                sqlCommand.ExecuteReader();
            sqlConnection.Close();
        }

        // <summary>
        // Karen Guillén A.
        // 13/05/2020
        // Efecto: Actualiza un proyecto de la base de datos
        // Requiere: Proyecto
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="Proyecto"></param>
        public void Editar(Proyecto proyecto)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            SqlCommand sqlCommand = new SqlCommand("update Proyecto set nombre=@nombre, descripcion=@descripcion, fecha_inicio=@fechaInicio, fecha_finalizacion=@fechaFinalizacion," +
                "disponible=@disponible, finalizado=@finalizado  where id_proyecto=@idProyecto;", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@idProyecto", proyecto.idProyecto);
            sqlCommand.Parameters.AddWithValue("@nombre", proyecto.nombre);
            sqlCommand.Parameters.AddWithValue("@descripcion", proyecto.descripcion);

            sqlCommand.Parameters.AddWithValue("@fechaInicio", proyecto.fechaInicio);
            sqlCommand.Parameters.AddWithValue("@fechaFinalizacion", proyecto.fechaFinalizacion);
            sqlCommand.Parameters.AddWithValue("@disponible", proyecto.disponible);
            sqlCommand.Parameters.AddWithValue("@finalizado", proyecto.finalizado);

            sqlConnection.Open();
                sqlCommand.ExecuteScalar();
            sqlConnection.Close();
        }


    }
}
