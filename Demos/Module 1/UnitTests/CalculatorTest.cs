using WithEF;

namespace UnitTests;

public class CalculatorTest
{
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(4, 7, 11)]
    [InlineData(4, 2, 6)]
    public void TestAdd(int x, int y, int expected)
    {
        // Arrange
        Calculator calc = new Calculator();

        // Act
        int actual = calc.Add(x, y);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1, 2, -1)]
    [InlineData(4, 7, -3)]
    [InlineData(4, 2, 2)]
    public void TestSubtract(int x, int y, int expected)
    {
        // Arrange
        Calculator calc = new Calculator();

        // Act
        int actual = calc.Subtract(x, y);

        // Assert
        Assert.Equal(expected, actual);
    }

}