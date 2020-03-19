using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class NombramientoServicios {


        NombramientoAsistenteDatos nombramientoDatos = new NombramientoAsistenteDatos();


        public int insertarNombramiento(Nombramiento nombramiento)         
        {
            return nombramientoDatos.insertarNombramientoAsistente(nombramiento);
        }
    }
}
