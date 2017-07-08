using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Person : MonoBehaviour
    {
		// Public Attributes
        public Animator animator;		// Animator Component for the Person


		/// <summary>
		/// Updates the view parameters.
		/// </summary>
		/// <param name="animParams">Animation parameters.</param>
		/// <param name="canSeeToyKey">Can see toy key.</param>
        public void UpdateViewParameters(Dictionary<string, bool> animParams, string canSeeToyKey)
        {
            if (animParams[canSeeToyKey]) // The Person can see the toy...
            {
                foreach (var animParam in animParams)
                {
                    animator.SetBool(animParam.Key, animParam.Value);
                }
                return;
            }
            else // The Person can NOT see the toy...
            {
                foreach (var animParam in animParams)
                {
                    animator.SetBool(animParam.Key, false);
                }
                return;
            }
        }

    } // end of class

} // end of namespace
