using System.Collections.Generic;
using MapGenerator.Scripts.View;
using UnityEngine;

public class MapVisualiser : MonoBehaviour
{
    [SerializeField] 
    private CellView cellPrefab;
    
    private List<GameObject> visualisedCells;

    public void Visualise(MapCell[,] map, int mapSize)
    {
        if (visualisedCells != null)
            CleanUpVisualisation();
        
        // Визуализация для отладки
        visualisedCells ??= new List<GameObject>();
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

                VisualiseCell(row, col, colorForRoom[room]);
            }
        }
    }

    private void VisualiseCell(int y, int x, Color cellColor)
    {
        var cellObject = Instantiate(cellPrefab);
        cellObject.SetColor(cellColor);
        cellObject.transform.position = new Vector3(x, y);

        visualisedCells.Add(cellObject.gameObject);
    }

    private void CleanUpVisualisation()
    {
        foreach (var cell in visualisedCells)
            Destroy(cell);
        visualisedCells.Clear();
    }
}