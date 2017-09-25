using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class SimpleCalculatorEngine:CalculatorEngine
    {
        protected double firstOperand;
        protected double secondOperand;

        public void setFirstOperand(string num)
        {
            firstOperand = Convert.ToDouble(num);
        }
        public void setSecondOperand(string num)
        {
            secondOperand = Convert.ToDouble(num);
        }
        public string calculate(string operate, string operand, int maxOutputSize = 8)
        {
            switch (operate)
            {
                case "√":
                    {
                        double result;
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
                        double result;
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
                    break;
            }
            return "E";
        }
        public string calculate(string oper)
        {
            double result;
            int maxOutputSize = 8;
            switch (oper)
            {
                case "+":
                    return (Convert.ToDouble(firstOperand) + Convert.ToDouble(secondOperand)).ToString();
                case "-":
                    return (Convert.ToDouble(firstOperand) - Convert.ToDouble(secondOperand)).ToString();
                case "X":
                    return (Convert.ToDouble(firstOperand) * Convert.ToDouble(secondOperand)).ToString();
                case "÷":
                    // Not allow devide be zero
                    if (secondOperand != 0)
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
                    break;
                case "%":
                    double  x, y;
                    x = Convert.ToDouble(firstOperand);
                    y = Convert.ToDouble(secondOperand);
                    result = x / 100 * y;
                    return Convert.ToString(result);

            }
            return "E";
           
        }
    }
}
