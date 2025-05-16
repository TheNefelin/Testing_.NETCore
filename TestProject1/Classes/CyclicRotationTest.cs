using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class CyclicRotationTest
{
    [Fact]
    public void CyclicRotationTest1()
    {
        // Arrange
        var cyclicRotation = new CyclicRotation();
        int[] A = { 3, 8, 9, 7, 6 };
        int K = 3;
        int[] expected = { 9, 7, 6, 3, 8 };
        // Act
        int[] result = cyclicRotation.Solution(A, K);
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CyclicRotationTest2()
    {
        // Arrange
        var cyclicRotation = new CyclicRotation();
        int[] A = { 0, 0, 0 };
        int K = 1;
        int[] expected = { 0, 0, 0 };
        // Act
        int[] result = cyclicRotation.Solution(A, K);
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CyclicRotationTest3()
    {
        // Arrange
        var cyclicRotation = new CyclicRotation();
        int[] A = { 1, 2, 3, 4 };
        int K = 4;
        int[] expected = { 1, 2, 3, 4 };
        // Act
        int[] result = cyclicRotation.Solution(A, K);
        // Assert
        Assert.Equal(expected, result);
    }
}
