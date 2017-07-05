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
        public Material darkLeverMat;
        public Material lightLeverMat;
        public List<MeshRenderer> leverObjects;

        private Animator animator;
        private bool isUnlocked;
        private MeshRenderer trackOneRenderer;
        private MeshRenderer trackTwoRenderer;

        public enum LeverState { Left, Right, Changing };
        private LeverState currentState;

        private TrackIndex currentTrackIndex;

        private void Awake()
        {
            isUnlocked = false;
            currentState = LeverState.Left;
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
            if (currentState != LeverState.Changing)
            {
                if (isUnlocked)
                {
                    if (currentState == LeverState.Left)
                    {
                        PullRight();
                    }
                    else
                    {
                        PullLeft();
                    }   
                }
                else
                {
                    PullFail();
                }
            }
        }

        private void PullLeft()
        {
            animator.SetTrigger("PullLeft");
            AudioManager.Instance.PlayAudio(AudioManager.Instance.leverPullLeft);

            currentState = LeverState.Changing;
        }

        private void PullRight()
        {
            animator.SetTrigger("PullRight");
            AudioManager.Instance.PlayAudio(AudioManager.Instance.leverPullRight);

            currentState = LeverState.Changing;
        }

        private void PullFail()
        {
            animator.SetTrigger("PullRight");
            AudioManager.Instance.PlayAudio(AudioManager.Instance.leverFail);

            currentState = LeverState.Changing;
        }

        public void PullStateComplete(int isLeftInteger)
        {
            if (isLeftInteger == 0)
            {
                currentState = LeverState.Right;
            }
            else
            {
                currentState = LeverState.Left;
            }

            carInstance.ToggleTrack();
            ChangeCurrentTrackIndex();
        }

        public void PullFailComplete()
        {
            currentState = LeverState.Left;
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

                    foreach(MeshRenderer obj in leverObjects)
                    {
                        obj.material = lightLeverMat;
                    }
                }
            }
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
