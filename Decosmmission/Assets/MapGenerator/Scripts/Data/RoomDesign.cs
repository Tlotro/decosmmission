using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class RoomDesign : MonoBehaviour
{
    [SerializeField]
    public CellArray[] Design;

    private (int y, int x)[] doorLocations;
    public (int y, int x)[] DoorLocations
    {
        get
        {
            if (doorLocations != null)
                return doorLocations;
            
            List<(int y, int x)> locations = new();
            for (int row = 0; row < Design.Length; row++)
            for (int col = 0; col < Design[0].Length; col++)
            {
                if (!Design[row][col])
                    continue;
                
                if (Design[row][col].IsDoor)
                    locations.Add((row, col));
            }

            doorLocations = locations.ToArray();
            return doorLocations;
        }
    }

    public IEnumerable<CellDesign> Doors => 
        DoorLocations.Select(coords => Design[coords.y][coords.x]);
    
    public (int y, int x) GetLocation(CellDesign cell)
    {
        for (int y = 0; y < Design.Length; y++)
        for (int x = 0; x < Design[0].Length; x++)
            if (Design[y][x] == cell)
                return (y, x);

        throw new ArgumentException("This design does not exist!");
    }
    
    public Room ToRoom()
    {
        // Рассчитывается, что в юнити Design настроен как двумерный
        // массив
        var copiedDesign = new RoomCell[Design.Length, Design[0].Length];
        
        for (var row = 0; row < Design.Length; row++)
        for (var col = 0; col < Design[row].Length; col++)
            if (Design[row][col] != null)
                copiedDesign[row, col] = Design[row][col].ToRoomCell();
            else
                copiedDesign[row, col] = RoomCell.NoDoor;

        return new Room(copiedDesign);
    }
}

// Чтобы можно было сериализовать в юнити двумерный массив
[Serializable]
public class CellArray
{
    public CellDesign[] cells;
    
    public CellDesign this[int index] => cells[index];

    public int Length => cells.Length;
}