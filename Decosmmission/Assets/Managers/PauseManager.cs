using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;

    public static PauseManager instance;
    public static PauseManager oldInstance;
    public AudioImp menuImp;

    public static bool GamePaused = false;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    private void Start()
    {
        PauseMenu.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    void Update()
    {
        Debug.Log(GamePaused);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GamePaused)
            {
                CanvasManager.instance.SwitchCanvas(CanvasType.PauseCanvas);
                Pause();
            }
            else if (CanvasManager.instance.activeCanvasStack.Peek().type == CanvasType.PauseCanvas)
            {
                CanvasManager.instance.CloseTopStackCanvas();
                Resume();
            }
        }

    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Resume_Button()
    {
        CanvasManager.instance.CloseTopStackCanvas();
        Resume();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Settings()
    {
        CanvasManager.instance.SwitchCanvas(CanvasType.SettingsCanvas);
    }

    public void Quit()
    {
        GamePaused = false;
        SceneLoader.instance.LoadScene("Main Menu");
    }

    public void Manual()
    {
        CanvasManager.instance.SwitchCanvas(CanvasType.ManualCanvas);
    }
    public void ButtonSound()
    {
        menuImp.Play("Click", MixerGroup.UI);
    }

}
