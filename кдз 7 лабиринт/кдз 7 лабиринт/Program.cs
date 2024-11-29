using System;

namespace MazeGame
{
    class Program
    {
        static char[,] maze;  // Лабиринт
        static int playerX, playerY;  // Позиция игрока
        static int exitX, exitY;  // Позиция выхода

        static void Main(string[] args)
        {
            int width = 21;  // Ширина лабиринта (должна быть нечетной)
            int height = 11; // Высота лабиринта (должна быть нечетной)

            maze = GenerateMaze(width, height);  // Генерация лабиринта
            playerX = 1;
            playerY = 1;
            exitX = width - 2;
            exitY = height - 2;

            // Основной игровой цикл
            while (true)
            {
                Console.Clear();
                PrintMaze();
                MovePlayer();

                if (playerX == exitX && playerY == exitY)
                {
                    Console.Clear();
                    PrintMaze();
                    Console.WriteLine("Поздравляем! Вы нашли выход!");
                    break;
                }
            }
        }

        // Генерация лабиринта алгоритмом Sidewinder
        static char[,] GenerateMaze(int width, int height)
        {
            char[,] maze = new char[height, width];

            // Заполняем лабиринт стенами
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    maze[y, x] = (x % 2 == 1 && y % 2 == 1) ? ' ' : '#';

            Random rand = new Random();

            // Генерация построчно
            for (int y = 1; y < height; y += 2)
            {
                int runStart = 1; // Начало рунки

                for (int x = 1; x < width; x += 2)
                {
                    if (x + 2 < width && (y == 1 || rand.Next(2) == 0))
                    {
                        // Продолжить рунку вправо
                        maze[y, x + 1] = ' ';
                    }
                    else
                    {
                        // Завершить рунку: соединить вверх
                        int runEnd = rand.Next(runStart, x + 1);
                        if (y > 1)
                            maze[y - 1, runEnd] = ' ';
                        runStart = x + 2; // Начать новую рунку
                    }
                }
            }

            // Обеспечиваем наличие старта и выхода
            maze[1, 1] = ' ';
            maze[height - 2, width - 2] = ' ';
            return maze;
        }

        // Печать лабиринта
        static void PrintMaze()
        {
            for (int y = 0; y < maze.GetLength(0); y++)
            {
                for (int x = 0; x < maze.GetLength(1); x++)
                {
                    if (x == playerX && y == playerY)
                        Console.Write('C'); // Игрок - character
                    else if (x == exitX && y == exitY)
                        Console.Write('E'); // Выход
                    else
                        Console.Write(maze[y, x]);
                }
                Console.WriteLine();
            }
        }

        // Перемещение игрока
        static void MovePlayer()
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            int newX = playerX, newY = playerY;

            if (key == ConsoleKey.UpArrow && playerY > 0 && maze[playerY - 1, playerX] != '#')
                newY--;
            else if (key == ConsoleKey.DownArrow && playerY < maze.GetLength(0) - 1 && maze[playerY + 1, playerX] != '#')
                newY++;
            else if (key == ConsoleKey.LeftArrow && playerX > 0 && maze[playerY, playerX - 1] != '#')
                newX--;
            else if (key == ConsoleKey.RightArrow && playerX < maze.GetLength(1) - 1 && maze[playerY, playerX + 1] != '#')
                newX++;

            // Если движение возможно, обновляем позицию игрока
            if (maze[newY, newX] != '#')
            {
                playerX = newX;
                playerY = newY;
            }
        }
    }
}
