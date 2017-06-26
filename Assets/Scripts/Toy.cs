using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Toy : MonoBehaviour
    {

        protected string canSeeToyKey;
        protected Dictionary<string, bool> animatorParamDictionary;


        public Dictionary<string, bool> GetAnimatorParams(bool canSee)
        {
            animatorParamDictionary[canSeeToyKey] = canSee;
            return animatorParamDictionary;
        }

        public string CanSeeToyKey
        {
            get { return canSeeToyKey; }
        }

    }

}
