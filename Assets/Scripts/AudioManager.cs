using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class AudioManager : MonoBehaviour
    {

        public static AudioManager Instance;

        public AudioSource sfxAudioSource;
        public AudioSource bgmAudioSource01;
        public AudioSource bgmAudioSource02;
        public AudioSource bgmAudioSource03;
        public AudioSource bgmAudioSource04;

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

        public void PlayBackground(int audioSourceIndex)
        {
            StopBackground();

            switch (audioSourceIndex)
            {
                case 1:
                    bgmAudioSource01.Play();
                    break;
                case 2:
                    bgmAudioSource02.Play();
                    break;
                case 3:
                    bgmAudioSource03.Play();
                    break;
                case 4:
                    bgmAudioSource04.Play();
                    break;
                default:
                    bgmAudioSource01.Play();
                    break;
            }
        }

        public void StopBackground()
        {
            bgmAudioSource01.Stop();
            bgmAudioSource02.Stop();
            bgmAudioSource03.Stop();
            bgmAudioSource04.Stop();
        }
    }
}
