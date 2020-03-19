using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    /// <summary>
    /// Mariela Calvo
    /// 20/noviembre/2019
    /// Clase para administrar la entidad Archivo
    /// </summary>
    public class ArchivoAsistente
    {
        public int idArchivo { get; set; }
        public Archivo expedienteAcademico { get; set; }
        public Archivo informeMatricula { get; set; }
        public Archivo cuentaBanco { get; set; }
        public Archivo CV { get; set; }
        
        
    }
}
