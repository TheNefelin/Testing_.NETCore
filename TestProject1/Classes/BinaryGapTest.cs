using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class BinaryGapTest
{
    [Fact]
    public void BinaryGapTest1()
    {
        // Arrange
        var binaryGap = new BinaryGap();
        int number = 1041; // Binary: 10000010001
        int expected = 5;
        // Act
        int result = binaryGap.Solution(number);
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void BinaryGapTest2()
    {
        // Arrange
        var binaryGap = new BinaryGap();
        int number = 15; // Binary: 1111
        int expected = 0;
        // Act
        int result = binaryGap.Solution(number);
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void BinaryGapTest3()
    {
        // Arrange
        var binaryGap = new BinaryGap();
        int number = 32; // Binary: 100000
        int expected = 0;
        // Act
        int result = binaryGap.Solution(number);
        // Assert
        Assert.Equal(expected, result);
    }
}
