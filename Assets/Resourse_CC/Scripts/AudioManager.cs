using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioClip bonusSound;

    void Awake()
    {
        instance = this;
    }

    public void PlaySoundBonus(Vector3 position)
    {
        GameObject obj = new GameObject("bonusSound");
        AudioSource audio = obj.AddComponent<AudioSource>();
        audio.clip = bonusSound;
        audio.Play();
        Destroy(obj, bonusSound.length);
    }
}
