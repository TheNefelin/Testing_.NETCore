using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class SortNumberTest
{
    [Fact]
    public void TestSortNumber1()
    {
        SortNumber sortNumber = new SortNumber();
        int[] result = sortNumber.Solution(new int[] { 3, 1, 2 });
        Assert.Equal(new int[] { 1, 2, 3 }, result);
    }

    [Fact]
    public void TestSortNumber2()
    {
        SortNumber sortNumber = new SortNumber();
        int[] result = sortNumber.Solution(new int[] { 5, 2, 9, 1, 7 });
        Assert.Equal(new int[] { 1, 2, 5, 7, 9 }, result);
    }
}
