using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private RoomRepository designs;

    // Для визуализации
    [SerializeField] 
    private RoomCell cellPrefab;
    
    private MapCell[,] map;
    private int mapSize;
    
    private List<RoomCell> visualisedCells;

    public void Generate(int roomCount)
    {
        if (map != null)
            CleanUp();
        
        // Создаём карту размера roomCount * roomCount * 10.
        // Места в теории может не хватить, но пока что пусть
        // будет так.
        const int magicNumber = 10;
        mapSize = roomCount * magicNumber;
        map = new MapCell[mapSize, mapSize];
        
        int x = 0;
        int y = 0;
        for (int i = 0; i < roomCount; i++)
        {
            var room = designs.GetRandomRoom();

            for (int row = 0; row < room.Height; row++, y++)
            {
                for (int col = 0; col < room.Width; col++, x++)
                {
                    if (room.Cells[row, col] == null)
                        continue;
                    
                    MapCell mapCell = new(room, row, col);
                    map[y, x] = mapCell;
                }

                x = 0;
            }
        }
        
        // Визуализация для отладки
        Dictionary<Room, Color> colorForRoom = new();
        for (int row = 0; row < mapSize; row++)
        {
            for (int col = 0; col < mapSize; col++)
            {
                if (map[row, col] == null)
                    continue;

                var room = map[row, col].Room;
                
                if (!colorForRoom.ContainsKey(room))
                    colorForRoom.Add(room, new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f)));
                
                // Временная штука чтобы визуализировать всё, что происходит
                VisualiseCell(row, col, colorForRoom[room]);
            }
        }
        
        Debug.Log("Generating...");
    }

    private void VisualiseCell(int y, int x, Color cellColor)
    {
        var cellObject = Instantiate(cellPrefab);

        var renderer = cellObject.GetComponent<SpriteRenderer>();

        renderer.color = cellColor;

        cellObject.transform.position = new Vector3(x, y);

        visualisedCells.Add(cellObject);
    }

    private void CleanUp()
    {
        foreach (var cell in visualisedCells)
            Destroy(cell);

        visualisedCells.Clear();
    }
}