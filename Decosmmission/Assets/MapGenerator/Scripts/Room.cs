public class Room
{
    public readonly int Width;
    public readonly int Height;
    
    public Room(RoomCell[,] cells)
    {
        Cells = cells;
        Width = cells.GetLength(0);
        Height = cells.GetLength(1);
    }
    
    public RoomCell[,] Cells { get; }
}