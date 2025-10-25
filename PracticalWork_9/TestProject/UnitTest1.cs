using NUnit.Framework;
using System;
using ArrayMultiplier;

namespace ArrayMultiplier.Tests
{
    /// <summary>
    /// Класс для тестирования ArrayProcessor с использованием NUnit
    /// </summary>
    [TestFixture]
    public class ArrayProcessorTests
    {
        // ====================================================================
        // ВАЛИДНЫЕ ТЕСТЫ (Классы эквивалентности)
        // ====================================================================

        [Test]
        [Category("КЭ1")]
        [Description("Тест #1: Один положительный элемент (граница n=1)")]
        public void Test01_SinglePositiveElement_ReturnsTripled()
        {
            // Arrange
            double[] input = { 5 };
            double[] expected = { 15 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Category("КЭ1")]
        [Description("Тест #2: Несколько положительных элементов")]
        public void Test02_MultiplePositiveElements_ReturnsTripled()
        {
            // Arrange
            double[] input = { 1, 2, 3, 4, 5 };
            double[] expected = { 3, 6, 9, 12, 15 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Category("КЭ2")]
        [Description("Тест #3: Отрицательные элементы")]
        public void Test03_NegativeElements_ReturnsTripled()
        {
            // Arrange
            double[] input = { -2, -5, -10 };
            double[] expected = { -6, -15, -30 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Category("КЭ3")]
        [Description("Тест #4: Нулевой элемент (граница)")]
        public void Test04_ZeroElement_ReturnsZero()
        {
            // Arrange
            double[] input = { 0 };
            double[] expected = { 0 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Category("КЭ3")]
        [Description("Тест #5: Массив с нулями")]
        public void Test05_ArrayWithZeros_ReturnsTripled()
        {
            // Arrange
            double[] input = { 0, 5, 0, -3 };
            double[] expected = { 0, 15, 0, -9 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Category("КЭ4")]
        [Description("Тест #6: Дробные числа")]
        public void Test06_DecimalNumbers_ReturnsTripled()
        {
            // Arrange
            double[] input = { 1.5, 2.7, 3.3 };
            double[] expected = { 4.5, 8.1, 9.9 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected).Within(1e-9));
        }

        [Test]
        [Category("КЭ5")]
        [Description("Тест #7: Смешанные значения")]
        public void Test07_MixedValues_ReturnsTripled()
        {
            // Arrange
            double[] input = { 10, -5, 0, 2.5 };
            double[] expected = { 30, -15, 0, 7.5 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected).Within(1e-9));
        }

        [Test]
        [Category("КЭ6")]
        [Description("Тест #8: Пустой массив (должен выбросить исключение)")]
        public void Test08_EmptyArray_ThrowsArgumentException()
        {
            // Arrange
            double[] input = { };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => ArrayProcessor.MultiplyBy3(input));
            Assert.That(ex.Message, Does.Contain("пустым"));
        }

        [Test]
        [Category("Граничные значения")]
        [Description("Тест #9: Большие числа")]
        public void Test09_LargeNumbers_ReturnsTripled()
        {
            // Arrange
            double[] input = { 1000000 };
            double[] expected = { 3000000 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Category("КЭ1")]
        [Description("Тест #10: Одинаковые элементы")]
        public void Test10_IdenticalElements_ReturnsTripled()
        {
            // Arrange
            double[] input = { 7, 7, 7, 7 };
            double[] expected = { 21, 21, 21, 21 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Category("КЭ4")]
        [Description("Тест #11: Отрицательные дробные числа")]
        public void Test11_NegativeDecimals_ReturnsTripled()
        {
            // Arrange
            double[] input = { -4.5, 0.3 };
            double[] expected = { -13.5, 0.9 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected).Within(1e-9));
        }

        [Test]
        [Category("Граничные значения")]
        [Description("Тест #12: Очень малое число")]
        public void Test12_VerySmallNumber_ReturnsTripled()
        {
            // Arrange
            double[] input = { 0.001 };
            double[] expected = { 0.003 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected).Within(1e-9));
        }

        [Test]
        [Category("КЭ7")]
        [Description("Тест #13: Null массив (должен выбросить исключение)")]
        public void Test13_NullArray_ThrowsArgumentNullException()
        {
            // Arrange
            double[]? input = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ArrayProcessor.MultiplyBy3(input!));
        }

        // ====================================================================
        // ПАРАМЕТРИЗОВАННЫЕ ТЕСТЫ (исправлены для избежания CS8600)
        // ====================================================================

        [Test]
        [Category("Параметризованные")]
        [Description("Тест #14a: Параметризованный - одно положительное число")]
        public void Test14a_Parameterized_SinglePositive()
        {
            // Arrange
            double[] input = { 1 };
            double[] expected = { 3 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Category("Параметризованные")]
        [Description("Тест #14b: Параметризованный - одно отрицательное число")]
        public void Test14b_Parameterized_SingleNegative()
        {
            // Arrange
            double[] input = { -1 };
            double[] expected = { -3 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Category("Параметризованные")]
        [Description("Тест #14c: Параметризованный - ноль")]
        public void Test14c_Parameterized_Zero()
        {
            // Arrange
            double[] input = { 0 };
            double[] expected = { 0 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Category("Параметризованные")]
        [Description("Тест #14d: Параметризованный - два числа")]
        public void Test14d_Parameterized_TwoNumbers()
        {
            // Arrange
            double[] input = { 2, 3 };
            double[] expected = { 6, 9 };

            // Act
            double[] result = ArrayProcessor.MultiplyBy3(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        // ====================================================================
        // SETUP И TEARDOWN
        // ====================================================================

        [SetUp]
        public void Setup()
        {
            // Выполняется перед каждым тестом
            TestContext.WriteLine("--- Начало теста ---");
        }

        [TearDown]
        public void TearDown()
        {
            // Выполняется после каждого теста
            TestContext.WriteLine("--- Конец теста ---");
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Выполняется один раз перед всеми тестами
            TestContext.WriteLine("====================================");
            TestContext.WriteLine("ЗАПУСК ТЕСТОВОГО НАБОРА");
            TestContext.WriteLine("====================================");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Выполняется один раз после всех тестов
            TestContext.WriteLine("====================================");
            TestContext.WriteLine("ЗАВЕРШЕНИЕ ТЕСТОВОГО НАБОРА");
            TestContext.WriteLine("====================================");
        }
    }
}


