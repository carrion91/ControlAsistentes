using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ArchivoServicios
    {
        ArchivoDatos archivoDatos = new ArchivoDatos();

        public int insertarArchivo(Archivo archivo)
        {
            return archivoDatos.insertarArchivo(archivo);
        }
    }
}

