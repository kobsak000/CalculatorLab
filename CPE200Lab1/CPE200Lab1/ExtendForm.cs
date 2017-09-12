﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
namespace CPE200Lab1
{
    public partial class ExtendForm : Form
    {
        private double memory;
        private bool isafterequal = false;
        private bool isNumberPart = false;
        private bool isContainDot = false;
        private bool isSpaceAllowed = false;
        private CalculatorEngine engine;
        private RpnCalculatorEngine rpn;
        public ExtendForm()
        {
            InitializeComponent();
            engine = new CalculatorEngine();
            rpn = new RpnCalculatorEngine();
        }

        private bool isOperator(char ch)
        {
            switch(ch) {
                case '+':
                case '-':
                case 'X':
                case '÷':
                    return true;
            }
            return false;
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (isafterequal)
            {
                lblDisplay.Text = "0";
                isafterequal = false;
            }
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            if (!isNumberPart)
            {
                isNumberPart = true;
                isContainDot = false;
            }
            lblDisplay.Text += ((Button)sender).Text;
            isSpaceAllowed = true;
        }

        private void btnBinaryOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            isNumberPart = false;
            isContainDot = false;
            string current = lblDisplay.Text;
           
                lblDisplay.Text += " " + ((Button)sender).Text + " ";
                isSpaceAllowed = false;
            isContainDot = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            // check if the last one is operator
            string current = lblDisplay.Text;
            if (current[current.Length - 1] is ' ' && current.Length > 2 && isOperator(current[current.Length - 2]))
            {
                lblDisplay.Text = current.Substring(0, current.Length - 3);
            } else
            {
                lblDisplay.Text = current.Substring(0, current.Length - 1);
            }
            if (lblDisplay.Text is "")
            {
                lblDisplay.Text = "0";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = "0";
            isContainDot = false;
            isNumberPart = false;
            isSpaceAllowed = false;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            string current = lblDisplay.Text;
            string result = rpn.RPNProcess(current);
            if(lblDisplay.Text==" ")
            {
                lblDisplay.Text = "0";
            }
            isafterequal = true;
            
            if (result is "E")
            {
                lblDisplay.Text = "Error";
            } else
            {
                lblDisplay.Text = result;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isNumberPart)
            {
                string x="0";
                x = lblDisplay.Text;
                if (x[0] != '-')
                x= "-" + x;
                else
                {

                    x=x.Remove(0,1);

                }
                lblDisplay.Text = x.ToString();
                return;
            }
            string current = lblDisplay.Text;
            if (current is "0")
            {
                lblDisplay.Text = "-";
            } else if (current[current.Length - 1] is '-')
            {
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if (lblDisplay.Text is "")
                {
                    lblDisplay.Text = "0";
                }
            } else
            {
                lblDisplay.Text = current + "-";
            }
            isSpaceAllowed = false;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if(!isContainDot)
            {
                isContainDot = true;
                lblDisplay.Text += ".";
                isSpaceAllowed = false;
            }
        }

        private void btnSpace_Click(object sender, EventArgs e)
        {
            if(lblDisplay.Text is "Error")
            {
                return;
            }
            if(isSpaceAllowed)
            {
                lblDisplay.Text += " ";
                isSpaceAllowed = false;
                isContainDot = false;
            }
        }

        private void btnUnaryOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            isNumberPart = false;
            isContainDot = false;
            string current = lblDisplay.Text;
            
                lblDisplay.Text += " " + ((Button)sender).Text + " ";
                isSpaceAllowed = false;
            
        }
        private void btnMP_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            memory += Convert.ToDouble(lblDisplay.Text);
            
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            memory = 0;
        }

        private void btnMM_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            memory -= Convert.ToDouble(lblDisplay.Text);
            
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "error")
            {
                return;
            }
            lblDisplay.Text = memory.ToString();
        }

        private void ExtendForm_Load(object sender, EventArgs e)
        {

        }
    }
}
