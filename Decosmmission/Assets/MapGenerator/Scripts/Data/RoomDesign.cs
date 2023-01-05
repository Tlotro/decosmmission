using System;
using UnityEngine;

[Serializable]
public class RoomDesign : MonoBehaviour
{
    [SerializeField]
    public CellArray[] Design;
    
    public Room ToRoom()
    {
        // Рассчитывается, что в юнити Design настроен как двумерный
        // массив
        var copiedCells = new RoomCell[Design.Length, Design[0].Length];
        
        for (var row = 0; row < Design.Length; row++)
        for (var col = 0; col < Design[row].Length; col++)
            if (Design[row][col] != null)
                copiedCells[row, col] = Design[row][col].ToRoomCell();
            else
                copiedCells[row, col] = RoomCell.NoDoor;

        return new Room(copiedCells);
    }
}

// Чтобы можно было сериальзовать в юнити двумерный массив
[Serializable]
public class CellArray
{
    public CellDesign[] cells;
    
    public CellDesign this[int index] => cells[index];

    public int Length => cells.Length;
}