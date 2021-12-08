using System;
using System.Collections.Generic;

namespace CO3402_Assignment
{
    class Program
    {
        static Dictionary<ConsoleKey, string> Problems = new Dictionary<ConsoleKey, string>()
        {
            { ConsoleKey.D1, $"1. NQueen"},
            { ConsoleKey.D2, $"2. Arithmetic Expression Evaluation"}
        };
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                
                ConsoleKey choice = Utility.MenuSelect(Problems);
                
                Console.WriteLine("__________________________________________");
                
                if (choice == ConsoleKey.D1)
                {
                    NQueen.Start();
                }
                else if (choice == ConsoleKey.D2)
                {
                    ExpressionEvaluation.Start();
                }
                Console.WriteLine("__________________________________________");
            }
        }
    }
}
