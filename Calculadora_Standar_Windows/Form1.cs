using Calculadora_Standar_Windows.identidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// repositorio de github
namespace Calculadora_Standar_Windows
{
    public partial class Form1 : Form
    {
        //variables e instancias
        Calculadora caluladora = new Calculadora();
        bool esfraccion;
        string operacion, resultado;
        string n1 = "0", n2 = "0";
        char operador, step;
        int nOperaciones = 0;
        private int plus = 0;

        public Form1()
        {
            InitializeComponent();
            
            // es para que al principio aparezca desactivados los botones de memroia
            activateButton(false);
        }

        //copia el texto en pantalla
        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtCal.Text);
        }

        //pegar el texto en pantalla
        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCal.Text += Clipboard.GetText();
        }

        // borra digito por digito
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (step == '1')
            {
                if (n1 != "0" && n1.Length != 0) n1 = n1.Remove(n1.Length - 1, 1);
                if(n1.Length == 0) n1 = "0";
            }
            else if (step == '3')
            {
                if (n2 != "0" && n2.Length != 0) n2 = n2.Remove(n2.Length - 1, 1);
                if (n2.Length == 0) n2 = "0";
            }
            DrawText();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetCalculadora();
            this.KeyPreview = true;

            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);
            }
        }

        // previene funciones del teclado.
        private void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }

        //seleciona la tecla de igual
        private void AutoReset()
        {
            this.ActiveControl = btnResultado;
        }
        
        //botones numericos
        private void btn9_Click(object sender, EventArgs e)
        {
            IngresarNumero('9');
            DrawText();
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            IngresarNumero('8');
            DrawText();
        }
        private void btn7_Click(object sender, EventArgs e)
        {
            IngresarNumero('7');
            DrawText();
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            IngresarNumero('6');
            DrawText();
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            IngresarNumero('5');
            DrawText();
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            IngresarNumero('4');
            DrawText();
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            IngresarNumero('3');
            DrawText();
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            IngresarNumero('2');
            DrawText();
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            IngresarNumero('1');
            DrawText();
        }
        private void btn0_Click(object sender, EventArgs e)
        {
            IngresarNumero('0');
            DrawText();
        }
        private void btnPunto_Click(object sender, EventArgs e)
        {
            if (!esfraccion) IngresarNumero('.');
            esfraccion = true;
            DrawText();
        }

        //botones de funciones

        //limpia el dijito inferior
        private void btnClearE_Click(object sender, EventArgs e)
        {  
            if (step == '1') n1 = "0";
            else if (step == '3') n2 = "0";
            DrawText("CleanE");
        }

        //limpia todo el texto
        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetCalculadora();
        }

        // botones de operaciones
        private void btnSumar_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('+');           
        }
        private void btnRestar_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('-');
        }
        private void btnMultiplicar_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('*');
        }
        private void btnDividir_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('/');
        }

        //botones modificadores
        private void btnRaiz_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('√');
            btnResultado_Click(null, null);
            NextStep('4');
            DrawText();
        }
        private void btnProciento_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('%');
        }
        private void btnMasoMenos_Click(object sender, EventArgs e)
        {
            if (step == '2' || step == 'P') NextStep();
            if (step == '1')
            {
                if (n1.Contains("-")) n1 = n1.Remove(0, 1);
                else n1 = n1.Insert(0, "-");
            }
            else if (step == '3')
            {
                if (n2.Contains("-")) n2 = n2.Remove(0, 1);
                else n2 = n2.Insert(0, "-");
            }
            else if (step == '4')
            {
                n2 = resultado;
                if (n2.Contains("-")) n2 = n2.Remove(0, 1);
                else n2 = n2.Insert(0, "-");
                resultado = n2;
            }
            DrawText();
            AutoReset();
        }

        // resultados o calcular ecuacion
        private void btnResultado_Click(object sender, EventArgs e)
        {
            if (operador == '√')
            {
                if (step == '1') resultado = caluladora.Calcular(operador, n1);
                if (step == '3') resultado = caluladora.Calcular(operador, n2);
                if (step == '4') resultado = caluladora.Calcular(operador, resultado);    
            }
            else if (step == '2')
            {
                resultado = caluladora.Calcular(operador, n1, n1);
                NextStep('4');
            }
            else if (step == '3')
            {
                resultado = caluladora.Calcular(operador, n1, n2);
                NextStep();          
                if (step == 'P')
                {
                    n1 = resultado;
                    n2 = resultado;
                }
            }
            else if (step == '4')
            {
                n1 = resultado;
                resultado = caluladora.Calcular(operador, n1, n2);
            }
            DrawText();
        }

        // detecta las teclas del teclado
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {        
            // detectar numereos o punto
            if (caluladora.checkCharacter(e.KeyChar))
            {
                if (e.KeyChar != '.')
                {
                    IngresarNumero(e.KeyChar);
                    DrawText();
                }
                else if(!esfraccion)
                {
                    esfraccion = true;
                    IngresarNumero(e.KeyChar);
                    DrawText();
                }
            }
            else if (caluladora.checkOperation(e.KeyChar)) //detecta operaciones por teclado * / + -
            {
                AlmacenarOperacion(e.KeyChar);
            }

            if (e.KeyChar == (char)8)
            {
                btnDelete_Click(null, null);
            }
        }

        //ingresar los numero por botones o por teclado
        private void IngresarNumero(char value)
        {
            if (step == '4') ResetCalculadora();
            if (step == '2') NextStep();

            if (step == '1')
            {
                if (n1 == "0" && value != '.') n1 = "";
                n1 += value;
            } 
            else if (step == '3')
            {
                if (n2 == "0" && value != '.') n2 = "";
                n2 += value;
            }   
        }

        // ingresar operaciones por botones o por teclado
        private void AlmacenarOperacion(char value)
        {
            if (value != '%' && value != '√') nOperaciones++;
            operador = value;

            if(step == '4')
            {
                n1 = resultado;
                n2 = resultado;
                operacion = n1 + " " + operador;
            }
            else if(nOperaciones >= 2)
            {
                operacion += n2 + " " + operador;
                NextStep('P');
                btnResultado_Click(null, null);
            }
            else
            {
                if (operador == '√') operacion = operador + " " + n1;
                else operacion = n1 + " " + operador;
            }
            esfraccion = false;
            if (value == '√') NextStep('4');
            else if (step == '4') NextStep('4');
            else NextStep();
            DrawText();
        }

        //resetea la calculadora
        private void ResetCalculadora()
        {
            n1 = "0";
            operador = ' ';
            n2 = "0";
            operacion = "";
            resultado = "";
            esfraccion = false;
            nOperaciones = 0;
            NextStep('1');
            DrawText();
        }

       

        //muestra todas las interacciones
        private void DrawText(string value = "")
        {
            //TestFunction("DrawText");
            if (value != "")
            {
                if (value == "Clean") txtCal.Text = "0";
                else if (value == "CleanE")
                {
                    if (step == '1' || step == '4') txtCal.Text = "0";
                    else txtCal.Text = operacion + "\n0";
                }
            }  
            else if (step == '1') txtCal.Text = n1;
            else if (step == '2') txtCal.Text = operacion + "\n" + n1;           
            else if (step == '3') txtCal.Text = operacion + "\n" + n2;
            else if (step == '4') txtCal.Text = resultado;
            else if (step == 'S') txtCal.Text = operacion + "\n" + n2;
            else if (step == 'P') txtCal.Text = operacion + "\n" + n2;
            AutoReset();
        }

       

        //pasa step por step
        private void NextStep(char value = '0')
        {
            if (value != '0')
            {
                step = value;
            }
            else 
            {
                switch (step)
                {
                    case '1':
                        step = '2';
                        break;
                    case '2':
                        step = '3';
                        break;
                    case '3':
                    case 'P':
                    case 'S':
                    case 'M':
                        step = '4';
                        break;
                    case '4':
                        step = '1';
                        break;
                    default:
                        break;
                }

            }
            
        }


        //muestra un mensaje de prueva
        private void TestFunction(string indicador)
        {
            MessageBox.Show(indicador + ": se ejecuta.");
        }
        // ESTE es para el button M+ suma lo que hay en memoria 
        private void btnMplus_Click(object sender, EventArgs e)
        {
            plus = plus + Convert.ToInt32(txtCal.Text);

        }
        // Este es el button M-  resta lo que hay en meoria 
        private void btnMmenos_Click(object sender, EventArgs e)
        {
            plus = plus - Convert.ToInt32(txtCal.Text);
        }
        // Este es el button para guardar por primera vez datos en la memoria Ms
        private void btnMs_Click(object sender, EventArgs e)
        {
            activateButton(true);
            plus = Convert.ToInt32(txtCal.Text);
        }

        // para 
        private void btnMC_Click(object sender, EventArgs e)
        {
            plus = 0;
            activateButton(false);
        }

        private void btnMr_Click(object sender, EventArgs e)
        {
            txtCal.Text = plus.ToString();
        }


        // esto es para actviar y desactivar los botones de memoria
        public  void activateButton (bool b)
        {          
                btnMC.Enabled = b;
                btnMr.Enabled = b;        
        }
    }
}
