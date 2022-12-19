public class MapCell
{
    public MapCell(Room room, int roomY, int roomX)
    {
        Room = room;
        RoomY = roomY;
        RoomX = roomX;
    }
    
    public Room Room { get; }
    public int RoomY { get; }
    public int RoomX { get; }
}