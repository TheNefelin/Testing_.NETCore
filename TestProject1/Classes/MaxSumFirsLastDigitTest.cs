using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class MaxSumFirsLastDigitTest
{
    [Fact]
    public void TestMaxSumFirsLastDigit1()
    {
        MaxSumFirsLastDigit maxSumFirsLastDigit = new MaxSumFirsLastDigit();
        int result = maxSumFirsLastDigit.Solution(new int[] { 12, 21, 33, 42 });
        Assert.Equal(-1, result);
    }

    [Fact]
    public void TestMaxSumFirsLastDigit2()
    {
        MaxSumFirsLastDigit maxSumFirsLastDigit = new MaxSumFirsLastDigit();
        int result = maxSumFirsLastDigit.Solution(new int[] { 101, 11, 10, 25 });
        Assert.Equal(112, result);
    }

    [Fact]
    public void TestMaxSumFirsLastDigit3()
    {
        MaxSumFirsLastDigit maxSumFirsLastDigit = new MaxSumFirsLastDigit();
        int result = maxSumFirsLastDigit.Solution(new int[] { 101, 11, 10, 25, 121, 101 });
        Assert.Equal(334, result);
    }
}
