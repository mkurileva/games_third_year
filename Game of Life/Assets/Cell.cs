using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive;  // Состояние клетки
    public List<Cell> neighbors;
    private Renderer cellRenderer;

    void Start()
    {
        cellRenderer = GetComponent<Renderer>();
        UpdateCellVisual();
    }

    // Метод для изменения состояния клетки при клике
    void OnMouseDown()
    {
        ToggleState();  // Переключаем состояние клетки (живая/мёртвая)
    }

    // Метод для переключения состояния клетки
    public void ToggleState()
    {
        isAlive = !isAlive;  // Меняем состояние клетки на противоположное
        UpdateCellVisual();  // Обновляем цвет клетки
    }

    public void SetState(bool alive)
    {
        isAlive = alive;
        UpdateCellVisual();
    }

    // Обновляем цвет клетки: зеленый для живых, черный для мертвых
    private void UpdateCellVisual()
    {
        if (cellRenderer != null)
        {
            cellRenderer.material.color = isAlive ? Color.green : Color.black;
        }
    }
}