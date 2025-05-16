using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class PairsWithTheSameSumTest
{
    [Fact]
    public void PairsWithTheSameSumTest1()
    {
        // Arrange
        var pairsWithTheSameSum = new PairsWithTheSameSum();
        int[] numbers = { 1, 3, 2, 2, 4, 0 };
        // Act
        var result = pairsWithTheSameSum.Solution(numbers);
        // Assert
        int sum1 = numbers[result.Item1.Item1] + numbers[result.Item1.Item2];
        int sum2 = numbers[result.Item2.Item1] + numbers[result.Item2.Item2];
        Assert.Equal(sum1, sum2);
        Assert.Equal(6, sum1);
        Assert.NotEqual(result.Item1, result.Item2);
    }

    [Fact]
    public void PairsWithTheSameSumTest2()
    {
        // Arrange
        var pairsWithTheSameSum = new PairsWithTheSameSum();
        int[] numbers = { 1, 2, 3, 4, 5 };
        // Act
        var result = pairsWithTheSameSum.Solution(numbers);
        // Assert
        int sum1 = numbers[result.Item1.Item1] + numbers[result.Item1.Item2];
        int sum2 = numbers[result.Item2.Item1] + numbers[result.Item2.Item2];
        Assert.Equal(sum1, sum2);
        Assert.Equal(7, sum1);
        Assert.NotEqual(result.Item1, result.Item2);
    }
}
