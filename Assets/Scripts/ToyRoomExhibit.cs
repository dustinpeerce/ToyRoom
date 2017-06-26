using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyRoom
{

    public class ToyRoomExhibit : MonoBehaviour
    {

        public Animator animator;

        public void SetGazedAt(bool gazedAt)
        {
            if (gazedAt)
            {
                animator.SetBool(GameVals.AnimatorParameterKeys.canSeeCar, true);
                animator.SetBool(GameVals.AnimatorParameterKeys.carIsDriving, true);
            }
            else
            {
                animator.SetBool(GameVals.AnimatorParameterKeys.canSeeCar, false);
                animator.SetBool(GameVals.AnimatorParameterKeys.carIsDriving, false);
            }
        }

        public void LoadToyRoom()
        {
            SceneManager.LoadScene("ToyRoom");
        }
    }
}
