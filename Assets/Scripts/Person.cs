using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



// TODO: Modify the rank of person triggers for a better balance between 'wandering' and 'engaged'

// TODO: Modify the speed/acceleration of the NavMeshAgent

// TODO: Find a smart way to update the Person's responses while still enforcing a minimum action time

namespace ToyRoom
{

    public class Person : MonoBehaviour
    {
        public enum PersonState { Idle, Engaged, Wandering }

        // Public Attributes
        public Animator animator;
        public bool debugLog;

        private Dictionary<string, PersonTrigger> triggers;
        private Transform myTransform;
        private NavMeshAgent agent;
        private string urgentTriggerKey;
        private float lookSpeed = 4.0f;
        private float fovDistance = 3f;
        private float fovAngle = 60f;
        private float walkDistance = 8.0f;
        private float brakeDistance = 0.1f;
        private Vector3 destination;
        private PersonState currentState;

        private float minActionTime = 0.0f;
        private float startActionTime = 0.0f;


        public void ClearTriggers()
        {
            if (startActionTime == 0)
            {
                urgentTriggerKey = "";
                ResetAnimator();
            }
        }

        private void ResetAnimator()
        {
            // Reset all animator parameters to FALSE
            foreach (var parameter in animator.parameters)
                animator.SetBool(parameter.name, false);
        }

        public void ProcessTriggers()
        {
            if (urgentTriggerKey != "" && triggers[urgentTriggerKey].Rank > 0)
            {

                float chanceToRespond = (triggers[urgentTriggerKey].Rank > 20) ? 1 : triggers[urgentTriggerKey].Rank / 100.0f;
                float rand = Random.Range(0f, 1f);

                if (rand <= chanceToRespond)
                {
                    // Set anim params based on most urgent trigger
                    if (CanSee(triggers[urgentTriggerKey].Toy))
                    {
                        CurrentState = PersonState.Engaged;
                        animator.SetBool(urgentTriggerKey, true);

                        // Set compatible triggers to their respective values
                        if (triggers[urgentTriggerKey].TriggerCombos != null)
                            foreach (var key in triggers[urgentTriggerKey].TriggerCombos)
                                animator.SetBool(key, (triggers[key].CanSee || triggers[key].CanHear));

                        if (chanceToRespond < 1f)
                        {
                            minActionTime = 3.0f;
                            startActionTime = Time.time;
                        }
                    }
                    else
                    {
                        CurrentState = PersonState.Idle;

                        if (urgentTriggerKey != "" && !CanHear(triggers[urgentTriggerKey].Toy, triggers[urgentTriggerKey].Sound))
                        {
                            urgentTriggerKey = "";
                        }

                        if (chanceToRespond < 1f)
                        {
                            minActionTime = 3.0f;
                            startActionTime = Time.time;
                        }
                    }
                    return;
                }
            }

            if (startActionTime == 0)
            {
                //ResetAnimator();
                CurrentState = PersonState.Wandering;
            }
        }


        public void UpdateViewParameters(Dictionary<string, bool> animParams, Transform target)
        {
            foreach (var animParam in animParams)
            {
                triggers[animParam.Key].Toy = target;
                bool see = CanSee(target) ? animParam.Value : false;
                bool hear = CanHear(target, triggers[animParam.Key].Sound) ? animParam.Value : false;
                triggers[animParam.Key].CanSee = see;
                triggers[animParam.Key].CanHear = hear;

                if ((see || hear) && (urgentTriggerKey == "" || triggers[animParam.Key].Rank > triggers[urgentTriggerKey].Rank))
                {
                    urgentTriggerKey = animParam.Key;
                    startActionTime = 0;
                }
            }
        }


        /// Determines whether Person can see a given Toy
        private bool CanSee(Transform toy)
        {
            var direction = toy.position - transform.position;
            return (direction.magnitude < fovDistance) && (Vector3.Angle(direction, transform.forward) < fovAngle);
        }


        /// Determines whether Person can hear a given Toy
        private bool CanHear(Transform toy, float sound)
        {
            return (toy.position - transform.position).magnitude < sound;
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

            myTransform = transform;
            agent = GetComponent<NavMeshAgent>();

            CurrentState = PersonState.Idle;
            urgentTriggerKey = "";
        }


        private void FixedUpdate()
        {
            if (startActionTime > 0 && (Time.time - startActionTime > minActionTime))
            {
                startActionTime = 0;
                urgentTriggerKey = "";
            }
            if (urgentTriggerKey != "")
            {
                LookAtToy(triggers[urgentTriggerKey].Toy, lookSpeed, Time.deltaTime);
            }
            else if (agent.hasPath)
            {
                agent.SetDestination(destination);

                if (agent.remainingDistance <= brakeDistance && CurrentState == PersonState.Wandering)
                {
                    SetNewDestination();
                }
            }
        }


        private void SetNewDestination()
        {
            Vector3 dir = Random.insideUnitSphere * walkDistance;
            dir += myTransform.position;

            NavMeshHit hit;
            NavMesh.SamplePosition(dir, out hit, walkDistance, 1);

            destination = hit.position;
            agent.SetDestination(destination);
        }

        private PersonState CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;

                destination = Vector3.zero;
                agent.ResetPath();

                if (value == PersonState.Idle)
                {
                    animator.SetBool(GameVals.AnimParams.personDefault, true);
                }
                else if (value == PersonState.Wandering)
                {
                    SetNewDestination();
                }
            }
        }

    }
}
