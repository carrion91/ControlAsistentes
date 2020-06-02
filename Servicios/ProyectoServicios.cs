﻿using AccesoDatos;
using Entidades;
using System.Collections.Generic;

namespace Servicios
{
    public class ProyectoServicios
    {
        ProyectoAsistenteDatos proyectoDatos = new ProyectoAsistenteDatos();

        public void insertar(Proyecto proyecto)
        {
            proyectoDatos.Insertar(proyecto);
        }

        public void insertarAsistenteProyecto(int idAsistente, int idProyecto)
        {
            proyectoDatos.AsignarAsistenteProyecto(idAsistente, idProyecto);
        }


        public List<Proyecto> ObtenerProyectos()
        {
            return proyectoDatos.ObtenerProyectos();
        }

        public List<Proyecto> ObtenerProyectoPorId(int idProyecto)
        {
            return proyectoDatos.ObtenerProyectoPorId(idProyecto);
        }

        public void Eliminar(int idProyecto)
        {
            proyectoDatos.EliminarProyecto(idProyecto);
        }

    }
}
