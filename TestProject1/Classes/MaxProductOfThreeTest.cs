using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class MaxProductOfThreeTest
{
    [Fact]
    public void MaxProductOfThreeTest1()
    {
        var maxProduct = new MaxProductOfThree();
        int[] A = { 1, 2, 3, 4 };
        int result = maxProduct.Solution(A);
        Assert.Equal(24, result);
    }

    [Fact]
    public void MaxProductOfThreeTest2()
    {
        var maxProduct = new MaxProductOfThree();
        int[] A = { -3, 1, 2, -2, 5, 6 };
        int result = maxProduct.Solution(A);
        Assert.Equal(60, result);
    }
}
