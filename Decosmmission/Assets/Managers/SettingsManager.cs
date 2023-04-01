using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq;
using System.IO;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public GameObject SettingsMenu;
    public GameObject Manual;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundsSlider;
    public Slider UISlider;
    
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreenToggle;

    public Resolution[] resolutions;
    public static SettingsManager instance;

    public SettingSave settingSave;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null) instance = this;
        else Destroy(gameObject);

        resolutions = Screen.resolutions.Where(x => /*x.refreshRate == 60 &&*/ ((x.width == 1920 && x.height == 1080) || (x.width == 1600 && x.height == 900) || (x.width == 1280 && x.height == 720))).ToArray();
        LoadData();
    }

    private void Start()
    {
        SetMasterVolume(masterSlider.value);
        SetFullScreen(fullscreenToggle.isOn);
        SetResolution(resolutionDropdown.value);
        SetQuality(qualityDropdown.value);
    }

    private void OnDestroy()
    {
        SaveData();
    }

    public float map(float value, float in_min, float in_max, float out_min, float out_max) => (value - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(map(value, masterSlider.minValue, masterSlider.maxValue, 0.0001f, 1f)) * 20);
        SetMusicVolume(musicSlider.value);
        SetSoundsVolume(soundsSlider.value);
        SetUIVolume(UISlider.value);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(map(map(value, musicSlider.minValue, musicSlider.maxValue, masterSlider.minValue, masterSlider.value), musicSlider.minValue, musicSlider.maxValue, 0.0001f, 1f)) * 20);
    }

    public void SetSoundsVolume(float value)
    {
        audioMixer.SetFloat("SoundsVolume", Mathf.Log10(map(map(value, soundsSlider.minValue, soundsSlider.maxValue, masterSlider.minValue, masterSlider.value), soundsSlider.minValue, soundsSlider.maxValue, 0.0001f, 1f)) * 20);
    }
    
    public void SetUIVolume(float value)
    {
        audioMixer.SetFloat("UIVolume", Mathf.Log10(map(map(value, UISlider.minValue, UISlider.maxValue, masterSlider.minValue, masterSlider.value), UISlider.minValue, UISlider.maxValue, 0.0001f, 1f)) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    public void GoBack()
    {
        CanvasManager.instance.CloseTopStackCanvas();
    }

    private void SaveData()
    {
        settingSave.masterVolume = masterSlider.value;
        settingSave.musicVolume = musicSlider.value;
        settingSave.soundsVolume = soundsSlider.value;
        settingSave.UIVolume = UISlider.value;

        settingSave.isFullScreen = fullscreenToggle.isOn;
        settingSave.resolutionIndex = resolutionDropdown.value;
        settingSave.qualityIndex = qualityDropdown.value;
    }

    private void LoadData()
    {
        musicSlider.value = settingSave.musicVolume;
        soundsSlider.value = settingSave.soundsVolume;
        UISlider.value = settingSave.UIVolume;
        masterSlider.value = settingSave.masterVolume;

        fullscreenToggle.isOn = settingSave.isFullScreen;

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions.Select(x => $"{x.width} x {x.height}").ToList());
        resolutionDropdown.value = settingSave.resolutionIndex;
        resolutionDropdown.RefreshShownValue();

        qualityDropdown.value = settingSave.qualityIndex;
    }


}

