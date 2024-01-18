using System;

public class Calculator
{
    public double PerformOperation(double num1, char operation, double num2)
    {
        switch (operation)
        {
            case '+':
                return Add(num1, num2);
            case '-':
                return Subtract(num1, num2);
            case '*':
                return Multiply(num1, num2);
            case '/':
                return Divide(num1, num2);
            default:
                throw new InvalidOperationException("Невідома операція.");
        }
    }

    private double Add(double num1, double num2) => num1 + num2;

    private double Subtract(double num1, double num2) => num1 - num2;

    private double Multiply(double num1, double num2) => num1 * num2;

    private double Divide(double num1, double num2)
    {
        if (num2 == 0)
        {
            throw new ArgumentException("Ділення на нуль неможливе.", nameof(num2));
        }
        return num1 / num2;
    }
}

class Program
{
    static void Main()
    {
        Calculator calculator = new Calculator();

        Console.WriteLine("Введіть перше число:");
        double num1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Введіть операцію (+, -, *, /):");
        char operation = Convert.ToChar(Console.ReadLine());

        Console.WriteLine("Введіть друге число:");
        double num2 = Convert.ToDouble(Console.ReadLine());

        try
        {
            double result = calculator.PerformOperation(num1, operation, num2);
            Console.WriteLine($"Результат: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}
