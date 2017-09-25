﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{

    public class RPNCalculatorEngine : CalculatorEngine
    {
        public Stack<string> rpnStack = new Stack<string>();

        protected string firstOperand;
        protected string secondOperand;
        public string calculate(string operand)
        {
            double result;
            int maxOutputSize = 8;
            switch (operand)
            {

                case "√":
                    {

                        string[] parts;
                        int remainLength;

                        result = Math.Sqrt(Convert.ToDouble(operand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return result.ToString("N" + remainLength);
                    }
                case "1/x":
                    if (operand != "0")
                    {

                        string[] parts;
                        int remainLength;

                        result = (1.0 / Convert.ToDouble(operand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return result.ToString("N" + remainLength);
                    }
                    else return "E";
                case "+":
                    return (Convert.ToDouble(firstOperand) + Convert.ToDouble(secondOperand)).ToString();
                case "-":
                    return (Convert.ToDouble(firstOperand) - Convert.ToDouble(secondOperand)).ToString();
                case "X":
                    return (Convert.ToDouble(firstOperand) * Convert.ToDouble(secondOperand)).ToString();
                case "÷":
                    // Not allow devide be zero
                    if (secondOperand != "0")
                    {

                        string[] parts;
                        int remainLength;

                        result = (Convert.ToDouble(firstOperand) / Convert.ToDouble(secondOperand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =

                        if ((Convert.ToDouble(firstOperand) % Convert.ToDouble(secondOperand)) == 0)
                        {
                            return Convert.ToString(Convert.ToInt32(result));
                        }
                        //string.Format("{0:G29}", decimal.Parse(Convert.ToString(result)));
                        //decimal.Parse(result).ToString("G29");
                        double r;
                        string h;
                        h = result.ToString("N4");
                        string[] deci = h.Split('.');
                        r = Convert.ToDouble(deci[1]);
                        if (r % 10 == 0 || Convert.ToDouble(deci[1]) % 100 == 0 || Convert.ToDouble(deci[1]) % 1000 == 0 || Convert.ToDouble(deci[1]) % 10000 == 0)
                        {
                            //return result.ToString("N4");
                            return result.ToString("G29");
                        }
                        //return result.ToString("G29");
                        return result.ToString("N4");
                    }
                    else return "E";

                case "%":
                    double x, y;
                    x = Convert.ToDouble(firstOperand);
                    y = Convert.ToDouble(secondOperand);
                    result = x / 100 * y;
                    return Convert.ToString(result);

            }
            return "E";
        }
        public new string Process(string str)
        {
            if (str == "0")
                return "0";

            if (str == "" || str == null)
                return "E";
            //bool isNumeric = int.TryParse(str);
            List<string> parts = str.Split(' ').ToList<string>();
            string result;
           



            foreach (string token in parts)
            {
                if (token == "√" || token == "1/x")
                {

                    firstOperand = rpnStack.Pop().ToString();
                    result = calculate(token);
                    rpnStack.Push(result);
                }
                else if (token == "%")
                {
                    secondOperand = rpnStack.Pop().ToString();
                    if (rpnStack.Count == 0)
                        return "E";
                    firstOperand = rpnStack.Pop().ToString();
                    rpnStack.Push(firstOperand.ToString());
                    result = calculate(token);
                    rpnStack.Push(result);

                }

                else if (isOperator(token))
                {

                    //FIXME, what if there is only one left in stack?
                    if (rpnStack.Count < 2)
                        return "E";
                    secondOperand = rpnStack.Pop();
                    firstOperand = rpnStack.Pop();
                    result = calculate(token);
                    if (result is "E")
                    {
                        return result;
                    }
                    rpnStack.Push(result);
                }
                else if (isNumber(token))
                {
                    int i;
                    for (i = 0; i < token.Length; i++)
                    {
                        if (token[i] == '+')
                            return "E";
                    }
                    rpnStack.Push(token);
                }
                else
                {
                    int i;
                    for (i = 0; i < token.Length; i++)
                    {
                        if (token[i] == '+')
                        {
                            if (token.Length > 1)
                            {
                                return "E";
                            }
                        }
                    }
                }

            }
            //FIXME, what if there is more than one, or zero, items in the stack?
            if (rpnStack.Count != 1)
                return "E";
            result = rpnStack.Pop();
            return result;
        }
    }

}

        
    



