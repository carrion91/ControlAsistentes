using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class AsistenteServicios
    {
        AsistenteDatos asistenteDatos = new AsistenteDatos();


        public List<Asistente> ObtenerAsistentes()
        {
            return asistenteDatos.ObtenerAsistentes();
        }


        public List<Asistente> ObtenerAsistentesPorUnidad(int idUnidad)
        {
            return asistenteDatos.ObtenerAsistentesPorUnidad(idUnidad);
        }

        public int insertarAsistente(Asistente asistente)
        {
            return asistenteDatos.insertarAsistente(asistente);
        }

		/// <summary>
		/// Jesús Torres
		/// 02/abr/2020
		/// Efecto: Obtiene los asistentes de la capa de datos 
		/// Requiere: - 
		/// Modifica: 
		/// Devuelve: Lista de asistentes 
		/// </summary>
		public List<Asistente> ObtenerAsistentesSinUsuarios()
		{
			return asistenteDatos.ObtenerAsistentesSinUsuarios();
		}
		
	}
}
