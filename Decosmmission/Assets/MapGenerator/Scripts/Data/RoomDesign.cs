using System;
using UnityEngine;

[Serializable]
public class RoomDesign : MonoBehaviour
{
    [SerializeField]
    public CellArray[] Design;

    public Room ToRoom()
    {
        var cells = new RoomCell[Design.Length, Design.Length];
        for (var row = 0; row < cells.length)
        {
            for (var col = 0; col < cells.length)
            {
                cells[row, col] = Design[row, col];
            }
        }

        return new Room(cells);
    }
    
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