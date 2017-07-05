using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Land : MonoBehaviour
    {

		// Public Attributes
        public Material gazeMat;		// Material for gaze on land
        public Material activeMat;		// Material for click on land

		// Private Attributes
		private MeshRenderer meshRenderer;		// Mesh Renderer component for game object
		private Material defaultMat;			// Initial material on the Mesh Renderer
		private bool isGazedAt;					// Tracks whether the land is gazed or not
		private bool isActive;					// Tracks whether the land is active or not


		/// <summary>
		/// Awake this instance.
		/// </summary>
        private void Awake()
        {
			// Initialize Private Attributes
            meshRenderer = GetComponent<MeshRenderer>();
            defaultMat = meshRenderer.material;
			isGazedAt = false;
			isActive = false;
        }


		/// <summary>
		/// Sets isGazedAt.
		/// </summary>
		/// <param name="gazedAt">If set to <c>true</c> this land is gazed at.</param>
        public void SetGazedAt(bool gazedAt)
        {
			isGazedAt = gazedAt;
			meshRenderer.material = isGazedAt ? gazeMat : isActive ? activeMat : defaultMat;
        }


		/// <summary>
		/// Sets isActive.
		/// </summary>
		/// <param name="active">If set to <c>true</c> this land is active.</param>
		public void SetActive(bool active) {
			isActive = active;
			meshRenderer.material = isActive ? activeMat : isGazedAt ? gazeMat : defaultMat;
		}

    } // end of class

} // end of namespace
