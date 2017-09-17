using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Person : MonoBehaviour
    {
        // Public Attributes
        public Animator animator;

        private Dictionary<string, PersonTrigger> triggers;


        private void Awake()
        {
            triggers = new Dictionary<string, PersonTrigger>();
            for(int i=0; i < GameVals.personTriggers.Length; i++)
            {
                triggers[GameVals.personTriggers[i].Name] = GameVals.personTriggers[i];
            }
        }

        
        public void old_UpdateViewParameters(Dictionary<string, bool> animParams, string canSeeToyKey)
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

        public void UpdateViewParameters(Dictionary<string, bool> animParams, string canSeeToyKey)
        {
            foreach (var animParam in animParams)
            {
                triggers[animParam.Key].Value = animParam.Value;
                //triggers[animParam.Key].CanSee = animParams[canSeeToyKey];
            }
        }

        public void ProcessTriggers()
        {
            string largest = "";
            foreach(var trigger in triggers)
            {
                if (trigger.Value.Value && (largest == "" || trigger.Value.Rank > triggers[largest].Rank))
                {
                    largest = trigger.Key;
                }
            }

            if (largest != "" && triggers[largest].Rank > 0)
            {
                animator.SetBool(largest, triggers[largest].Value);
            }
        }

    }

}
