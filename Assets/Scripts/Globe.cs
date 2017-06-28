using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Globe : Toy
    {

        public Material[] globeDanceMat;

        private MeshRenderer meshRenderer;
        private Material defaultMat;
        private bool isDanceMode = false;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            defaultMat = meshRenderer.material;

            canSeeToyKey = GameVals.AnimatorParameterKeys.canSeeGlobe;
            animatorParamDictionary = new Dictionary<string, bool>();
            animatorParamDictionary.Add(canSeeToyKey, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.globeIsDancing, false);
        }


        public void SetGazedAt(bool gazedAt)
        {
            
        }


        public void SpinGlobe()
        {
            StartCoroutine(SpinCoroutine(10));
        }

        public void ToggleDanceMode()
        {
            isDanceMode = !isDanceMode;

            if (isDanceMode)
            {
                meshRenderer.material = globeDanceMat[0];
            }
            else
            {
                meshRenderer.material = defaultMat;
            }

            animatorParamDictionary[GameVals.AnimatorParameterKeys.globeIsDancing] = isDanceMode;
        }

        private IEnumerator SpinCoroutine(int rotationCount)
        {
            for (int i = 0; i < rotationCount; i++)
            {

                transform.Rotate(0, 0, -10f);

                yield return new WaitForSeconds(0.025f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Dart"))
            {
                AudioManager.Instance.PlayAudio(AudioManager.Instance.dartHitGlobe);
                SpinGlobe();
            }
        }

    }

}
