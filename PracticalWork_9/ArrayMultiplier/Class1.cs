using System;

namespace ArrayMultiplier
{
    /// <summary>
    /// Класс для работы с массивами
    /// </summary>
    public class ArrayProcessor
    {
        /// <summary>
        /// Увеличивает каждый элемент массива в 3 раза
        /// </summary>
        /// <param name="array">Исходный массив</param>
        /// <returns>Новый массив с элементами, увеличенными в 3 раза</returns>
        public static double[] MultiplyBy3(double[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Массив не может быть null");
            }

            if (array.Length == 0)
            {
                throw new ArgumentException("Массив не может быть пустым", nameof(array));
            }

            double[] result = new double[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i] * 3;
            }

            return result;
        }
    }
}

namespace ArrayMultiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=" + new string('=', 79));
            Console.WriteLine("Программа: Увеличение элементов массива в 3 раза");
            Console.WriteLine("=" + new string('=', 79));
            Console.WriteLine("\nДля запуска тестов используйте:");
            Console.WriteLine("  - Visual Studio Test Explorer");
            Console.WriteLine("  - Команду: dotnet test");
            Console.WriteLine("  - ReSharper Test Runner");

            Console.WriteLine("\n" + new string('-', 80));
            Console.WriteLine("ИНТЕРАКТИВНЫЙ РЕЖИМ");
            Console.WriteLine(new string('-', 80));

            InteractiveMode();
        }

        static void InteractiveMode()
        {
            Console.WriteLine("Введите элементы массива через пробел (или 'exit' для выхода):");

            while (true)
            {
                Console.Write("\n> ");
                string input = Console.ReadLine();

                if (input?.ToLower() == "exit")
                {
                    Console.WriteLine("Выход из программы.");
                    break;
                }

                try
                {
                    string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    double[] array = Array.ConvertAll(parts, double.Parse);

                    double[] result = ArrayProcessor.MultiplyBy3(array);

                    Console.WriteLine($"Исходный массив: [{string.Join(", ", array)}]");
                    Console.WriteLine($"Результат (×3):  [{string.Join(", ", result)}]");
                }
                catch (FormatException)
                {
                    Console.WriteLine("❌ Ошибка: Введите корректные числа через пробел");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Ошибка: {ex.Message}");
                }
            }
        }
    }
}