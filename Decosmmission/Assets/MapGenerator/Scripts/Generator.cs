using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private RoomRepository designs;

    public MapCell[,] Map;
    public int MapSize;

    private int x;
    private int y;

    private Room RandomRoom => designs.GetRandomRoom();
    private IEnumerable<RoomDesign> Designs => designs.AllDesigns;

    public void Generate(int roomCount)
    {
        x = 0;
        y = 0;
        // Создаём карту размера roomCount * 10 x roomCount * 10.
        // То есть, пока что рассчёт на то, что максимальная комнату
        // в ширину и в высоту: 10х10
        const int maxRoomSize = 10;
        MapSize = roomCount * maxRoomSize;
        Map = new MapCell[MapSize, MapSize];
        
        var room = RandomRoom;

        PlaceRoom(room);

        foreach (var door in room.Doors)
            GenerateFrom(door);

        for (int i = 0; i < roomCount; i++)
        {
            // Генерим от двери новую комнату
            // foreach (var door in room.Doors) 
            //     GenerateFrom(door);
            // По координатам: когда генерим от двери,
            // нужно +1 к соответсвующей координате
            // в зависимости от двери ('ЮГ: -1y') и т.д.
            // И потом вычитать из полученных координат
            // локальные координаты двери, к которой присоединяем,
            // чтобы левый верхний угол оказался в нужном месте.
        }
    }

    private void GenerateFrom(RoomCell cell)
    {
        if (cell.North)
            GenerateNorth(cell);
        if (cell.East)
            GenerateEast(cell);
        if (cell.South)
            GenerateSouth(cell);
        if (cell.West)
            GenerateWest(cell);
    }

    private void GenerateNorth(RoomCell cell)
    {
        var fittingDesigns = Designs.Where(design => design.Doors.Any(door => door.South));
        if (fittingDesigns.Count() == 0)
            print($"No designs found for door {cell}");
        print("Designs found!");

        var randomDesign = fittingDesigns.First();
        PlaceRoom(randomDesign.ToRoom());
    }

    private void GenerateEast(RoomCell cell)
    {
        var fittingDesigns = Designs.Where(design => design.Doors.Any(door => door.East));
        if (fittingDesigns.Count() == 0)
            print($"No designs found for door {cell}");
        print("Designs found!");
        
        var randomDesign = fittingDesigns.First();
        PlaceRoom(randomDesign.ToRoom());
    }
    
    private void GenerateSouth(RoomCell cell)
    {
        var fittingDesigns = Designs.Where(design => design.Doors.Any(door => door.North));
        if (fittingDesigns.Count() == 0)
            print($"No designs found for door {cell}");
        print("Designs found!");
        
        var randomDesign = fittingDesigns.First();
        PlaceRoom(randomDesign.ToRoom());
    }

    private void GenerateWest(RoomCell cell)
    {
        var fittingDesigns = Designs.Where(design => design.Doors.Any(door => door.West));
        if (fittingDesigns.Count() == 0)
            print($"No designs found for door {cell}");
        print("Designs found!");
        
        var randomDesign = fittingDesigns.First();
        PlaceRoom(randomDesign.ToRoom());
    }

    // Помещает все клетки комнаты на главную карту.
    // x и y - координаты левого верхнего угла,
    // после установки комнаты становятся координатами
    // верхнего правого угла.
    private void PlaceRoom(Room room)
    {
        int initialX = x;
        for (int row = 0; row < room.Height; row++, y++)
        {
            x = initialX;
            
            for (int col = 0; col < room.Width; col++, x++)
            {
                if (room.Cells[row, col].IsEmptySpace)
                    continue;
                    
                MapCell mapCell = new(room, row, col);
                Map[y, x] = mapCell;
            }
        }
    }
}