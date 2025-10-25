using System;
using System.IO;
using Xunit;
using LogisticsManagementSystem;

namespace LogisticsManagementSystem.Tests
{
    public class LogisticsUnitTests
    {
        public LogisticsUnitTests()
        {
            // ����� ������ ������ ������ ������ ����� � ��������� �������
            ClearData();
            Program.FillTestData();
        }

        private void ClearData()
        {
            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (Directory.Exists(dataPath))
            {
                foreach (var file in Directory.GetFiles(dataPath))
                {
                    try { File.Delete(file); } catch { }
                }
            }
        }

        // ���� 1: �������� �������� ������ - ��� ������
        [Fact]
        public void Test_FillTestData_CreatesDataFiles()
        {
            // Arrange & Act
            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

            // Assert
            Assert.True(Directory.Exists(dataPath), "����� Data ������ ������������");
            Assert.True(File.Exists(Path.Combine(dataPath, "Clients.json")), "Clients.json ������ ������������");
            Assert.True(File.Exists(Path.Combine(dataPath, "Cargos.json")), "Cargos.json ������ ������������");
        }

        // ���� 2: ���������� ������� - ��� ������
        [Fact]
        public void Test_AddClient_Test_AddsNewClient()
        {
            // Act
            Program.AddClient_Test("����� ������", "new@client.ru");

            // Assert
            var text = Program.ShowClients_Test();
            Assert.Contains("����� ������", text);
            Assert.Contains("new@client.ru", text);
        }

        // ���� 3: ����������� ������ �������� - ��� ������
        [Fact]
        public void Test_ShowClients_Test_ReturnsList()
        {
            // Act
            var output = Program.ShowClients_Test();

            // Assert
            Assert.Contains("=== ������ �������� ===", output);
            Assert.Contains("��� �������", output);
            Assert.Contains("���� ������", output);
        }

        // ���� 4: ���������� ������ - ��� ������
        [Fact]
        public void Test_AddOrder_Test_AddsOrder()
        {
            // Act
            Program.AddOrder_Test(1, 1);

            // Assert
            string orders = Program.ListOrdersLastMonth_Test();
            Assert.Contains("��� �������", orders);
            Assert.Contains("������", orders);
        }

        // ���� 5: ������ ������� �� ����� - � ������� (������� ������ ��� ������������)
        [Fact]
        public void Test_ListOrdersLastMonth_Test_WrongPeriod()
        {
            // Arrange - �������� �������������� �����
            Program.AddOrder_Test(1, 1);

            // Act
            var output = Program.ListOrdersLastMonth_Test();

            // Assert - ������: ������� ������� ����������, �� ����� ���� ������
            int orderCount = output.Split(new string[] { "------------------------------" }, StringSplitOptions.None).Length - 1;

            // ������� ����� 2 ������, �� ����� ���� 3+ ��-�� ������������ ����������
            Assert.Equal(2, orderCount); // ����� ������, ���� ������� ������
        }

        // ���� 6: �������� ������� ������� ������� - � �������
        [Fact]
        public void Test_ShowClients_OrderHistory_WrongFormat()
        {
            // Arrange
            Program.AddOrder_Test(1, 1); // ������� ����� ������� 1

            // Act
            var output = Program.ShowClients_Test();

            // Assert - ������: ������� ������ ��� �������� "1,3" �� ����� "1, 3"
            Assert.Contains("������� �������: 1,3", output); // ������ - ������ � ���������
        }

        // ���� 7: ������ �������� - ��� ������
        [Fact]
        public void Test_DeliveryStatus_Test_ValidCargo()
        {
            // Act
            var output = Program.DeliveryStatus_Test(2); // ���� 2 ����� ��������

            // Assert
            Assert.Contains("������ ��������", output);
            Assert.Contains("��������", output);
        }

        // ���� 8: ������� ����� �������� - ��� ������
        [Fact]
        public void Test_AverageDeliveryTime_Test_ShowsReport()
        {
            // Act
            var output = Program.AverageDeliveryTime_Test();

            // Assert - ������ �������� ����� ��� ��������� � ������ ������
            Assert.True(
                output.Contains("������� ����� ��������") ||
                output.Contains("��� ����������� ��������")
            );
        }

        // ���� 9: ��������� ������� - � ������� (�������� � ��������)
        [Fact]
        public void Test_TransportLoadToday_Test_FindsTransport()
        {
            // Arrange - ������� ����� � ����������� �� �������
            Program.AddOrder_Test(1, 1, 1, 1);

            // Act
            var output = Program.TransportLoadToday_Test();

            // Assert - ������: ����� �� ����� ��-�� ������� ��������� �������
            Assert.Contains("A123BC", output); // ������ ����� ���������
            Assert.DoesNotContain("������� ��������� �� ������������", output);
        }

        // ���� 10: �������� �������� - ��� ������
        [Fact]
        public void Test_ActiveDrivers_Test_ReturnsDriverList()
        {
            // Act
            var output = Program.ActiveDrivers_Test();

            // Assert
            Assert.Contains("=== �������� � ��������� �������� ===", output);
            Assert.Contains("���� ������", output);
        }

        // ���� 11: �������� ������� ������ ��������
        [Fact]
        public void Test_AverageDeliveryTime_EmptyList()
        {
            // Arrange - ������� ��������
            ClearData();
            // �������� ������ ��� ��������
            var clients = new System.Collections.Generic.List<LogisticsManagementSystem.Client> {
                new LogisticsManagementSystem.Client{Id=1,Name="����",Contact="test@test.ru"}
            };
            LogisticsManagementSystem.DataStore<LogisticsManagementSystem.Client>.Save(clients);

            var orders = new System.Collections.Generic.List<LogisticsManagementSystem.Order> {
                new LogisticsManagementSystem.Order{Id=1,ClientId=1,CargoId=1,DateCreated=DateTime.Now,Status="������"}
            };
            LogisticsManagementSystem.DataStore<LogisticsManagementSystem.Order>.Save(orders);

            // Act
            var output = Program.AverageDeliveryTime_Test();

            // Assert
            Assert.Contains("��� ����������� ��������", output);
        }
    }
}