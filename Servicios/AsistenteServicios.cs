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
        AsistenteDatos asistenteDatos=new AsistenteDatos();


        public List<Asistente> ObtenerAsistentes()
        {
            return asistenteDatos.ObtenerAsistentes();
        }


        public List<Asistente> ObtenerAsistentesPorUnidad(int idUnidad)
        {
            return asistenteDatos.ObtenerAsistentesPorUnidad(idUnidad);
        }
    }
}
