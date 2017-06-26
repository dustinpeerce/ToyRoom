using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class GameManager : MonoBehaviour
    {

        public float fovDistance = 2f;
        public float fovAngle = 60f;

        private GameObject[] personPrefabs;
        private GameObject[] toyPrefabs;

        private List<Person> personScripts;
        private List<Toy> toyScripts;

        private bool gameIsRunning = false;

        private Vector3 direction;
        private bool canSee;
        private Dictionary<string, bool> animParams;


        // Grab all necessary objects
        private void Awake()
        {
            CachePersonObjects();
            CacheToyObjects();
        }

        private void CachePersonObjects()
        {
            personPrefabs = GameObject.FindGameObjectsWithTag("Person");
            personScripts = new List<Person>();

            for (int i = 0; i < personPrefabs.Length; i++)
            {
                personScripts.Add(personPrefabs[i].GetComponent<Person>());
            }
        }

        private void CacheToyObjects()
        {
            toyPrefabs = GameObject.FindGameObjectsWithTag("Toy");
            toyScripts = new List<Toy>();

            for (int i = 0; i < toyPrefabs.Length; i++)
            {
                toyScripts.Add(toyPrefabs[i].GetComponent<Toy>());
            }
        }

        private void Start()
        {
            gameIsRunning = true;
            StartCoroutine(TrackViewOfToys());
        }


        private IEnumerator TrackViewOfToys()
        {
            while (true)
            {
                if (gameIsRunning)
                {
                    for (int p = 0; p < personPrefabs.Length; p++)
                    {
                        for (int t = 0; t < toyPrefabs.Length; t++)
                        {
                            // does the player see the toy?
                            canSee = CanSeeToy(personPrefabs[p].transform, toyPrefabs[t].transform);

                            // store the animator key-value pairs for this toy
                            animParams = toyScripts[t].GetAnimatorParams(canSee);

                            // Update the animator key-value pairs for the Person
                            personScripts[p].UpdateViewParameters(animParams, toyScripts[t].CanSeeToyKey);
                        }
                    }

                }

                yield return new WaitForSeconds(0.35f);
            }
        }


        private bool CanSeeToy(Transform person, Transform toy)
        {
            direction = toy.position - person.position;

            // Detect if toy is within view distance
            if (direction.magnitude < fovDistance)
            {
                // Detect if toy is within the field of view
                if ((Vector3.Angle(direction, person.forward)) < fovAngle)
                {
                    return true;
                }
            }

            return false;
        }
    }

}
