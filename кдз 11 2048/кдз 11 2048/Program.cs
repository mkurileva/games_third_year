using System;

class Game2048
{
    const int Size = 4;
    static int[,] board = new int[Size, Size];
    static Random rand = new Random();
    static int score = 0;

    static void Main()
    {
        SpawnTile();
        SpawnTile();
        while (true)
        {
            Console.Clear();
            PrintBoard();
            if (IsGameOver())
            {
                Console.WriteLine("Гамовер! Твой счёт: " + score);
                break;
            }

            ConsoleKey key = Console.ReadKey(true).Key;
            bool moved = key switch
            {
                ConsoleKey.UpArrow => MoveUp(),
                ConsoleKey.DownArrow => MoveDown(),
                ConsoleKey.LeftArrow => MoveLeft(),
                ConsoleKey.RightArrow => MoveRight(),
                _ => false
            };

            if (moved)
            {
                SpawnTile();
            }
        }
    }

    static void SpawnTile()
    {
        var emptyCells = new System.Collections.Generic.List<(int, int)>();
        for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; j++)
                if (board[i, j] == 0)
                    emptyCells.Add((i, j));

        if (emptyCells.Count > 0)
        {
            var (x, y) = emptyCells[rand.Next(emptyCells.Count)];
            board[x, y] = rand.Next(10) < 9 ? 2 : 4; //вероятность
        }
    }

    static void PrintBoard()
    {
        Console.WriteLine("Счёт: " + score);
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Console.Write(board[i, j] == 0 ? ".\t" : board[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static bool MoveLeft()
    {
        bool moved = false;
        for (int i = 0; i < Size; i++)
        {
            int[] row = new int[Size];
            int pos = 0;
            for (int j = 0; j < Size; j++)
            {
                if (board[i, j] != 0)
                {
                    row[pos++] = board[i, j];
                }
            }

            for (int j = 0; j < Size - 1; j++)
            {
                if (row[j] != 0 && row[j] == row[j + 1])
                {
                    row[j] *= 2;
                    score += row[j];
                    row[j + 1] = 0;
                }
            }

            pos = 0;
            for (int j = 0; j < Size; j++)
            {
                if (row[j] != 0)
                {
                    if (board[i, pos] != row[j])
                    {
                        moved = true;
                    }
                    board[i, pos++] = row[j];
                }
            }

            while (pos < Size)
            {
                if (board[i, pos] != 0)
                {
                    moved = true;
                }
                board[i, pos++] = 0;
            }
        }
        return moved;
    }

    static bool MoveRight()
    {
        RotateBoard();
        RotateBoard();
        bool moved = MoveLeft();
        RotateBoard();
        RotateBoard();
        return moved;
    }

    static bool MoveUp()
    {
        RotateBoard();
        RotateBoard();
        RotateBoard();
        bool moved = MoveLeft();
        RotateBoard();
        return moved;
    }

    static bool MoveDown()
    {
        RotateBoard();
        bool moved = MoveLeft();
        RotateBoard();
        RotateBoard();
        RotateBoard();
        return moved;
    }

    static void RotateBoard()
    {
        int[,] temp = new int[Size, Size];
        for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; j++)
                temp[i, j] = board[Size - j - 1, i];

        Array.Copy(temp, board, Size * Size);
    }

    static bool IsGameOver()
    {
        for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; j++)
            {
                if (board[i, j] == 0)
                    return false;
                if (j < Size - 1 && board[i, j] == board[i, j + 1])
                    return false;
                if (i < Size - 1 && board[i, j] == board[i + 1, j])
                    return false;
            }
        return true;
    }
}

