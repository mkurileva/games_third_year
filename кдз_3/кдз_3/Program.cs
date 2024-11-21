using System;

namespace ConsoleGameProPole
{
    class Program
    {
        static char[,] map;
        static int playerX, playerY;
        static readonly ConsoleColor GrassColor = ConsoleColor.Green;
        static readonly ConsoleColor StoneColor = ConsoleColor.Gray;
        static readonly ConsoleColor TreeColor = ConsoleColor.DarkGreen;
        static readonly ConsoleColor PlateColor = ConsoleColor.Yellow;
        static readonly ConsoleColor ActivatedPlateColor = ConsoleColor.Cyan;
        static readonly ConsoleColor PlayerColor = ConsoleColor.Magenta;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Для корректного отображения символов
            int currentLevel = 0;
            char[][,] levels = {
                new char[,] {
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', 'T', '#', 'R', '#', 'O', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', 'O', '#', '#', 'T', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', 'C', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                },
                new char[,] {
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', 'T', '#', '#', '#', '#', '#', '#'},
                    {'#', 'R', 'O', '#', '#', 'O', '#', 'T', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', 'C', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                },
                new char[,] {
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', 'O', '#', '#', 'R', '#', '#'},
                    {'#', '#', '#', '#', 'T', '#', '#', 'T', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', 'T', 'O', '#', '#', 'O', '#', '#', '#'},
                    {'#', '#', 'T', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', 'R', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', 'C', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                },
            };

            while (currentLevel < levels.Length)
            {
                map = levels[currentLevel];
                InitializePlayerPosition();
                Console.Clear();
                Console.WriteLine($"Level {currentLevel + 1}");
                while (!IsLevelComplete())
                {
                    DrawMap();
                    HandleInput();
                }

                Console.Clear();
                Console.WriteLine($"Уровень {currentLevel + 1} пройден!");
                Console.ReadKey();
                currentLevel++;
            }

            Console.Clear();
            Console.WriteLine("Ура! Ты прошел все уровни!");
        }

        static void InitializePlayerPosition()
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == 'C')
                    {
                        playerX = x;
                        playerY = y;
                        return;
                    }
                }
            }
        }

        static void DrawMap()
        {
            Console.Clear();
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    switch (map[y, x])
                    {
                        case '#': SetColor(GrassColor); Console.Write('#'); break;
                        case 'R': SetColor(StoneColor); Console.Write('R'); break;
                        case 'T': SetColor(TreeColor); Console.Write('T'); break;
                        case 'O': SetColor(PlateColor); Console.Write('O'); break;
                        case 'Ⓡ': case 'Ⓒ': SetColor(ActivatedPlateColor); Console.Write(map[y, x]); break;
                        case 'C': SetColor(PlayerColor); Console.Write('C'); break;
                        default: Console.Write(map[y, x]); break;
                    }
                }
                Console.WriteLine();
            }
        }

        static void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        static void HandleInput()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            int newX = playerX, newY = playerY;

            switch (key)
            {
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.RightArrow: newX++; break;
            }

            // Проверка выхода за границы поля
            if (newX < 0 || newX >= map.GetLength(1) || newY < 0 || newY >= map.GetLength(0))
            {
                return; // Игнорируем ход
            }

            if (IsWalkable(newX, newY))
            {
                MovePlayer(newX, newY);
            }
            else if (map[newY, newX] == 'R')
            {
                int pushX = newX + (newX - playerX);
                int pushY = newY + (newY - playerY);

                // Проверка выхода за границы поля для толкания камня
                if (pushX < 0 || pushX >= map.GetLength(1) || pushY < 0 || pushY >= map.GetLength(0))
                {
                    return; // Игнорируем ход
                }

                if (IsPushable(pushX, pushY))
                {
                    map[pushY, pushX] = map[pushY, pushX] == 'O' ? 'Ⓡ' : 'R';
                    map[newY, newX] = map[newY, newX] == 'Ⓡ' ? 'O' : '#';
                    MovePlayer(newX, newY);
                }
            }
        }


        static bool IsWalkable(int x, int y)
        {
            return x >= 0 && x < map.GetLength(1) &&
                   y >= 0 && y < map.GetLength(0) &&
                   (map[y, x] == '#' || map[y, x] == 'O');
        }

        static bool IsPushable(int x, int y)
        {
            return x >= 0 && x < map.GetLength(1) &&
                   y >= 0 && y < map.GetLength(0) &&
                   (map[y, x] == '#' || map[y, x] == 'O');
        }

        static void MovePlayer(int x, int y)
        {
            map[playerY, playerX] = map[playerY, playerX] == 'Ⓒ' ? 'O' : '#';
            playerX = x;
            playerY = y;
            map[playerY, playerX] = map[playerY, playerX] == 'O' ? 'Ⓒ' : 'C';
        }

        static bool IsLevelComplete()
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == 'O') return false;
                }
            }
            return true;
        }
    }
}
