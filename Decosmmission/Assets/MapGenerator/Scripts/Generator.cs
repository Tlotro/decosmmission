using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private RoomRepository designs;

    public MapCell[,] Map;
    public int MapSize;

    public void Generate(int roomCount)
    {
        // Создаём карту размера roomCount * 10 x roomCount * 10.
        // То есть, пока что рассчёт на то, что максимальная комнату
        // в ширину и в высоту: 10х10
        const int maxRoomSize = 10;
        MapSize = roomCount * maxRoomSize;
        Map = new MapCell[MapSize, MapSize];
        
        int x = 0;
        int y = 0;
        for (int i = 0; i < roomCount; i++)
        {
            var room = designs.GetRandomRoom();

            PlaceRoom(room, ref x, ref y);

            // Генерим от двери новую комнату
            // foreach (var door in room.Doors) 
            //     GenerateFrom(door);
        }
    }

    // Помещает все клетки комнаты на главную карту.
    // x и y - координаты левого верхнего угла.
    private void PlaceRoom(Room room, ref int x, ref int y)
    {
        for (int row = 0; row < room.Height; row++, y++)
        {
            for (int col = 0; col < room.Width; col++, x++)
            {
                if (room.Cells[row, col].IsEmptySpace)
                    continue;
                    
                MapCell mapCell = new(room, row, col);
                Map[y, x] = mapCell;
            }

            x = 0;
        }
    }
}