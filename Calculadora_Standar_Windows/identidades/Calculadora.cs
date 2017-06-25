using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora_Standar_Windows.identidades
{
    class Calculadora
    {
        //Atributos
        private double n1, n2, r;
        private string txtN1, txtN2, txtR;

        //Getters and Setter
        public double N1
        {
            get { return n1; }
            set { n1 = value; }
        }
        public double N2
        {
            get { return n2; }
            set { n2 = value; }
        }
        public double R
        {
            get { return r; }
            set { r = value; }
        }
        public string TxtN1
        {
            get { return txtN1; }
            set { txtN1 = value; }
        }
        public string TxtN2
        {
            get { return txtN2; }
            set { txtN2 = value; }
        }
        public string TxtR
        {
            get { return txtR; }
            set { txtR = value; }
        }

        //metodos
        public bool checkCharacter(char value)
        {
            if (char.IsNumber(value)) return true;
            else if (value == '.') return true;
            else return false;
        }
        public bool checkOperation(char value)
        {
            switch (value)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '%':
                case '√':
                    return true;
                default:
                    return false;
            }
        }
        public string Calcular(char operador, string n1 = "0", string n2 = "0")
        {
            ConvertirValores(n1, n2);
            switch (operador)
            {
                case '+':
                    txtR = Sumar();
                    break;
                case '-':
                    txtR = Restar();
                    break;
                case '*':
                    txtR = Multiplicar();
                    break;
                case '/':
                    if(n2 != "0") txtR = Dividir();
                    else txtR = "No se puede dividir por 0";
                    break;
                case '√':
                    if (n1 != "0") txtR = Raiz('1');
                    else txtR = "√(0)";
                    break;
                default:
                    MessageBox.Show("Operacion Incorrecta");
                    break;
            }
            return txtR;
        }

        //metodos protegidos
        protected void ConvertirValores(string txtn1, string txtn2)
        {
            n1 = Convert.ToDouble(txtn1);
            n2 = Convert.ToDouble(txtn2);
        }
        protected string Sumar()
        {
            r = n1 + n2;
            return Convert.ToString(r);
        }
        protected string Restar()
        {
            r = n1 - n2;
            return Convert.ToString(r);
        }
        protected string Multiplicar()
        {
            r = n1 * n2;
            return Convert.ToString(r);
        }
        protected string Dividir()
        {
            r = n1 / n2;
            return Convert.ToString(r);
        }
        protected string Raiz(char n)
        {
            r = Math.Sqrt(n1);
            return Convert.ToString(r);
        }

        //testeo de funciones
        private void TestFuncion(string funcion, string valor1 = "0", string valor2 = "0")
        {
            MessageBox.Show(funcion + ": Esto se ejecuta. \nN1: " + valor1 + "\tN2:" + valor2);
        }
    }
}
