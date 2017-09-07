using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;  //to use Stack

namespace CPE200Lab1
{
    class RpnCalculatorEngine : CalculatorEngine {
        private CalculatorEngine engine;
        public RpnCalculatorEngine()
        {
            engine = new CalculatorEngine();   
        }
        public string Method(string str)
        {
            string[] word = str.Split(' ');
            string output=null;
            int size = word.Length;
            Stack processtack = new Stack();

            string first, second, op;
            
            for (int i =0;i<size;i++)
            {
                if(word[i]== "√" || word[i] == "1/x")
                {
                    op = word[i];
                    first = processtack.Pop().ToString();
                    output =engine.unaryCalculate(op,first);
                    processtack.Push(output);
                }
                else if (word[i] == "%")
                {
                    second = processtack.Pop().ToString();
                    first = processtack.Pop().ToString();
                    processtack.Push(first.ToString());
                    output = engine.percent(first, second);
                    processtack.Push(output);
                    
                }
                else if (word[i] == "+" || word[i] == "-" || word[i] == "X" || word[i] == "/")
                {
                    
                    second = processtack.Pop().ToString();
                    first = processtack.Pop().ToString();
                    
                    op = word[i];
                    output=engine.calculate(op, first, second);
                    processtack.Push(output);
                }
                else // if they put number
                {
                    if(word[i]!="")
                    processtack.Push(word[i]);
                }
            }
            if (processtack.Count == 1)
                return output;
            return "E";
        }

         
    }

    

}
