using CalculatorApplication;
public class Program
{
    public static void Main()
    {
        string input;
        bool calculateNewExp = true;
        Console.WriteLine("Welcome to calculator!!");
        Console.WriteLine("Operations accepted => addition - add, subtraction - sub, multiplication - mult, division - div ");
        Console.WriteLine("Each operation takes only two arguments");
        Console.WriteLine("Input expression format example : add(mult(2,sub(2,3)),div(3,3))" + "\n");
        Calculator calculator = new Calculator();
        Console.WriteLine("The result is : " + calculator.getExpressionResult(getUserInput()));
        while (calculateNewExp == true)
        {
            Console.WriteLine("Do you want to continue:");
            Console.WriteLine("Enter Y or N");
            input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                Console.WriteLine("The result is : " + calculator.getExpressionResult(getUserInput()));
            }
            else
            {
                calculateNewExp = false;
            }
        }
        Console.WriteLine("Completed!!");
    }


    /// <summary>
    /// This method gets the user input and validates it
    /// </summary>
    /// <returns></returns>
    public static string getUserInput()
    {
        bool valid_input = false;
        string user_input;
        string input_lower = "";
        Calculator calculator = new Calculator();
        while (!valid_input)
        {
            Console.WriteLine("Enter the input:");
            user_input = Console.ReadLine();
            input_lower = user_input.ToLower();
            valid_input = calculator.validateInput(input_lower);
        }
        return input_lower;
    }

}
