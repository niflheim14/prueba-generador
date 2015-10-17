using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorCompiladores
{
    class Program
    {
        static void Main(string[] args)
        {
            Lenguaje l = new Lenguaje("C:\\java\\prueba");
            //while (!l.archivo.EndOfStream)
            //{
            //    Console.WriteLine(l.T.GETContenido() + "" + l.convert(l.T.GETClasifcacion()));
            //    l.NextToken();
            //    Console.ReadKey();
            //}
            l.principal();
            l.salida.Close();
            Console.WriteLine("Programa generado");
            Console.ReadKey();
Console.WriteLine("Esto es un cambio");
        }
    }
}
