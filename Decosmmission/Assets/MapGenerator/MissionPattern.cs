using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "MissionPattern")]
public class MissionPattern : ScriptableObject
{
    public string MissionName;
    public string Description;
    public string[] factions;
    public int MaxRoomCount;
    public int MinRoomCount;
    public int MinLevel;
    public int MaxLevel;
    [SerializeField]
    public RoomCategory[] specialDesigns;
    public int[] specialDesignsCount;
    public int specialDesignSum { get { return specialDesignsCount.Sum(); } }
    /// <summary>
    /// SpecialAccess missions do not generate as part of general pool and have to be acquired somehow else
    /// </summary>
    public bool specialAccess;
    public int minTime;
    public int maxTime;
}
