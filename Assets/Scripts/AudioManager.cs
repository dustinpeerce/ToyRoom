using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    public AudioSource sfxAudioSource;

    // Audio Clips
    public AudioClip dartFire;
    public AudioClip dartHitCar;
    public AudioClip dartHitPerson;
    public AudioClip dartHitGlobe;
    public AudioClip houseOpen;
    public AudioClip leverFail;
    public AudioClip leverPullRight;
    public AudioClip leverPullLeft;
    public AudioClip leverUnlock;
    public AudioClip lightsOff;
    public AudioClip lightsOn;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudio(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip, sfxAudioSource.volume);
    }
}
