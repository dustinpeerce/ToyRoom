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
            // Reset all animator parameters to FALSE
            foreach (var parameter in animator.parameters)
            {
                animator.SetBool(parameter.name, false);
            }

            // Find the most urgent trigger
            string maxTriggerKey = "";
            foreach(var trigger in triggers)
            {
                if (trigger.Value.Value && (maxTriggerKey == "" || trigger.Value.Rank > triggers[maxTriggerKey].Rank))
                {
                    maxTriggerKey = trigger.Key;
                }
            }

            // Set anim params based on most urgent trigger
            if (maxTriggerKey != "" && triggers[maxTriggerKey].Rank > 0)
            {
                animator.SetBool(maxTriggerKey, true);

                // Set compatible triggers to their respective values
                if (triggers[maxTriggerKey].TriggerCombos != null)
                {
                    foreach (var key in triggers[maxTriggerKey].TriggerCombos)
                    {
                        animator.SetBool(key, triggers[key].Value);
                    }
                }
            }
            else
            {
                animator.SetBool(GameVals.AnimParams.personDefault, true);
            }
        }

    }

}
