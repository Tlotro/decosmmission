public class Room
{
    public readonly int Height;
    public readonly int Width;
    
    public Room(RoomCell[,] cells)
    {
        Cells = cells;
        Height = cells.GetLength(0);
        Width = cells.GetLength(1);
    }
    
    public RoomCell[,] Cells { get; }
}