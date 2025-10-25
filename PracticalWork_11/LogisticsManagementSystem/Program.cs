using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace LogisticsApp
{
    // Модели данных
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public List<int> OrderHistory { get; set; } = new List<int>();
    }

    public class Cargo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public double Weight { get; set; }
        public double Volume { get; set; }
        public string Requirements { get; set; } = string.Empty;
    }

    public class Transport
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public double Capacity { get; set; }
        public string Registration { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
    }

    public class Driver
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public int? AssignedTransportId { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CargoId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? TransportId { get; set; }
        public int? DriverId { get; set; }
    }

    public class Route
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public double Distance { get; set; }
        public TimeSpan EstimatedTime { get; set; }
    }

    public class Delivery
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime? ActualArrival { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    // Класс для работы с JSON-файлами в папке Data
    public static class DataStore<T> where T : class, new()
    {
        private static readonly string _directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private static readonly string _filePath = Path.Combine(_directory, typeof(T).Name + "s.json");

        static DataStore()
        {
            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);
        }

        public static List<T> Load()
        {
            if (!File.Exists(_filePath))
                return new List<T>();

            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }

        public static void Save(List<T> items)
        {
            string json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }

    class Program
    {
        static void Main()
        {
            // точный путь к папке с JSON файлами на пк
            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Console.WriteLine($"=== РАСПОЛОЖЕНИЕ ФАЙЛОВ ===");
            //Console.WriteLine($"Папка с данными: {dataPath}");
            Console.WriteLine($"Полный путь: {Path.GetFullPath(dataPath)}");
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();

            FillTestData();
            bool first_event = false;
            while (true)
            {
                if (first_event)
                {
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
                else first_event = true;
                Console.Clear();
                ShowMainMenu();
                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": ClientsMenu(); break;
                    case "2": OrdersMenu(); break;
                    case "3": CargosMenu(); break;
                    case "4": TransportMenu(); break;
                    case "5": DriversMenu(); break;
                    case "6": RoutesMenu(); break;
                    case "7": DeliveriesMenu(); break;
                    case "8": ReportsMenu(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор."); break;
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine("=== Система управления логистикой ===");
            Console.WriteLine("1. Клиенты");
            Console.WriteLine("2. Заказы");
            Console.WriteLine("3. Грузы");
            Console.WriteLine("4. Транспорт");
            Console.WriteLine("5. Водители");
            Console.WriteLine("6. Маршруты");
            Console.WriteLine("7. Доставки");
            Console.WriteLine("8. Отчёты");
            Console.WriteLine("0. Выход");
            Console.Write("Ваш выбор: ");
        }

        // Меню работы с клиентами
        static void ClientsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление клиентами ===");
                Console.WriteLine("1. Просмотр всех клиентов");
                Console.WriteLine("2. Добавить клиента");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("Ваш выбор: ");

                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": ShowClients(); WaitForKey(); break;
                    case "2": AddClient(); WaitForKey(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор."); WaitForKey(); break;
                }
            }
        }

        // Меню работы с заказами
        static void OrdersMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление заказами ===");
                Console.WriteLine("1. Добавить заказ");
                Console.WriteLine("2. Список заказов за последний месяц");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("Ваш выбор: ");

                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": AddOrder(); WaitForKey(); break;
                    case "2": ListOrdersLastMonth(); WaitForKey(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор."); WaitForKey(); break;
                }
            }
        }

        // Меню работы с грузами
        static void CargosMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление грузами ===");
                Console.WriteLine("1. Просмотр всех грузов");
                Console.WriteLine("2. Статус доставки груза");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("Ваш выбор: ");

                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": ShowCargos(); WaitForKey(); break;
                    case "2": DeliveryStatus(); WaitForKey(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор."); WaitForKey(); break;
                }
            }
        }

        // Меню работы с транспортом
        static void TransportMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление транспортом ===");
                Console.WriteLine("1. Просмотр всего транспорта");
                Console.WriteLine("2. Загрузка транспорта на текущий день");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("Ваш выбор: ");

                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": ShowTransports(); WaitForKey(); break;
                    case "2": TransportLoadToday(); WaitForKey(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор."); WaitForKey(); break;
                }
            }
        }

        // Меню работы с водителями
        static void DriversMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление водителями ===");
                Console.WriteLine("1. Просмотр всех водителей");
                Console.WriteLine("2. Список водителей с активными заказами");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("Ваш выбор: ");

                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": ShowDrivers(); WaitForKey(); break;
                    case "2": ActiveDrivers(); WaitForKey(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор."); WaitForKey(); break;
                }
            }
        }

        // Меню работы с маршрутами
        static void RoutesMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление маршрутами ===");
                Console.WriteLine("1. Просмотр всех маршрутов");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("Ваш выбор: ");

                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": ShowRoutes(); WaitForKey(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор."); WaitForKey(); break;
                }
            }
        }

        // Меню работы с доставками
        static void DeliveriesMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление доставками ===");
                Console.WriteLine("1. Просмотр всех доставок");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("Ваш выбор: ");

                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": ShowDeliveries(); WaitForKey(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор."); WaitForKey(); break;
                }
            }
        }

        // Меню отчётов
        static void ReportsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Отчёты и аналитика ===");
                Console.WriteLine("1. Среднее время доставки по маршрутам");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("Ваш выбор: ");

                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": AverageDeliveryTime(); WaitForKey(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор."); WaitForKey(); break;
                }
            }
        }

        static void WaitForKey()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        // Функции по работе с данными
        static void AddClient()
        {
            var clients = DataStore<Client>.Load();
            int nextId = clients.Any() ? clients.Max(c => c.Id) + 1 : 1;
            var client = new Client { Id = nextId };

            Console.Write("Название ФИО/компании: ");
            client.Name = Console.ReadLine() ?? string.Empty;
            Console.Write("Контактные данные: ");
            client.Contact = Console.ReadLine() ?? string.Empty;

            clients.Add(client);
            DataStore<Client>.Save(clients);
            Console.WriteLine("Клиент добавлен.");
        }

        static void ShowClients()
        {
            var clients = DataStore<Client>.Load();
            Console.WriteLine("=== Список клиентов ===");
            foreach (var c in clients)
            {
                Console.WriteLine($"Id: {c.Id}");
                Console.WriteLine($"ФИО/Компания: {c.Name}");
                Console.WriteLine($"Контакт: {c.Contact}");
                Console.WriteLine("История заказов: " + (c.OrderHistory.Any() ? string.Join(", ", c.OrderHistory) : "нет"));
                Console.WriteLine(new string('-', 30));
            }
        }

        static void ShowCargos()
        {
            var list = DataStore<Cargo>.Load();
            Console.WriteLine("=== Список грузов ===");
            foreach (var o in list)
            {
                Console.WriteLine($"Id: {o.Id}");
                Console.WriteLine($"Название: {o.Name}");
                Console.WriteLine($"Тип: {o.Type}");
                Console.WriteLine($"Вес: {o.Weight} кг");
                Console.WriteLine($"Объем: {o.Volume} м³");
                Console.WriteLine($"Требования: {o.Requirements}");
                Console.WriteLine(new string('-', 30));
            }
        }

        static void ShowTransports()
        {
            var list = DataStore<Transport>.Load();
            Console.WriteLine("=== Список транспорта ===");
            foreach (var o in list)
            {
                Console.WriteLine($"Id: {o.Id}");
                Console.WriteLine($"Тип: {o.Type}");
                Console.WriteLine($"Грузоподъемность: {o.Capacity} кг");
                Console.WriteLine($"Рег. номер: {o.Registration}");
                Console.WriteLine($"Состояние: {o.Condition}");
                Console.WriteLine(new string('-', 30));
            }
        }

        static void ShowDrivers()
        {
            var list = DataStore<Driver>.Load();
            Console.WriteLine("=== Список водителей ===");
            foreach (var o in list)
            {
                Console.WriteLine($"Id: {o.Id}");
                Console.WriteLine($"ФИО: {o.FullName}");
                Console.WriteLine($"Контакт: {o.Contact}");
                Console.WriteLine($"Опыт (лет): {o.ExperienceYears}");
                Console.WriteLine($"Закрепленный транспорт: " + (o.AssignedTransportId?.ToString() ?? "нет"));
                Console.WriteLine(new string('-', 30));
            }
        }

        static void ShowRoutes()
        {
            var list = DataStore<Route>.Load();
            Console.WriteLine("=== Список маршрутов ===");
            foreach (var o in list)
            {
                Console.WriteLine($"Id: {o.Id}");
                Console.WriteLine($"Заказ: {o.OrderId}");
                Console.WriteLine($"Откуда: {o.From}");
                Console.WriteLine($"Куда: {o.To}");
                Console.WriteLine($"Расстояние: {o.Distance} км");
                Console.WriteLine($"Ожид. время: {o.EstimatedTime}");
                Console.WriteLine(new string('-', 30));
            }
        }

        static void ShowDeliveries()
        {
            var list = DataStore<Delivery>.Load();
            Console.WriteLine("=== Список доставок ===");
            foreach (var o in list)
            {
                Console.WriteLine($"Id: {o.Id}");
                Console.WriteLine($"Заказ: {o.OrderId}");
                Console.WriteLine($"Фактическая дата прибытия: " + (o.ActualArrival?.ToString() ?? "неизвестно"));
                Console.WriteLine($"Статус: {o.Status}");
                Console.WriteLine(new string('-', 30));
            }
        }

        static void AddOrder()
        {
            var orders = DataStore<Order>.Load();
            var clients = DataStore<Client>.Load();
            var cargos = DataStore<Cargo>.Load();
            var transports = DataStore<Transport>.Load();
            var drivers = DataStore<Driver>.Load();

            if (!clients.Any()) { Console.WriteLine("Нет доступных клиентов. Сначала добавьте клиента."); return; }
            if (!cargos.Any()) { Console.WriteLine("Нет доступных грузов. Сначала добавьте груз."); return; }

            Console.WriteLine("Доступные клиенты:");
            clients.ForEach(c => Console.WriteLine($"Id {c.Id}: {c.Name}"));
            Console.Write("Выберите Id клиента: ");
            if (!int.TryParse(Console.ReadLine(), out int clientId) || !clients.Any(c => c.Id == clientId))
            {
                Console.WriteLine("Неверный Id клиента.");
                return;
            }

            Console.WriteLine("Доступные грузы:");
            cargos.ForEach(c => Console.WriteLine($"Id {c.Id}: {c.Name}"));
            Console.Write("Выберите Id груза: ");
            if (!int.TryParse(Console.ReadLine(), out int cargoId) || !cargos.Any(c => c.Id == cargoId))
            {
                Console.WriteLine("Неверный Id груза.");
                return;
            }

            Console.WriteLine("Доступный транспорт (Enter чтобы пропустить):");
            transports.ForEach(t => Console.WriteLine($"Id {t.Id}: {t.Registration}"));
            Console.Write("Выберите Id транспорта: ");
            int? transportId = int.TryParse(Console.ReadLine(), out int tId) ? tId : (int?)null;

            Console.WriteLine("Доступные водители (Enter чтобы пропустить):");
            drivers.ForEach(d => Console.WriteLine($"Id {d.Id}: {d.FullName}"));
            Console.Write("Выберите Id водителя: ");
            int? driverId = int.TryParse(Console.ReadLine(), out int dId) ? dId : (int?)null;

            int nextId = orders.Any() ? orders.Max(o => o.Id) + 1 : 1;
            var order = new Order
            {
                Id = nextId,
                ClientId = clientId,
                CargoId = cargoId,
                TransportId = transportId,
                DriverId = driverId,
                DateCreated = DateTime.Now,
                Status = "Создан"
            };
            orders.Add(order);
            DataStore<Order>.Save(orders);

            var client = clients.First(c => c.Id == clientId);
            client.OrderHistory.Add(order.Id);
            DataStore<Client>.Save(clients);

            Console.WriteLine("Заказ добавлен.");
        }

        // Остальные запросы:
        static void ListOrdersLastMonth()
        {
            var orders = DataStore<Order>.Load();
            var clients = DataStore<Client>.Load();
            var lastMonth = DateTime.Now.AddMonths(-1);
            var recentOrders = orders.Where(o => o.DateCreated >= lastMonth).ToList();

            Console.WriteLine("=== Заказы за последний месяц ===");
            if (!recentOrders.Any())
            {
                Console.WriteLine("Заказов за последний месяц не найдено.");
                return;
            }

            foreach (var o in recentOrders)
            {
                var client = clients.FirstOrDefault(c => c.Id == o.ClientId);
                Console.WriteLine($"Id: {o.Id}");
                Console.WriteLine($"Клиент: {client?.Name ?? "Неизвестен"}");
                Console.WriteLine($"Статус: {o.Status}");
                Console.WriteLine($"Дата создания: {o.DateCreated:dd.MM.yyyy HH:mm}");
                Console.WriteLine(new string('-', 30));
            }
        }

        static void DeliveryStatus()
        {
            var cargos = DataStore<Cargo>.Load();
            var orders = DataStore<Order>.Load();
            var deliveries = DataStore<Delivery>.Load();

            if (!cargos.Any()) { Console.WriteLine("Нет доступных грузов."); return; }

            Console.WriteLine("Доступные грузы:");
            cargos.ForEach(c => Console.WriteLine($"Id {c.Id}: {c.Name}"));
            Console.Write("Выберите Id груза: ");

            if (!int.TryParse(Console.ReadLine(), out int cargoId) || !cargos.Any(c => c.Id == cargoId))
            {
                Console.WriteLine("Неверный Id груза.");
                return;
            }

            var order = orders.FirstOrDefault(o => o.CargoId == cargoId);
            if (order == null) { Console.WriteLine("Заказ для данного груза не найден."); return; }

            var delivery = deliveries.FirstOrDefault(d => d.OrderId == order.Id);
            if (delivery == null)
            {
                Console.WriteLine($"Статус заказа: {order.Status}");
                Console.WriteLine("Доставка еще не создана.");
            }
            else
            {
                Console.WriteLine($"Статус доставки: {delivery.Status}");
                if (delivery.ActualArrival.HasValue)
                    Console.WriteLine($"Дата доставки: {delivery.ActualArrival:dd.MM.yyyy HH:mm}");
            }
        }

        static void AverageDeliveryTime()
        {
            var orders = DataStore<Order>.Load();
            var deliveries = DataStore<Delivery>.Load();

            var completedDeliveries = deliveries
                .Where(d => d.ActualArrival.HasValue)
                .Join(orders, d => d.OrderId, o => o.Id, (d, o) => new { Delivery = d, Order = o })
                .ToList();

            if (!completedDeliveries.Any())
            {
                Console.WriteLine("Нет завершенных доставок для расчета среднего времени.");
                return;
            }

            var times = completedDeliveries
                .Select(x => (x.Delivery.ActualArrival!.Value - x.Order.DateCreated).TotalHours)
                .ToList();

            var avgHours = times.Average();
            var avgTime = TimeSpan.FromHours(avgHours);

            Console.WriteLine($"Количество завершенных доставок: {completedDeliveries.Count}");
            Console.WriteLine($"Среднее время доставки: {avgTime.Days} дн. {avgTime.Hours} ч. {avgTime.Minutes} мин.");
        }

        static void ActiveDrivers()
        {
            var orders = DataStore<Order>.Load();
            var drivers = DataStore<Driver>.Load();

            var activeOrders = orders.Where(o => o.Status != "Завершен" && o.DriverId.HasValue).ToList();
            var activeDriverIds = activeOrders.Select(o => o.DriverId!.Value).Distinct().ToList();

            Console.WriteLine("=== Водители с активными заказами ===");
            if (!activeDriverIds.Any())
            {
                Console.WriteLine("Нет водителей с активными заказами.");
                return;
            }

            foreach (var id in activeDriverIds)
            {
                var driver = drivers.FirstOrDefault(d => d.Id == id);
                if (driver != null)
                {
                    var driverOrders = activeOrders.Where(o => o.DriverId == id).Count();
                    Console.WriteLine($"Id: {id}, ФИО: {driver.FullName}, Активных заказов: {driverOrders}");
                }
            }
        }

        static void TransportLoadToday()
        {
            var orders = DataStore<Order>.Load();
            var transports = DataStore<Transport>.Load();
            var today = DateTime.Now.Date;

            var todayOrders = orders.Where(o => o.DateCreated.Date == today && o.TransportId.HasValue).ToList();
            var usedTransportIds = todayOrders.Select(o => o.TransportId!.Value).Distinct().ToList();

            Console.WriteLine("=== Транспорт, задействованный сегодня ===");
            if (!usedTransportIds.Any())
            {
                Console.WriteLine("Сегодня транспорт не задействован.");
                return;
            }

            foreach (var id in usedTransportIds)
            {
                var transport = transports.FirstOrDefault(t => t.Id == id);
                if (transport != null)
                {
                    var transportOrders = todayOrders.Where(o => o.TransportId == id).Count();
                    Console.WriteLine($"Id: {id}, Рег. номер: {transport.Registration}, Заказов: {transportOrders}");
                }
            }
        }

        // Тестовые данные
        static void FillTestData()
        {
            if (DataStore<Client>.Load().Any()) return; // уже есть данные

            var clients = new List<Client> {
                new Client{Id=1,Name="ООО Ромашка",Contact="info@romashka.ru"},
                new Client{Id=2,Name="Иван Иванов",Contact="ivan@mail.com"}
            };
            DataStore<Client>.Save(clients);

            var cargos = new List<Cargo> {
                new Cargo{Id=1,Name="Компьютеры",Type="Электроника",Weight=500,Volume=2,Requirements="Охлаждение"},
                new Cargo{Id=2,Name="Мебель",Type="Габаритный",Weight=1000,Volume=15,Requirements="Осторожно"}
            };
            DataStore<Cargo>.Save(cargos);

            var transports = new List<Transport> {
                new Transport{Id=1,Type="Грузовик",Capacity=2000,Registration="A123BC",Condition="Хорошее"},
                new Transport{Id=2,Type="Фура",Capacity=5000,Registration="B456CD",Condition="Отличное"}
            };
            DataStore<Transport>.Save(transports);

            var drivers = new List<Driver> {
                new Driver{Id=1,FullName="Петр Петров",Contact="petr@logistics.ru",ExperienceYears=5,AssignedTransportId=1},
                new Driver{Id=2,FullName="Сергей Сергеев",Contact="sergey@logistics.ru",ExperienceYears=8,AssignedTransportId=2}
            };
            DataStore<Driver>.Save(drivers);

            var orders = new List<Order> {
                new Order{Id=1,ClientId=1,CargoId=1,DateCreated=DateTime.Now.AddDays(-3),Status="В пути",TransportId=1,DriverId=1},
                new Order{Id=2,ClientId=2,CargoId=2,DateCreated=DateTime.Now.AddDays(-10),Status="Завершен",TransportId=2,DriverId=2}
            };
            DataStore<Order>.Save(orders);

            var routes = new List<Route> {
                new Route{Id=1,OrderId=1,From="Москва",To="Санкт-Петербург",Distance=700,EstimatedTime=TimeSpan.FromHours(9)},
                new Route{Id=2,OrderId=2,From="Казань",To="Нижний Новгород",Distance=420,EstimatedTime=TimeSpan.FromHours(6)}
            };
            DataStore<Route>.Save(routes);

            var deliveries = new List<Delivery> {
                new Delivery{Id=1,OrderId=2,ActualArrival=DateTime.Now.AddDays(-5),Status="Завершен"}
            };
            DataStore<Delivery>.Save(deliveries);
        }
    }
}