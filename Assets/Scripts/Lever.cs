using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class Lever : MonoBehaviour
    {
		// Public Attributes
        public enum TrackIndex { TrackOne, TrackTwo }		// Possible states for the Road
		public enum LeverState { Left, Right, Changing }	// Possible states for the Lever
		public Car carInstance;					// Car instance in the scene
        public GameObject trackOne;				// Road Mesh for Track One
        public GameObject trackTwo;				// Road Mesh for Track Two
        public Material darkRoadMat;			// Material for inactive Road
        public Material lightRoadMat;			// Material for active Road
        public Material darkLeverMat;			// Material for inactive Lever
        public Material lightLeverMat;			// Material for active Lever
        public List<MeshRenderer> leverObjects;	// List of Lever Mesh Renderers

		// Private Attributes
        private Animator animator;				// Animator component for the Lever
        private bool isUnlocked;				// Tracks whether the Lever has been unlocked or not
        private MeshRenderer trackOneRenderer;	// Mesh Renderer component for Track One
        private MeshRenderer trackTwoRenderer;	// Mesh Renderer component for Track Two
        private LeverState currentState;		// Tracks the current state of the Lever
        private TrackIndex currentTrackIndex;	// Tracks the current state of the Road


		/// <summary>
		/// Awake this instance.
		/// </summary>
        private void Awake()
        {
            isUnlocked = false;
            currentState = LeverState.Left;
			currentTrackIndex = TrackIndex.TrackOne;
            animator = GetComponent<Animator>();
            trackOneRenderer = trackOne.GetComponent<MeshRenderer>();
            trackTwoRenderer = trackTwo.GetComponent<MeshRenderer>();
        }


		/// <summary>
		/// Start this instance.
		/// </summary>
        private void Start()
        {
            UpdateTrack();
        }


		/// <summary>
		/// Sets gazedAt.
		/// </summary>
		/// <param name="gazedAt">If set to <c>true</c> gazed at.</param>
        public void SetGazedAt(bool gazedAt)
        {
            
        }


		/// <summary>
		/// Pulls the lever.
		/// </summary>
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


		/// <summary>
		/// Pulls the lever left.
		/// </summary>
        private void PullLeft()
        {
            animator.SetTrigger("PullLeft");
            AudioManager.Instance.PlayAudio(AudioManager.Instance.leverPullLeft);

            currentState = LeverState.Changing;
        }


		/// <summary>
		/// Pulls the lever right.
		/// </summary>
        private void PullRight()
        {
            animator.SetTrigger("PullRight");
            AudioManager.Instance.PlayAudio(AudioManager.Instance.leverPullRight);

            currentState = LeverState.Changing;
        }


		/// <summary>
		/// Fails to pull the lever
		/// </summary>
        private void PullFail()
        {
            animator.SetTrigger("PullRight");
            AudioManager.Instance.PlayAudio(AudioManager.Instance.leverFail);

            currentState = LeverState.Changing;
        }


		/// <summary>
		/// Executed when the Lever is done changing (called as an Animation Event)
		/// </summary>
		/// <param name="isLeftInteger">Is left integer.</param>
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


		/// <summary>
		/// Executed when the Pull Failure is complete (called as an Animation Event)
		/// </summary>
        public void PullFailComplete()
        {
            currentState = LeverState.Left;
        }


		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>
		/// <param name="other">Other.</param>
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


		/// <summary>
		/// Updates the track.
		/// </summary>
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


		/// <summary>
		/// Changes the active Track.
		/// </summary>
        private void ChangeCurrentTrackIndex()
        {
            if (currentTrackIndex == TrackIndex.TrackOne)
                currentTrackIndex = TrackIndex.TrackTwo;
            else
                currentTrackIndex = TrackIndex.TrackOne;

            UpdateTrack();
        }
    
	} // end of class

} // end of namespace
