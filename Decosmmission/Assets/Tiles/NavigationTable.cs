using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationTable : Interactable
{
    public GameObject navigationMenu;

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
    }

    public void Menu_Back()
    {
        CanvasManager.instance.CloseTopStackCanvas();
        PauseManager.instance.Resume();
    }

    public void Menu_Select()
    {

    }

    public void Menu_QuickStart()
    {

    }
}
