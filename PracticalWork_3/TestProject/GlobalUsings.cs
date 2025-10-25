global using NUnit.Framework;

namespace PiecewiseFunctionApp.Tests
{
    /// <summary>
    /// Класс для вычисления функции (извлечён из Form1 для тестирования)
    /// </summary>
    public class FunctionCalculator
    {
        public class CalculationResult
        {
            public bool Success { get; set; }
            public double Value { get; set; }
            public string ErrorMessage { get; set; }
            public int FormulaUsed { get; set; } // 1, 2, или 3
        }

        public static CalculationResult Calculate(double x, double a, double b, double c)
        {
            var result = new CalculationResult();

            try
            {
                // Первый случай: F = 1/(ax) - b
                if (x + 5 < 0 && c == 0)
                {
                    if (a * x == 0)
                    {
                        result.Success = false;
                        result.ErrorMessage = "Деление на ноль (a*x = 0)";
                        return result;
                    }
                    result.Value = 1.0 / (a * x) - b;
                    result.FormulaUsed = 1;
                    result.Success = true;
                }
                // Второй случай: F = (x - a)/(x - 1)
                else if (x + 5 > 0 && c != 0)
                {
                    if (x - 1 == 0)
                    {
                        result.Success = false;
                        result.ErrorMessage = "Деление на ноль (x - 1 = 0)";
                        return result;
                    }
                    result.Value = (x - a) / (x - 1);
                    result.FormulaUsed = 2;
                    result.Success = true;
                }
                // Третий случай: F = 10x/(c - 2)
                else
                {
                    if (c - 2 == 0)
                    {
                        result.Success = false;
                        result.ErrorMessage = "Деление на ноль (c - 2 = 0)";
                        return result;
                    }
                    result.Value = (10 * x) / (c - 2);
                    result.FormulaUsed = 3;
                    result.Success = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }
    }

    [TestFixture]
    [Description("Тесты для вычисления кусочно-заданной функции")]
    public class PiecewiseFunctionTests
    {
        private const double Epsilon = 0.01; // Точность сравнения

        #region Позитивные тесты

        [Test]
        [Category("Positive")]
        [Description("Тест №1: Первая формула F = 1/(ax) - b при x+5<0 и c=0")]
        public void Calculate_FirstFormula_ReturnsCorrectResult()
        {
            // Arrange
            double x = -10, a = 2, b = 3, c = 0;
            double expected = -3.05;

            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.True, "Вычисление должно быть успешным");
            Assert.That(result.FormulaUsed, Is.EqualTo(1), "Должна использоваться формула 1");
            Assert.That(result.Value, Is.EqualTo(expected).Within(Epsilon),
                $"Ожидается {expected}, получено {result.Value}");
        }

        [Test]
        [Category("Positive")]
        [Description("Тест №2: Вторая формула F = (x-a)/(x-1) при x+5>0 и c≠0")]
        public void Calculate_SecondFormula_ReturnsCorrectResult()
        {
            // Arrange
            double x = 5, a = 2, b = 1, c = 3;
            double expected = 0.75;

            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.FormulaUsed, Is.EqualTo(2), "Должна использоваться формула 2");
            Assert.That(result.Value, Is.EqualTo(expected).Within(Epsilon));
        }

        [Test]
        [Category("Positive")]
        [Description("Тест №3: Третья формула F = 10x/(c-2) в остальных случаях")]
        public void Calculate_ThirdFormula_ReturnsCorrectResult()
        {
            // Arrange
            double x = -3, a = 1, b = 1, c = 5;
            double expected = -10.00;

            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.FormulaUsed, Is.EqualTo(3), "Должна использоваться формула 3");
            Assert.That(result.Value, Is.EqualTo(expected).Within(Epsilon));
        }

        #endregion

        #region Негативные тесты (деление на ноль)

