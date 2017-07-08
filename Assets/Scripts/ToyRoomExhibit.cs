using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyRoom
{

    public class ToyRoomExhibit : MonoBehaviour
    {
		// Public Attributes
        public Animator animator;	// Animator Component for the object


		/// <summary>
		/// Sets gazedAt.
		/// </summary>
		/// <param name="gazedAt">If set to <c>true</c> gazed at.</param>
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


		/// <summary>
		/// Loads the toy room scene.
		/// </summary>
        public void LoadToyRoom()
        {
            SceneManager.LoadScene("ToyRoom");
        }

    } // end of class

} // end of namespace
