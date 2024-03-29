using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;

    public static PauseManager instance;
    public static PauseManager oldInstance;

    public static bool GamePaused = false;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GamePaused)
                Pause();
            else if (CanvasManager.instance.activeCanvasStack.Peek().type == CanvasType.PauseCanvas)
                Resume();
        }

    }

    public void Resume()
    {
        CanvasManager.instance.CloseTopStackCanvas();
        Time.timeScale = 1f;
        GamePaused = false;
    }

    private void Pause()
    {
        CanvasManager.instance.SwitchCanvas(CanvasType.PauseCanvas);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Settings()
    {
        CanvasManager.instance.SwitchCanvas(CanvasType.SettingsCanvas);
    }

    public void Quit()
    {
        SceneLoader.instance.LoadScene("Scene1");
    }

    public void Manual()
    {
        // ����� �������
    }

}
