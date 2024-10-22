using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public int gridSize = 15;
    public float spacing = 1.0f;
    public float updateInterval = 1.0f;

    public Button startButton;
    public Button stopButton;
    public Button clearButton;

    private Cell[,] grid;
    private bool simulationRunning = false;

    void Start()
    {
        GenerateGrid();
        FindNeighbors();

        // Назначаем обработчики для кнопок
        startButton.onClick.AddListener(StartSimulation);
        stopButton.onClick.AddListener(StopSimulation);
        clearButton.onClick.AddListener(ClearGrid);

        // Изначально кнопка Stop отключена
        stopButton.interactable = false;
    }

    void GenerateGrid()
    {
        grid = new Cell[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 position = new Vector3(x * spacing, y * spacing, 0);
                GameObject newCell = Instantiate(cellPrefab, position, Quaternion.identity);
                Cell cell = newCell.GetComponent<Cell>();
                cell.SetState(false);  // Все клетки по умолчанию мертвы
                grid[x, y] = cell;
            }
        }
    }

    // Метод для старта симуляции
    public void StartSimulation()
    {
        if (!simulationRunning)
        {
            simulationRunning = true;
            StartCoroutine(UpdateGridCoroutine());

            // Меняем состояние кнопок
            startButton.interactable = false;  // Делаем кнопку Start неактивной
            stopButton.interactable = true;    // Делаем кнопку Stop активной
            clearButton.interactable = false;  // Clear не активна во время симуляции
        }
    }

    // Метод для остановки симуляции
    public void StopSimulation()
    {
        if (simulationRunning)
        {
            simulationRunning = false;

            // Меняем состояние кнопок
            startButton.interactable = true;   // Start снова активна
            stopButton.interactable = false;   // Stop теперь неактивна
            clearButton.interactable = true;   // Clear активна после остановки
        }
    }

    // Метод для очистки поля (все клетки умирают)
    public void ClearGrid()
    {
        if (!simulationRunning)
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    grid[x, y].SetState(false);  // Устанавливаем состояние каждой клетки в "мертвая"
                }
            }
        }
    }

    IEnumerator UpdateGridCoroutine()
    {
        while (simulationRunning)
        {
            UpdateGrid();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    void UpdateGrid()
    {
        List<Cell> cellsToUpdate = new List<Cell>();

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Cell cell = grid[x, y];
                int aliveNeighbors = CountAliveNeighbors(cell);

                if (cell.isAlive && (aliveNeighbors < 2 || aliveNeighbors > 3))
                {
                    cellsToUpdate.Add(cell);
                }
                else if (!cell.isAlive && aliveNeighbors == 3)
                {
                    cellsToUpdate.Add(cell);
                }
            }
        }

        foreach (Cell cell in cellsToUpdate)
        {
            cell.SetState(!cell.isAlive);
        }
    }

    int CountAliveNeighbors(Cell cell)
    {
        int count = 0;

        foreach (Cell neighbor in cell.neighbors)
        {
            if (neighbor.isAlive)
            {
                count++;
            }
        }

        return count;
    }

    void FindNeighbors()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Cell cell = grid[x, y];
                cell.neighbors = new List<Cell>();

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                            continue;

                        int nx = x + i;
                        int ny = y + j;

                        if (nx >= 0 && ny >= 0 && nx < gridSize && ny < gridSize)
                        {
                            cell.neighbors.Add(grid[nx, ny]);
                        }
                    }
                }
            }
        }
    }
}

