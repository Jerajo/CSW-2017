using Calculadora_Standar_Windows.identidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        bool step1, step2, step22, step3, step4, esfraccion;
        string operacion, resultado;
        string n1 = "0", n2 = "0";
        char operador;
        int nOperaciones = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtCal.Text);
        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCal.Text += Clipboard.GetText();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (step1)
            {
                if (n1 != "0" && n1.Length != 0) n1 = n1.Remove(n1.Length - 1, 1);
                if(n1.Length == 0) n1 = "0";
            }
            else if (step3)
            {
                if (n2 != "0" && n2.Length != 0) n2 = n2.Remove(n2.Length - 1, 1);
                if (n2.Length == 0) n2 = "0";
            }
            DrawText();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawText("Clean");
            step1 = true;
            this.KeyPreview = true;
            AutoReset();

            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);
            }
        }

        private void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }

        private void AutoReset()
        {
            this.ActiveControl = btnResultado;
        }
 
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

        private void btnClearE_Click(object sender, EventArgs e)
        {  
            if (step1) n1 = "0";
            else if (step3) n2 = "0";
            DrawText("CleanE");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetCalculadora();
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('+');
            NextStep("step3");
            DrawText();
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('-');
            NextStep("step3");
            DrawText();
        }

        private void btnMultiplicar_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('*');
            if (nOperaciones >= 2) NextStep("step22");
            NextStep("step3");
            DrawText();
        }

        private void btnDividir_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('/');
            NextStep("step3");
            DrawText();
        }

        private void btnRaiz_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('√');
            btnResultado_Click(null, null);
            NextStep("step4");
            DrawText();
        }

        private void btnProciento_Click(object sender, EventArgs e)
        {
            AlmacenarOperacion('%');
            NextStep("step3");
            DrawText();
        }

        private void btnMasoMenos_Click(object sender, EventArgs e)
        {
            if (txtCal.Text.Contains("-"))
                txtCal.Text = txtCal.Text.Remove(0, 1);
            else
                txtCal.Text = txtCal.Text.Insert(0, "-");
            AutoReset();
        }

        private void btnResultado_Click(object sender, EventArgs e)
        {
            if (operador == '√')
            {
                if (step1) resultado = caluladora.Calcular(operador, n1);
                if (step3) resultado = caluladora.Calcular(operador, n2);
                if (step4) resultado = caluladora.Calcular(operador, resultado);    
            }
            else if (step3)
            {
                NextStep();
                if (step22 || step4)
                {
                    resultado = caluladora.Calcular(operador, n1, n2);
                    DrawText();
                    if (step22)
                    {
                        n1 = resultado;
                        n2 = resultado;
                    }
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {        
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
            else if (caluladora.checkOperation(e.KeyChar))
            {
                AlmacenarOperacion(e.KeyChar);
                NextStep("step3");
                DrawText();
            }

            if (e.KeyChar == (char)8)
            {
                btnDelete_Click(null, null);
            }
        }

        private void IngresarNumero(char value)
        {
            if (step4) ResetCalculadora();
            if (step1)
            {
                if (n1 == "0" && value != '.') n1 = "";
                n1 += value;
            } 
            else if (step3)
            {
                if (n2 == "0" && value != '.') n2 = "";
                else if (resultado != "") n2 = "";
                n2 += value;
                resultado = "";
            }   
        }

        private void AlmacenarOperacion(char value)
        {
            if(value != '%' && value != '√') nOperaciones++;
            operador = value;
            if(nOperaciones >= 2)
            {
                operacion += n2 + " " + operador;
                NextStep("step22");
                btnResultado_Click(null, null);
            }
            else
            {
                if (operador == '√') operacion = operador + " " + n1;
                else if (step1) operacion = n1 + " " + operador;
            }
            esfraccion = false;
        }

        private void ResetCalculadora()
        {
            n1 = "0";
            operador = ' ';
            n2 = "0";
            operacion = "";
            resultado = "";
            esfraccion = false;
            nOperaciones = 0;
            NextStep("step1");
            DrawText("Clean");
        }

        private void DrawText(string value = "")
        {
            if (value != "")
            {
                if (value == "Clean") txtCal.Text = "0";
                else if (value == "CleanE")
                {
                    if (step1 || step4) txtCal.Text = "0";
                    else txtCal.Text = operacion + "\n0";
                }
            }  
            else if (step1) txtCal.Text = n1;
            else if (step2) txtCal.Text = operacion + "\n0";
            else if (step22) txtCal.Text = operacion + "\n" + resultado;
            else if (step3) txtCal.Text = operacion + "\n" + n2;
            else if (step4) txtCal.Text = resultado;
            AutoReset();
        }

        private void NextStep(string value = "")
        {
            if (value != "")
            {       
                step1 = false;
                step2 = false;
                step22 = false;
                step3 = false;
                step4 = false;
                if (value == "step1") step1 = true;
                else if (value == "step2") step2 = true;
                else if (value == "step22") step22 = true;
                else if (value == "step3") step3 = true;
                else if (value == "step4") step4 = true;
            }
            else if (step1)
            {
                step1 = false;
                step2 = true;
            }
            else if (step2)
            {
                step2 = false;
                step3 = true;
            }
            else if (step3)
            {
                step3 = false;
                step4 = true;
            }
            else if (step4)
            {
                step4 = false;
                step1 = true;
            }
        }

        private void TestFunction(string indicador)
        {
            MessageBox.Show(indicador + ": se ejecuta.");
        }
    }
}
