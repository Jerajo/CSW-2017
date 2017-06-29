using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_Standar_Windows.identidades
{
    public partial class Dibujador
    {
        //atributos
        private char step;
        private string n1, operacion, n2, resultado;
        private double[] memoria = new double[5];

        //constructor
        public void Actualizar(char stp, string N1, string opr, string N2, string rsl, double[] mer)
        {
            step = stp;
            n1 = N1;
            operacion = opr;
            n2 = N2;
            resultado = rsl;
            memoria = mer;
        }

        //metodos
        public string DrawText(string value = "")
        {
            //TestFunction("DrawText");
            if (value != "")
            {
                if (value == "Clean") return "0";
                else if (value == "CleanE")
                {
                    if (step == '1' || step == '4') return "0";
                    else return operacion + "\n0";
                }
                else return "System Error";
            }
            else if (step == '1') return FormatoDecimal(n1);
            else if (step == '2') return operacion + "\n" + FormatoDecimal(memoria[3].ToString());
            else if (step == '3') return operacion + "\n" + FormatoDecimal(n2);
            else if (step == '4') return FormatoDecimal(resultado);
            else if (step == 'S') return operacion + "\n" + FormatoDecimal(memoria[4].ToString());
            else if (step == 'P') return operacion + "\n" + FormatoDecimal(memoria[4].ToString());
            else if (step == 'M') return operacion + "\n" + FormatoDecimal(memoria[3].ToString());
            else return "System Error";
        }

        private string FormatoDecimal(string value)
        {
            if (value.Substring(0, 1) == "0") return value;
            else if (value.Length >= 20) return value; // return string.Format("{0:E,n0}", Convert.ToDouble(value)); // error
            else if (value.Contains("."))
            {
                int index = value.LastIndexOf(".");
                int length = value.Length;
                int distance = length - index;
                return string.Format("{0:#,##0.}", Convert.ToDouble(value)) + value.Substring(index, distance);
            }
            else return string.Format("{0:#,##0}", Convert.ToDouble(value));
        }
    }
}
