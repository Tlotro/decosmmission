using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomRepository : MonoBehaviour
{
    [SerializeField] private RoomDesign[] designs;

    public int Count => designs.Length;

    private void Awake()
    {
        foreach (var design in designs)
        {
            // (IsInitialized всегда был true)
            design.IsInitialized = false;
        }
    }

    public Room GetRandomRoom()
    {
        var design = designs[Random.Range(0, Count)];
        if (!design.IsInitialized)
            design.Initialize();
        
        return design.ToRoom();
    }
}