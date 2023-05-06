using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class StaticGameData
{
    public static StaticGameData instance;
    public int[] resources;
    public List<Item> items;
    public List<Weapon> weapons;
    public List<MissionData> missions;
    public int SelectedMission = 0;
    public MissionData SelectedMissionData { get { return missions.Count > SelectedMission ? missions[SelectedMission] : new MissionData(); } }
    public List<Player> players;
    public int SelectedPlayer = 0;
    public Player SelectedPlayerData { get { return players[SelectedPlayer]; } }

    public StaticGameData()
    {
        resources = new int[6];
        missions = new List<MissionData>();
        missions.Add(new MissionData());
        weapons = new List<Weapon>();
        weapons.Add(Resources.Load<GameObject>("PlayerStuff/Weapons/Gun/Gun").GetComponent<Weapon>());
        weapons.Add(Resources.Load<GameObject>("PlayerStuff/Weapons/Wrench/Wrench").GetComponent<Weapon>());
        players = new List<Player>();
        players.Add(Resources.Load<GameObject>("PlayerStuff/Players/PlayerTestPrefab").GetComponent<Player>());
    }

    public static void Save(int saveSlot)
    {
        string filename = "SaveData" + saveSlot.ToString()+ ".json";
        File.WriteAllText(filename, JsonUtility.ToJson(instance));
    }

    public static void Load(int saveSlot)
    {
        string filename = "SaveData" + saveSlot.ToString();
        if (File.Exists(filename))
        instance = (StaticGameData)JsonUtility.FromJson(File.ReadAllText(filename),typeof(StaticGameData));
        else instance = new StaticGameData();
    }
}
