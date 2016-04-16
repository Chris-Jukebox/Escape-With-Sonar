using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{

    public static AudioManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }
    

    #region SFX Method
    public AudioSource Play(AudioClip clip)
    {
        return Play(clip, this.gameObject, 1, clip.length, false);
    }
    public AudioSource Play(AudioClip clip, GameObject emitter, bool space)
    {
        return Play(clip, emitter, 1, clip.length, space);
    }

    public AudioSource Play(AudioClip clip, GameObject emitter, float volume, float length, bool space)
    {
        //Create an empty game object
        GameObject go = new GameObject("Audio: " + clip.name);
        go.transform.parent = emitter.transform;

        //Create source
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.Play();

        // For 3D sound
        if (space)
        {
            source.spatialBlend = 1;
            source.rolloffMode = AudioRolloffMode.Custom;
            source.maxDistance = 800;
        }

        Destroy(go, length);
        return source;
    }
    #endregion



    #region Volume Fade Method
    private void VolumeFadeOn(AudioSource audioSource)
    {
        StartCoroutine(VolumeFade(audioSource, 1f));
    }

    private void VolumeFadeOff(AudioSource audioSource)
    {
        StartCoroutine(VolumeFade(audioSource, 0f));
    }

    private void VolumeFadeTo(AudioSource audioSource, float volume)
    {
        StartCoroutine(VolumeFade(audioSource, volume));
    }

    private IEnumerator VolumeFade(AudioSource audioSource, float targetVolume)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        bool isIncrease = audioSource.volume < targetVolume;
        while (isIncrease == (audioSource.volume < targetVolume))
        {
            audioSource.volume += (isIncrease ? 0.01f : -0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        if (targetVolume == 0)
        {
            audioSource.Stop();
        }
    }
    #endregion
}