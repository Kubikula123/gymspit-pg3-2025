bool calculation = true;
bool IsNumber = true;
while (calculation == true)
{
    Console.WriteLine("Choose an operator (+; -; *; /; exit): ");
    string input = Console.ReadLine();
    if (input == "exit")
    {
        calculation = false;
    }
    else if (input == "+" || input == "-" || input == "*" || input == "/")
    {
        Console.WriteLine("Enter the first number: ");
        IsNumber = float.TryParse(Console.ReadLine(), out float num1);
        if (IsNumber == false)
        {
            Console.WriteLine("Invalid number, try again");
            continue;
        }
        Console.WriteLine("Enter the second number: ");
        IsNumber = float.TryParse(Console.ReadLine(), out float num2);
        if (IsNumber == false)
        {
            Console.WriteLine("Invalid number, try again");
            continue;
        }
        if (input == "+")
        {
            Console.WriteLine("{0} + {1} = " + (num1 + num2), num1, num2);
        }
        else if (input == "-")
        {
            Console.WriteLine("{0} - {1} = " + (num1 - num2), num1, num2);
        }
        else if (input == "*")
        {
            Console.WriteLine("{0} * {1} = " + (num1 * num2), num1, num2);
        }
        else if (input == "/")
        {
            Console.WriteLine("{0} / {1} = " + (num1 / num2), num1, num2);
        }

        
    }

    else
    {
        Console.WriteLine("Invalid operator, try again");
    }
}

    
