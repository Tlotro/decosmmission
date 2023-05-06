using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStartDoor : Interactable
{
    public override void Interact()
    {
        AudioMaster.instance.Fade("Theme", 0.0f);
        if (StaticGameData.instance.missions.Count < 1)
            StaticGameData.instance.missions.Add(new MissionData());
        SceneLoader.instance.LoadScene("GeneratorSandbox");
    }
}
