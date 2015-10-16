using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GeneradorCompiladores
{
    class Lexico
    {
        public Token T = new Token();
        public StreamReader archivo;
        public int Linea = 0;

        public int ST = 0;
        public int SNT = 1;
        public int Flechita = 2;
        public int ORIzq = 3;
        public int ORDer = 4;
        public int EpsilonIzq = 5;
        public int EpsilonDer = 6;
        public int FinProduccion = 7;

        public Lexico()
        {
            archivo = new StreamReader("C:\\java\\prueba.prd");
            NextToken();
        }

        public Lexico(string p_nombre)
        {
            archivo = new StreamReader(p_nombre + ".prd");
            NextToken();
        }
        private int Columna(Char c)
        {
            if (Char.IsWhiteSpace(c))
                return 0;
            else if (Char.IsLower(c))
                return 1;
            else if (Char.IsUpper(c))
                return 7;
            else if (c == '\\')
                return 2;
            else if (c == '[')
                return 3;
            else if (c == ']')
                return 4;
            else if (c == '{')
                return 5;
            else if (c == '}')
                return 6;
            else if (c == '-')
                return 8;
            else if (c == '>')
                return 9;
            else if (c == ';')
                return 10;
            else
                return 11;
        }

        public void NextToken()
        {
            int estado = 0, F = -1, E = -2;
            String contenido = null;
            int clasificacion = 0;
            Char c;
            /*                 WS, l, \,  [,  ],  {,  }, L, -,  >,  ;,  X      */
            int[,] TRAND = { {  0, 1, 2, 11, 11, 11, 11, 8, 9, 11, 11, 11},    //0
                             {  F, 1, F,  F,  F,  F,  F, F, F,  F,  F,  F},    //1
                             {  F, F, 7,  3,  4,  5,  6, F, F,  F, 12,  F},    //2
                             {  F, F, F,  F,  F,  F,  F, F, F,  F,  F,  F},    //3
                             {  F, F, F,  F,  F,  F,  F, F, F,  F,  F,  F},    //4
                             {  F, F, F,  F,  F,  F,  F, F, F,  F,  F,  F},    //5
                             {  F, F, F,  F,  F,  F,  F, F, F,  F,  F,  F},    //6
                             {  F, F, F,  F,  F,  F,  F, F, F,  F,  F,  F},    //7
                             {  F, 8, F,  F,  F,  F,  F, 8, F,  F,  F,  F},    //8
                             {  F, F, F,  F,  F,  F,  F, F, F, 10,  F,  F},    //9
                             {  F, F, F,  F,  F,  F,  F, F, F,  F,  F,  F},    //10
                             {  F, F, F,  F,  F,  F,  F, F, F,  F,  F,  F},    //11
                             {  F, F, F,  F,  F,  F,  F, F, F,  F,  F,  F},    //12
                             };
            while (estado != F)
            {
                c = (char)archivo.Peek();
                if (c == 10)
                    Linea++;
                estado = TRAND[estado, Columna(c)];
                if (estado == 1)
                    clasificacion = ST;
                else if (estado == 2)
                    clasificacion = ST;
                else if (estado == 3)
                    clasificacion = EpsilonIzq;
                else if (estado == 4)
                    clasificacion = EpsilonDer;
                else if (estado == 5)
                    clasificacion = ORIzq;
                else if (estado == 6)
                    clasificacion = ORDer;
                else if (estado == 7)
                    clasificacion = ST;
                else if (estado == 8)
                    clasificacion = SNT;
                else if (estado == 9)
                    clasificacion = ST;
                else if (estado == 10)
                    clasificacion = Flechita;
                else if (estado == 11)
                    clasificacion = ST;
                else if (estado == 12)
                    clasificacion = FinProduccion;
                else if (estado == 0)
                    contenido = null;
                if (estado > 0)
                    contenido += c;
                if (estado >= 0)
                {
                    c = (char)archivo.Read();
                    if (c == 11)
                        Linea++;
                }
            }
            //if (estado == E)
            //{
            //    Console.WriteLine("Error +Lexico: Se esperaba un digito.");
            //}
            T.SET(contenido, clasificacion);
            if (archivo.EndOfStream)
                estado = F;
        }
        public string convert(int p_clasificacion)
        {
            switch (p_clasificacion)
            {
                case 0: return "Simbolo Terminal";
                case 1: return "Simbolo No Terminal";
                case 2: return "Flechita";
                case 3: return "Operador OR izquierda";
                case 4: return "Operador OR derecha";
                case 5: return "Operador ? izquierda";
                case 6: return "Operador ? derecha";
                case 7: return "FinProduccion";
                default: return "Clasificacion no definida";
            }
        }
    }
}
