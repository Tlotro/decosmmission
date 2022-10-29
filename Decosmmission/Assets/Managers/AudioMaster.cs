using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum MixerGroup
{
    Music,
    Sounds,
    UI
}

public class AudioMaster : MonoBehaviour
{
    public AudioMixer audioMixer;
    [HideInInspector]
    public List<AudioImp> imps;
    
    public static AudioMaster instance;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
            imps = new List<AudioImp>();

            GameObject a = new GameObject();
            DontDestroyOnLoad(a);
            a.name = "Theme";
            AudioImp imp = a.AddComponent<AudioImp>();
            imp.audioMaster = this;
            imps.Add(imp);
            imp.source = a.AddComponent<AudioSource>();
            imp.clips = new List<AudioClip>();
        }
        else Destroy(gameObject);
    }


    public void Play(string impName, string clipName, MixerGroup mixerGroup, float volume = 0.6f, int timing = 0, bool loop = false)
    {
        AudioImp imp = imps.Find(x => x.name == impName);
        if (imp == null)
        {
            Debug.Log("Imp does not exist.");
            return;
        }

        imp.Play(clipName, mixerGroup, volume, timing, loop);
    }

    public void Appear(string impName, string clipName, MixerGroup mixerGroup, float volume = 0.6f, int timing = 0, bool loop = false, float appearTime = 1f)
    {
        AudioImp imp = imps.Find(x => x.name == impName);
        if (imp == null)
        {
            Debug.Log("Imp does not exist.");
            return;
        }

        imp.Appear(clipName, mixerGroup, volume, timing, loop, appearTime);
    }

    public void Stop(string impName)
    {
        AudioImp imp = imps.Find(x => x.name == impName);
        if (imp == null)
        {
            Debug.Log("Imp does not exist.");
            return;
        }

        imp.Stop();
    }

    public void Fade(string impName, float timeToFade = 1f)
    {
        AudioImp imp = imps.Find(x => x.name == impName);
        if (imp == null)
        {
            Debug.Log("Imp does not exist.");
            return;
        }

        imp.Fade(timeToFade);
    }

    private AudioImp SetupTempImp(string clipName, bool dontDestroyOnLoad = false)
    {
        GameObject a = new GameObject();
        if (dontDestroyOnLoad) DontDestroyOnLoad(a);
        a.name = clipName;
        TempAudioImp imp = a.AddComponent<TempAudioImp>();
        imp.audioMaster = this;
        imps.Add(imp);
        imp.source = a.AddComponent<AudioSource>();
        imp.clips = new List<AudioClip>();

        return imp;
    }

    public void Play_tempImp(string clipName, MixerGroup group, float volume = 0.6f, int timing = 0, bool loop = false, bool dontDestroyOnLoad = false)
    {
        SetupTempImp(clipName, dontDestroyOnLoad).Play(clipName, group, volume, timing, loop);
    }

    public void Appear_tempImp(string clipName, MixerGroup mixerGroup, float volume = 0.6f, int timing = 0, bool loop = false, float appearTime = 1f, bool dontDestroyOnLoad = false)
    {
        SetupTempImp(clipName, dontDestroyOnLoad).Appear(clipName, mixerGroup, volume, timing, loop, appearTime);
    }

}
