using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Person : MonoBehaviour
    {

        public Animator animator;

        public void UpdateViewParameters(Dictionary<string, bool> animParams, string canSeeToyKey)
        {
            if (animParams[canSeeToyKey])
            {
                foreach (var animParam in animParams)
                {
                    animator.SetBool(animParam.Key, animParam.Value);
                }
                return;
            }
            else
            {
                foreach (var animParam in animParams)
                {
                    animator.SetBool(animParam.Key, false);
                }
                return;
            }
        }

    }

}
