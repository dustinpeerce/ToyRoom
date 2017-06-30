using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class Lever : MonoBehaviour
    {
        public enum TrackIndex { TrackOne, TrackTwo };

        public Car carInstance;
        public GameObject trackOne;
        public GameObject trackTwo;
        public Material darkRoadMat;
        public Material lightRoadMat;

        private Animator animator;
        private bool isUnlocked;
        private bool isPulledLeft;
        private bool isBeingPulled;
        private MeshRenderer trackOneRenderer;
        private MeshRenderer trackTwoRenderer;

        private TrackIndex currentTrackIndex;

        private void Awake()
        {
            isUnlocked = false;
            isPulledLeft = true;
            isBeingPulled = false;
            animator = GetComponent<Animator>();

            trackOneRenderer = trackOne.GetComponent<MeshRenderer>();
            trackTwoRenderer = trackTwo.GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            currentTrackIndex = TrackIndex.TrackOne;
            UpdateTrack();
        }

        public void SetGazedAt(bool gazedAt)
        {
            
        }

        public void PullLever()
        {
            if (!isBeingPulled)
            {
                if (isUnlocked)
                {

                    if (isPulledLeft)
                    {
                        animator.SetTrigger("PullRight");
                        AudioManager.Instance.PlayAudio(AudioManager.Instance.leverPullRight);
                    }
                    else
                    {
                        animator.SetTrigger("PullLeft");
                        AudioManager.Instance.PlayAudio(AudioManager.Instance.leverPullLeft);
                    }

                    isPulledLeft = !isPulledLeft;
                    carInstance.ToggleTrack();
                    ChangeCurrentTrackIndex();
                }
                else
                {
                    animator.SetTrigger("PullRight");
                    AudioManager.Instance.PlayAudio(AudioManager.Instance.leverFail);
                }

                isBeingPulled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Dart"))
            {
                if (!isUnlocked)
                {
                    AudioManager.Instance.PlayAudio(AudioManager.Instance.leverUnlock);
                    animator.SetTrigger("Unlock");
                    animator.SetBool("IsUnlocked", true);
                    isUnlocked = true;
                }
            }
        }

        public void PullStateComplete()
        {
            isBeingPulled = false;
        }

        private void UpdateTrack()
        {
            if (currentTrackIndex == TrackIndex.TrackOne)
            {
                trackOneRenderer.material = lightRoadMat;
                trackTwoRenderer.material = darkRoadMat;
            }
            else
            {
                trackOneRenderer.material = darkRoadMat;
                trackTwoRenderer.material = lightRoadMat;
            }
        }

        private void ChangeCurrentTrackIndex()
        {
            if (currentTrackIndex == TrackIndex.TrackOne)
                currentTrackIndex = TrackIndex.TrackTwo;
            else
                currentTrackIndex = TrackIndex.TrackOne;

            UpdateTrack();
        }
    }

}
