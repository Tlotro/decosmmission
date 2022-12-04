using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Button panelButton;
    public Animator animator;
    public Animator characterAnimator;

    public bool panelToggle;

    private void Awake()
    {
        AudioMaster.instance.Appear("Theme", "Chesed-1", MixerGroup.Music, 0.6f, 0, true, 0.5f);
        InvokeRepeating("SipCoffee", 3, 5);
    }

    private void SipCoffee()
    {
        if (Random.value < 1.0 / 3.0)
            characterAnimator.SetTrigger("SipCoffee");
    }

    public void PanelButtonPressed()
    {
        if (panelToggle) animator.SetTrigger("ClosePanel");
        else animator.SetTrigger("OpenPanel");
    }

    public void NewGameButtonPressed()
    {
        // ������� ����, ������� � �����
    }

    public void ContinueButtonPressed()
    {
        //SceneLoader.instance.LoadScene(""); // ������� � �����
    }

    public void SettingsButtonPressed()
    {
        CanvasManager.instance.SwitchCanvas(CanvasType.SettingsCanvas);
    }

    public void ManualButtonPressed()
    {
        CanvasManager.instance.SwitchCanvas(CanvasType.ManualCanvas);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
