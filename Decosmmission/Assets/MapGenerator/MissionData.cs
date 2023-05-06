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


[Serializable]
public class MissionData
{
    public string MissionName { get { return Pattern.MissionName; } }
    public string Description { get { return Pattern.Description; } }
    public MissionPattern Pattern;
    public int RoomCount;
    public int Level;
    public int Time;
    public string[] factions { get { return Pattern.factions; } }
    public int MaxRoomCount { get { return Pattern.MaxRoomCount; } }
    public int MinRoomCount { get { return Pattern.MinRoomCount; } }
    public int MinLevel { get { return Pattern.MinLevel; } }
    public int MaxLevel { get { return Pattern.MaxLevel; } }
    public int minTime { get { return Pattern.minTime; } }
    public int maxTime { get { return Pattern.maxTime; } }
    public RoomCategory[] specialDesigns { get { return Pattern.specialDesigns; } }
    public int[] specialDesignsCount;
    public int specialDesignSum { get { return specialDesignsCount.Sum(); } }
    public float specialDesignRelation { get { return specialDesignSum / ((float)Pattern.specialDesignSum+1); } }

    public MissionData(MissionPattern pattern)
    {
        Pattern = pattern;
        RoomCount = UnityEngine.Random.Range(MinRoomCount, MaxRoomCount+1);
        Time = UnityEngine.Random.Range( Mathf.Min(Mathf.Max(minTime,1),maxTime), maxTime+1);
        Level = UnityEngine.Random.Range(MinLevel, MaxLevel+1);
        specialDesignsCount = Pattern.specialDesignsCount.Clone() as int[];
    }
    public MissionData(string patternReference) : this(Resources.Load<MissionPattern>(patternReference)) { }

    public MissionData() : this("Mission/MilitaryDefeatHelicopter") { }
}
