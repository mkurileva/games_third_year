using System;

namespace zadanie1
{
    internal class Program
    {
        static double Factorial(int n)
        {

            if (n == 0)
            {
                return 1;
            }
            else
            {
                return Factorial(n - 1) * n;
            }

        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Введите число x");
            double x = Convert.ToUInt32(Console.ReadLine());
            double rez = 0;
            double znak = 1;
            for (int i = 1; i < 14; i = i + 2)
            {
                rez = rez + znak * (Math.Pow(x, i) / Factorial(i));
                znak = znak * (-1);
            }
            Console.Write($"Результат равен {rez}");
        }
    }
}