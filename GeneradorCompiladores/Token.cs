using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorCompiladores
{
    class Token
    {
        private string contenido;
        private int clasificacion;

        public void SET (string p_contenido, int p_clasificacion)
        {
            contenido = p_contenido;
            clasificacion = p_clasificacion;
        }
                
        public string GETContenido()
        {
            return contenido;
        }

        public int GETClasifcacion()
        {
            return clasificacion;
        }
    }
}