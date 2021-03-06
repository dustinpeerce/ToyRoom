﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Globe : Toy
    {
		// Public Attributes
		public Material activeMat;		// Material for dance mode

		// Private Attributes
        private MeshRenderer meshRenderer;		// Mesh Renderer component for game object
        private Material defaultMat;			// Initial material on the Mesh Renderer
		private List<Land> lands;				// Land component of each child game object
        private bool isDanceMode;				// Tracks whether dance mode is active or not
        private int spinCount;					// How many times the globe was told to spin


		/// <summary>
		/// Awake this instance.
		/// </summary>
        private void Awake()
        {
			// Initialize Private Attributes
            meshRenderer = GetComponent<MeshRenderer>();
            defaultMat = meshRenderer.material;
			isDanceMode = false;
			lands = new List<Land> ();
			foreach (Transform child in transform) {
				lands.Add (child.GetComponent<Land> ());
			}

            // Initialize Animator Parameter Dictionary
            defaultToyKey = GameVals.AnimParams.globeDefault;
            animParamDictionary = new Dictionary<string, bool>();
            animParamDictionary.Add(defaultToyKey, false);
            animParamDictionary.Add(GameVals.AnimParams.globeIsSpinning, false);
            animParamDictionary.Add(GameVals.AnimParams.globeIsDancing, false);
        }
			

		/// <summary>
		/// Spins the globe.
		/// </summary>
        public void SpinGlobe()
        {
            StartCoroutine(SpinCoroutine(10));
        }


		/// <summary>
		/// Toggles the dance mode.
		/// </summary>
		/// <param name="landNumber">The Land number that was clicked.</param>
        public void ToggleDanceMode(int landNumber)
        {
            isDanceMode = !isDanceMode;

			if (isDanceMode) // dancing just started...
            {
                AudioManager.Instance.PlayBackground(landNumber);
				meshRenderer.material = activeMat;

				lands [landNumber - 1].SetActive (true);
            }
            else // dancing just stopped...
            {
                AudioManager.Instance.StopBackground();
                meshRenderer.material = defaultMat;

				foreach (Land land in lands) {
					land.SetActive (false);
				}
            }
				
            animParamDictionary[GameVals.AnimParams.globeIsDancing] = isDanceMode;
        }


		/// <summary>
		/// Coroutine for spinning the globe.
		/// </summary>
		/// <returns>The coroutine.</returns>
		/// <param name="rotationCount">Number of times to rotate.</param>
        private IEnumerator SpinCoroutine(int rotationCount)
        {
            SpinCount++;

            for (int i = 0; i < rotationCount; i++)
            {

                transform.Rotate(0, 0, -10f);

                yield return new WaitForSeconds(0.025f);
            }

            SpinCount--;
        }


		/// <summary>
		/// Raises the collision enter event.
		/// </summary>
		/// <param name="collision">Collision object.</param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Dart"))
            {
                AudioManager.Instance.PlayAudio(AudioManager.Instance.dartHitGlobe);
                SpinGlobe();
            }
        }


		/// <summary>
		/// Gets or sets the spin count.
		/// </summary>
		/// <value>The spin count.</value>
        public int SpinCount
        {
            get { return spinCount; }
            set
            {
                if (spinCount <= 0) // not spinning
                {
                    spinCount = value;

                    if (spinCount > 0) // we just started spinning...
                    {
                        animParamDictionary[GameVals.AnimParams.globeIsSpinning] = true;
                    }
                }
                else // currently spinning
                {
                    spinCount = value;

                    if (spinCount <= 0) // we just stopped spinning...
                    {
                        animParamDictionary[GameVals.AnimParams.globeIsSpinning] = false;
                    }
                }
            }
        }

    } // end of class

} // end of namespace
