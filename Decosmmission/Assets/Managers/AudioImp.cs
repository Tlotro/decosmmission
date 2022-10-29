using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioImp : MonoBehaviour
{
    public AudioMaster audioMaster;
    public AudioSource source;
    public List<AudioClip> clips;

    public void OnDestroy()
    {
        audioMaster.imps.Remove(this);
    }

    public AudioImp(AudioMaster master, AudioSource source, List<AudioClip> clips)
    {
        this.audioMaster = master;
        master.imps.Add(this);
        this.source = source;
        this.clips = clips;
    }

    private AudioMixerGroup GetAudioMixerGroup(MixerGroup mixerGroup)
    {
        return audioMaster.audioMixer.FindMatchingGroups(mixerGroup.ToString())[0];
    }

    public void Play(string clipName, MixerGroup mixerGroup, float volume = 0.6f, int timing = 0, bool loop = false)
    {
        AudioClip clip = clips.Find(x => x.name == clipName);
        if (clip == null)
        {
            Debug.Log("Clip does not exist. Loading...");
            clips.Add(Resources.Load<AudioClip>(clipName));
            Play(clipName, mixerGroup, volume, timing, loop);
        }
        else source.clip = clip;

        if (timing >= 0)
            source.time = timing;

        source.outputAudioMixerGroup = GetAudioMixerGroup(mixerGroup);
        source.volume = volume;
        source.loop = loop;
        source.Play();
    }

    public void Appear(string clipName, MixerGroup mixerGroup, float volume = 0.6f, int timing = 0, bool loop = false, float appearTime = 1f)
    {
        AudioClip clip = clips.Find(x => x.name == clipName);
        if (clip == null)
        {
            Debug.Log("Clip does not exist. Loading...");
            clips.Add(Resources.Load<AudioClip>(clipName));
            Appear(clipName, mixerGroup, volume, timing, loop, appearTime);
        }
        else source.clip = clip;

        source.outputAudioMixerGroup = GetAudioMixerGroup(mixerGroup);
        source.loop = loop;

        StartCoroutine(Appear_ext(source, volume, timing, appearTime));
    }

    private IEnumerator Appear_ext(AudioSource audio, float volume, int timing, float appearTime)
    {
        float timeElapsed = 0;

        if (timing >= 0)
            audio.time = timing;

        audio.Play();

        while (timeElapsed < appearTime)
        {
            audio.volume = Mathf.Lerp(0, volume, timeElapsed / appearTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }




    public void Fade(float fadeTime = 1f)
    {
        StartCoroutine(Fade_ext(source, fadeTime));
    }

    private IEnumerator Fade_ext(AudioSource audio, float fadeTime)
    {
        float timeElapsed = 0;

        float a = audio.volume;

        while (timeElapsed < fadeTime)
        {
            audio.volume = Mathf.Lerp(a, 0, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        audio.Pause();
    }


    public void Stop()
    {
        source.Pause();
    }

}


public class TempAudioImp : AudioImp
{
    public TempAudioImp(AudioMaster master, AudioSource source, List<AudioClip> clips) : base(master, source, clips) { }

    public void Update()
    {
        if (!source.isPlaying) Destroy(gameObject);
    }
}