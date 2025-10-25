using LogisticsApp;
using LogisticsManagementSystem;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace LogisticsApp.Tests
{
    [TestFixture]
    public class LogisticsStringTests
    {
        /// <summary>
        /// Тест 1: Проверка, что контактная информация клиента содержит символ '@' (email)
        /// Использует StringAssert.Contains
        /// </summary>
        [Test]
        public void ClientContact_ShouldContainAtSymbol_WhenEmailProvided()
        {
            // Arrange
            var client = new Client
            {
                Id = 1,
                Name = "Тестовая Компания",
                Contact = "test@company.ru"
            };

            // Act & Assert
            StringAssert.Contains(client.Contact, "@",
                "Контактная информация должна содержать символ '@' для email адреса");
        }

        /// <summary>
        /// Тест 2: Проверка, что регистрационный номер транспорта начинается с буквы
        /// Использует StringAssert.StartsWith
        /// </summary>
        [Test]
        public void TransportRegistration_ShouldStartWithLetter_WhenRegistrationNumberValid()
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

            // Act & Assert
            StringAssert.StartsWith(transport.Registration, "A",
                "Регистрационный номер должен начинаться с буквы 'A'");
        }

        /// <summary>
        /// Тест 3: Проверка, что тип груза соответствует допустимому формату (буквы и пробелы)
        /// Использует StringAssert.Matches
        /// </summary>
        [Test]
        public void CargoType_ShouldMatchValidPattern_WhenTypeIsCorrect()
        {
            // Arrange
            var cargo = new Cargo
            {
                Id = 1,
                Name = "Компьютеры",
                Type = "Электроника",
                Weight = 500,
                Volume = 2,
                Requirements = "Охлаждение"
            };

            // Регулярное выражение: только кириллица и пробелы
            var validTypePattern = new Regex(@"^[А-Яа-яЁё\s]+$");

            // Act & Assert
            StringAssert.Matches(cargo.Type, validTypePattern,
                "Тип груза должен содержать только кириллические буквы и пробелы");
        }

        /// <summary>
        /// Тест 4: Проверка, что имя водителя не содержит цифр
        /// Использует StringAssert.DoesNotMatch
        /// </summary>
        [Test]
        public void DriverFullName_ShouldNotContainDigits_WhenNameIsValid()
        {
            // Arrange
            var driver = new Driver
            {
                Id = 1,
                FullName = "Петр Петров",
                Contact = "petr@logistics.ru",
                ExperienceYears = 5,
                AssignedTransportId = 1
            };

            // Регулярное выражение: поиск цифр
            var digitPattern = new Regex(@"\d");

            // Act & Assert
            StringAssert.DoesNotMatch(driver.FullName, digitPattern,
                "ФИО водителя не должно содержать цифры");
        }

        /// <summary>
        /// Дополнительный тест: Проверка статуса заказа
        /// Использует StringAssert.Contains
        /// </summary>
        [TestMethod]
        public void OrderStatus_ShouldContainValidKeyword_WhenStatusIsSet()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                ClientId = 1,
                CargoId = 1,
                DateCreated = DateTime.Now,
                Status = "В пути",
                TransportId = 1,
                DriverId = 1
            };

            // Act & Assert
            StringAssert.Contains(order.Status, "пути",
                "Статус заказа должен содержать ключевое слово 'пути'");
        }

        /// <summary>
        /// Дополнительный тест: Проверка формата email
        /// Использует StringAssert.Matches
        /// </summary>
        [TestMethod]
        public void ClientEmail_ShouldMatchEmailPattern_WhenEmailIsValid()
        {
            // Arrange
            var client = new Client
            {
                Id = 2,
                Name = "Иван Иванов",
                Contact = "ivan@mail.com"
            };

            // Простое регулярное выражение для email
            var emailPattern = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            // Act & Assert
            StringAssert.Matches(client.Contact, emailPattern,
                "Контакт должен соответствовать формату email адреса");
        }

        /// <summary>
        /// Дополнительный тест: Проверка, что название города начинается с заглавной буквы
        /// Использует StringAssert.StartsWith
        /// </summary>
        [TestMethod]
        public void RouteFrom_ShouldStartWithCapitalLetter_WhenCityNameIsValid()
        {
            // Arrange
            var route = new Route
            {
                Id = 1,
                OrderId = 1,
                From = "Москва",
                To = "Санкт-Петербург",
                Distance = 700,
                EstimatedTime = TimeSpan.FromHours(9)
            };

            // Act & Assert
            StringAssert.StartsWith(route.From, "М",
                "Название города отправления должно начинаться с заглавной буквы 'М'");
        }

        /// <summary>
        /// Дополнительный тест: Проверка, что состояние транспорта не содержит спецсимволов
        /// Использует StringAssert.DoesNotMatch
        /// </summary>
        [TestMethod]
        public void TransportCondition_ShouldNotContainSpecialChars_WhenConditionIsValid()
        {
            // Arrange
            var transport = new Transport
            {
                Id = 2,
                Type = "Фура",
                Capacity = 5000,
                Registration = "B456CD",
                Condition = "Отличное"
            };

            // Регулярное выражение: поиск спецсимволов
            var specialCharsPattern = new Regex(@"[!@#$%^&*()_+=\[\]{};:'""\\|,.<>/?]");

            // Act & Assert
            StringAssert.DoesNotMatch(transport.Condition, specialCharsPattern,
                "Состояние транспорта не должно содержать специальные символы");
        }
    }
}