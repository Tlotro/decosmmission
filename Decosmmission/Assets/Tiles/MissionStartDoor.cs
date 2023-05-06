using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStartDoor : Interactable
{
    

    public override void Interact()
    {
        StaticGameData.tickMissions();
        AudioMaster.instance.Fade("Theme", 0.0f);
        SceneLoader.instance.LoadScene("GeneratorSandbox");
    }
}
