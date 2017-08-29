using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool morethan2;
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private string firstOperand;
        
        private string operate="0";
        
        CalculatorEngine calculatorEngine = new CalculatorEngine { };

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            morethan2 = false;
            operate = "0";
        }

       

        public MainForm()
        {
            InitializeComponent();

            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if(morethan2)
            {
                lblDisplay.Text = "0";
                isAllowBack = true;
                hasDot = false;
                isAfterOperater = false;
                isAfterEqual = false;
                morethan2 = false;
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if(lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }
            if (operate != "0")
            {
                if (((Button)sender).Text == "%")
                {
                    string secondOperand = lblDisplay.Text;
                    double x = Convert.ToDouble(secondOperand) * Convert.ToDouble(firstOperand) / 100;
                    secondOperand = Convert.ToString(x);
                    firstOperand = calculatorEngine.calculate(operate, firstOperand, secondOperand);
                    lblDisplay.Text = "";
                    lblDisplay.Text = firstOperand;
                    morethan2 = true;
                    operate = "0";
                }
                else
                {
                    string secondOperand = lblDisplay.Text;
                    firstOperand = calculatorEngine.calculate(operate, firstOperand, secondOperand);
                    operate = ((Button)sender).Text;
                    lblDisplay.Text = "";
                    lblDisplay.Text = firstOperand;
                    morethan2 = true;
                }
                return;
            }
            else
            {

                operate = ((Button)sender).Text;
                switch (operate)
                {
                    case "+":
                    case "-":
                    case "X":
                    case "÷":

                        firstOperand = lblDisplay.Text;
                        isAfterOperater = true;
                        break;
                    case "%":
                        firstOperand = lblDisplay.Text;
                        double x = Convert.ToDouble(firstOperand);
                        x=x / 100;
                        firstOperand=Convert.ToString(x);
                        lblDisplay.Text = firstOperand;
                        operate = "0";
                        isAfterEqual = true;
                        return ;
                        break;
                }
            }
            isAllowBack = false;
            if (isAfterEqual)
            {
                operate = ((Button)sender).Text;
                isAfterEqual = false;
            }
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (operate == "0")
            {

            }
            else
            {
                if(morethan2)
                {
                    return;
                }
                if (lblDisplay.Text is "Error")
                {
                    return;
                }
                string secondOperand = lblDisplay.Text;
                string result = calculatorEngine.calculate(operate, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else
                {
                    lblDisplay.Text = result;
                }
                isAfterEqual = true;
                morethan2 = false;
                operate = "0";
            }
            }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            } else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if(lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void btnsqrt_Click(object sender, EventArgs e)
        {
            if(operate=="0")
            {
                firstOperand = lblDisplay.Text;
                double x= Convert.ToDouble(firstOperand);
                x=Math.Sqrt(x);
                firstOperand=Convert.ToString(x);
                lblDisplay.Text = firstOperand;
                isAfterEqual = true;
            }
            else
            {
                string secondOperand = lblDisplay.Text;
                double x = Convert.ToDouble(secondOperand) ;
                x = Math.Sqrt(x);
                secondOperand = Convert.ToString(x);
                firstOperand = calculatorEngine.calculate(operate, firstOperand, secondOperand);
                lblDisplay.Text = "";
                lblDisplay.Text = firstOperand;
                morethan2 = true;
                operate = "0";
                morethan2 = true;
            }

        }

        private void divinex(object sender, EventArgs e)
        {
            if (operate == "0")
            {
                firstOperand = lblDisplay.Text;
                double x = Convert.ToDouble(firstOperand);
                x =1/x;
                firstOperand = Convert.ToString(x);
                lblDisplay.Text = firstOperand;
                isAfterEqual = true;
            }
            else
            {
                string secondOperand = lblDisplay.Text;
                double x = Convert.ToDouble(secondOperand);
                x = 1 / x;
                secondOperand = Convert.ToString(x);
                firstOperand = calculatorEngine.calculate(operate, firstOperand, secondOperand);
                lblDisplay.Text = "";
                lblDisplay.Text = firstOperand;
                morethan2 = true;
                operate = "0";
                morethan2 = true;
            }

        }
    }
}
