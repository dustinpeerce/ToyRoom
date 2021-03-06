﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class AudioManager : MonoBehaviour
    {

		// Static Instance
        public static AudioManager Instance;

		// Public Audio Source Components
        public AudioSource sfxAudioSource;
        public AudioSource bgmAudioSource01;
        public AudioSource bgmAudioSource02;
        public AudioSource bgmAudioSource03;
        public AudioSource bgmAudioSource04;

        // Public Audio Clip files
        public AudioClip dartFire;
        public AudioClip dartHitCar;
        public AudioClip dartHitPerson;
        public AudioClip dartHitGlobe;
        public AudioClip houseShake;
        public AudioClip houseOpen;
        public AudioClip leverFail;
        public AudioClip leverPullRight;
        public AudioClip leverPullLeft;
        public AudioClip leverUnlock;
        public AudioClip lightsOff;
        public AudioClip lightsOn;


		/// <summary>
		/// Awake this instance.
		/// </summary>
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


		/// <summary>
		/// Plays a given Audio Clip
		/// </summary>
		/// <param name="clip">Clip to play.</param>
        public void PlayAudio(AudioClip clip)
        {
            sfxAudioSource.PlayOneShot(clip, sfxAudioSource.volume);
        }


		/// <summary>
		/// Plays a Background Audio Source file.
		/// </summary>
		/// <param name="audioSourceIndex">Which Audio Source to Play.</param>
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


		/// <summary>
		/// Stops all background music.
		/// </summary>
        public void StopBackground()
        {
            bgmAudioSource01.Stop();
            bgmAudioSource02.Stop();
            bgmAudioSource03.Stop();
            bgmAudioSource04.Stop();
        }

    } // end of class

} // end of namespace
