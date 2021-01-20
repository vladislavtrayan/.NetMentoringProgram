using System;
using System.Linq;

namespace BinaryGap
{
    public class BinarySolver
    {
        public static int Solve(int number)
        {
            string binaryNumber = Convert.ToString(number, 2);
            return binaryNumber.Trim('0').Split('1').Max(con => con.Length);
        }
    }
}