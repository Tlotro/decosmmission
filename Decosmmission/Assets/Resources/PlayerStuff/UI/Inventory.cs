using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject inventoryMenu;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    public void Open()
    {
        CanvasManager.instance.SwitchCanvas(CanvasType.InventoryCanvas);
        PauseManager.instance.Pause();
    }

    public void Close()
    {
        CanvasManager.instance.CloseTopStackCanvas();
        PauseManager.instance.Resume();
    }

    public void Use()
    {

    }

    public void Discard()
    {

    }
}
