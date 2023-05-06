using UnityEngine;

[CreateAssetMenu(fileName = "CellDesign")]
public class CellDesign : ScriptableObject
{
    public bool North;
    public bool South;
    public bool West;
    public bool East;
    public bool Forward;
    public bool Backward;

    public bool HasDirection(Direction d)
    {
        switch (d)
        {
            case Direction.north: return North;
            case Direction.south: return South;
            case Direction.west: return West;
            case Direction.east: return East;
            case Direction.forward: return Forward;
            case Direction.backward: return Backward;
            default: return false;
        }
    }

    public const int TilePerCell = 16;
}
