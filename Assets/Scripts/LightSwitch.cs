using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{


    public class LightSwitch : MonoBehaviour {

        public House houseInstance;
        public Material upMat;
        public Material downMat;

        private Vector3 downPos;
        private Vector3 upPos;
        private MeshRenderer mesh;

        private void Awake()
        {
            upPos = transform.position;
            downPos = new Vector3(upPos.x, upPos.y - 0.04f, upPos.z);
            mesh = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Car")
            {
                houseInstance.IsOn = true;
                transform.position = downPos;
                mesh.material = downMat;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == "Car")
            {
                houseInstance.IsOn = false;
                transform.position = upPos;
                mesh.material = upMat;
            }
        }
    }
}
