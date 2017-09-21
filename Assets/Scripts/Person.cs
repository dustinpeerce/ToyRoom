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

        public void UpdateViewParameters(Dictionary<string, bool> animParams, bool canSee, bool canHear)
        {
            foreach (var animParam in animParams)
            {
                triggers[animParam.Key].Value = (canSee || canHear) ? animParam.Value : false;
            }
        }

        public void ProcessTriggers()
        {
            foreach (var parameter in animator.parameters)
            {
                animator.SetBool(parameter.name, false);
            }

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
                animator.SetBool(largest, true);

                // TODO: implement triggerCombos, so triggers can be combined
            }
            else
            {
                animator.SetBool("Default", true);
            }
        }

    }

}
