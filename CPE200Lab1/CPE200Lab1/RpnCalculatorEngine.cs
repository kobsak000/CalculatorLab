using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;  //to use Stack

namespace CPE200Lab1
{
    class RpnCalculatorEngine : CalculatorEngine {

        private ExtendForm aform;

        public string testStackMethod(string str)
        {
            string[] word = str.Split(' ');
            string x = word[0];
            int size=word.Length; 
            Stack processtack = new Stack();
           for(int i =size;i>0;i++)
            {
                processtack.Push(word[i]);
            }
            while(processtack.Count == 0)

            return x;
        }


    }

    

}
