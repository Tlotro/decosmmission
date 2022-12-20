using UnityEngine;

public class RoomCell : MonoBehaviour
{
    public bool North;
    public bool West;
    public bool South;
    public bool East;

    public RoomCell(bool north, bool west, bool south, bool east)
    {
        North = north;
        West = west;
        South = south;
        East = east;
    }

    public RoomCell Copy()
    {
        return new RoomCell(North, West, South, East);
    }
}