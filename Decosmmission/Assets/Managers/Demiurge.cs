using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demiurge : MonoBehaviour
{
    public Image image;
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
        if (image != null)
        {
            SpaceGenerator.generateSpaceValues();
            SpaceGenerator.getSpace(1920, 1080, image);
        }
    }
}
