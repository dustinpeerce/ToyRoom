using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Toy : MonoBehaviour
    {

        // TODO: add protected string for defaultToyKey
        // TODO: on each Toy script, set the defaultToyKey

		// Protected Attributes
        protected Dictionary<string, bool> animParamDictionary;		// Dictionary for storing all Animation Parameters


		/// <summary>
		/// Gets the animator parameters.
		/// </summary>
		/// <returns>The animator parameters.</returns>
        public Dictionary<string, bool> GetAnimParams()
        {
            // TODO: if animParams are all false, set animParams[defaultToyKey] = true
            return animParamDictionary;
        }

    } // end of class

} // end of namespace
