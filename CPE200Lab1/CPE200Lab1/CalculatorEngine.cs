using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    public class CalculatorEngine
    {
        protected bool isNumber(string str)
        {
            double retNum;
            return Double.TryParse(str, out retNum);
        }

        protected bool isOperator(string str)
        {
            switch(str) {
                case "+":
                case "-":
                case "X":
                case "÷":
                    return true;
            }
            return false;
        }

        public string Process(string str)
        {
            if (str == "0")
                return "0";

            if (str == null || str == "")
                return "E";
            //Split input string to multiple parts by space
            List<string> parts = str.Split(' ').ToList<string>();
            string result;
            //As long as we have more than one part
            while(parts.Count > 1)
            {
                //Check if the first three is ready for calcuation
                if(!(isNumber(parts[0]) && isOperator(parts[1]) && isNumber(parts[2])))
                {
                    return "E";

                } else
                {
                    //Calculate the first three
                    result = calculate(parts[1], parts[0], parts[2], 4);
                    //Remove the first three
                    parts.RemoveRange(0, 3);
                    // Put back the result
                    parts.Insert(0, result);
                }
            }
            return parts[0];
        }
        public string unaryCalculate(string operate, string operand, int maxOutputSize = 8)
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
                    if(operand != "0")
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

        public virtual string percent(string firstOperand, string secondOperand, int maxOutputSize = 8)
        {
            double result, x, y;
            x = Convert.ToDouble(firstOperand);
            y = Convert.ToDouble(secondOperand);
            result = x / 100 * y;
            return Convert.ToString(result);
        }

        public string calculate(string operate, string firstOperand, string secondOperand, int maxOutputSize = 8)
        {
            switch (operate)
            {
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
                        double result;
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

                        if((Convert.ToDouble(firstOperand) % Convert.ToDouble(secondOperand))==0)
                        {
                            return Convert.ToString(Convert.ToInt32(result));
                        }
                        //string.Format("{0:G29}", decimal.Parse(Convert.ToString(result)));
                        //decimal.Parse(result).ToString("G29");
                        double r;
                        string h;
                        h= result.ToString("N4");
                        string[] deci= h.Split('.');
                        r=Convert.ToDouble(deci[1]);
                        if(r%10==0 || Convert.ToDouble(deci[1]) % 100 == 0 || Convert.ToDouble(deci[1]) % 1000 == 0 || Convert.ToDouble(deci[1]) % 10000== 0)
                        {
                            //return result.ToString("N4");
                            return result.ToString("G29");
                        }
                        //return result.ToString("G29");
                        return result.ToString("N4");
                    }
                    break;
                case "%":
                    //your code here
                    break;
            }
            return "E";
        }
    }
}
