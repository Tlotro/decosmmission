using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (StaticGameData.instance.missions.Count < 3)
        {
            MissionPattern[] patterns = Resources.LoadAll<MissionPattern>("Mission").Where(x=>!x.specialAccess).ToArray();
            for (int i = StaticGameData.instance.missions.Count; i < 3; i++)
            {
                StaticGameData.instance.missions.Add(new MissionData(patterns[Random.Range(0,patterns.Length)]));
            }
        }
        StaticGameData.Save(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
