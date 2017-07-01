using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class Land : MonoBehaviour
    {

        public Material gazeMat;
        public Material activeMat;

        private MeshRenderer meshRenderer;
        private Material defaultMat;
		private bool isGazedAt;
		private bool isActive;


        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            defaultMat = meshRenderer.material;
			isGazedAt = false;
			isActive = false;
        }


        public void SetGazedAt(bool gazedAt)
        {
			isGazedAt = gazedAt;
			meshRenderer.material = isGazedAt ? gazeMat : isActive ? activeMat : defaultMat;
        }

		public void SetActive(bool active) {
			isActive = active;
			meshRenderer.material = isActive ? activeMat : isGazedAt ? gazeMat : defaultMat;
		}

    }
}
