using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите делимое a: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Введите делитель b: ");
        int b = int.Parse(Console.ReadLine());

        if (b == 0)
        {
            Console.WriteLine("Ошибка: делитель не может быть равен нулю.");
            return;
        }

        int q = FindQuotient(a, b);
        Console.WriteLine("Неполное частное от деления " + a + " на " + b + " равно: " + q);
    }

    static int FindQuotient(int a, int b)
    {
        int q = 0;
        int absA = Math.Abs(a);
        int absB = Math.Abs(b);

        while (absA >= absB * (q + 1))
        {
            q++;
        }

        if ((a < 0 && b > 0) || (a > 0 && b < 0))
            q = -q;

        return q;
    }
}
