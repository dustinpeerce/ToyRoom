using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Toy : MonoBehaviour
    {
		// Protected Attributes
        protected string canSeeToyKey;		// The Dictionary Key for accessing the "canSee" Animation Parameter
        protected Dictionary<string, bool> animatorParamDictionary;		// Dictionary for storing all Animation Parameters


		/// <summary>
		/// Gets the animator parameters.
		/// </summary>
		/// <returns>The animator parameters.</returns>
		/// <param name="canSee">If set to <c>true</c> can see.</param>
        public Dictionary<string, bool> GetAnimatorParams(bool canSee)
        {
            animatorParamDictionary[canSeeToyKey] = canSee;
            return animatorParamDictionary;
        }


		/// <summary>
		/// Gets a value for canSeeToyKey.
		/// </summary>
		/// <value>A string that corresponds to the Key Animation Parameter for this Toy</value>
        public string CanSeeToyKey
        {
            get { return canSeeToyKey; }
        }

    } // end of class

} // end of namespace
