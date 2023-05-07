using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using System.Threading;
using TMPro;

public class NavigationTable : Interactable
{
    public GameObject navigationMenu;
    public GameObject MissionBox;
    public GameObject Scroll;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    [HideInInspector]
    public int chosenMission;

    public AudioImp menuImp;

    public static NavigationTable instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    public override void Interact()
    {
        CanvasManager.instance.SwitchCanvas(CanvasType.NavigationCanvas);
        PauseManager.instance.Pause();
        foreach (Transform child in Scroll.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < StaticGameData.instance.missions.Count; i++)
        {
            MissionBoxScript boxScript = Instantiate(MissionBox,Scroll.transform).GetComponent<MissionBoxScript>();
            boxScript.Init(i);
        }
        StartCoroutine(UpdateText(chosenMission));
    }

    public IEnumerator UpdateText(int mission)
    {
        if (mission != chosenMission)
        chosenMission = mission;
        MissionData data = StaticGameData.instance.missions[chosenMission];
        Name.text = data.MissionName;
            for (int i = 0; i < data.Description.Length + 1; i++)
            {
                Description.text = data.Description.Substring(0, i);
                yield return new WaitForSeconds(2 / data.Description.Length);
            }
    }

    public void Menu_Back()
    {
        CanvasManager.instance.CloseTopStackCanvas();
        PauseManager.instance.Resume();
    }

    public void Menu_Select()
    {
        StaticGameData.instance.SelectedMissionData = StaticGameData.instance.missions[chosenMission];
    }

    public void Menu_QuickStart()
    {
        StaticGameData.instance.SelectedMissionData = StaticGameData.instance.missions[chosenMission];
        StaticGameData.tickMissions();
        PauseManager.instance.Resume();
        AudioMaster.instance.Fade("Theme", 0.0f);
        SceneLoader.instance.LoadScene("GeneratorSandbox");
    }

    public void ButtonSound()
    {
        menuImp.Play("Click", MixerGroup.UI);
    }
}
