using System;
using System.Linq;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string line = Console.ReadLine();
            try
            {
                Console.WriteLine(line.First());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("You sent empty string.");
            }
        }
    }
}