using System.Collections;
using System.Text;

namespace CalculatorApplication
{
    /*
     * This class validates the user input and if it is a valid expression it calculates and returns the output of the expression.
     * validateInput(): 
     * To validate the given input:  I used five different variables.
     * openBraceCount and closeBraceCount are incremented whenever we find '(' and ')'. If they are not equal it displays the error message.
     * validFunction is used to check if the input has correct function names or not.
     * operandCount and operationCount are used to check if the input has exactly two arguments.

     * Main approach: getExpressionResult()
     * I used two stacks: operations and operands, currentOperation is a string and number is floating type.
     * First I loop through every character in the given expression. 
     * If the character is letter add it to currentOperation.
     * If it is a digit I loop it over again to get the entire number. For example: add(12.3,4). I loop it until I get 12.3 as one number.
     * If it is '(' push the function to operations stack.
     * If it is comma push the numbers to operands stack and reset number to null
     * If it is ')' push the numbers to operands stack and compute the most recent expression and this result is pushed back to stack.
     * Once we reach the end of the string we return the result from the stack operands.     
     */
    public class Calculator
    {
        StringBuilder currentOperation = new StringBuilder();
        Stack operations = new Stack();
        Stack<float> operands = new Stack<float>();
        Nullable<float> number = null;

        /// <summary>
        /// This method is used to validate the user input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool validateInput(string input)
        {
            int openBraceCount = 0;
            int closeBraceCount = 0;
            int operandCount = 0;
            int operationCount = 0;
            bool validFunction = true;

            // It checks if the string is null or empty
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input is empty");
                return false;
            }


            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];


                if (Char.IsLetter(c))
                {
                    currentOperation.Append(c);
                }

                /* It is used to increment operands count. 
                 * add(12.3,4) - Operands count is going to be 2
                 */
                else if (char.IsDigit(c))
                {
                    string substr = "";
                    for (int j = i; j < input.Length; j++)
                    {
                        // If the input is: add(1sss2,3) it shows error message
                        if (char.IsLetter(input[j]))
                        {
                            Console.WriteLine("Input not in correct format. There should not be letters between numbers");
                            return false;
                        }

                        if (input[j] != Constants.comma && input[j] != Constants.close_brace)
                        {
                            substr += input[j];
                        }
                        else
                        {
                            i = j - 1;
                            operandCount++;
                            break;

                        }
                    }
                    number = float.Parse(substr);

                    if (number > Int32.MaxValue || number < Int32.MinValue)
                    {
                        Console.WriteLine("Input is not in the range. Please enter input between 2147483647 and -2147483647");
                        return false;
                    }

                }

                // If the operation is a valid function it increments the operationCount. 
                else if (c == Constants.open_brace)
                {
                    openBraceCount++;
                    if ((currentOperation.ToString() == Constants.add) || (currentOperation.ToString() == Constants.sub) ||
                      (currentOperation.ToString() == Constants.mult) || (currentOperation.ToString() == Constants.div))
                    {
                        operationCount++;
                    }
                    else
                    {
                        validFunction = false;
                    }
                    currentOperation.Clear();
                }

                else if (c == Constants.comma)
                {
                    number = null;
                }

                else if (c == Constants.close_brace)
                {
                    closeBraceCount++;
                }
            }

            if (openBraceCount != closeBraceCount)
            {
                Console.WriteLine("Incorrect braces. Enter the input in the correct format.");
                return false;
            }
            if (!validFunction)
            {
                Console.WriteLine("Wrong function names. Please enter function names as: add(),sub(),mult(),div()");
                return false;
            }
            if (operandCount != operationCount + 1)
            {
                Console.WriteLine("Invalid input format.");
                return false;
            }


            return true;
        }


        /// <summary>
        /// This function returns the output based on given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public float getExpressionResult(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (Char.IsLetter(c))
                {
                    currentOperation.Append(c);
                }

                /* If the character is digit it loops over to the next char to get the complete number.
                 * Example: add(12.3,4) - So it gets 12.3 as one number and i is incremented to next char which is comma
                 */
                else if (Char.IsDigit(c))
                {
                    string substr = "";
                    for (int j = i; j < input.Length; j++)
                    {
                        if (input[j] != Constants.comma && input[j] != Constants.close_brace)
                        {
                            substr += input[j];
                        }
                        else
                        {
                            i = j - 1;
                            break;
                        }
                    }
                    number = float.Parse(substr);
                }

                // Pushes the current operation into operations stack and reset the currentOperation to null
                else if (c == Constants.open_brace)
                {
                    operations.Push(currentOperation.ToString());
                    currentOperation.Clear();
                }

                // Pushes the operands into stack and compute the recent expression
                else if (c == Constants.close_brace)
                {
                    if (number != null)
                    {
                        operands.Push((float)number);
                    }
                    computeExpression();
                    number = null;
                }

                // Pushes the operands into stack
                else if (c == Constants.comma)
                {
                    if (number != null)
                    {
                        operands.Push((float)number);
                    }
                    number = null;
                }
            }

            if (operands.Count == 0)
            {
                Console.WriteLine("Error: Invalid expression");
            }

            return operands.Pop();
        }

        /// <summary>
        /// This method computes the result of the operation and push it to stack.
        /// </summary>
        private void computeExpression()
        {
            float? functionValue = null;
            if (operands.Count > 1)
            {
                functionValue = performOperations(operations.Pop().ToString(), operands.Pop(), operands.Pop());
            }
            if (functionValue != null)
                operands.Push(functionValue.Value);
        }

        /// <summary>
        /// This function is used to perform the operations for given numbers.
        /// </summary>
        /// <param name="function"></param>
        /// <param name="operator1"></param>
        /// <param name="operator2"></param>
        /// <returns></returns>
        private float performOperations(string function, float operator1, float operator2)
        {
            switch (function)
            {
                case "add":
                    return operator1 + operator2;
                case "sub":
                    return Math.Abs(operator1 - operator2);
                case "mult":
                    return operator1 * operator2;
                case "div":
                    float result = 0;
                    try
                    {
                        result = Convert.ToInt32(operator2) / Convert.ToInt32(operator1);
                    }
                    catch (DivideByZeroException e)
                    {
                        Console.WriteLine("Exception caught: {0}", e.Message);
                        Program.Main();
                    }
                    return result;

                default:
                    return 0;
            }
        }
    }
}
