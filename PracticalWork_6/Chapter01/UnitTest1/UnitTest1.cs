using Chapter01;

namespace CalculatorLibUnitTests
{
    public class CalculatorUnitTests
    {
        [Fact]
        public void Test_Add()
        {
            // Arrange
            double a = 2;
            double b = 3;
            double expected = 5;
            Calculator calc = new();

            // Act
            double actual = calc.Add(a, b);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}