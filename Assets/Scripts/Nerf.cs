using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Nerf : Toy
    {

        public Transform dartPos;
        public GameObject dartPrefab;

        private Animator animator;


        private void Awake()
        {
            animator = GetComponent<Animator>();

            canSeeToyKey = GameVals.AnimatorParameterKeys.canSeeGun;
            animatorParamDictionary = new Dictionary<string, bool>();
            animatorParamDictionary.Add(canSeeToyKey, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.gunIsGazed, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.gunHasShotFront, false);
        }


        public void SetGazedAt(bool gazedAt)
        {
            animatorParamDictionary[GameVals.AnimatorParameterKeys.gunIsGazed] = gazedAt;

            animator.SetBool("IsGazedAt", animatorParamDictionary[GameVals.AnimatorParameterKeys.gunIsGazed]);
        }

        public void FireDart()
        {

            GameObject obj = Instantiate(dartPrefab, dartPos.position, transform.rotation) as GameObject;

            Vector3 vForce = transform.right * -5f;

            obj.GetComponent<Rigidbody>().AddForce(vForce);
        }

    }

}
