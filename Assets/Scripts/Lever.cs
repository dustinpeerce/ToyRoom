using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class Lever : MonoBehaviour
    {

        public Car carInstance;
        public TrackTracer trackTracer;

        private Animator animator;
        private bool isUnlocked;
        private bool isPulledLeft;

        private void Awake()
        {
            isUnlocked = false;
            isPulledLeft = true;
            animator = GetComponent<Animator>();
        }

        public void SetGazedAt(bool gazedAt)
        {
            
        }

        public void PullLever()
        {
            if (isPulledLeft)
            {
                animator.SetTrigger("PullRight");
            }
            else
            {
                animator.SetTrigger("PullLeft");
            }

            if (isUnlocked)
            {
                if (isPulledLeft) AudioManager.Instance.PlayAudio(AudioManager.Instance.leverPullRight);
                else AudioManager.Instance.PlayAudio(AudioManager.Instance.leverPullLeft);

                isPulledLeft = !isPulledLeft;
                carInstance.ToggleTrack();
                trackTracer.ChangeCurrentTrackIndex();
            }
            else
            {
                AudioManager.Instance.PlayAudio(AudioManager.Instance.leverFail);
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
    }

}
