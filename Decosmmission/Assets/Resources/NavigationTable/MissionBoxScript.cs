using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionBoxScript : MonoBehaviour
{
    [HideInInspector]
    public int savedMission;
    public TextMeshProUGUI MName;
    public TextMeshProUGUI Details;
    public void Init(int mission)
    {
        savedMission = mission;
        MissionData missionData = StaticGameData.instance.missions[savedMission];
        MName.text = missionData.MissionName;
        Details.text = (missionData.Time>0?"Time left: " + missionData.Time + " ": "")+ "Length: " + missionData.RoomCount + " Level: " + missionData.Level;
    }
    public void OnSelect()
    {
        StartCoroutine(transform.parent.parent.parent.parent.parent.GetComponent<NavigationTable>().UpdateText(savedMission));
    }
}
