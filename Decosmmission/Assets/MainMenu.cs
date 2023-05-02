using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class MainMenu : MonoBehaviour
{
    public Button panelButton;
    public Animator animator;
    public Animator characterAnimator;
    public AudioImp menuImp;

    public bool panelToggle;

    private void Awake()
    {
        InvokeRepeating("SipCoffee", 3, 5);
    }

    private void Start()
    {
        while (AudioMaster.instance == null);

        AudioMaster.instance.StopAll();
        AudioMaster.instance.Appear("Theme", "Test_Tune_2", MixerGroup.Music, 0.6f, 0, true, 0.0f);

    }

    private void SipCoffee()
    {
        if (UnityEngine.Random.value < 1.0 / 3.0)
            characterAnimator.SetTrigger("SipCoffee");
    }

    public void PanelButtonPressed()
    {
        if (panelToggle) animator.SetTrigger("ClosePanel");
        else animator.SetTrigger("OpenPanel");
    }

    public void NewGameButtonPressed()
    {
        //AudioMaster.instance.Fade("Theme", 0.0f);
        SceneLoader.instance.LoadScene("Player Ship");
        // Стереть сэйв, перейти в лобби
    }

    public void ContinueButtonPressed()
    {
        //SceneLoader.instance.LoadScene(""); // Переход в лобби
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

    public void ButtonSound()
    {
        menuImp.Play("Click", MixerGroup.UI);
    }
}
