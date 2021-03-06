﻿using System.Collections;
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
                animator.SetBool(GameVals.AnimParams.carIsDriving, true);
            }
            else
            {
                animator.SetBool(GameVals.AnimParams.carIsDriving, false);
            }
        }


		/// <summary>
		/// Loads the toy room scene.
		/// </summary>
        public void LoadToyRoom()
        {
            SceneManager.LoadScene("ToyRoom");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

    } // end of class

} // end of namespace
