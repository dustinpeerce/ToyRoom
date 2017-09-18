using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class GameManager : MonoBehaviour
    {

		// Private Attributes
		private float fovDistance;						// Field of View Distance for People
		private float fovAngle;							// Field of View Angle for People
        private float hearingDistance;
        private GameObject[] personPrefabs;				// Stores all Person prefabs in the scene
        private GameObject[] toyPrefabs;				// Stores all Toy prefabs in the scene
        private List<Person> personScripts;				// List of Person components on each Person prefab
        private List<Toy> toyScripts;					// List of Toy components on each Toy prefab
        private Vector3 direction;						// The direction vector between a Person and Toy
        private bool canSee;							// Tracks whether a Person can see a Toy
        private bool canHear;
        private Dictionary<string, bool> animParams;	// Stores animator params for the Toy instances


       	/// <summary>
       	/// Awake this instance.
       	/// </summary>
        private void Awake()
        {
			// Initialize Private Attributes
			fovDistance = 3f;
			fovAngle = 60f;
            hearingDistance = 2f;

            CachePersonObjects();
            CacheToyObjects();
        }


		/// <summary>
		/// Caches the person objects.
		/// </summary>
        private void CachePersonObjects()
        {
            personPrefabs = GameObject.FindGameObjectsWithTag("Person");
            personScripts = new List<Person>();

            for (int i = 0; i < personPrefabs.Length; i++)
            {
                personScripts.Add(personPrefabs[i].GetComponent<Person>());
            }
        }


		/// <summary>
		/// Caches the toy objects.
		/// </summary>
        private void CacheToyObjects()
        {
            toyPrefabs = GameObject.FindGameObjectsWithTag("Toy");
            toyScripts = new List<Toy>();

            for (int i = 0; i < toyPrefabs.Length; i++)
            {
                toyScripts.Add(toyPrefabs[i].GetComponent<Toy>());
            }
        }


		/// <summary>
		/// Start this instance.
		/// </summary>
        private void Start()
        {
            StartCoroutine(TrackViewOfToys());
        }


		/// <summary>
		/// Tracks the view of toys.
		/// </summary>
		/// <returns>The view of toys.</returns>
        private IEnumerator TrackViewOfToys()
        {
            while (true)
            {
                    for (int p = 0; p < personPrefabs.Length; p++)
                    {
                        for (int t = 0; t < toyPrefabs.Length; t++)
                        {
                            // does the player see the toy?
                            canSee = CanSeeToy(personPrefabs[p].transform, toyPrefabs[t].transform);

                            // does the player hear the toy?
                            canHear = CanHearToy(personPrefabs[p].transform, toyPrefabs[t].transform);

                            // store the animator key-value pairs for this toy
                            animParams = toyScripts[t].GetAnimParams();

                            // Update the animator key-value pairs for the Person
                            personScripts[p].UpdateViewParameters(animParams, canSee, canHear);
                        }
                        personScripts[p].ProcessTriggers();
                    }

                yield return new WaitForSeconds(0.35f);
            }
        }


		/// <summary>
		/// Determines whether a given Person can see a given Toy
		/// </summary>
		/// <returns><c>true</c> if the Person can see the toy; otherwise, <c>false</c>.</returns>
		/// <param name="person">Person.</param>
		/// <param name="toy">Toy.</param>
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

        private bool CanHearToy(Transform person, Transform toy)
        {
            direction = toy.position - person.position;

            // Detect if toy is within hearing distance
            if (direction.magnitude < hearingDistance)
            {
                return true;
            }

            return false;
        }

    } // end of class

} // end of namespace
