using System;
using System.Diagnostics;
using System.Numerics;

class Program
{
    // Рекурсивный факториал
    static BigInteger FactorialRecursive(int n)
    {
        if (n == 0 || n == 1) return 1;
        return n * FactorialRecursive(n - 1);
    }

    // Итеративный факториал
    static BigInteger FactorialIterative(int n)
    {
        BigInteger result = 1;
        for (int i = 2; i <= n; i++)
            result *= i;
        return result;
    }

    // Рекурсивные числа Фибоначчи
    static BigInteger FibonacciRecursive(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 1;
        return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
    }

    // Итеративные числа Фибоначчи
    static BigInteger FibonacciIterative(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 1;

        BigInteger prev = 0, curr = 1;
        for (int i = 2; i <= n; i++)
        {
            BigInteger temp = curr;
            curr += prev;
            prev = temp;
        }
        return curr;
    }

    static void Main()
    {
        const int FactorialLimit = 20;   // Ограничение для факториала
        const int FibonacciLimit = 30;  // Ограничение для Фибоначчи
        const int SignificantDifference = 1000; // Минимальная разница в тиках для вывода

        Console.WriteLine("(Выводятся те значения n, при которых рекурсивный вариант заметно медленнее итеративного)\n\nРезультаты для факториала:");
        for (int n = 0; n <= FactorialLimit; n++)
        {
            Stopwatch swRecursive = Stopwatch.StartNew();
            FactorialRecursive(n);
            swRecursive.Stop();

            Stopwatch swIterative = Stopwatch.StartNew();
            FactorialIterative(n);
            swIterative.Stop();

            long recursiveTime = swRecursive.ElapsedTicks;
            long iterativeTime = swIterative.ElapsedTicks;

            if (Math.Abs(recursiveTime - iterativeTime) > SignificantDifference)
            {
                Console.WriteLine($"n={n}: Рекурсивный - {recursiveTime} тиков, итеративный - {iterativeTime} тиков");
            }
        }

        Console.WriteLine("\nРезультаты для Фибоначчи:");
        for (int n = 0; n <= FibonacciLimit; n++)
        {
            Stopwatch swRecursive = Stopwatch.StartNew();
            FibonacciRecursive(n);
            swRecursive.Stop();

            Stopwatch swIterative = Stopwatch.StartNew();
            FibonacciIterative(n);
            swIterative.Stop();

            long recursiveTime = swRecursive.ElapsedTicks;
            long iterativeTime = swIterative.ElapsedTicks;

            if (Math.Abs(recursiveTime - iterativeTime) > SignificantDifference)
            {
                Console.WriteLine($"n={n}: рекурсивный - {recursiveTime} тиков, итеративный - {iterativeTime} тиков");
            }
        }
    }
}

