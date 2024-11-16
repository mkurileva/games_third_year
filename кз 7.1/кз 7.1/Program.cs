using System;
using System.Numerics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите число:");

        try
        {
            string input = Console.ReadLine();

            if (!BigInteger.TryParse(input, out BigInteger bigValue))
            {
                throw new FormatException("Введено не число.");
            }

            if (bigValue > int.MaxValue)
            {
                throw new OverflowException("Введено слишком большое число.");
            }

            if (bigValue < int.MinValue)
            {
                throw new OverflowException("Введено слишком маленькое число.");
            }

            int number = (int)bigValue;
            Console.WriteLine($"Вы ввели число: {number}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
        }
    }
}
