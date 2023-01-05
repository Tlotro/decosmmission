public class Room
{
    public Room(RoomCell[,] cells)
    {
        Cells = cells;
    }
    
    public RoomCell[,] Cells { get; }

    public int Height => Cells.GetLength(0);
    public int Width => Cells.GetLength(1);
}