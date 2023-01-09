public class RoomCell
{
    public bool North { get; }
    public bool East { get; }
    public bool South { get; }
    public bool West { get; }
    public bool IsEmptySpace { get; }

    public bool IsDoor => North || East || South || West;

    public static RoomCell NoDoor => 
        new (false, false, false, false, false);

    public RoomCell(bool north, bool east, bool south, bool west, bool isEmptySpace)
    {
        North = north;
        East = east;
        South = south;
        West = west;
        IsEmptySpace = isEmptySpace;
    }
}