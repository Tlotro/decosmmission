using UnityEngine;

[System.Serializable]
public class CellDesign : MonoBehaviour
{
    public bool North;
    public bool East;
    public bool South;
    public bool West;

    public bool IsEmptySpace;

    public RoomCell ToRoomCell()
    {
        return new RoomCell(North, East, South, West, IsEmptySpace);
    }
}