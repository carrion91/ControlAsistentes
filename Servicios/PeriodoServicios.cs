using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    /// <summary>
    /// Mariela Calvo
    /// marzo/2020
    /// Clase para administrar los servicios de Periodo
    /// </summary>
    public class PeriodoServicios
    {
        PeriodoDatos periodoDatos = new PeriodoDatos();


        public List<Periodo> ObtenerPeriodos()
        {
            return this.periodoDatos.ObtenerPeriodos();
        }


        public bool HabilitarPeriodo(int anoPeriodo)
        {
            return periodoDatos.HabilitarPeriodo(anoPeriodo);
        }


        public int InsertarPeriodo(Periodo periodo)
        {
            return periodoDatos.InsertarPeriodo(periodo);
        }

        public void EliminarPeriodo(int anoPeriodo)
        {
           periodoDatos.EliminarPeriodo(anoPeriodo);
        }
    }
}
