// See https://aka.ms/new-console-template for more information
int num;
Console.WriteLine("Enter a number:");
num = int.Parse(Console.ReadLine());

for (int i = 1;i <= num; i++)
{
    if (i % 3 == 0 && i % 5 == 0)
    {
        Console.Write("Fizz Buzz, ");
    }
    else if (i % 3 == 0) 
    {
        Console.Write("Fizz, ");
    }
    else if (i % 5 == 0)
    {
        Console.Write("Buzz, ");
    }
    else
    {
        Console.Write(i + ", ");
    }
}
