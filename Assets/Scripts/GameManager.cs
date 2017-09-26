using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class GameManager : MonoBehaviour
    {

		// Private Attributes
        private GameObject[] personPrefabs;				// Stores all Person prefabs in the scene
        private GameObject[] toyPrefabs;				// Stores all Toy prefabs in the scene
        private List<Person> personScripts;				// List of Person components on each Person prefab
        private List<Toy> toyScripts;					// List of Toy components on each Toy prefab
        private Dictionary<string, bool> animParams;	// Stores animator params for the Toy instances


		/// Caches the person objects.
        private void CachePersonObjects()
        {
            personPrefabs = GameObject.FindGameObjectsWithTag("Person");
            personScripts = new List<Person>();

            for (int i = 0; i < personPrefabs.Length; i++)
            {
                personScripts.Add(personPrefabs[i].GetComponent<Person>());
            }
        }


		/// Caches the toy objects.
        private void CacheToyObjects()
        {
            toyPrefabs = GameObject.FindGameObjectsWithTag("Toy");
            toyScripts = new List<Toy>();

            for (int i = 0; i < toyPrefabs.Length; i++)
            {
                toyScripts.Add(toyPrefabs[i].GetComponent<Toy>());
            }
        }


		/// Tracks the view of toys.
        private IEnumerator TrackViewOfToys()
        {
            while (true)
            {
                for (int p = 0; p < personPrefabs.Length; p++)
                {
                    personScripts[p].ClearTriggers();
                    for (int t = 0; t < toyPrefabs.Length; t++)
                        personScripts[p].UpdateViewParameters(toyScripts[t].GetAnimParams(), toyPrefabs[t].transform);
                    personScripts[p].ProcessTriggers();
                }
                yield return new WaitForSeconds(0.35f);
            }
        }


        /// Awake this instance.
        private void Awake()
        {
            CachePersonObjects();
            CacheToyObjects();
        }


        /// Start this instance.
        private void Start()
        {
            StartCoroutine(TrackViewOfToys());
        }

    } // end of class

} // end of namespace
