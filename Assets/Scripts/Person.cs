using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: If a person hears a toy, they should turn towards the toy but NOT start animating

// TODO: If a person sees a toy, they start animating

// TODO: When not responding to events, people should wander around the table according to a NavMesh

namespace ToyRoom
{

    public class Person : MonoBehaviour
    {
        // Public Attributes
        public Animator animator;

        private Dictionary<string, PersonTrigger> triggers;
        private string urgentTriggerKey;
        private Transform currentTarget;
        private float lookSpeed = 4.0f;
        private float fovDistance = 3f;
        private float fovAngle = 60f;
        private float hearingDistance = 2f;


        public void ClearTriggers()
        {
            urgentTriggerKey = "";
            currentTarget = null;

            // Reset all animator parameters to FALSE
            foreach (var parameter in animator.parameters)
                animator.SetBool(parameter.name, false);
        }


        public void ProcessTriggers()
        {
            // Set anim params based on most urgent trigger
            if (urgentTriggerKey != "" && triggers[urgentTriggerKey].Rank > 0 && CanSee(currentTarget))
            {
                animator.SetBool(urgentTriggerKey, true);

                // Set compatible triggers to their respective values
                if (triggers[urgentTriggerKey].TriggerCombos != null)
                    foreach (var key in triggers[urgentTriggerKey].TriggerCombos)
                        animator.SetBool(key, triggers[key].Value);
            }
            else
            {
                animator.SetBool(GameVals.AnimParams.personDefault, true);

                if (currentTarget && !CanHear(currentTarget))
                    currentTarget = null;
            }
        }


        public void UpdateViewParameters(Dictionary<string, bool> animParams, Transform target)
        {
            ClearTriggers();
            foreach (var animParam in animParams)
            {
                triggers[animParam.Key].Value = (CanSee(target) || CanHear(target)) ? animParam.Value : false;

                if (triggers[animParam.Key].Value && (urgentTriggerKey == "" || triggers[animParam.Key].Rank > triggers[urgentTriggerKey].Rank))
                {
                    urgentTriggerKey = animParam.Key;
                    currentTarget = target;
                }
            }
            ProcessTriggers();
        }


        /// Determines whether Person can see a given Toy
        private bool CanSee(Transform toy)
        {
            var direction = toy.position - transform.position;
            return (direction.magnitude < fovDistance) && (Vector3.Angle(direction, transform.forward) < fovAngle);
        }


        /// Determines whether Person can hear a given Toy
        private bool CanHear(Transform toy)
        {
            return (toy.position - transform.position).magnitude < hearingDistance;
        }


        private void LookAtToy(Transform toy, float speed, float time)
        {
            var bodyRotation = toy.position - transform.position;
            bodyRotation.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(bodyRotation), time * speed);
        }


        private void Awake()
        {
            triggers = new Dictionary<string, PersonTrigger>();
            for (int i = 0; i < GameVals.personTriggers.Length; i++)
                triggers[GameVals.personTriggers[i].Name] = GameVals.personTriggers[i];
        }

        private void FixedUpdate()
        {
            if (currentTarget)
                LookAtToy(currentTarget, lookSpeed, Time.deltaTime);
        }

    }
}
