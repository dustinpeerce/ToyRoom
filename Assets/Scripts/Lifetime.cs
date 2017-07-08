using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class Lifetime : MonoBehaviour
    {
		// Public Attributes
        public float lifespan = 5.0f;	// Time in seconds for the instance to exist

		// Private Attributes
        private float lifeCounter;		// Tracks how long the instance has been alive


		/// <summary>
		/// Start this instance.
		/// </summary>
        private void Start()
        {
            lifeCounter = lifespan;
        }


		/// <summary>
		/// Update this instance.
		/// </summary>
        private void Update()
        {
            lifeCounter -= Time.deltaTime;

            if (lifeCounter <= 0)
            {
                Destroy(gameObject);
            }
        }

    } // end of class

} // end of namespace
