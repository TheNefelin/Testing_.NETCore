using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class NumberOfDiscIntersectionsTest
{
    [Fact]  
    public void NumberOfDiscIntersectionsTest1()
    {
        // Arrange
        var numberOfDiscIntersections = new NumberOfDiscIntersections();
        int[] A = { 1, 5, 2, 1, 4, 0 };
        // Act
        int result = numberOfDiscIntersections.Solution(A);
        // Assert
        Assert.Equal(11, result);
    }

    [Fact]
    public void NumberOfDiscIntersectionsTest2()
    {
        // Arrange
        var numberOfDiscIntersections = new NumberOfDiscIntersections();
        int[] A = { 0, 0, 0 };
        // Act
        int result = numberOfDiscIntersections.Solution(A);
        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void NumberOfDiscIntersectionsTest3()
    {
        // Arrange
        var numberOfDiscIntersections = new NumberOfDiscIntersections();
        int[] A = { 2, 2, 2 };
        // Act
        int result = numberOfDiscIntersections.Solution(A);
        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void NumberOfDiscIntersectionsTest4()
    {
        // Arrange
        var numberOfDiscIntersections = new NumberOfDiscIntersections();
        int[] A = { 10, 0, 0, 0, 0 };
        // Act
        int result = numberOfDiscIntersections.Solution(A);
        // Assert
        Assert.Equal(4, result);
    }
}
