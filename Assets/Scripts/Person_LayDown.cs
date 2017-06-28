using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class Person_LayDown : MonoBehaviour {

        public Animator animator;

        private void Awake()
        {
            animator.SetBool(GameVals.AnimatorParameterKeys.layDown, true);
        }
    }
}
