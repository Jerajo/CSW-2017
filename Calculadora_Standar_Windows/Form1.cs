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
        Dibujador dibujar = new Dibujador();
        bool esfraccion, editable, negacion;
        string operacion, resultado = "0";
        string n1 = "0", n2 = "0";
        char operador, step;
        int nOperaciones = 0, nSifras = 0;
        double[] memoria = new double[5];

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
                if (n1 != "0" && n1.Length != 0)
                {
                    n1 = n1.Remove(n1.Length - 1, 1);
                    if (n1.Length > 0) nSifras = n1.Length;
                }
                if(n1.Length == 0)
                {
                    n1 = "0";
                    editable = false;
                }
            }
            else if (step == '3')
            {
                if (n2 != "0" && n2.Length != 0)
                {
                    n2 = n2.Remove(n2.Length - 1, 1);
                    if (n2.Length > 0) nSifras = n2.Length;
                }
                if (n2.Length == 0)
                {
                    n2 = "0";
                    editable = false;
                }
            }
            DrawText();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetCalculadora();
            this.KeyPreview = true;
            //limpia memoria
            for (int i = 0; i < memoria.Length; i++) memoria[i] = 0;

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
            operador = '√';
            if (step == '1') operacion = operador + "(" + n1 + ")";
            else if (step == '3') operacion = operador + "(" + n2 + ")";
            else if (step == '4') operacion = operador + "(" + resultado + ")";
            btnResultado_Click(null, null);
        }
        private void btnProciento_Click(object sender, EventArgs e)
        {
            if (step == '1')
            {
                operacion = "0";
                n1 = "0";
                memoria[3] = Convert.ToDouble(n1);
                NextStep('M');
                DrawText();
            }
            else
            {
                operador = '%';
                if (step == '2') operacion += " " + n1 + operador;
                if (step == '3') operacion += " " + n2 + operador;
                else if (step == '4') operacion += " " + resultado + operador;
                btnResultado_Click(null, null);
            }
        }
        private void btnMasoMenos_Click(object sender, EventArgs e)
        {
            if (negacion) negacion = false;
            else negacion = true;
            if (step == '1')
            {
                if (n1.Contains("-")) n1 = n1.Remove(0, 1);
                else n1 = n1.Insert(0, "-");
            }
            else if (step == '2' || step == 'P')
            {
                if (negacion) operacion += " Negate(" + n1 + ")";
                else
                {
                    int index = operacion.LastIndexOf(" ");
                    operacion = operacion.Remove(index, operacion.Length - index);
                }
                memoria[3] *= -1;
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
                if (step <= '2') resultado = caluladora.Calcular(operador, n1);
                if (step == '3') resultado = caluladora.Calcular(operador, n2);
                if (step == '4') resultado = caluladora.Calcular(operador, resultado);
                memoria[4] = Convert.ToDouble(resultado);
                NextStep('S');
            }
            else if (operador == '%')
            {
                if (step <= '2') resultado = caluladora.Calcular(operador, n1, memoria[3].ToString());
                if (step == '3') resultado = caluladora.Calcular(operador, n1, n2);
                if (step == '4') resultado = caluladora.Calcular(operador, resultado, n2);                
                memoria[4] = Convert.ToDouble(resultado);
                NextStep('S');
            }
            else if (step == '2')
            {
                resultado = caluladora.Calcular(operador, n1, memoria[3].ToString());
                memoria[4] = Convert.ToDouble(resultado);
                n2 = n1;
                NextStep('4');
            }
            else if (step == 'P')
            {
                memoria[4] = Convert.ToDouble(resultado);
                if (editable) resultado = caluladora.Calcular(operador, n1, n2);
                else resultado = caluladora.Calcular(operador, n1, resultado);
                if (resultado != "0") n1 = resultado;
                NextStep('4');
            }
            else if (step == 'M')
            {
                resultado = caluladora.Calcular('+', n1, memoria[3].ToString());
                NextStep('4');
            }
            else if (step == '3')
            {
                if (nOperaciones >= 2) resultado = caluladora.Calcular(operador, resultado, n2);
                else resultado = caluladora.Calcular(operador, n1, n2);
                NextStep();          
            }
            else if (step == '4')
            {
                if(editable) resultado = caluladora.Calcular(operador, resultado, n2);
                else resultado = caluladora.Calcular(operador, resultado, memoria[4].ToString());
            }
            esfraccion = false;
            editable = false;
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
                if (e.KeyChar == '%') btnProciento_Click(null, null);
                else AlmacenarOperacion(e.KeyChar);
            }

            if (e.KeyChar == (char)8)
            {
                btnDelete_Click(null, null);
            }
        }

        //ingresar los numero por botones o por teclado
        private void IngresarNumero(char value)
        {
            if (step == '2' || step == 'P') NextStep();
            if (step == '4' || step == 'S' || step == 'M') ResetCalculadora();
            if (step == '1' && nSifras < 15)
            {
                if (!editable && value != '.') n1 = "";
                n1 += value;
                editable = true;
                nSifras = n1.Length;
            } 
            else if (step == '3' && nSifras < 15)
            {
                if (!editable && value != '.') n2 = "";
                n2 += value;
                editable = true;
                nSifras = n2.Length;
            }   
        }

        // ingresar operaciones por botones o por teclado
        private void AlmacenarOperacion(char value)
        {
            if (step != '2') nOperaciones++;
            if(nOperaciones >= 2 && step != '2'  && step != '4')
            {  
                if (step != 'P')
                {
                    NextStep('P'); 
                    btnResultado_Click(null, null);
                    operador = value;
                    operacion += " " + n2 + " " + operador;
                    memoria[4] = Convert.ToDouble(resultado);
                }
                else
                {
                    operador = value;
                    operacion = operacion.Remove(operacion.Length - 1, 1) + operador;
                }
            }
            else
            {
                operador = value;
                if (step == '4')
                {
                    n1 = n2 = resultado;
                    operacion = resultado + " " + operador;
                }
                else operacion = n1 + " " + operador;
                memoria[3] = Convert.ToDouble(n1);
            }
            //resetea indicadores
            nSifras = 1;
            esfraccion = false; 
            editable = false;
            if (step == '1') NextStep('2');
            else if (step == '4') NextStep('P');
            DrawText();
        }

        //resetea la calculadora
        private void ResetCalculadora()
        {
            n1 = "0";
            n2 = "0";
            operacion = "";
            resultado = "0";
            esfraccion = false;
            editable = false;
            negacion = false;
            nOperaciones = 0;
            nSifras = 1;
            for (int i = 3; i <= 4; i++) memoria[i] = 0;
            NextStep('1');
            DrawText("Clean");
        }

        //muestra todas las interacciones
        private void DrawText(string value = "")
        {
            dibujar.Actualizar(step, n1, operacion, n2, resultado, memoria);
            txtCal.Text = dibujar.DrawText(value);
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
                    case 'P':
                    case 'S':
                    case 'M':
                        step = '3';
                        break;
                    case '3':      
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

        // ESTE es para el button M+ suma lo que hay en memoria 
        private void btnMplus_Click(object sender, EventArgs e)
        {     
            memoria[0] = memoria[1] + almacenarMemoria();
            activateButton(true);
            AutoReset();
        }
        // Este es el button M-  resta lo que hay en meoria 
        private void btnMmenos_Click(object sender, EventArgs e)
        {
            memoria[0] = memoria[1] - almacenarMemoria();
            activateButton(true);
            AutoReset();
        }
        // Este es el button para guardar por primera vez datos en la memoria Ms
        private void btnMs_Click(object sender, EventArgs e)
        {
            memoria[0] = almacenarMemoria();
            activateButton(true);
            AutoReset();
        }
        private double almacenarMemoria()
        {
            if (step == '1' || step == '2') return Convert.ToDouble(n1);
            else if (step == '3') return Convert.ToDouble(n2);
            else if (step == '4' || step == 'P') return Convert.ToDouble(resultado);
            else return 0;
        }

        // limpia memoria 
        private void btnMC_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++) memoria[i] = 0;
            activateButton(false);
            AutoReset();
        }

        private void btnMr_Click(object sender, EventArgs e)
        {
            if (step == '1')
            {
                n1 = memoria[0].ToString();
            } 
            else if (step == '2')
            {
                n2 = memoria[0].ToString();
            }
            else if (step == 'P')
            {
                n2 = memoria[0].ToString();
            }
            else if (step == '3')
            {
                n2 = memoria[0].ToString();
                NextStep('2');
            }
            else if (step == '4')
            {
                resultado = memoria[0].ToString();             
            }
            editable = false;
            AutoReset();
            DrawText();
        }


        // esto es para actviar y desactivar los botones de memoria
        public  void activateButton (bool b)
        {          
                btnMC.Enabled = b;
                btnMr.Enabled = b;        
        }

        //muestra un mensaje de prueva
        private void TestFunction(string funcion, string valor1 = "0", string valor2 = "0")
        {
            MessageBox.Show(funcion + ": Esto se ejecuta. \nN1: " + valor1 + "\tN2:" + valor2);
        }
    }
}
