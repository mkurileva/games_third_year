using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество элементов массива: ");
        int n = int.Parse(Console.ReadLine());

        int[] array = new int[n];
        Console.WriteLine("Введите элементы массива:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Элемент {i + 1}: ");
            array[i] = int.Parse(Console.ReadLine());
        }

        BubbleSort(array);

        Console.WriteLine("Отсортированный массив:");
        foreach (int item in array)
        {
            Console.Write(item + " ");
        }
    }

    static void BubbleSort(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }
}

