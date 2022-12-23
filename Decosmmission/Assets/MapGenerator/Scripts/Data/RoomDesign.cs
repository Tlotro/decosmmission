using System;
using UnityEngine;
using Exception = System.Exception;

[Serializable]
public class RoomDesign : MonoBehaviour
{
    [SerializeField]
    public CellArray[] Design;

    [SerializeField] 
    private DoorDesign[] doors;

    [SerializeField] 
    private int width;
    
    [SerializeField] 
    private int height;

    private RoomCell[,] cells;

    public bool IsInitialized { get; set; }
    
    public void Initialize()
    {
        cells = new RoomCell[height, width];
        
        for (int row = 0; row < height; row++)
        for (int col = 0; col < width; col++)
            cells[row, col] = new RoomCell();

        IsInitialized = true;
    }

    public Room ToRoom()
    {
        if (!IsInitialized)
            throw new Exception("Design must first be initialized and only then converted to a room!");
        
        var copiedCells = new RoomCell[height, width];
        
        for (var row = 0; row < height; row++)
        for (var col = 0; col < width; col++)
            copiedCells[row, col] = cells[row, col].Copy();

        return new Room(copiedCells);
    }
}

// Чтобы можно было сериальзовать в юнити двумерный массив
[Serializable]
public class CellArray
{
    public RoomCell[] cells;
    
    public RoomCell this[int index] => cells[index];
}