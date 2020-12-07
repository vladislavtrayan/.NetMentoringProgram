using System;
using System.Linq;
using Core;

namespace NetCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Too many arguments.");
                return;
            }

            if (args.Length == 0)
            {
                Console.WriteLine("Pls, enter user name as argument");
                return;
            }

            Console.WriteLine(StringFormatter.AddCurrentTimeToString(args.First()));
        }
    }
}
