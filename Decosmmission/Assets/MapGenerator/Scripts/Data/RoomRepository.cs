using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomRepository : MonoBehaviour
{
    [SerializeField] private RoomDesign[] designs;

    public int Count => designs.Length;

    public Room GetRandomRoom()
    {
        var design = designs[Random.Range(0, Count)];
        return design.ToRoom();
    }

    public IEnumerable<RoomDesign> AllDesigns => designs;
}