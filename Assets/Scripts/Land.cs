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


        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            defaultMat = meshRenderer.material;
        }


        public void SetGazedAt(bool gazedAt)
        {

        }


    }
}