        [Test]
        [Category("Negative")]
        [Description("Тест №4: Деление на ноль в первой формуле (a*x=0)")]
        public void Calculate_FirstFormula_DivisionByZero_ReturnsError()
        {
            // Arrange
            double x = 0, a = 5, b = 1, c = 0;

            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.False, "Должна быть ошибка");
            Assert.That(result.ErrorMessage, Does.Contain("деление на ноль").IgnoreCase);
        }

        [Test]
        [Category("Negative")]
        [Description("Тест №5: Деление на ноль во второй формуле (x-1=0)")]
        public void Calculate_SecondFormula_DivisionByZero_ReturnsError()
        {
            // Arrange
            double x = 1, a = 2, b = 1, c = 3;

            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessage, Does.Contain("деление на ноль").IgnoreCase);
        }

        [Test]
        [Category("Negative")]
        [Description("Тест №6: Деление на ноль в третьей формуле (c-2=0)")]
        public void Calculate_ThirdFormula_DivisionByZero_ReturnsError()
        {
            // Arrange
            double x = 5, a = 1, b = 1, c = 2;

            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessage, Does.Contain("деление на ноль").IgnoreCase);
        }

        #endregion

        #region Граничные тесты

        [Test]
        [Category("Boundary")]
        [Description("Тест №7: Граничное значение x=-5 (x+5=0)")]
        public void Calculate_BoundaryValue_XEqualsMinusFive_UsesThirdFormula()
        {
            // Arrange
            double x = -5, a = 1, b = 1, c = 5;
            double expected = -16.67;

            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.FormulaUsed, Is.EqualTo(3), "x+5=0, должна использоваться формула 3");
            Assert.That(result.Value, Is.EqualTo(expected).Within(Epsilon));
        }

        [Test]
        [Category("Boundary")]
        [Description("Тест №10: Очень малые числа")]
        public void Calculate_VerySmallNumbers_NoOverflow()
        {
            // Arrange
            double x = -10, a = 0.0000001, b = 1, c = 0;

            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.True, "Вычисление должно завершиться успешно");
            Assert.That(result.Value, Is.Not.NaN, "Результат не должен быть NaN");
            Assert.That(result.Value, Is.Not.EqualTo(double.PositiveInfinity));
            Assert.That(result.Value, Is.Not.EqualTo(double.NegativeInfinity));
        }

        [Test]
        [Category("Boundary")]
        [Description("Тест №11: Очень большие числа (проверка на Infinity)")]
        public void Calculate_VeryLargeNumbers_MayProduceInfinity()
        {
            // Arrange
            double x = 1e100, a = 1e100, b = 1, c = 3;

            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.True);
            // ВНИМАНИЕ: Этот тест документирует известную проблему
            // В идеале нужно добавить проверку на Infinity в FunctionCalculator
            Assert.Warn("Результат может быть Infinity при очень больших числах");
        }

        #endregion

        #region Параметризованные тесты

        [TestCase(-10, 2, 3, 0, -3.05, 1, TestName = "Формула 1: x=-10")]
        [TestCase(-6, 1, 0, 0, -1.0, 1, TestName = "Формула 1: x=-6")]
        [TestCase(5, 2, 1, 3, 0.75, 2, TestName = "Формула 2: x=5")]
        [TestCase(10, 3, 2, 5, 0.78, 2, TestName = "Формула 2: x=10")]
        [TestCase(-3, 1, 1, 5, -10.0, 3, TestName = "Формула 3: x=-3")]
        [TestCase(0, 2, 3, 10, 0.0, 3, TestName = "Формула 3: x=0")]
        [Category("Parametrized")]
        [Description("Параметризованные тесты для всех формул")]
        public void Calculate_VariousInputs_ReturnsExpectedResults(
            double x, double a, double b, double c,
            double expected, int expectedFormula)
        {
            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.FormulaUsed, Is.EqualTo(expectedFormula));
            Assert.That(result.Value, Is.EqualTo(expected).Within(Epsilon));
        }

        [TestCase(0, 5, 1, 0, TestName = "Деление на ноль: a*x=0")]
        [TestCase(1, 2, 1, 3, TestName = "Деление на ноль: x-1=0")]
        [TestCase(5, 1, 1, 2, TestName = "Деление на ноль: c-2=0")]
        [Category("Parametrized")]
        [Description("Параметризованные негативные тесты")]
        public void Calculate_DivisionByZero_ReturnsError(double x, double a, double b, double c)
        {
            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessage, Does.Contain("деление на ноль").IgnoreCase);
        }

        #endregion

        #region Дополнительные тесты

        [Test]
        [Category("Coverage")]
        [Description("Проверка покрытия всех трёх формул")]
        public void Calculate_AllFormulas_AreCovered()
        {
            // Act
            var result1 = FunctionCalculator.Calculate(-10, 2, 3, 0);
            var result2 = FunctionCalculator.Calculate(5, 2, 1, 3);
            var result3 = FunctionCalculator.Calculate(-3, 1, 1, 5);

            // Assert
            Assert.That(result1.FormulaUsed, Is.EqualTo(1), "Формула 1 не покрыта");
            Assert.That(result2.FormulaUsed, Is.EqualTo(2), "Формула 2 не покрыта");
            Assert.That(result3.FormulaUsed, Is.EqualTo(3), "Формула 3 не покрыта");
        }

        [Test]
        [Category("EdgeCase")]
        [Description("Нулевые значения параметров")]
        public void Calculate_ZeroParameters_ReturnsZero()
        {
            // Arrange
            double x = 0, a = 0, b = 0, c = 5;
            double expected = 0.0; // 10*0/(5-2) = 0

            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Value, Is.EqualTo(expected).Within(Epsilon));
        }

        [Test]
        [Category("EdgeCase")]
        [Description("Отрицательные значения всех параметров")]
        public void Calculate_AllNegativeValues_ReturnsCorrectResult()
        {
            // Arrange
            double x = -10, a = -2, b = -3, c = 0;
            double expected = 3.05; // F = 1/(-2*-10) - (-3) = 1/20 + 3 = 3.05

            // Act
            var result = FunctionCalculator.Calculate(x, a, b, c);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.FormulaUsed, Is.EqualTo(1));
            Assert.That(result.Value, Is.EqualTo(expected).Within(Epsilon));
        }

        #endregion

        #region Setup и TearDown (опционально)

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Выполняется один раз перед всеми тестами
            TestContext.WriteLine("=== Начало тестирования PiecewiseFunctionApp ===");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Выполняется один раз после всех тестов
            TestContext.WriteLine("=== Завершение тестирования ===");
        }

        [SetUp]
        public void Setup()
        {
            // Выполняется перед каждым тестом
            TestContext.WriteLine($"Запуск теста: {TestContext.CurrentContext.Test.Name}");
        }

        [TearDown]
        public void TearDown()
        {
            // Выполняется после каждого теста
            var result = TestContext.CurrentContext.Result;
            TestContext.WriteLine($"Результат: {result.Outcome.Status}");
        }

        #endregion
    }
}