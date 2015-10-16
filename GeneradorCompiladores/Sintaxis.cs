using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorCompiladores
{
    class Sintaxis:Lexico
    {
        public void MATCH(string p_esperado)
        {
            if (T.GETContenido() == p_esperado)
                NextToken();
            else
                Console.WriteLine("Error de sintaxis: Se espera un " + p_esperado + " en la linea: " + Linea);
        }

        public void MATCH(int p_esperado)
        {
            //Console.WriteLine("Contenido = " + T.GETContenido());
            //Console.WriteLine("Clasificacion = " + T.GETClasifcacion());
            if (T.GETClasifcacion() == p_esperado)
                NextToken();
            else
                Console.WriteLine("\tError de sintaxis: Se espera un " + convert(p_esperado) + " en la linea: " + Linea);
        }
        public Sintaxis( )
        {
        }

        public Sintaxis(string p_nombre) : base(p_nombre)
        {
        }
    }
}
