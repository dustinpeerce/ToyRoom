using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Toy : MonoBehaviour
    {
		// Protected Attributes
        protected Dictionary<string, bool> animParamDictionary;
        protected string defaultToyKey;


        public Dictionary<string, bool> GetAnimParams()
        {
            if (!animParamDictionary.ContainsValue(true))
                animParamDictionary[defaultToyKey] = true;

            return animParamDictionary;
        }
    }

}
