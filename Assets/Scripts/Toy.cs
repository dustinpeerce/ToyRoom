using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Toy : MonoBehaviour
    {
		// Protected Attributes
        protected Dictionary<string, bool> animParamDictionary;		// Dictionary for storing all Animation Parameters


		/// <summary>
		/// Gets the animator parameters.
		/// </summary>
		/// <returns>The animator parameters.</returns>
        public Dictionary<string, bool> GetAnimParams()
        {
            return animParamDictionary;
        }

    } // end of class

} // end of namespace
