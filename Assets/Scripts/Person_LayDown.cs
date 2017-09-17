using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class Person_LayDown : MonoBehaviour
	{
		// Public Attributes
        public Animator animator;	// Animator Component for the Person


		/// <summary>
		/// Awake this instance.
		/// </summary>
        private void Awake()
        {
            animator.SetBool(GameVals.AnimParams.layDown, true);
        }

    } // end of class

} // end of namespace
