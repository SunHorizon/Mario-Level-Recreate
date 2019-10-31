using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    static SoundManager _Instance = null;
    public AudioSource sfxSource;
    public AudioSource sfxMusic;

    // Use this for initialization
    void Start () {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, float Volume = 1.0f)
    {
        if (sfxSource)
        {
            sfxSource.clip = clip;
            sfxSource.volume = Volume;
            sfxSource.Play();
        }
    }

    public void PlayMusic(AudioClip clip, float Volume = 1.0f)
    {

        sfxMusic.clip = clip;
        sfxMusic.volume = Volume;
        sfxMusic.Play();
    }

    //public void StopMusic()
    //{
    //    if (sfxMusic)
    //    {
    //        sfxMusic.Stop();

    //    }
    //}
    public static SoundManager instance
    {
        set { _Instance = value; }
        get { return _Instance; }
    }
}
