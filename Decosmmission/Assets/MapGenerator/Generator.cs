using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private Room[] rooms;

    // Для визуализации
    [SerializeField] 
    private RoomCell cellPrefab;
    
    // Вся карта.
    // Один элемент - комната в этой точке
    // и локальная позиция в этой комнате.
    private MapCell[,] map;
    private int mapSize;
    
    private Room RandomRoom => rooms[Random.Range(0, rooms.Length - 1)];
    
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
            var room = RandomRoom;
            var design = room.Design;

            for (int row = 0; row < design.Length; row++, y++)
            {
                for (int col = 0; col < design[row].Length; col++, x++)
                {
                    if (design[row][col] == null)
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
                InstantiateCell(row, col, colorForRoom[room]);
            }
        }
        
        Debug.Log("Generating...");
    }

    private void InstantiateCell(int y, int x, Color cellColor)
    {
        var cellObject = Instantiate(cellPrefab);

        var renderer = cellObject.GetComponent<SpriteRenderer>();

        renderer.color = cellColor;

        cellObject.transform.position = new Vector3(x, y);
    }

    private void CleanUp()
    {
        for (int row = 0; row < mapSize; row++)
        for (int col = 0; col < mapSize; col++)
            Destroy(map[row, col].Room);
    }
}