using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GeneradorCompiladores
{
    class Lenguaje:Sintaxis
    {
        public StreamWriter salida;
        private string PrimeraProd;
        private bool EsPrimeraProd = false;
        public Lenguaje( )
        {
            salida = new StreamWriter("C:\\java\\prueba.cs");
        }

        public Lenguaje(string p_nombre) : base(p_nombre)
        {
            salida = new StreamWriter(p_nombre + ".cs");
        }

        public void Producciones( )
        {
            produccion();
            if (T.GETClasifcacion() == SNT)
                Producciones();
        }
        public void produccion()
        {
            if (EsPrimeraProd == false)
            {
                EsPrimeraProd = true;
                PrimeraProd = T.GETContenido();
            }
            salida.WriteLine("      public void "+ T.GETContenido() + "()");
            MATCH(SNT);
            salida.WriteLine("      {");
            MATCH(Flechita);
            LadoDerecho();
            MATCH(FinProduccion);
            salida.WriteLine("      }");
        }

        public void LadoDerecho( )
        {
            if (T.GETClasifcacion()==ST)
            {
                salida.Write("          MATCH(");
                if (T.GETContenido() == "identificador")
                    salida.Write("identificador");
                else if (T.GETContenido() == "numero")
                    salida.Write("numero");
                else if (T.GETContenido() == "caracter")
                    salida.Write("caracter");
                else
                    salida.Write("\"" + T.GETContenido() + "\"");
                salida.WriteLine(");");
                MATCH(ST);
            }
            else if(T.GETClasifcacion()==SNT)
            {
                salida.WriteLine("          " + T.GETContenido() + "();");
                MATCH(SNT);
            }
            else if(T.GETClasifcacion() == EpsilonIzq)
            {
                MATCH(EpsilonIzq);
                simbolos();
                MATCH(EpsilonDer);
            }
            else if(T.GETClasifcacion() == ORIzq)
            {
                MATCH(ORIzq);
                MATCH(ORDer);
            }
            if (T.GETClasifcacion() != FinProduccion)
                LadoDerecho();
        }

        private void simbolos()
        {
            do
            salida.Write("          if(");
            while(T.GETClasifcacion == FinProduccion) {
              if (T.GETContenido() == "identificador")
              {
                  salida.Write("T.GETClasificacion() == identificador");
                  salida.WriteLine(")");
                  salida.WriteLine("          {");
                  salida.WriteLine("              MATCH(identificador);");
              }
              else if (T.GETContenido() == "numero")
              {
                  salida.Write("T.GETClasificacion() == numero");
                  salida.WriteLine(")");
                  salida.WriteLine("          {");
                  salida.WriteLine("              MATCH(Numero);");
              }
              else if (T.GETContenido() == "caracter")
              {
                  salida.Write("T.GETClasificacion() == caracter");
                  salida.WriteLine(")");
                  salida.WriteLine("          {");
                  salida.WriteLine("              MATCH(Caracter);");
              }
              else
              {
                  salida.Write("T.GETContenido() == \"" + T.GETContenido() + "\"");
                  salida.WriteLine(")");
                  salida.WriteLine("          {");
                  salida.Write("              MATCH(\"" + T.GETContenido() + "\"");
                  salida.WriteLine(");");
              }
              MATCH(ST);
              salida.WriteLine("          }");

          }

        }
        public void principal( )
        {
            salida.WriteLine("using System;");
            salida.WriteLine("using System.Collections.Generic;");
            salida.WriteLine("using System.Linq;");
            salida.WriteLine("using System.Text;");
            salida.WriteLine("using System.Threading.Tasks;");
            salida.WriteLine("using System.IO;");
            salida.WriteLine("namespace Compilador");
            salida.WriteLine("{");
            salida.WriteLine("  class Lenguaje:Sintaxis");
            salida.WriteLine("  {");
            Producciones();
            salida.WriteLine("  }");
            Programa();
            salida.WriteLine("}");
        }
        public void Programa( )
        {
            salida.WriteLine("");
            salida.WriteLine("  class Program");
            salida.WriteLine("  {");
            salida.WriteLine("      static void Main(string[] args)");
            salida.WriteLine("      {");
            salida.WriteLine("          Lenguaje l = new Lenguaje(\"C:\\java\\prueba\");");
            salida.WriteLine("          l."+PrimeraProd+"();");
            salida.WriteLine("          Console.ReadKey();");
            salida.WriteLine("      }");
            salida.WriteLine("  }");
        }
    }
}
