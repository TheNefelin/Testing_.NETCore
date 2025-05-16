using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class SortNamesTest
{
    [Fact]
    public void SortNamesTest1()
    {
        // Arrange
        var sortNames = new SortNames();
        string[] names = { "John", "Alice", "Bob" };
        string[] expected = { "Alice", "Bob", "John" };
        // Act
        string[] result = sortNames.Solution(names);
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void SortNamesTest2() {
        // Arrange
        var sortNames = new SortNames();
        string[] names = { "Juanka", "Carlos", "Alejandro", "Rodrigo" };
        string[] expected = { "Alejandro", "Carlos", "Juanka", "Rodrigo" };
        // Act
        string[] result = sortNames.Solution(names);
        // Assert
        Assert.Equal(expected, result);
    }
}
