using NUnit.Framework;
using System;
using System.Collections.Generic;
using LogisticsApp;

namespace LogisticsApp.Tests
{
    /// <summary>
    /// Тесты с использованием Assert.That для проверки бизнес-логики
    /// </summary>
    [TestFixture]
    public class LogisticsBusinessLogicTests
    {
        /// <summary>
        /// Проверка равенства ID клиента
        /// </summary>
        [Test]
        public void CreateClient_ShouldAssignCorrectId()
        {
            // Arrange
            var clients = new List<Client>();
            int expectedId = 1;

            // Act
            var client = new Client
            {
                Id = expectedId,
                Name = "Тестовый клиент",
                Contact = "test@test.ru"
            };
            clients.Add(client);

            // Assert
            Assert.That(client.Id, Is.EqualTo(expectedId), "ID клиента должен быть равен 1");
        }

        /// <summary>
        /// Проверка неравенства ID клиентов
        /// </summary>
        [Test]
        public void TwoClients_ShouldHaveDifferentIds()
        {
            // Arrange & Act
            var client1 = new Client { Id = 1, Name = "Клиент 1", Contact = "client1@test.ru" };
            var client2 = new Client { Id = 2, Name = "Клиент 2", Contact = "client2@test.ru" };

            // Assert
            Assert.That(client1.Id, Is.Not.EqualTo(client2.Id), "ID клиентов должны отличаться");
        }

        /// <summary>
        /// Проверка, что объекты ссылаются на один экземпляр
        /// </summary>
        [Test]
        public void SameClientReference_ShouldPointToSameObject()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Клиент", Contact = "client@test.ru" };
            var clientReference = client;

            // Assert
            Assert.That(clientReference, Is.SameAs(client), "Ссылки должны указывать на один объект");
        }

        /// <summary>
        /// Проверка, что объекты разные
        /// </summary>
        [Test]
        public void TwoClientObjects_ShouldBeDifferentInstances()
        {
            // Arrange
            var client1 = new Client { Id = 1, Name = "Клиент 1", Contact = "client1@test.ru" };
            var client2 = new Client { Id = 1, Name = "Клиент 1", Contact = "client1@test.ru" };

            // Assert
            Assert.That(client1, Is.Not.SameAs(client2), "Это должны быть разные экземпляры объектов");
        }

        /// <summary>
        /// Проверка, что вес груза положительный
        /// </summary>
        [Test]
        public void Cargo_WeightShouldBePositive()
        {
            // Arrange
            var cargo = new Cargo { Id = 1, Name = "Товар", Weight = 100, Volume = 5 };

            // Assert
            Assert.That(cargo.Weight, Is.GreaterThan(0), "Вес груза должен быть положительным числом");
        }

        /// <summary>
        /// Проверка, что грузоподъемность не отрицательная
        /// </summary>
        [Test]
        public void Transport_CapacityShouldNotBeNegative()
        {
            // Arrange
            var transport = new Transport { Id = 1, Capacity = 1000, Registration = "A123BC" };

            // Assert
            Assert.That(transport.Capacity, Is.GreaterThanOrEqualTo(0), "Грузоподъемность не может быть отрицательной");
        }

