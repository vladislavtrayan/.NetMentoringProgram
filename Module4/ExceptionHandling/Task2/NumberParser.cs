using System;
using System.Collections;
using System.Linq;
using Microsoft.VisualBasic;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        private const char Minus = '-';
        private const char Plus = '+';
        private const string Numbers = "0123456789";
        
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


            if (stringValue.Contains(Minus) || stringValue.Contains(Plus))
            {
                if (stringValue[0] == Minus) positive = false;
                signlessValue += stringValue.Remove(0, 1);
            }
            else
            {
                signlessValue += stringValue;
            }

            foreach (var c in signlessValue)
            {
                if (!Numbers.Contains(c))
                    throw new FormatException();
                result *= 10;
                result += c - '0';

                if(positive && result - int.MaxValue > 0)
                    throw new OverflowException();
                if(!positive && int.MinValue + result > 0)
                    throw new OverflowException();
            }
            if(positive)
                return (int)result;

            return -(int) result;
        }
    }
}