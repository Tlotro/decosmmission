public class MapCell
{
    public MapCell(int y, int x, Room room, int roomY, int roomX)
    {
        Y = y;
        X = x;
        Room = room;
        RoomY = roomY;
        RoomX = roomX;
    }

    public int Y { get; }
    public int X { get; }
    public Room Room { get; }
    public int RoomY { get; }
    public int RoomX { get; }

    public RoomCell Cell => Room.Cells[RoomY, RoomX];
}