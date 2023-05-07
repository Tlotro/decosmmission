using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStartDoor : Interactable
{
    

    public override void Interact()
    {
        NavigationTable.StartMission();
    }
}
