﻿using System;
using System.Collections.Generic;

namespace CO3402_Assignment
{
    public static class ExpressionEvaluation
    {
        static Dictionary<char, int> Operators = new Dictionary<char, int>()
        {
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 },
            { '%', 2 },
            { '^', 3 },
            { '(', 4 },
            { ')', 4 }
        };

        public static bool ValidateExpression(string expression)
        {
            // Empty string
            if (String.IsNullOrEmpty(expression))
            {
                return false;
            }

            // Keep track of first and last operand or operator found
            char first = ' ';
            char last = ' ';

            // Ensure each parenthesis is closed
            Stack<char> parentheses = new Stack<char>();
            foreach (char c in expression)
            {
                if (c == '(')
                {
                    parentheses.push(c);
                }
                else if (c == ')')
                {
                    if (parentheses.pop() != '(')
                    {
                        return false;
                    }
                }
                else
                {
                    // Two operators in a row are not allowed (not including parentheses)
                    if (Operators.ContainsKey(last) && Operators.ContainsKey(c))
                    {
                        return false;
                    }
                    // If first hasn't been found yet
                    if (first == ' ')
                    {
                        // Expression cannot start with an operator
                        if (Operators.ContainsKey(c))
                        {
                            return false;
                        }
                        first = c;
                    }
                    // Keep track of the most recent 
                    if (c != ' ')
                    {
                        last = c;
                    }
                }
            }

            // Unclosed parenthesis
            if (!parentheses.isEmpty())
            {
                return false;
            }

            // Expression cannot end with an operator or consist entirely of spaces
            if (last == ' ' || Operators.ContainsKey(last))
            {
                return false;
            }

            return true;
        }

        public static string InfixToPostfix(string infix)
        {
            string output = "";
            Stack<char> operators = new Stack<char>();
            bool firstChar = false;

            foreach (char c in infix)
            {
                // If c is in the dictionary, that means it is a known operator.
                if (Operators.ContainsKey(c))
                {
                    if (c == '(')
                    {
                        // Beginning parenthesis is pushed to stack.
                        operators.push(c);
                    }
                    else if (c == ')')
                    {
                        // When an ending parenthesis is found,
                        // everything on the stack is popped and the parentheses are discarded.
                        while (operators.top() != '(')
                        {
                            // This shouldn't happen if ValidateExpression() is used.
                            if (operators.isEmpty())
                            {
                                return "Error: Incorrect number of parentheses.";
                            }

                            Utility.AddSpace(ref output);
                            output += operators.pop();
                        }
                        operators.pop();
                    }
                    else
                    {
                        // Any operators on the stack that have higher precedence than c are popped.
                        while (!operators.isEmpty() && Operators[c] <= Operators[operators.top()] && operators.top() != '(')
                        {
                            Utility.AddSpace(ref output);
                            output += operators.pop();
                        }

                        operators.push(c);
                    }
                    // This signifies that the last evaluated character was an operator. 
                    firstChar = true;
                }
                else if (c != ' ')
                {
                    // Add a space to output if an operator was previously evaluated.
                    if (firstChar)
                    {
                        Utility.AddSpace(ref output);
                        firstChar = false;
                    }

                    // Output the operand.
                    output += c;
                }
                else
                {
                    // If c is a space, add it through a function that ensures no double spaces. 
                    Utility.AddSpace(ref output);
                }
            }
            while (!operators.isEmpty())
            {
                Utility.AddSpace(ref output);
                output += operators.pop();
            }

            return output;
        }

        static string ReverseExpression(string expression)
        {
            string reverse = "";

            // Iterate the expression in reverse order
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                char c = expression[i];

                // Parentheses are reversed
                if (c == '(')
                {
                    reverse += ')';
                }
                else if (c == ')')
                {
                   reverse += '(';
                }
                // Operands do not need to be reversed, as this function is applied twice
                else
                {
                    reverse += c;
                }
            }

            return reverse;
        }

        public static string InfixToPrefix(string infix)
        {
            // Reverse the infix expression.
            string reversed = ReverseExpression(infix);

            // Convert it to a postfix notation.
            string reversedPostfix = InfixToPostfix(reversed);

            // Reverse again.
            string output = ReverseExpression(reversedPostfix);

            return output;
        }

        public static bool TryEvaluate(string prefix, out float result)
        {
            Stack<float> operands = new Stack<float>();
            Stack<char> currentOperand = new Stack<char>();
            result = 0;

            for (int i = prefix.Length - 1; i >= 0; i--)
            {
                char c = prefix[i];

                // If c is a space and an operand is being held, process the operand
                if (c == ' ')
                {
                    if (!currentOperand.isEmpty())
                    {
                        string operandString = "";
                        while (!currentOperand.isEmpty())
                        {
                            operandString += currentOperand.pop();
                        }

                        if (float.TryParse(operandString, out float operand))
                        {
                            operands.push(operand);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                // If c is an operator, try to evaluate the last two operants on the stack
                else if (Operators.ContainsKey(c))
                {
                    if (operands.size() < 2)
                    {
                        // Not enough operands.
                        return false;
                    }

                    float s1 = operands.pop();
                    float s2 = operands.pop();
                    switch(c)
                    {
                        case '+':
                        {
                            operands.push(s1 + s2);
                            break;
                        }
                        case '-':
                        {
                            operands.push(s1 - s2);
                            break;
                        }
                        case '*':
                        {
                            operands.push(s1 * s2);
                            break;
                        }
                        case '/':
                        {
                            operands.push(s1 / s2);
                            break;
                        }
                        case '%':
                        {
                            operands.push(s1 % s2);
                            break;
                        }
                        case '^':
                        {
                            // Expotentiation. Assumes s2 is a whole number.
                            float s = s1;
                            while (s2 > 1)
                            {
                                s *= s1;
                                s2--;
                            }
                            operands.push(s);
                            break;
                        }
                        default:
                        {
                            // Invalid operator, so expression can't be evaluated.
                            return false;
                        }
                    }
                }
                // If c is not an operator or a space, that means it's an operand or part of it. 
                // Append it to a string to process later.
                else
                {
                    currentOperand.push(c);
                }
            }

            result = operands.top();
            return true;
        }

        public static void Start()
        {
            Console.WriteLine("Enter an infix expression:");

            string infixExpression = Console.ReadLine();

            if (ValidateExpression(infixExpression))
            {
                Console.WriteLine("Postfix notation:");
                Console.WriteLine(InfixToPostfix(infixExpression));

                Console.WriteLine("Prefix notation:");
                string prefix = InfixToPrefix(infixExpression);
                Console.WriteLine(prefix);

                // Only show evaluation result if the expression can be evaluated.
                if (TryEvaluate(prefix, out float result))
                {
                    Console.WriteLine("Evaluation result:");
                    Console.WriteLine(result);
                }
            }
            else
            {
                Console.WriteLine("Invalid expression. Please try again.");
            }
        }
    }
}
