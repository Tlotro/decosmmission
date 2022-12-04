using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demiurge : MonoBehaviour
{
    public bool AudioMasterFlag;
    public bool SceneLoaderFlag;
    public bool SettingsManagerFlag;
    public bool PauseManagerFlag;
    public bool CanvasManagerFlag;

    private void Awake()
    {
        if (AudioMasterFlag) Instantiate(Resources.Load<GameObject>("Manager_Prefabs/AudioMaster"));
        if (SceneLoaderFlag) Instantiate(Resources.Load<GameObject>("Manager_Prefabs/SceneLoader"));
        if (SettingsManagerFlag) Instantiate(Resources.Load<GameObject>("Manager_Prefabs/SettingsManager"));
        if (PauseManagerFlag) Instantiate(Resources.Load<GameObject>("Manager_Prefabs/PauseManager"));
        if (CanvasManagerFlag) Instantiate(Resources.Load<GameObject>("Manager_Prefabs/CanvasManager"));
        
        DialogWindow dw = DialogWindow.Create();
        dw.SetSide(Side.Right);
        dw.SetCharacterIcon("UI_Sprites/TestIcon");
        dw.SetText("Twirp-Twirp");
    }
}
