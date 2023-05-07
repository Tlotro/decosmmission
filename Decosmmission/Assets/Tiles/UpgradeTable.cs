using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTable : Interactable
{
    public GameObject upgradeMenu;
    public AudioImp menuImp;

    public static UpgradeTable instance;

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
        CanvasManager.instance.SwitchCanvas(CanvasType.UpgradeCanvas);
        PauseManager.instance.Pause();
    }

    public void Menu_Back()
    {
        CanvasManager.instance.CloseTopStackCanvas();
        PauseManager.instance.Resume();
    }

    public void Menu_Select()
    {

    }

    public void ButtonSound()
    {
        menuImp.Play("Click", MixerGroup.UI);
    }
}
