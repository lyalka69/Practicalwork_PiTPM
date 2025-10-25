using System;
using System.IO;
using Xunit;
using LogisticsManagementSystem;
using System.Linq;

namespace LogisticsManagementSystem.Tests
{
    /// <summary>
    /// Набор тестов для Лабораторной работы №8
    /// Тестирование методом "Белого ящика"
    /// Фокус: покрытие путей, условий, граничные значения, цикломатическая сложность
    /// </summary>
    public class WhiteBoxTests
    {
        public WhiteBoxTests()
        {
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

        // ========================================
        // ТЕСТ 1: Граничные значения - Пустой список клиентов
        // Цель: Проверка обработки пустого списка (0 элементов)
        // Путь: узел проверки Any() -> false
        // ========================================
        [Fact]
        public void Test1_ShowClients_EmptyList_ReturnsHeaderOnly()
        {
            // Arrange - очищаем всех клиентов
            ClearData();
            var emptyClients = new System.Collections.Generic.List<Client>();
            DataStore<Client>.Save(emptyClients);

            // Act
            var output = Program.ShowClients_Test();

            // Assert
            Assert.Contains("=== Список клиентов ===", output);
            // ОШИБКА: программа должна выводить сообщение "Список пуст"
            Assert.Contains("Список пуст", output); // УПАДЕТ - нет обработки пустого списка
        }

        // ========================================
        // ТЕСТ 2: Покрытие условий - Множественные проверки в AddOrder
        // Цель: Проверка сложного условия с AND (клиент И груз существуют)
        // Путь: проверка (!clients.Any(c => c.Id == clientId)) -> true
        // Цикломатическая сложность: V(G) = 3
        // ========================================
        [Fact]
        public void Test2_AddOrder_InvalidClientId_ThrowsException()
        {
            // Arrange
            int invalidClientId = 999; // несуществующий ID
            int validCargoId = 1;

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(
                () => Program.AddOrder_Test(invalidClientId, validCargoId)
            );

            // ОШИБКА: Сообщение об ошибке не локализовано
            Assert.Equal("Клиент не найден", exception.Message); // УПАДЕТ - будет "Client not found"
        }

        // ========================================
        // ТЕСТ 3: Покрытие путей - Вложенные условия в DeliveryStatus
        // Цель: Проверка пути order == null -> delivery != null
        // Граф потока: узлы 1->2->3(false)->4->5->6(true)->7
        // ========================================
        [Fact]
        public void Test3_DeliveryStatus_OrderNotFound_WrongMessage()
        {
            // Arrange
            int cargoIdWithoutOrder = 999;

            // Добавим груз без заказа
            var cargos = DataStore<Cargo>.Load();
            cargos.Add(new Cargo { Id = 999, Name = "Тестовый груз", Type = "Обычный", Weight = 100, Volume = 1 });
            DataStore<Cargo>.Save(cargos);

            // Act
            var output = Program.DeliveryStatus_Test(cargoIdWithoutOrder);

            // Assert
            // ОШИБКА: метод не различает "заказ не найден" и "доставка не создана"
            Assert.Contains("Заказ для данного груза не найден", output);
            Assert.DoesNotContain("Доставка еще не создана", output); // УПАДЕТ - оба сообщения похожи
        }

        // ========================================
        // ТЕСТ 4: Покрытие решений - Цикл foreach с условием внутри
        // Цель: Проверка ветвления внутри цикла (driver != null)
        // Путь: цикл с 0, 1, множественными итерациями
        // ========================================
        [Fact]
        public void Test4_ActiveDrivers_DriverNotInDatabase_SkipsEntry()
        {
            // Arrange - создадим заказ с несуществующим водителем
            var orders = DataStore<Order>.Load();
            orders.Add(new Order
            {
                Id = 100,
                ClientId = 1,
                CargoId = 1,
                DateCreated = DateTime.Now,
                Status = "В пути",
                DriverId = 999 // несуществующий водитель
            });
            DataStore<Order>.Save(orders);

            // Act
            var output = Program.ActiveDrivers_Test();

            // Assert
            // ОШИБКА: программа не обрабатывает случай, когда водитель не найден
            // Должна быть строка с предупреждением или пропуск, но без краша
            Assert.DoesNotContain("Id: 999", output); // Не должно быть несуществующего ID
            // УПАДЕТ если будет NullReferenceException или неожиданный вывод
        }

        // ========================================
        // ТЕСТ 5: Граничные значения - Один элемент в списке
        // Цель: Проверка работы с единственной доставкой
        // Покрытие: Average() на списке из 1 элемента
        // ========================================
        [Fact]
        public void Test5_AverageDeliveryTime_SingleDelivery_IncorrectCalculation()
        {
            // Arrange - оставим только одну доставку
            ClearData();

            var clients = new System.Collections.Generic.List<Client> {
                new Client{Id=1, Name="Тест", Contact="test@test.ru"}
            };
            DataStore<Client>.Save(clients);

            var orders = new System.Collections.Generic.List<Order> {
                new Order{
                    Id=1,
                    ClientId=1,
                    CargoId=1,
                    DateCreated=DateTime.Now.AddHours(-10), // 10 часов назад
                    Status="Завершен"
                }
            };
            DataStore<Order>.Save(orders);

            var deliveries = new System.Collections.Generic.List<Delivery> {
                new Delivery{
                    Id=1,
                    OrderId=1,
                    ActualArrival=DateTime.Now, // сейчас
                    Status="Завершен"
                }
            };
            DataStore<Delivery>.Save(deliveries);

            // Act
            var output = Program.AverageDeliveryTime_Test();

            // Assert
            Assert.Contains("Количество завершенных доставок: 1", output);
            // ОШИБКА: при 1 доставке среднее время может быть некорректно округлено
            // Ожидаем "0 дн. 10 ч. 0 мин.", но можем получить неточность
            Assert.Contains("10 ч.", output); // МОЖЕТ УПАСТЬ из-за округления TimeSpan
        }

        // ========================================
        // ТЕСТ 6: Покрытие путей - Сложное условие с OR
        // Цель: Проверка всех комбинаций условия (a≤1 ИЛИ b=0)
        // Базис путей: нужно минимум V(G) = 2 теста
        // Этот тест проверяет путь: оба условия FALSE
        // ========================================
        [Fact]
        public void Test6_TransportLoadToday_MultipleTransports_WrongCount()
        {
            // Arrange - создадим несколько заказов на сегодня
            var orders = DataStore<Order>.Load();

            // Очистим старые заказы
            orders.Clear();

            // Добавим 3 заказа с разным транспортом на сегодня
            for (int i = 1; i <= 3; i++)
            {
                orders.Add(new Order
                {
                    Id = i,
                    ClientId = 1,
                    CargoId = 1,
                    DateCreated = DateTime.Now, // СЕГОДНЯ
                    Status = "В пути",
                    TransportId = (i <= 2) ? 1 : 2 // транспорт 1 используется дважды, транспорт 2 - один раз
                });
            }
            DataStore<Order>.Save(orders);

            // Act
            var output = Program.TransportLoadToday_Test();

            // Assert
            // ОШИБКА: программа должна показывать количество УНИКАЛЬНЫХ транспортов
            // Ожидаем 2 транспорта, но вывод может показать 3 строки (если считает заказы, а не транспорт)
            int transportLines = output.Split(new[] { "Рег. номер:" }, StringSplitOptions.None).Length - 1;
            Assert.Equal(2, transportLines); // УПАДЕТ если подсчет неверен
        }
    }
}