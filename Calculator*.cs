
class Program
{
    static void Main()
    {
        RunCalculator();
    }

    static void RunCalculator()
    {
        bool calculation = true;

        while (calculation)
        {
            string operation = GetOperator();

            if (operation == "exit")
            {
                calculation = false;
                Console.WriteLine("Goodbye!");
                break;
            }

            if (IsValidOperator(operation))
            {
                float num1 = GetNumber("Enter the first number: ");
                float num2 = GetNumber("Enter the second number: ");

                if (operation == "/" && num2 == 0)
                {
                    Console.WriteLine("Cannot divide by zero.\n");
                    continue;
                }

                float result = Calculate(num1, num2, operation);
                DisplayResult(num1, num2, operation, result);
            }
            else
            {
                Console.WriteLine("Invalid operator, try again.\n");
            }
        }
    }

    static string GetOperator()
    {
        Console.WriteLine("Choose an operator (+, -, *, /, exit): ");
        return Console.ReadLine();
    }

    static bool IsValidOperator(string op)
    {
        return op == "+" || op == "-" || op == "*" || op == "/";
    }

    // Safely get a number from the user
    static float GetNumber(string message)
    {
        float number;
        bool valid;
        do
        {
            Console.Write(message);
            valid = float.TryParse(Console.ReadLine(), out number);
            if (!valid)
            {
                Console.WriteLine("Invalid number, try again.");
            }
        } while (!valid);
        return number;
    }

    static float Calculate(float num1, float num2, string op)
    {
        return op switch
        {
            "+" => num1 + num2,
            "-" => num1 - num2,
            "*" => num1 * num2,
            "/" => num1 / num2,
            _ => 0
        };
    }

    static void DisplayResult(float num1, float num2, string op, float result)
    {
        Console.WriteLine($"{num1} {op} {num2} = {result}\n");
    }
}
