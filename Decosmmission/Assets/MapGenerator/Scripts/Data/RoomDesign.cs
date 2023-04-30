using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum RoomState { unexplored, explored, cleared }

[Serializable]
public class RoomDesign : MonoBehaviour
{
    [SerializeField]
    public CellArray[] Design;
    //The type of mission the room is associated with
    public string Faction;
    //The list of tags
    public string[] RoomTags;
    public Door[] doors;
    public RoomState roomState = RoomState.unexplored;

    
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
        // Рассчитывается, что в юнити Design настроен как двумерный массив
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