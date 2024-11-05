using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите коэффициент a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Введите коэффициент b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Введите коэффициент c: ");
        double c = double.Parse(Console.ReadLine());

        if (a == 0)
        {
            // Если a == 0, уравнение становится линейным: bx + c = 0
            if (b == 0)
            {
                if (c == 0)
                {
                    Console.WriteLine("Бесконечное количество решений.");
                }
                else
                {
                    Console.WriteLine("Нет решений.");
                }
            }
            else
            {
                // Решение для линейного уравнения bx + c = 0
                double x = -c / b;
                Console.WriteLine("Одно решение: x = " + x);
            }
        }
        else
        {
            // Решение квадратного уравнения ax^2 + bx + c = 0
            double discriminant = b * b - 4 * a * c;

            if (discriminant > 0)
            {
                // Два различных корня
                double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                Console.WriteLine("Два корня: x1 = " + x1 + ", x2 = " + x2);
            }
            else if (discriminant == 0)
            {
                // Один корень
                double x = -b / (2 * a);
                Console.WriteLine("Одно решение: x = " + x);
            }
            else
            {
                Console.WriteLine("Нет вещественных корней.");
            }
        }
    }
}

