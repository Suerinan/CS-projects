using System;
using System.Collections.Generic;

namespace Calculator
{
    class Calculator
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Insert the complete expression: ");
            string expression = Console.ReadLine().Replace(" ", "");

            try
            {
                // Evaluate the expression using the 'evalExpression' method
                double result = evalExpression(expression);
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at evaluating expression: " + ex.Message);
            }
        }

        // Method to evaluate the mathematical expression
        public static double evalExpression(string expression)
        {
            Stack<double> values = new Stack<double>();
            Stack<char> operands = new Stack<char>();

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                if (char.IsDigit(c))
                {
                    string value = "";

                    // Read the entire number (could be multiple digits or include a decimal point)
                    while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                    {
                        value += expression[i];
                        i++;
                    }
                    i--; // Step back one position because the loop will increment it

                    // Push the parsed number onto the values stack
                    values.Push(double.Parse(value));
                }
                // If the current character is an opening parenthesis
                else if (c == '(')
                {
                    // Push it onto the operands stack
                    operands.Push(c);
                }
                // If the current character is a closing parenthesis
                else if (c == ')')
                {
                    // Evaluate the expression inside the parentheses
                    while (operands.Peek() != '(')
                    {
                        values.Push(RealizarOperacion(operands.Pop(), values.Pop(), values.Pop()));
                    }
                    operands.Pop(); // Remove the opening parenthesis from the stack
                }
                // If the current character is an operator
                else if (isOperand(c))
                {
                    // While there's an operator with higher or equal precedence on the stack
                    while (operands.Count > 0 && priority(operands.Peek()) >= priority(c))
                    {
                        // Evaluate the topmost operation
                        values.Push(RealizarOperacion(operands.Pop(), values.Pop(), values.Pop()));
                    }
                    // Push the current operator onto the stack
                    operands.Push(c);
                }
            }

            // Evaluate any remaining operations
            while (operands.Count > 0)
            {
                values.Push(RealizarOperacion(operands.Pop(), values.Pop(), values.Pop()));
            }

            // The final result is the last value remaining in the stack
            return values.Pop();
        }

        // Method to check if a character is a valid operator
        public static bool isOperand(char c){return c == '+' || c == '-' || c == '*' || c == '/';}

        // Method to determine the priority of operators
        public static int priority(char c)
        {
            if (c == '+' || c == '-') return 1;
            if (c == '*' || c == '/') return 2;

            // Default (should not happen)
            return 0;
        }

        // Method to perform the arithmetic operation based on the operator
        public static double RealizarOperacion(char operand, double b, double a)
        {
            switch (operand)
            {
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '/': return a / b;
                default:
                    throw new InvalidOperationException("Non valid operand.");
            }
        }
    }
}
