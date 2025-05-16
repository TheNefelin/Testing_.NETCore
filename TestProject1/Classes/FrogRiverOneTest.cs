using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class FrogRiverOneTest
{
    [Fact]
    public void FrogRiverOneTest1()
    {
        // Arrange
        var frogRiverOne = new FrogRiverOne();
        int X = 5;
        int[] A = { 1, 3, 1, 4, 2, 3, 5, 4 };
        // Act
        int result = frogRiverOne.Solution(X, A);
        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void FrogRiverOneTest2()
    {
        // Arrange
        var frogRiverOne = new FrogRiverOne();
        int X = 5;
        int[] A = { 1, 3, 3, 4 };
        // Act
        int result = frogRiverOne.Solution(X, A);
        // Assert
        Assert.Equal(-1, result);
    }

    [Fact]
    public void FrogRiverOneTest3()
    {
        // Arrange
        var frogRiverOne = new FrogRiverOne();
        int X = 1;
        int[] A = { 1 };
        // Act
        int result = frogRiverOne.Solution(X, A);
        // Assert
        Assert.Equal(0, result);
    }
}
