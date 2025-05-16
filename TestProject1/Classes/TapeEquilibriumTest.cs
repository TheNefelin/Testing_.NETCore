using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class TapeEquilibriumTest
{
    [Fact]
    public void TapeEquilibriumTest1()
    {
        // Arrange
        var tapeEquilibrium = new TapeEquilibrium();
        int[] A = { 3, 1, 2, 4, 3 };
        int expected = 1;
        // Act
        int result = tapeEquilibrium.Solution(A);
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TapeEquilibriumTest2()
    {
        // Arrange
        var tapeEquilibrium = new TapeEquilibrium();
        int[] A = { 5, 6, 7, 8, 9 };
        int expected = 1;
        // Act
        int result = tapeEquilibrium.Solution(A);
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TapeEquilibriumTest3()
    {
        // Arrange
        var tapeEquilibrium = new TapeEquilibrium();
        int[] A = { 10, 5 };
        int expected = 5;
        // Act
        int result = tapeEquilibrium.Solution(A);
        // Assert
        Assert.Equal(expected, result);
    }
}
