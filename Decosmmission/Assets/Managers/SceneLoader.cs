using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public static AudioListener tempListener;

    public Animator transitionAnimator;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void Start()
    {
        transitionAnimator.SetTrigger("Fade_In");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        transitionAnimator.SetTrigger("Fade_Out");

        yield return new WaitForSecondsRealtime(0.7f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
        while (!operation.isDone)
        {
            yield return null;
        }

        transitionAnimator.SetTrigger("Fade_In");
    }

    public void LoadAdditiveScene(string sceneName)
    {
        CanvasManager.oldInstance = CanvasManager.instance;

        PauseManager.oldInstance = PauseManager.instance;
        PauseManager.instance.gameObject.SetActive(false);

        tempListener = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>();
        tempListener.enabled = false;

        StartCoroutine(LoadAdditiveAsynchronously(sceneName));
    }

    IEnumerator LoadAdditiveAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void UnloadAdditiveScene(string sceneName)
    {
        CanvasManager.instance = CanvasManager.oldInstance;
        CanvasManager.oldInstance = null;

        PauseManager.instance = PauseManager.oldInstance;
        PauseManager.oldInstance = null;
        PauseManager.instance.gameObject.SetActive(true);

        StartCoroutine(UnloadAdditiveAsynchronously(sceneName));

        tempListener.enabled = true;
        tempListener = null;
    }

    IEnumerator UnloadAdditiveAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

}