        /// <summary>
        /// Проверка на null
        /// </summary>
        [Test]
        public void Order_OptionalDriverId_CanBeNull()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                ClientId = 1,
                CargoId = 1,
                DateCreated = DateTime.Now,
                Status = "Создан",
                DriverId = null
            };

            // Assert
            Assert.That(order.DriverId, Is.Null, "DriverId может быть null для нового заказа");
        }

        /// <summary>
        /// Проверка на не null
        /// </summary>
        [Test]
        public void Client_OrderHistory_ShouldNotBeNull()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Клиент", Contact = "test@test.ru" };

            // Assert
            Assert.That(client.OrderHistory, Is.Not.Null, "История заказов должна быть инициализирована");
        }

        /// <summary>
        /// Проверка типа объекта
        /// </summary>
        [Test]
        public void Driver_ShouldBeInstanceOfDriverType()
        {
            // Arrange
            var driver = new Driver
            {
                Id = 1,
                FullName = "Иван Иванов",
                ExperienceYears = 5
            };

            // Assert
            Assert.That(driver, Is.InstanceOf<Driver>(), "Объект должен быть типа Driver");
        }

        /// <summary>
        /// Проверка, что клиент не является водителем
        /// </summary>
        [Test]
        public void Client_ShouldNotBeInstanceOfDriverType()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Клиент", Contact = "test@test.ru" };

            // Assert
            Assert.That(client, Is.Not.InstanceOf<Driver>(), "Client не должен быть типа Driver");
        }

        /// <summary>
        /// Проверка свойств заказа
        /// </summary>
        [Test]
        public void CreateOrder_ShouldSetCorrectProperties()
        {
            // Arrange
            var clientId = 1;
            var cargoId = 1;
            var expectedStatus = "Создан";

            // Act
            var order = new Order
            {
                Id = 1,
                ClientId = clientId,
                CargoId = cargoId,
                DateCreated = DateTime.Now,
                Status = expectedStatus
            };

            // Assert
            Assert.That(order.ClientId, Is.EqualTo(clientId), "ClientId должен совпадать");
            Assert.That(order.CargoId, Is.EqualTo(cargoId), "CargoId должен совпадать");
            Assert.That(order.Status, Is.EqualTo(expectedStatus), "Статус должен быть 'Создан'");
            Assert.That(order.TransportId, Is.Null, "TransportId должен быть null");
            Assert.That(order, Is.Not.Null, "Заказ должен быть создан");
        }

        /// <summary>
        /// Проверка опыта водителя
        /// </summary>
        [Test]
        public void Driver_ExperienceShouldBeValid()
        {
            // Arrange
            var driver = new Driver
            {
                Id = 1,
                FullName = "Опытный водитель",
                ExperienceYears = 10
            };

            // Assert
            Assert.That(driver.ExperienceYears, Is.GreaterThan(0), "Опыт должен быть больше 0");
            Assert.That(driver.ExperienceYears, Is.GreaterThanOrEqualTo(5), "Для данной должности нужен опыт >= 5 лет");
        }

        /// <summary>
        /// Проверка, что история заказов нового клиента пуста
        /// </summary>
        [Test]
        public void Client_OrderHistoryShouldBeEmpty_WhenNewClientCreated()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Новый клиент", Contact = "new@test.ru" };

            // Assert
            Assert.That(client.OrderHistory, Is.Empty, "История заказов нового клиента должна быть пустой");
        }

        /// <summary>
        /// Проверка добавления заказа в историю клиента
        /// </summary>
        [Test]
        public void Client_AddOrderToHistory_ShouldIncreaseCount()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Клиент", Contact = "test@test.ru" };
            var orderId = 100;

            // Act
            client.OrderHistory.Add(orderId);

            // Assert
            Assert.That(client.OrderHistory, Is.Not.Empty, "История заказов не должна быть пустой");
            Assert.That(client.OrderHistory.Count, Is.EqualTo(1), "В истории должен быть 1 заказ");
            Assert.That(client.OrderHistory, Does.Contain(orderId), "История должна содержать добавленный ID заказа");
        }

        /// <summary>
        /// Проверка свойств транспорта с множественными проверками
        /// </summary>
        [Test]
        public void Transport_ShouldHaveValidProperties()
        {
            // Arrange
            var transport = new Transport
            {
                Id = 1,
                Type = "Грузовик",
                Capacity = 2000,
                Registration = "A123BC",
                Condition = "Хорошее"
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(transport.Id, Is.EqualTo(1));
                Assert.That(transport.Type, Is.EqualTo("Грузовик"));
                Assert.That(transport.Capacity, Is.GreaterThan(0));
                Assert.That(transport.Registration, Is.Not.Empty);
                Assert.That(transport.Condition, Is.Not.Null);
            });
        }

        /// <summary>
        /// Пример использования Assert.That
        /// </summary>
        [Test]
        public void Cargo_ShouldHaveValidWeightAndVolume()
        {
            // Arrange
            var cargo = new Cargo
            {
                Id = 1,
                Name = "Товар",
                Weight = 500,
                Volume = 2
            };

            // Assert
            Assert.That(cargo.Weight, Is.GreaterThan(0));
            Assert.That(cargo.Volume, Is.Positive);
            Assert.That(cargo.Name, Is.Not.Empty);
        }
    }
}
