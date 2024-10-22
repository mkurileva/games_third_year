using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive;  // ��������� ������
    public List<Cell> neighbors;
    private Renderer cellRenderer;

    void Start()
    {
        cellRenderer = GetComponent<Renderer>();
        UpdateCellVisual();
    }

    // ����� ��� ��������� ��������� ������ ��� �����
    void OnMouseDown()
    {
        ToggleState();  // ����������� ��������� ������ (�����/������)
    }

    // ����� ��� ������������ ��������� ������
    public void ToggleState()
    {
        isAlive = !isAlive;  // ������ ��������� ������ �� ���������������
        UpdateCellVisual();  // ��������� ���� ������
    }

    public void SetState(bool alive)
    {
        isAlive = alive;
        UpdateCellVisual();
    }

    // ��������� ���� ������: ������� ��� �����, ������ ��� �������
    private void UpdateCellVisual()
    {
        if (cellRenderer != null)
        {
            cellRenderer.material.color = isAlive ? Color.green : Color.black;
        }
    }
}