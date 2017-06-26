using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class Lifetime : MonoBehaviour
    {

        public float lifespan = 5.0f;

        private float lifeCounter;

        private void Start()
        {
            lifeCounter = lifespan;
        }

        private void Update()
        {
            lifeCounter -= Time.deltaTime;

            if (lifeCounter <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
