using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static int width = 20;
    static int height = 20;
    static bool[,] grid = new bool[width, height];
    static HashSet<string> previousStates = new HashSet<string>();

    static void Main()
    {
        InitializeGrid();
        PrintGrid();
        while (true)
        {
            string currentState = GetCurrentState();
            if (previousStates.Contains(currentState) || !AnyLiveCells())
            {
                break;
            }
            previousStates.Add(currentState);
            grid = GetNextGeneration();
            PrintGrid();
            System.Threading.Thread.Sleep(500);
        }
    }

    static void InitializeGrid()
    {
        Console.WriteLine("Введите координаты 5 живых клеток (x y), разделенные пробелом:");
        for (int i = 0; i < 5; i++)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int x = input[0];
            int y = input[1];
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                grid[x, y] = true;
            }
        }
    }

    static bool[,] GetNextGeneration()
    {
        bool[,] newGrid = new bool[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int liveNeighbors = CountLiveNeighbors(x, y);
                if (grid[x, y])
                {
                    newGrid[x, y] = liveNeighbors == 2 || liveNeighbors == 3;
                }
                else
                {
                    newGrid[x, y] = liveNeighbors == 3;
                }
            }
        }
        return
        newGrid;
    }



    static int CountLiveNeighbors(int x, int y)
    {
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                int nx = (x + i + width) % width;
                int ny = (y + j + height) % height;
                if (grid[nx, ny]) count++;
            }
        }
        return count;
    }



    static void PrintGrid()
    {
        Console.Clear();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(grid[x, y] ? "O" : ".");
            }
            Console.WriteLine();
        }
    }



    static string GetCurrentState()
    {
        string state = "";
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                state += grid[x, y] ? "1" : "0";
            }
        }
        return state;
    }



    static bool AnyLiveCells()
    {
        return grid.Cast<bool>().Any(cell => cell);
    }
}
