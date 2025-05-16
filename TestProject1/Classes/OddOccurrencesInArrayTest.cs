using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class OddOccurrencesInArrayTest
{
    [Fact]
    public void OddOccurrencesInArrayTest1()
    {
        // Arrange
        var oddOccurrencesInArray = new OddOccurrencesInArray();
        int[] input = { 9, 3, 9, 3, 9, 7, 9 };
        int expected = 7;
        // Act
        int result = oddOccurrencesInArray.solution(input);
        // Assert
        Assert.Equal(expected, result);
    }
}
