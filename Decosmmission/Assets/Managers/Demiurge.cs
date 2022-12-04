using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demiurge : MonoBehaviour
{
    public Image image;
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

        if (image != null)
        {
            SpaceGenerator.generateSpaceValues();
            SpaceGenerator.getSpace(1920, 1080, image);
        }
        if (AudioMasterFlag) Instantiate(AudioMaster);
        if (SceneLoaderFlag) Instantiate(SceneLoader);
        if (SettingsManagerFlag) Instantiate(SettingsManager);
        if (PauseManagerFlag) Instantiate(PauseManager);
        if (CanvasManagerFlag) Instantiate(CanvasManager);
    }
}
