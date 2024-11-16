using System;

class ComplexNumber
{
    public double Real { get; private set; } // Действительная часть
    public double Imaginary { get; private set; } // Мнимая часть

    public ComplexNumber(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    // Сложение
    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    // Умножение
    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(
            a.Real * b.Real - a.Imaginary * b.Imaginary,
            a.Real * b.Imaginary + a.Imaginary * b.Real
        );
    }

    // Деление
    public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
    {
        double denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
        if (denominator == 0)
            throw new DivideByZeroException("Попытка деления на ноль.");

        return new ComplexNumber(
            (a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator,
            (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator
        );
    }

    // Возведение в степень
    public ComplexNumber Pow(int power)
    {
        if (power == 0) return new ComplexNumber(1, 0);
        if (power < 0) return (new ComplexNumber(1, 0) / Pow(-power));

        ComplexNumber result = new ComplexNumber(1, 0);
        for (int i = 0; i < power; i++)
            result *= this;

        return result;
    }

    // Извлечение корня (для корня квадратного)
    public ComplexNumber Sqrt()
    {
        double magnitude = Math.Sqrt(this.Magnitude());
        double angle = this.Angle() / 2;

        return new ComplexNumber(
            magnitude * Math.Cos(angle),
            magnitude * Math.Sin(angle)
        );
    }

    // Модуль
    public double Magnitude()
    {
        return Math.Sqrt(Real * Real + Imaginary * Imaginary);
    }

    // Угол (в радианах)
    public double Angle()
    {
        return Math.Atan2(Imaginary, Real);
    }

    // Преобразование в строку
    public override string ToString()
    {
        string sign = Imaginary >= 0 ? "+" : "-";
        return $"{Real} {sign} {Math.Abs(Imaginary)}i";
    }
}

class Program
{
    static void Main()
    {
        ComplexNumber z1 = new ComplexNumber(3, 4);
        ComplexNumber z2 = new ComplexNumber(1, -2);

        Console.WriteLine($"z1: {z1}");
        Console.WriteLine($"z2: {z2}");

        Console.WriteLine($"Сложение: {z1 + z2}");
        Console.WriteLine($"Умножение: {z1 * z2}");
        Console.WriteLine($"Деление: {z1 / z2}");
        Console.WriteLine($"Модуль z1: {z1.Magnitude()}");
        Console.WriteLine($"Угол z1 (в радианах): {z1.Angle()}");
        Console.WriteLine($"z1^2: {z1.Pow(2)}");
        Console.WriteLine($"Квадратный корень из z1: {z1.Sqrt()}");
    }
}

