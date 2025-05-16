using ConsoleApp1.Classes;

namespace TestProject1.Classes
{
    public class MissingIntegerTest
    {
        [Fact]
        public void MissingIntegerTest1()
        {
            // Arrange
            MissingInteger missingInteger = new MissingInteger();
            int[] A = { 1, 3, 6, 4, 1, 2 };
            // Act
            int result = missingInteger.Solution(A);
            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void MissingIntegerTest2()
        {
            // Arrange
            var missingInteger = new MissingInteger();
            int[] input = { 1, 2, 3 };
            int expected = 4;
            // Act
            int result = missingInteger.Solution(input);
            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MissingIntegerTest3()
        {
            // Arrange
            var missingInteger = new MissingInteger();
            int[] input = { -1, -3 };
            int expected = 1;
            // Act
            int result = missingInteger.Solution(input);
            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MissingIntegerTest4()
        {
            // Arrange
            var missingInteger = new MissingInteger();
            int[] input = Enumerable.Range(1, 100000).ToArray();
            int expected = 100001;
            // Act
            int result = missingInteger.Solution(input);
            // Assert
            Assert.Equal(expected, result);
        }
    }
}
