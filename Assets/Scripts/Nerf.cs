using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Nerf : Toy
    {
		// Public Attributes
        public Transform dartPos;		// Dart Spawn Position
        public GameObject dartPrefab;	// Dart Prefab

		// Private Attributes
        private Animator animator;		// Animator component for Nerf Gun


		/// <summary>
		/// Awake this instance.
		/// </summary>
        private void Awake()
        {
            animator = GetComponent<Animator>();

            animParamDictionary = new Dictionary<string, bool>();
            animParamDictionary.Add(GameVals.AnimParams.gunDefault, false);
            animParamDictionary.Add(GameVals.AnimParams.gunIsGazed, false);
            animParamDictionary.Add(GameVals.AnimParams.gunHasShotFront, false);
        }


		/// <summary>
		/// Sets gazedAt.
		/// </summary>
		/// <param name="gazedAt">If set to <c>true</c> gazed at.</param>
        public void SetGazedAt(bool gazedAt)
        {
            animParamDictionary[GameVals.AnimParams.gunIsGazed] = gazedAt;

            animator.SetBool("IsGazedAt", animParamDictionary[GameVals.AnimParams.gunIsGazed]);
        }


		/// <summary>
		/// Fires the dart.
		/// </summary>
        public void FireDart()
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.dartFire);

            GameObject obj = Instantiate(dartPrefab, dartPos.position, transform.rotation) as GameObject;

            Vector3 vForce = transform.right * -5f;

            obj.GetComponent<Rigidbody>().AddForce(vForce);
        }

    } // end of class

} // end of namespace
