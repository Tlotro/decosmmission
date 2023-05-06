using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "MissionPattern")]
public class MissionPattern : ScriptableObject
{
    public string[] factions;
    public int MaxRoomCount;
    public int MinRoomCount;
    [SerializeField]
    public RoomCategory[] specialDesigns;
    public int[] specialDesignsCount;
    public int specialDesignSum { get { return specialDesignsCount.Sum(); } }
}
