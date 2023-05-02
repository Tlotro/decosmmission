using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum CanvasType{
    MainCanvas,
    PauseCanvas,
    SettingsCanvas,
    DialogWindowCanvas,
    ManualCanvas
}

public class CanvasManager : MonoBehaviour
{
    [HideInInspector]
    public List<ControlledCanvas> controlledCanvasList;
    [HideInInspector]
    public Stack<ControlledCanvas> activeCanvasStack;

    public static CanvasManager instance;
    public static CanvasManager oldInstance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Reload();
    }

    public void Reload()
    {
        controlledCanvasList = Object.FindObjectsOfType<ControlledCanvas>().ToList();
        activeCanvasStack = new Stack<ControlledCanvas>();

        if (SettingsManager.instance != null)
        {
            controlledCanvasList.Add(SettingsManager.instance.SettingsMenu.GetComponent<ControlledCanvas>());
            controlledCanvasList.Add(SettingsManager.instance.Manual.GetComponent<ControlledCanvas>());
        }
        if (PauseManager.instance != null)
            controlledCanvasList.Add(PauseManager.instance.PauseMenu.GetComponent<ControlledCanvas>());

        ControlledCanvas mainCanvas = controlledCanvasList.Find(x => x.type == CanvasType.MainCanvas);
        if(mainCanvas != null)
        {
            activeCanvasStack.Push(mainCanvas);
        }
    }

    public void SwitchCanvas(CanvasType type)
    {
        ControlledCanvas desiredCanvas = controlledCanvasList.Find(x => x.type == type);
        if(desiredCanvas != null)
        {
            if(activeCanvasStack.Count > 0)
            {
                activeCanvasStack.Peek().SetInteractable(false);
                desiredCanvas.SetSortingOrder(activeCanvasStack.Peek().GetSortingOrder() + 1);
            } 
            desiredCanvas.SetActive(true);
            activeCanvasStack.Push(desiredCanvas);
            //StackState();
        }
        else Debug.Log("Desired Canvas does not exist.");
    }

    public void CloseTopStackCanvas()
    {

        activeCanvasStack.Peek().SetActive(false);
        activeCanvasStack.Pop();

        if (activeCanvasStack.Count > 0)
            activeCanvasStack.Peek().SetInteractable(true);
    }

    public void StackState()
    {
        foreach (ControlledCanvas a in activeCanvasStack)
            Debug.Log(a.name);
        Debug.Log("________");
    }

    public void ListState()
    {
        Debug.Log("__List__");
        foreach (ControlledCanvas a in controlledCanvasList)
            Debug.Log(a.name);
        Debug.Log("________");
    }
}
