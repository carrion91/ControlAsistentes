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
    /// 27/noviembre/2019
    /// Clase para administrar el CRUD para las Periodoes
    /// </summary>
    public class PeriodoDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos los periodos de la base de datos
        /// </summary>
        /// <returns>Retorna una lista <code>LinkedList<Periodo></code> que contiene todos los periodos</returns>
        public List<Periodo> ObtenerPeriodos()
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Periodo> periodos = new List<Periodo>();
            SqlCommand sqlCommand = new SqlCommand("SELECT id_periodo,ano_periodo, habilitado,semestre FROM Periodo; ", sqlConnection);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Periodo periodo = new Periodo();
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                periodo.habilitado = Convert.ToBoolean(reader["habilitado"].ToString());
                periodo.semestre = reader["semestre"].ToString();
                periodos.Add(periodo);
            }
            sqlConnection.Close();

            return periodos;
        }

        public Periodo ObtenerPeriodoActual()
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            SqlCommand sqlCommand = new SqlCommand("SELECT id_periodo,ano_periodo, habilitado, semestre FROM Periodo WHERE habilitado=1; ", sqlConnection);
            Periodo periodo = new Periodo();
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                periodo.habilitado = Convert.ToBoolean(reader["habilitado"].ToString());
                periodo.semestre = reader["semestre"].ToString();
            }
            sqlConnection.Close();
            return periodo;
        }

        /// <summary>
        /// Primer deshabilita todos los periodos existentes, y habilita el periodo indicado según los criterios del usuario
        /// </summary>
        /// <param name="anoPeriodo">Valor de tipo <code>int</code> que representa el año del periodo que se desea habilitar</param>
        /// <returns>Retorna si el periodo fue habilitado</returns>
        public bool HabilitarPeriodo(int anoPeriodo)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            bool habilitado = false;
            SqlCommand sqlCommandDeshabilitarTodos = new SqlCommand("update Periodo set habilitado=@habilitado_;", sqlConnection);
            sqlCommandDeshabilitarTodos.Parameters.AddWithValue("@habilitado_", 0);

            SqlCommand sqlCommandHabilitarPeriodo = new SqlCommand("update Periodo set habilitado=@habilitado_ output INSERTED.ano_periodo where ano_periodo=@ano_periodo_;", sqlConnection);
            sqlCommandHabilitarPeriodo.Parameters.AddWithValue("@ano_periodo_", anoPeriodo);
            sqlCommandHabilitarPeriodo.Parameters.AddWithValue("@habilitado_", 1);

            sqlConnection.Open();
            sqlCommandDeshabilitarTodos.ExecuteScalar();
            int idPeriodo = (int)sqlCommandHabilitarPeriodo.ExecuteScalar();

            if (idPeriodo > 0)
            {
                habilitado = true;
            }

            sqlConnection.Close();

            return habilitado;
        }
    }
}
