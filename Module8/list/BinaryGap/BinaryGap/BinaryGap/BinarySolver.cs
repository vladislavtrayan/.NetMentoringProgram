using System;
using System.Linq;

namespace BinaryGap
{
    public class BinarySolver
    {
        public static int Solve(int number)
        {
            if(number <= 0 || number > int.MaxValue)
                throw new ArgumentOutOfRangeException();
            
            var binaryNumber = Convert.ToString(number, 2);
            return binaryNumber.Trim('0').Split('1').Max(con => con.Length);
        }
    }
}