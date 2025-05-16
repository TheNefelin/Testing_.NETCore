using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class EquilibriumIndexTest
{
    [Theory]
    [InlineData(new int[] { -1, 3, -4, 5, 1, -6, 2, 1 }, new int[] { 1, 3, 7 })]
    [InlineData(new int[] { 1, 2, 3 }, new int[] { })]
    [InlineData(new int[] { 2, 4, 2 }, new int[] { 1 })]
    [InlineData(new int[] { 0, -1, 1 }, new int[] { 0 })]
    [InlineData(new int[] { -7, 1, 2, 3, 4, -3 }, new int[] { })]
    [InlineData(new int[] { }, new int[] { })]
    [InlineData(new int[] { 0 }, new int[] { 0 })]
    public void TestEquilibriumIndexes(int[] input, int[] expected)
    {
        var solver = new EquilibriumIndex();
        var result = solver.Solution(input);
        Assert.Equal(expected, result);
    }
}
