using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demiurge : MonoBehaviour
{
    public GameObject AudioMaster;
    public GameObject SceneLoader;
    public GameObject SettingsManager;
    public GameObject PauseManager;
    public GameObject CanvasManager;

    public bool AudioMasterFlag;
    public bool SceneLoaderFlag;
    public bool SettingsManagerFlag;
    public bool PauseManagerFlag;
    public bool CanvasManagerFlag;

    private void Awake()
    {
        if (AudioMasterFlag) Instantiate(AudioMaster);
        if (SceneLoaderFlag) Instantiate(SceneLoader);
        if (SettingsManagerFlag) Instantiate(SettingsManager);
        if (PauseManagerFlag) Instantiate(PauseManager);
        if (CanvasManagerFlag) Instantiate(CanvasManager);
    }
}
