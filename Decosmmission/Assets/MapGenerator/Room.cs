using System;
using UnityEngine;

[Serializable]
public class Room : MonoBehaviour
{
    [SerializeField]
    public CellArray[] Design;

    public RoomCell this[int row, int col] => Design[row][col];
}

// Чтобы можно было сериальзовать в юнити двумерный массив
[Serializable]
public class CellArray
{
    public RoomCell[] cells;

    public int Length => cells.Length;
    
    public RoomCell this[int index] => cells[index];
}