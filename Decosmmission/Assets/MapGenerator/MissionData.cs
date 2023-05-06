using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class RoomCategory
{
    public RoomDesign[] designs;
}



public class MissionData
{
    public MissionPattern Pattern;
    public int RoomCount;
    public string[] factions { get { return Pattern.factions; } }
    public int MaxRoomCount { get { return Pattern.MaxRoomCount; } }
    public int MinRoomCount { get { return Pattern.MaxRoomCount; } }

    public RoomCategory[] specialDesigns { get { return Pattern.specialDesigns; } }
    public int[] specialDesignsCount;
    public int specialDesignSum { get { return specialDesignsCount.Sum(); } }
    public float specialDesignRelation { get { return specialDesignSum / (float)Pattern.specialDesignSum; } }

    public MissionData(MissionPattern pattern)
    {
        Pattern = pattern;
        RoomCount = UnityEngine.Random.Range(MinRoomCount, MaxRoomCount+1);
        specialDesignsCount = Pattern.specialDesignsCount.Clone() as int[];
    }
    public MissionData(string patternReference) : this(Resources.Load<MissionPattern>(patternReference)) { }

    public MissionData() : this("Mission/MilitaryDefeatHelicopter") { }
}
