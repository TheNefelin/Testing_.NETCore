using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class GroupNumsSameSumValueTest
{

    [Fact]
    public void GroupNumsTest1()
    {
        // Arrange
        GroupNumsSameSumValue groupNumsSameSumValue = new();
        int[] numbers = { 12, 21, 3, 30, 102, 201, 111 };
        // Act
        int[] result = groupNumsSameSumValue.Solution(numbers);
        // Assert
        Assert.Equal(new int[] { 12, 21, 3, 30, 102, 201, 111 }, result);
    }

    [Fact]
    public void GroupNumsTest2() 
    {
        // Arrange
        GroupNumsSameSumValue groupNumsSameSumValue = new();
        int[] numbers = { 12, 21, 3, 30, 102, 201, 111, 45, 54 };
        // Act
        int[] result = groupNumsSameSumValue.Solution(numbers);
        // Assert
        Assert.Equal(new int[] { 12, 21, 3, 30, 102, 201, 111 }, result);
    }

    [Fact]
    public void GroupNumsTest3() 
    {
        // Arrange
        GroupNumsSameSumValue groupNumsSameSumValue = new();
        int[] numbers = { 5 };
        // Act
        int[] result = groupNumsSameSumValue.Solution(numbers);
        // Assert
        Assert.Equal(new int[] { 5 }, result);
    }

    [Fact]
    public void GroupNumsTest4()
    {
        // Arrange
        GroupNumsSameSumValue groupNumsSameSumValue = new();
        int[] numbers = { 123, 456, 789, 12, 21, 3 };
        // Act
        int[] result = groupNumsSameSumValue.Solution(numbers);
        // Assert
        Assert.Equal(new int[] { 12, 21, 3 }, result);
    }

    [Fact]
    public void GroupNumsTest5() 
    {
        // Arrange
        GroupNumsSameSumValue groupNumsSameSumValue = new();
        int[] numbers = { 1234, 4321, 5678, 8765 };
        // Act
        int[] result = groupNumsSameSumValue.Solution(numbers);
        // Assert
        Assert.Equal(new int[] { 5678, 8765 }, result);
    }
}
