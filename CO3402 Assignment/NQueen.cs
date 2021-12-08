using System;
using System.Collections.Generic;

namespace CO3402_Assignment
{
    public static class NQueen
    {
        public enum eDisplayMode
        {
            None = 0,
            Detailed = 1,
            FirstSolution = 2,
            SolutionCount = 3
        }

        // This is the algorithm that finds solutions for the given N.
        public static int SolveN(int n, eDisplayMode displayMode)
        {
            NQueenStack queens = new NQueenStack();

            int column = 1;
            int row = 1;
            int solutionsFound = 0;
            bool showDetails = displayMode == eDisplayMode.Detailed;
            bool showSolutions = showDetails || displayMode == eDisplayMode.FirstSolution;

            while (true)
            {
                if (column <= n)
                {
                    if (queens.CanPush(column)) // Valid position to place queen
                    {
                        if (showDetails)
                        {
                            Console.WriteLine($"Placed a queen on [{row}, {column}].");
                        }

                        if (row == n) // Solution was found
                        {
                            solutionsFound++;

                            if (showSolutions)
                            {
                                Console.WriteLine($"Solution found for n {n}:");
                                queens.push(column); // Last queen is only pushed to show the board
                                queens.ShowBoard(n);

                                if (displayMode == eDisplayMode.FirstSolution)
                                {
                                    return solutionsFound;
                                }

                                queens.pop();
                            }

                            // Start checking for the next solution
                            row--;
                            column = queens.pop() + 1;
                        }
                        else // Need to place more queens to find solution
                        {
                            // Add the queen to stack and move on to next row
                            queens.push(column);
                            row++;
                            column = 1;
                            if (showDetails)
                            {
                                queens.ShowBoard(n);
                            }
                        }
                    }
                    else // Invalid solution
                    {
                        if (showDetails)
                        {
                            Console.WriteLine($"Failed to place a queen on [{row}, {column}].");
                            queens.ShowBoard(n, row, column);
                        }

                        // Move on to the next column
                        column++;
                    }
                }
                else // None of the columns on this row had a solution.
                {
                    if (showDetails)
                    {
                        Console.WriteLine($"Failed to place a queen on row {row}.");
                    }

                    // Backtrack to the previous row
                    row--;

                    if (row <= 0) // All possible solutions have been checked.
                    {
                        if (displayMode != eDisplayMode.None)
                        {
                            Console.WriteLine($"Search complete. Solutions found: {solutionsFound}");
                        }
                        return solutionsFound;
                    }

                    // Remove the recently placed queen and continue checking columns on its row
                    column = queens.pop() + 1; // Start search from the next column

                    if (showDetails)
                    {
                        Console.WriteLine($"Going back to row {row}.");
                    }
                }

                if (showDetails)
                {
                    Console.WriteLine();
                    Console.Read();
                }
            }
        }

        #region Setup
        static Dictionary<eDisplayMode, int> maxN = new Dictionary<eDisplayMode, int>()
        {
            {eDisplayMode.Detailed, 7},
            {eDisplayMode.FirstSolution, 30},
            {eDisplayMode.SolutionCount, 16}
        };

        static Dictionary<ConsoleKey, string> DisplayOptions = new Dictionary<ConsoleKey, string>()
        {
            { ConsoleKey.D1, $"1. Show Detailed Search (Max N: {maxN[eDisplayMode.Detailed]})"},
            { ConsoleKey.D2, $"2. Show First Solution  (Max N: {maxN[eDisplayMode.FirstSolution]})"},
            { ConsoleKey.D3, $"3. Show Solution Count  (Max N: {maxN[eDisplayMode.SolutionCount]})"}
        };

        public static void Start()
        {
            eDisplayMode displayMode = eDisplayMode.SolutionCount;
            ConsoleKey choice = Utility.MenuSelect(DisplayOptions); 

            switch (choice)
            {
                case ConsoleKey.D1:
                displayMode = eDisplayMode.Detailed;
                break;
                case ConsoleKey.D2:
                displayMode = eDisplayMode.FirstSolution;
                break;
                case ConsoleKey.D3:
                displayMode = eDisplayMode.SolutionCount;
                break;
            }

            int n = Utility.GetInt(1, maxN[displayMode], "Enter the size of the board: ");

            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Searching...");
            SolveN(n, displayMode);

            Console.WriteLine($"Time elapsed: {sw.Elapsed.TotalSeconds} seconds");
        }

    }
    #endregion
}
