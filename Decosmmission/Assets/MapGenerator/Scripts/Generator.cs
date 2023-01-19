using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private RoomRepository designs;

    public int MapSize { get; private set; }
    public MapCell[,] Map;

    private int x;
    private int y;

    private Room RandomRoom => designs.GetRandomRoom();
    private IEnumerable<RoomDesign> Designs => designs.AllDesigns;

    public void Generate(int roomCount)
    {
        // Создаём карту размера roomCount * 10 x roomCount * 10.
        // То есть, пока что рассчёт на то, что максимальная комнату
        // в ширину и в высоту: 10х10
        const int maxRoomSize = 20;
        MapSize = roomCount * maxRoomSize;
        Map = new MapCell[MapSize, MapSize];
        x = MapSize / 2;
        y = MapSize / 2;
        
        var startingRoom = RandomRoom;
        PlaceRoom(startingRoom);

        roomCount -= startingRoom.Doors.Count();
        foreach (var door in startingRoom.Doors)
            GenerateFrom(door);
    }

    private void GenerateFrom(RoomCell cell)
    {
        var fittingDesigns = Designs
            .Where(design => design.Doors.Any(door => IsOpposite(cell, door)))
            .ToList();
        var randomIndex = Random.Range(0, fittingDesigns.Count);
        var roomDesign = fittingDesigns[randomIndex];

        var door = roomDesign.Doors.First(door => IsOpposite(cell, door));
        var doorLocation = roomDesign.GetLocation(door);
        var room = roomDesign.ToRoom();
        
        print($"Current y: {y}, x: {x}");
        print($"Door location for {roomDesign.name}: {doorLocation}");

        var offset = GetDoorOffset(cell);
        y = cell.MapRepresentation.Y - doorLocation.y + offset.y;
        x = cell.MapRepresentation.X - doorLocation.x + offset.x;
        print($"New y: {y}, x: {x}");
        
        PlaceRoom(room);

        bool IsOpposite(RoomCell cell1, CellDesign cell2)
        {
            if (cell1.North)
                return cell2.South;
            if (cell1.East)
                return cell2.West;
            if (cell1.South)
                return cell2.North;
            if (cell1.West)
                return cell2.East;
            
            return false;
        }

        (int y, int x) GetDoorOffset(RoomCell cell)
        {
            if (cell.North)
                return (-1, 0);
            if (cell.East)
                return (0, 1);
            if (cell.South)
                return (1, 0);
            if (cell.West)
                return (0, -1);

            return (0, 0);
        }
    }

    // Помещает все клетки комнаты на главную карту.
    // x и y - координаты левого верхнего угла,
    // после установки комнаты становятся координатами
    // верхнего правого угла.
    private bool PlaceRoom(Room room)
    {
        int initialX = x;
        for (int row = 0; row < room.Height; row++, y++)
        {
            x = initialX;
            
            for (int col = 0; col < room.Width; col++, x++)
            {
                if (room.Cells[row, col].IsEmptySpace)
                    continue;
                    
                MapCell mapCell = new(y, x, room, row, col);
                Map[y, x] = mapCell;

                room.Cells[row, col].MapRepresentation = mapCell;
            }
        }

        return true;
    }
}