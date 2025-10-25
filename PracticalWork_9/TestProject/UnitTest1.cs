using NUnit.Framework;
using System;
using ArrayMultiplier;

namespace ArrayMultiplier.Tests
{
    /// <summary>
    /// ����� ��� ������������ ArrayProcessor � �������������� NUnit
    /// </summary>
    [TestFixture]
    public class ArrayProcessorTests
    {
        // ====================================================================
        // �������� ����� (������ ���������������)
        // ====================================================================

        [Test]
        [Category("��1")]
        [Description("���� #1: ���� ������������� ������� (������� n=1)")]
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
        [Category("��1")]
        [Description("���� #2: ��������� ������������� ���������")]
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
        [Category("��2")]
        [Description("���� #3: ������������� ��������")]
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
        [Category("��3")]
        [Description("���� #4: ������� ������� (�������)")]
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
        [Category("��3")]
        [Description("���� #5: ������ � ������")]
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
        [Category("��4")]
        [Description("���� #6: ������� �����")]
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
        [Category("��5")]
        [Description("���� #7: ��������� ��������")]
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
        [Category("��6")]
        [Description("���� #8: ������ ������ (������ ��������� ����������)")]
        public void Test08_EmptyArray_ThrowsArgumentException()
        {
            // Arrange
            double[] input = { };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => ArrayProcessor.MultiplyBy3(input));
            Assert.That(ex.Message, Does.Contain("������"));
        }

        [Test]
        [Category("��������� ��������")]
        [Description("���� #9: ������� �����")]
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
        [Category("��1")]
        [Description("���� #10: ���������� ��������")]
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
        [Category("��4")]
        [Description("���� #11: ������������� ������� �����")]
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
        [Category("��������� ��������")]
        [Description("���� #12: ����� ����� �����")]
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
        [Category("��7")]
        [Description("���� #13: Null ������ (������ ��������� ����������)")]
        public void Test13_NullArray_ThrowsArgumentNullException()
        {
            // Arrange
            double[]? input = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ArrayProcessor.MultiplyBy3(input!));
        }

        // ====================================================================
        // ����������������� ����� (���������� ��� ��������� CS8600)
        // ====================================================================

        [Test]
        [Category("�����������������")]
        [Description("���� #14a: ����������������� - ���� ������������� �����")]
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
        [Category("�����������������")]
        [Description("���� #14b: ����������������� - ���� ������������� �����")]
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
        [Category("�����������������")]
        [Description("���� #14c: ����������������� - ����")]
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
        [Category("�����������������")]
        [Description("���� #14d: ����������������� - ��� �����")]
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
        // SETUP � TEARDOWN
        // ====================================================================

        [SetUp]
        public void Setup()
        {
            // ����������� ����� ������ ������
            TestContext.WriteLine("--- ������ ����� ---");
        }

        [TearDown]
        public void TearDown()
        {
            // ����������� ����� ������� �����
            TestContext.WriteLine("--- ����� ����� ---");
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // ����������� ���� ��� ����� ����� �������
            TestContext.WriteLine("====================================");
            TestContext.WriteLine("������ ��������� ������");
            TestContext.WriteLine("====================================");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // ����������� ���� ��� ����� ���� ������
            TestContext.WriteLine("====================================");
            TestContext.WriteLine("���������� ��������� ������");
            TestContext.WriteLine("====================================");
        }
    }
}


