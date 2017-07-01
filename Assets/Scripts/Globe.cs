using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class Globe : Toy
    {

		public Material activeMat;
        public Material[] globeDanceMat;

        private MeshRenderer meshRenderer;
        private Material defaultMat;
        private bool isDanceMode = false;
        private int spinCount;

		private List<Land> lands;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            defaultMat = meshRenderer.material;

            canSeeToyKey = GameVals.AnimatorParameterKeys.canSeeGlobe;
            animatorParamDictionary = new Dictionary<string, bool>();
            animatorParamDictionary.Add(canSeeToyKey, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.globeIsSpinning, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.globeIsDancing, false);

			lands = new List<Land> ();
			foreach (Transform child in transform) {
				lands.Add (child.GetComponent<Land> ());
			}
        }
			
        public void SpinGlobe()
        {
            StartCoroutine(SpinCoroutine(10));
        }

        public void ToggleDanceMode(int landNumber)
        {
            isDanceMode = !isDanceMode;

            if (isDanceMode)
            {
                AudioManager.Instance.PlayBackground(landNumber);
                //meshRenderer.material = globeDanceMat[landNumber - 1];
				meshRenderer.material = activeMat;

				lands [landNumber - 1].SetActive (true);
            }
            else
            {
                AudioManager.Instance.StopBackground();
                meshRenderer.material = defaultMat;

				foreach (Land land in lands) {
					land.SetActive (false);
				}
            }

            animatorParamDictionary[GameVals.AnimatorParameterKeys.globeIsDancing] = isDanceMode;
        }

        private IEnumerator SpinCoroutine(int rotationCount)
        {
            this.SpinCount++;

            for (int i = 0; i < rotationCount; i++)
            {

                transform.Rotate(0, 0, -10f);

                yield return new WaitForSeconds(0.025f);
            }

            this.SpinCount--;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Dart"))
            {
                AudioManager.Instance.PlayAudio(AudioManager.Instance.dartHitGlobe);
                SpinGlobe();
            }
        }

        public int SpinCount
        {
            get { return spinCount; }
            set
            {
                if (spinCount <= 0) // not spinning
                {
                    spinCount = value;

                    if (spinCount > 0)
                    {
                        animatorParamDictionary[GameVals.AnimatorParameterKeys.globeIsSpinning] = true;
                    }
                }
                else // currently spinning
                {
                    spinCount = value;

                    if (spinCount <= 0)
                    {
                        animatorParamDictionary[GameVals.AnimatorParameterKeys.globeIsSpinning] = false;
                    }
                }
            }
        }

		public bool IsDanceMode {
			get { return isDanceMode; }
		}

    }

}
