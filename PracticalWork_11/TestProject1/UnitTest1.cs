using NUnit.Framework;
using System;
using System.Collections.Generic;
using LogisticsApp;

namespace LogisticsApp.Tests
{
    /// <summary>
    /// ����� � �������������� Assert.That ��� �������� ������-������
    /// </summary>
    [TestFixture]
    public class LogisticsBusinessLogicTests
    {
        /// <summary>
        /// �������� ��������� ID �������
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
                Name = "�������� ������",
                Contact = "test@test.ru"
            };
            clients.Add(client);

            // Assert
            Assert.That(client.Id, Is.EqualTo(expectedId), "ID ������� ������ ���� ����� 1");
        }

        /// <summary>
        /// �������� ����������� ID ��������
        /// </summary>
        [Test]
        public void TwoClients_ShouldHaveDifferentIds()
        {
            // Arrange & Act
            var client1 = new Client { Id = 1, Name = "������ 1", Contact = "client1@test.ru" };
            var client2 = new Client { Id = 2, Name = "������ 2", Contact = "client2@test.ru" };

            // Assert
            Assert.That(client1.Id, Is.Not.EqualTo(client2.Id), "ID �������� ������ ����������");
        }

        /// <summary>
        /// ��������, ��� ������� ��������� �� ���� ���������
        /// </summary>
        [Test]
        public void SameClientReference_ShouldPointToSameObject()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "������", Contact = "client@test.ru" };
            var clientReference = client;

            // Assert
            Assert.That(clientReference, Is.SameAs(client), "������ ������ ��������� �� ���� ������");
        }

        /// <summary>
        /// ��������, ��� ������� ������
        /// </summary>
        [Test]
        public void TwoClientObjects_ShouldBeDifferentInstances()
        {
            // Arrange
            var client1 = new Client { Id = 1, Name = "������ 1", Contact = "client1@test.ru" };
            var client2 = new Client { Id = 1, Name = "������ 1", Contact = "client1@test.ru" };

            // Assert
            Assert.That(client1, Is.Not.SameAs(client2), "��� ������ ���� ������ ���������� ��������");
        }

        /// <summary>
        /// ��������, ��� ��� ����� �������������
        /// </summary>
        [Test]
        public void Cargo_WeightShouldBePositive()
        {
            // Arrange
            var cargo = new Cargo { Id = 1, Name = "�����", Weight = 100, Volume = 5 };

            // Assert
            Assert.That(cargo.Weight, Is.GreaterThan(0), "��� ����� ������ ���� ������������� ������");
        }

        /// <summary>
        /// ��������, ��� ���������������� �� �������������
        /// </summary>
        [Test]
        public void Transport_CapacityShouldNotBeNegative()
        {
            // Arrange
            var transport = new Transport { Id = 1, Capacity = 1000, Registration = "A123BC" };

            // Assert
            Assert.That(transport.Capacity, Is.GreaterThanOrEqualTo(0), "���������������� �� ����� ���� �������������");
        }

        /// <summary>
        /// �������� �� null
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
                Status = "������",
                DriverId = null
            };

            // Assert
            Assert.That(order.DriverId, Is.Null, "DriverId ����� ���� null ��� ������ ������");
        }

        /// <summary>
        /// �������� �� �� null
        /// </summary>
        [Test]
        public void Client_OrderHistory_ShouldNotBeNull()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "������", Contact = "test@test.ru" };

            // Assert
            Assert.That(client.OrderHistory, Is.Not.Null, "������� ������� ������ ���� ����������������");
        }

        /// <summary>
        /// �������� ���� �������
        /// </summary>
        [Test]
        public void Driver_ShouldBeInstanceOfDriverType()
        {
            // Arrange
            var driver = new Driver
            {
                Id = 1,
                FullName = "���� ������",
                ExperienceYears = 5
            };

            // Assert
            Assert.That(driver, Is.InstanceOf<Driver>(), "������ ������ ���� ���� Driver");
        }

        /// <summary>
        /// ��������, ��� ������ �� �������� ���������
        /// </summary>
        [Test]
        public void Client_ShouldNotBeInstanceOfDriverType()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "������", Contact = "test@test.ru" };

            // Assert
            Assert.That(client, Is.Not.InstanceOf<Driver>(), "Client �� ������ ���� ���� Driver");
        }

        /// <summary>
        /// �������� ������� ������
        /// </summary>
        [Test]
        public void CreateOrder_ShouldSetCorrectProperties()
        {
            // Arrange
            var clientId = 1;
            var cargoId = 1;
            var expectedStatus = "������";

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
            Assert.That(order.ClientId, Is.EqualTo(clientId), "ClientId ������ ���������");
            Assert.That(order.CargoId, Is.EqualTo(cargoId), "CargoId ������ ���������");
            Assert.That(order.Status, Is.EqualTo(expectedStatus), "������ ������ ���� '������'");
            Assert.That(order.TransportId, Is.Null, "TransportId ������ ���� null");
            Assert.That(order, Is.Not.Null, "����� ������ ���� ������");
        }

        /// <summary>
        /// �������� ����� ��������
        /// </summary>
        [Test]
        public void Driver_ExperienceShouldBeValid()
        {
            // Arrange
            var driver = new Driver
            {
                Id = 1,
                FullName = "������� ��������",
                ExperienceYears = 10
            };

            // Assert
            Assert.That(driver.ExperienceYears, Is.GreaterThan(0), "���� ������ ���� ������ 0");
            Assert.That(driver.ExperienceYears, Is.GreaterThanOrEqualTo(5), "��� ������ ��������� ����� ���� >= 5 ���");
        }

        /// <summary>
        /// ��������, ��� ������� ������� ������ ������� �����
        /// </summary>
        [Test]
        public void Client_OrderHistoryShouldBeEmpty_WhenNewClientCreated()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "����� ������", Contact = "new@test.ru" };

            // Assert
            Assert.That(client.OrderHistory, Is.Empty, "������� ������� ������ ������� ������ ���� ������");
        }

        /// <summary>
        /// �������� ���������� ������ � ������� �������
        /// </summary>
        [Test]
        public void Client_AddOrderToHistory_ShouldIncreaseCount()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "������", Contact = "test@test.ru" };
            var orderId = 100;

            // Act
            client.OrderHistory.Add(orderId);

            // Assert
            Assert.That(client.OrderHistory, Is.Not.Empty, "������� ������� �� ������ ���� ������");
            Assert.That(client.OrderHistory.Count, Is.EqualTo(1), "� ������� ������ ���� 1 �����");
            Assert.That(client.OrderHistory, Does.Contain(orderId), "������� ������ ��������� ����������� ID ������");
        }

        /// <summary>
        /// �������� ������� ���������� � �������������� ����������
        /// </summary>
        [Test]
        public void Transport_ShouldHaveValidProperties()
        {
            // Arrange
            var transport = new Transport
            {
                Id = 1,
                Type = "��������",
                Capacity = 2000,
                Registration = "A123BC",
                Condition = "�������"
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(transport.Id, Is.EqualTo(1));
                Assert.That(transport.Type, Is.EqualTo("��������"));
                Assert.That(transport.Capacity, Is.GreaterThan(0));
                Assert.That(transport.Registration, Is.Not.Empty);
                Assert.That(transport.Condition, Is.Not.Null);
            });
        }

        /// <summary>
        /// ������ ������������� Assert.That
        /// </summary>
        [Test]
        public void Cargo_ShouldHaveValidWeightAndVolume()
        {
            // Arrange
            var cargo = new Cargo
            {
                Id = 1,
                Name = "�����",
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
