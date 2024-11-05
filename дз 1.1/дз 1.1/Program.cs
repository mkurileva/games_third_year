using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите число: ");
        int limit;

        if (!int.TryParse(Console.ReadLine(), out limit) || limit < 2)
        {
            Console.WriteLine("Пожалуйста, введите целое число, большее или равное 2.");
            return;
        }

        bool[] isPrime = new bool[limit + 1];
        for (int i = 2; i <= limit; i++)
            isPrime[i] = true;

        for (int i = 2; i * i <= limit; i++)
        {
            if (isPrime[i])
            {
                for (int j = i * i; j <= limit; j += i)
                {
                    isPrime[j] = false;
                }
            }
        }

        Console.WriteLine("Простые числа, не превосходящие " + limit + ":");
        for (int i = 2; i <= limit; i++)
        {
            if (isPrime[i])
                Console.Write(i + " ");
        }
        Console.WriteLine();
    }
}

