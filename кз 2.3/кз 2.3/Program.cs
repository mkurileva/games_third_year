using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите число n: ");
        if (int.TryParse(Console.ReadLine(), out int n) && n > 0)
        {
            int count = 0;

            while (n != 1)
            {
                if (n % 2 == 0)
                {
                    n /= 2;
                }
                else
                {
                    n = 3 * n + 1;
                }
                count++;
            }

            Console.WriteLine($"Количество замен для достижения 1: {count}");
        }
        else
        {
            Console.WriteLine("Введите положительное целое число.");
        }
    }
}
