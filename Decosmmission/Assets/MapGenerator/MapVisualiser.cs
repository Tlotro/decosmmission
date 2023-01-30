using System.Collections.Generic;
using System.Linq;
using MapGenerator.Scripts.View;
using UnityEngine;

public class MapVisualiser : MonoBehaviour
{
    [SerializeField] 
    private CellView cellPrefab;
    [SerializeField] 
    private CellView doorPrefab;
    
    private List<GameObject> visualisedCells;

    public void Visualise(MapCell[,] map, int mapSize)
    {
        if (visualisedCells != null)
            CleanUpVisualisation();
        
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

                var cell = map[row, col].Cell;
                if (cell.IsDoor)
                {
                    if (cell.IsUnused)
                        VisualiseCell(mapSize - row, col, Color.red, doorPrefab);
                    else
                        VisualiseCell(mapSize - row, col, colorForRoom[room], doorPrefab);
                }
                else
                {
                    VisualiseCell(mapSize - row, col, colorForRoom[room], cellPrefab);
                }
            }
        }
    }

    private void VisualiseCell(int y, int x, Color cellColor, CellView view)
    {
        var cellObject = Instantiate(view, transform, true);
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