using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStartDoor : Interactable
{
    public override void Interact()
    {
        AudioMaster.instance.Fade("Theme", 0.0f);
        StaticGameData.GlobalInstance["GeneratorValues"] = new StaticGameData();
        StaticGameData GeneratorValues = StaticGameData.GlobalInstance["GeneratorValues"] as StaticGameData;
        GeneratorValues["RoomCount"] = 100;
        GeneratorValues["PlayerObject"] = Resources.Load<GameObject>("Units/PlayerTestPrefab");
        SceneLoader.instance.LoadScene("GeneratorSandbox");
    }
}
