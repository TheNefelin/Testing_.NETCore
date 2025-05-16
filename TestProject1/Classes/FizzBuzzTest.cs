using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class FizzBuzzTest
{
    [Fact]
    public void FizzBuzzTest1()
    {
        FizzBuzz fizzBuzz = new FizzBuzz();
        Dictionary<int, string> result = fizzBuzz.Solution(1, 15);

        Assert.Equal("1", result[1]);
        Assert.Equal("2", result[2]);
        Assert.Equal("Fizz", result[3]);
        Assert.Equal("4", result[4]);
        Assert.Equal("Buzz", result[5]);
        Assert.Equal("Fizz", result[6]);
        Assert.Equal("7", result[7]);
        Assert.Equal("8", result[8]);
        Assert.Equal("Fizz", result[9]);
        Assert.Equal("Buzz", result[10]);
        Assert.Equal("11", result[11]);
        Assert.Equal("Fizz", result[12]);
        Assert.Equal("13", result[13]);
        Assert.Equal("14", result[14]);
        Assert.Equal("FizzBuzz", result[15]);
    }
}
