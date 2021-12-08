using System;

namespace CO3402_Assignment
{
    public class NQueenStack : Stack<int>
    {
        public bool CanPush(int pos)
        {
            // Empty stack
            if (Top == null)
            {
                return true;
            }

            Element t = Top;
            int step = 1;

            // Go through all queens on stack
            while (t != null)
            {
                // Ensure no vertical and diagonal overlap
                if (t.value == pos || t.value == pos - step || t.value == pos + step)
                {
                    return false;
                }

                t = t.next;
                step++;
            }

            // None of the queens on stack overlap with this position
            return true;
        }

        public void ShowBoard(int n, int failRow = -1, int failColumn = -1)
        {
            Element t = Top;

            Stack<string> rowText = new Stack<string>();
            string currentRowText;

            // Go through all recorded queens
            while (t != null)
            {
                currentRowText = "";

                for (int column = 1; column <= n; column++)
                {
                    if (t.value == column)
                    {
                        // Queen is here
                        currentRowText += "  ";
                    }
                    else
                    {
                        // Empty cell
                        currentRowText += "[]";
                    }
                }

                // Add the string showing current row to stack
                rowText.push(currentRowText);

                // Move on to next row
                t = t.next;
            }

            // Display column numbers
            Console.Write("  ");
            for (int column = 1; column <= n; column++)
            {
                Console.Write((column.ToString()).PadLeft(2));
            }
            Console.Write("\n");

            // Display the board
            for (int row = 1; row <= n; row++)
            {
                // Row number
                Console.Write((row.ToString()).PadLeft(2));

                // Row cells
                currentRowText = rowText.pop();

                // If row doesn't have a queen, draw an empty row
                if (currentRowText == null)
                {
                    currentRowText = "";
                    for (int column = 1; column <= n; column++)
                    {
                        if (row == failRow && column == failColumn)
                        {
                            currentRowText += "XX";
                        }
                        else
                        {
                            currentRowText += "[]";
                        }
                    }
                }

                // Display row
                Console.WriteLine(currentRowText);
            }
        }
    }
}
