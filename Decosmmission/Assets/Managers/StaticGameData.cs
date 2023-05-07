using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class StaticGameData
{
    public static StaticGameData instance;
    public int credits;
    public int[] resources;
    public List<Item> items;
    public List<Weapon> weapons;
    public List<MissionData> missions;
    //public int SelectedMission = 0;
    [SerializeReference]
    public MissionData SelectedMissionData;
    public List<Player> players;
    public int SelectedPlayer = 0;
    public Player SelectedPlayerData { get { return players[SelectedPlayer]; } }
    public InteractebleDelegate GlobalinteractebleDelegate;
    public EmptyUnitDelegade GlobalStartDelegate;
    public EmptyUnitDelegade GlobalDeathDelegate; 
    public OnRoomGenerationDelegate roomGenerationDelegate;
    public PostGenerationDelegate PostGeneration;

    public StaticGameData()
    {
        resources = new int[6];
        missions = new List<MissionData>();
        missions.Add(new MissionData());
        weapons = new List<Weapon>();
        weapons.Add(Resources.Load<GameObject>("PlayerStuff/Weapons/Gun/Gun").GetComponent<Weapon>());
        weapons.Add(Resources.Load<GameObject>("PlayerStuff/Weapons/Wrench/Wrench").GetComponent<Weapon>());
        weapons.Add(Resources.Load<GameObject>("PlayerStuff/Weapons/R.Riffle/RRifle").GetComponent<Weapon>());
        players = new List<Player>();
        players.Add(Resources.Load<GameObject>("PlayerStuff/Players/PlayerTestPrefab").GetComponent<Player>());
    }

    public static void Save(int saveSlot)
    {
        Debug.Log("Saving");
        string filename = "SaveData" + saveSlot.ToString()+ ".json";
        File.WriteAllText(filename, JsonUtility.ToJson(instance));
    }

    public static void Load(int saveSlot)
    {
        string filename = "SaveData" + saveSlot.ToString() + ".json";
        if (File.Exists(filename))
        {
            Debug.Log("LoadingSave");
            instance = (StaticGameData)JsonUtility.FromJson(File.ReadAllText(filename), typeof(StaticGameData));
        }
        else instance = new StaticGameData();
    }

    public static void tickMissions()
    {
        Debug.Log("Tick");
        if (instance.SelectedMissionData == null)
            instance.SelectedMissionData = instance.missions[0];
        instance.missions.Remove(instance.SelectedMissionData);
        int iter = instance.missions.Count - 1;
        while (iter >= 0)
        {
            if (instance.missions[iter].Time > 0)
            {
                instance.missions[iter].Time--;
                if (instance.missions[iter].Time == 0)
                {
                    instance.missions.RemoveAt(iter);
                }
            }
            iter--;
        }
    }
}
