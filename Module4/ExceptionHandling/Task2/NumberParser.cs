using System;
using System.Collections;
using System.Linq;
using Microsoft.VisualBasic;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        const string signs = "+-";
        const string numbers = "0123456789";
        
        public int Parse(string stringValue)
        {
            if(stringValue == null)
                throw new ArgumentNullException();
            stringValue = stringValue.Trim();
            if (stringValue == string.Empty)
                throw new FormatException();
            
            long result = 0;
            var signlessValue = string.Empty;
            var positive = true;


            if (stringValue.Contains(signs[0]) || stringValue.Contains(signs[1]))
            {
                if (stringValue[0] == '-') positive = false;
                signlessValue += stringValue.Remove(0, 1);
            }
            else
            {
                signlessValue += stringValue;
            }

            foreach (var c in signlessValue)
            {
                if (!numbers.Contains(c))
                    throw new FormatException();
                result *= 10;
                result += c - '0';
            }

            if (positive)
            {
                if(result - int.MaxValue > 0)
                    throw new OverflowException();
            }
            else
            {
                result = (-result);
                if(result - int.MinValue < 0)
                    throw new OverflowException();
            }
            return (int)result;
        }
    }
}