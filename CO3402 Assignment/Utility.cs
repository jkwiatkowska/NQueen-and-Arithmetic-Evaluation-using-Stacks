using System;
using System.Collections.Generic;

namespace CO3402_Assignment
{
    public static class Utility
    {
        public static int GetInt(int min, int max, string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);

                string line = Console.ReadLine();
                int value;

                if (int.TryParse(line, out value))
                {
                    if (value > max)
                    {
                        Console.WriteLine($"Value entered is too large. Max is {max}.");
                    }
                    else if (value < min)
                    {
                        Console.WriteLine($"Value entered is too small. Min is {min}.");
                    }
                    else
                    {
                        return value;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }

        public static ConsoleKey MenuSelect(Dictionary<ConsoleKey, string> options)
        {
            foreach (var option in options)
            {
                Console.WriteLine(option.Value);
            }

            ConsoleKey choice = Console.ReadKey().Key;
            Console.Write("\r");

            while (!options.ContainsKey(choice))
            {
                if (choice == ConsoleKey.Escape)
                {
                    Environment.Exit(1);
                }

                Console.WriteLine("Invalid input.");

                choice = Console.ReadKey().Key;
                Console.Write("\r");
            }

            return choice;
        }
        public static void AddSpace(ref string output)
        {
            if (String.IsNullOrEmpty(output) || output[output.Length - 1] == ' ')
            {
                return;
            }
            else
            {
                output += " ";
            }
        }
    }
}
