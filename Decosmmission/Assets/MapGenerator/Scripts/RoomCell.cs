using System;

public class RoomCell
{
    public bool North { get; }
    public bool East { get; }
    public bool South { get; }
    public bool West { get; }
    public bool IsEmptySpace { get; }
    public bool IsUnused { get; set; }

    public bool IsDoor => North || East || South || West;

    private MapCell _mapRepresentation;
    public MapCell MapRepresentation {
        get
        {
            if (_mapRepresentation == null)
                throw new InvalidOperationException("Map representation has not been set yet!");
            return _mapRepresentation;
        }
        set => _mapRepresentation = value;
    }

    public static RoomCell NoDoor => 
        new (false, false, false, false);

    public RoomCell(bool north, bool east, bool south, bool west)
    {
        North = north;
        East = east;
        South = south;
        West = west;
    }
}