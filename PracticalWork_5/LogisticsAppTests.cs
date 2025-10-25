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
            // Перед каждым тестом создаём чистые файлы с тестовыми данными
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

        // ТЕСТ 1: Проверка создания файлов - БЕЗ ОШИБОК
        [Fact]
        public void Test_FillTestData_CreatesDataFiles()
        {
            // Arrange & Act
            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

            // Assert
            Assert.True(Directory.Exists(dataPath), "Папка Data должна существовать");
            Assert.True(File.Exists(Path.Combine(dataPath, "Clients.json")), "Clients.json должен существовать");
            Assert.True(File.Exists(Path.Combine(dataPath, "Cargos.json")), "Cargos.json должен существовать");
        }

        // ТЕСТ 2: Добавление клиента - БЕЗ ОШИБОК
        [Fact]
        public void Test_AddClient_Test_AddsNewClient()
        {
            // Act
            Program.AddClient_Test("Новый клиент", "new@client.ru");

            // Assert
            var text = Program.ShowClients_Test();
            Assert.Contains("Новый клиент", text);
            Assert.Contains("new@client.ru", text);
        }

        // ТЕСТ 3: Отображение списка клиентов - БЕЗ ОШИБОК
        [Fact]
        public void Test_ShowClients_Test_ReturnsList()
        {
            // Act
            var output = Program.ShowClients_Test();

            // Assert
            Assert.Contains("=== Список клиентов ===", output);
            Assert.Contains("ООО Ромашка", output);
            Assert.Contains("Иван Иванов", output);
        }

        // ТЕСТ 4: Добавление заказа - БЕЗ ОШИБОК
        [Fact]
        public void Test_AddOrder_Test_AddsOrder()
        {
            // Act
            Program.AddOrder_Test(1, 1);

            // Assert
            string orders = Program.ListOrdersLastMonth_Test();
            Assert.Contains("ООО Ромашка", orders);
            Assert.Contains("Создан", orders);
        }

        // ТЕСТ 5: Список заказов за месяц - С ОШИБКОЙ (изменим период для демонстрации)
        [Fact]
        public void Test_ListOrdersLastMonth_Test_WrongPeriod()
        {
            // Arrange - создадим дополнительный заказ
            Program.AddOrder_Test(1, 1);

            // Act
            var output = Program.ListOrdersLastMonth_Test();

            // Assert - ОШИБКА: ожидаем строгое количество, но может быть больше
            int orderCount = output.Split(new string[] { "------------------------------" }, StringSplitOptions.None).Length - 1;

            // Ожидаем ровно 2 заказа, но может быть 3+ из-за неправильной фильтрации
            Assert.Equal(2, orderCount); // Может упасть, если заказов больше
        }

        // ТЕСТ 6: Проверка формата истории заказов - С ОШИБКОЙ
        [Fact]
        public void Test_ShowClients_OrderHistory_WrongFormat()
        {
            // Arrange
            Program.AddOrder_Test(1, 1); // Добавим заказ клиенту 1

            // Act
            var output = Program.ShowClients_Test();

            // Assert - ОШИБКА: ожидаем формат без пробелов "1,3" но будет "1, 3"
            Assert.Contains("История заказов: 1,3", output); // Упадет - формат с пробелами
        }

        // ТЕСТ 7: Статус доставки - БЕЗ ОШИБОК
        [Fact]
        public void Test_DeliveryStatus_Test_ValidCargo()
        {
            // Act
            var output = Program.DeliveryStatus_Test(2); // Груз 2 имеет доставку

            // Assert
            Assert.Contains("Статус доставки", output);
            Assert.Contains("Завершен", output);
        }

        // ТЕСТ 8: Среднее время доставки - БЕЗ ОШИБОК
        [Fact]
        public void Test_AverageDeliveryTime_Test_ShowsReport()
        {
            // Act
            var output = Program.AverageDeliveryTime_Test();

            // Assert - должен показать отчет или сообщение о пустом списке
            Assert.True(
                output.Contains("Среднее время доставки") ||
                output.Contains("Нет завершенных доставок")
            );
        }

        // ТЕСТ 9: Транспорт сегодня - С ОШИБКОЙ (проблема с временем)
        [Fact]
        public void Test_TransportLoadToday_Test_FindsTransport()
        {
            // Arrange - добавим заказ с транспортом на сегодня
            Program.AddOrder_Test(1, 1, 1, 1);

            // Act
            var output = Program.TransportLoadToday_Test();

            // Assert - ОШИБКА: может не найти из-за точного сравнения времени
            Assert.Contains("A123BC", output); // Должен найти транспорт
            Assert.DoesNotContain("Сегодня транспорт не задействован", output);
        }

        // ТЕСТ 10: Активные водители - БЕЗ ОШИБОК
        [Fact]
        public void Test_ActiveDrivers_Test_ReturnsDriverList()
        {
            // Act
            var output = Program.ActiveDrivers_Test();

            // Assert
            Assert.Contains("=== Водители с активными заказами ===", output);
            Assert.Contains("Петр Петров", output);
        }

        // ТЕСТ 11: Проверка пустого списка доставок
        [Fact]
        public void Test_AverageDeliveryTime_EmptyList()
        {
            // Arrange - очистим доставки
            ClearData();
            // Создадим данные без доставок
            var clients = new System.Collections.Generic.List<LogisticsManagementSystem.Client> {
                new LogisticsManagementSystem.Client{Id=1,Name="Тест",Contact="test@test.ru"}
            };
            LogisticsManagementSystem.DataStore<LogisticsManagementSystem.Client>.Save(clients);

            var orders = new System.Collections.Generic.List<LogisticsManagementSystem.Order> {
                new LogisticsManagementSystem.Order{Id=1,ClientId=1,CargoId=1,DateCreated=DateTime.Now,Status="Создан"}
            };
            LogisticsManagementSystem.DataStore<LogisticsManagementSystem.Order>.Save(orders);

            // Act
            var output = Program.AverageDeliveryTime_Test();

            // Assert
            Assert.Contains("Нет завершенных доставок", output);
        }
    }
}