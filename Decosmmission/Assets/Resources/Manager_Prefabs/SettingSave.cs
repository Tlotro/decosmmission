using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingSave", menuName = "SettingSave")]
public class SettingSave : ScriptableObject
{
    public float masterVolume { get; set; }
    public float musicVolume { get; set; }
    public float soundsVolume { get; set; }
    public float UIVolume { get; set; }

    public bool isFullScreen { get; set; }
    public int resolutionIndex { get; set; }
    public int qualityIndex { get; set; }
}