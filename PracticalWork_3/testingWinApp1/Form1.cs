using System;
using System.Windows.Forms;

namespace PiecewiseFunctionApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Настройка формы при загрузке
            this.Text = "Вычисление функции F";

            // Настройка меток для полей ввода
            label1.Text = "x =";
            label2.Text = "a =";
            label3.Text = "b =";
            label4.Text = "c =";

            // Настройка кнопок
            button1.Text = "Вычислить";
            button2.Text = "Очистить";

            // Привязка событий к кнопкам
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;

            // Настройка меток результата
            label5.Text = "Используемая формула:";
            label5.AutoSize = true;
            label5.MaximumSize = new System.Drawing.Size(450, 0);

            label6.Text = "Результат:";
            label6.AutoSize = true;
            label6.MaximumSize = new System.Drawing.Size(450, 0);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // ОШИБКА 1 (Ошибка анализа): Отсутствует проверка на пустые поля
                // Чтение значений из текстовых полей
                double x = double.Parse(textBox1.Text);
                double a = double.Parse(textBox2.Text);
                double b = double.Parse(textBox3.Text);
                double c = double.Parse(textBox4.Text);

                double result;
                string formula;

                // Проверка условий и вычисление функции
                if (x + 5 < 0 && c == 0)
                {
                    // Первый случай: F = 1/(ax) - b
                    // ОШИБКА 2 (Ошибка проектирования): Неточная проверка деления на ноль
                    // Используется == вместо Math.Abs для сравнения с нулем
                    if (a * x == 0)
                    {
                        MessageBox.Show("Ошибка: деление на ноль (a*x = 0)!",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    result = 1.0 / (a * x) - b;
                    formula = $"Используемая формула:\nF = 1/(a*x) - b\n" +
                             $"При x + 5 < 0 и c = 0\n" +
                             $"F = 1/({a}*{x}) - {b}";
                }
                else if (x + 5 > 0 && c != 0)
                {
                    // Второй случай: F = (x - a)/(x - 1)
                    if (x - 1 == 0)
                    {
                        MessageBox.Show("Ошибка: деление на ноль (x - 1 = 0)!",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    result = (x - a) / (x - 1);
                    formula = $"Используемая формула:\nF = (x - a)/(x - 1)\n" +
                             $"При x + 5 > 0 и c ≠ 0\n" +
                             $"F = ({x} - {a})/({x} - 1)";
                }
                else
                {
                    // Третий случай: F = 10x/(c - 2)
                    if (c - 2 == 0)
                    {
                        MessageBox.Show("Ошибка: деление на ноль (c - 2 = 0)!",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    result = (10 * x) / (c - 2);
                    // ОШИБКА 3 (Ошибка документации): Неполное описание условий
                    formula = $"Используемая формула:\nF = 10*x/(c - 2)\n" +
                             $"В остальных случаях\n" +
                             $"F = 10*{x}/({c} - 2)";
                }

                // Вывод результата
                label5.Text = formula;
                // ОШИБКА 4 (Ошибка программной реализации): 
                // Отсутствует проверка на бесконечность и NaN
                label6.Text = $"Результат: F = {result:F4}";
                label6.ForeColor = System.Drawing.Color.DarkGreen;
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения!",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Очистка всех полей
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            label5.Text = "Используемая формула:";
            label6.Text = "Результат:";
            label6.ForeColor = System.Drawing.Color.Black;
            textBox1.Focus();
        }
    }
}