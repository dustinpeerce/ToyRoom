using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class LightSwitch : MonoBehaviour 
	{
		// Public Attributes
        public House houseInstance;		// Instance of the House
        public Material upMat;			// Material for unpressed Light Switch
        public Material downMat;		// Material for pressed Light Switch

		// Private Attributes
        private Vector3 downPos;		// Position for pressed Light Switch
        private Vector3 upPos;			// Position for unpressed Light Switch
        private MeshRenderer mesh;		// Mesh Renderer for the Light Switch


		/// <summary>
		/// Awake this instance.
		/// </summary>
        private void Awake()
        {
            upPos = transform.position;
            downPos = new Vector3(upPos.x, upPos.y - 0.04f, upPos.z);
            mesh = GetComponent<MeshRenderer>();
        }


		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>
		/// <param name="other">Other.</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Car")
            {
                AudioManager.Instance.PlayAudio(AudioManager.Instance.lightsOn);
                houseInstance.IsOn = true;
                transform.position = downPos;
                mesh.material = downMat;
            }
        }


		/// <summary>
		/// Raises the trigger exit event.
		/// </summary>
		/// <param name="other">Other.</param>
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == "Car")
            {
                AudioManager.Instance.PlayAudio(AudioManager.Instance.lightsOff);
                houseInstance.IsOn = false;
                transform.position = upPos;
                mesh.material = upMat;
            }
        }

    } // end of class

} // end of namespace
