using System;
using System.Collections.Generic;
using System.Linq;

public class Room
{
    public Room(RoomCell[,] cells)
    {
        Cells = cells;

        List<(int y, int x)> doors = new();
        for (int row = 0; row < Height; row++)
        for (int col = 0; col < Width; col++)
        {
            if (cells[row, col].IsDoor)
                doors.Add((row, col));
        }
        DoorLocations = doors.ToArray();
    }
    
    public RoomCell[,] Cells { get; }
    public (int y, int x)[] DoorLocations { get; }
    public IEnumerable<RoomCell> Doors => DoorLocations.Select(location => Cells[location.y, location.x]);

    public int Height => Cells.GetLength(0);
    public int Width => Cells.GetLength(1);
    
    public (int y, int x) GetLocation(RoomCell cell)
    {
        for (int y = 0; y < Cells.GetLength(0); y++)
        for (int x = 0; x < Cells.GetLength(1); x++)
            if (Cells[y, x] == cell)
                return (y, x);

        throw new ArgumentException("This design does not exist!");
    }
}