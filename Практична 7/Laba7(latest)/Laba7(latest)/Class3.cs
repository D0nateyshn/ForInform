using System;
using Xunit;

public class CalculatorTests
{
    [Fact]
    public void PerformOperation_Addition()
    {
        Calculator calculator = new Calculator();

        double result = calculator.PerformOperation(5, '+', 3);

        Assert.Equal(8, result);
    }

    [Fact]
    public void PerformOperation_Subtraction()
    {
        Calculator calculator = new Calculator();

        double result = calculator.PerformOperation(8, '-', 3);

        Assert.Equal(5, result);
    }

    [Fact]
    public void PerformOperation_Multiplication()
    {
        Calculator calculator = new Calculator();

        double result = calculator.PerformOperation(4, '*', 6);

        Assert.Equal(24, result);
    }

    [Fact]
    public void PerformOperation_Division()
    {
        Calculator calculator = new Calculator();

        double result = calculator.PerformOperation(10, '/', 2);

        Assert.Equal(5, result);
    }

    [Fact]
    public void PerformOperation_DivideByZero()
    {
        Calculator calculator = new Calculator();

        Assert.Throws<ArgumentException>(() => calculator.PerformOperation(10, '/', 0));
    }
}
