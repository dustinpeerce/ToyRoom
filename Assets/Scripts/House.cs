using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class House : Toy
    {

        public Material lightsOnHouseMat;
        public Material lightsOffHouseMat;
        public Material lightsOnWindowMat;
        public Material lightsOffWindowMat;
        public List<MeshRenderer> windowObjects;
        public List<MeshRenderer> houseObjects;
        private Animator animator;
        private bool isOpen;
        private bool isOn;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            canSeeToyKey = GameVals.AnimatorParameterKeys.canSeeHouse;
            animatorParamDictionary = new Dictionary<string, bool>();
            animatorParamDictionary.Add(canSeeToyKey, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.houseIsOpen, false);

            IsOpen = false;
            IsOn = false;
        }


        public void SetGazedAt(bool gazedAt)
        {
            animator.SetBool("IsGazedAt", gazedAt);
        }

        public void ToggleHouseOpen()
        {
            if (IsOn)
            {
                if (IsOpen)
                {
                    CloseHouse();
                }
                else
                {
                    OpenHouse();
                }
            }
            else
            {
                ShakeHouse();
            }
        }

        private void ShakeHouse()
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.houseShake);
            animator.SetTrigger("Shake");
        }

        private void OpenHouse()
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.houseOpen);
            animator.SetTrigger("Open");

            IsOpen = true;
        }

        private void CloseHouse()
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.houseOpen);
            animator.SetTrigger("Close");

            IsOpen = false; 
        }

        public bool IsOn
        {
            get { return isOn; }
            set
            {
                isOn = value;
                if (isOn)
                {
                    foreach(MeshRenderer window in windowObjects)
                    {
                        window.material = lightsOnWindowMat;
                    }

                    foreach (MeshRenderer house in houseObjects)
                    {
                        house.material = lightsOnHouseMat;
                    }
                }
                else {
                    foreach (MeshRenderer window in windowObjects)
                    {
                        window.material = lightsOffWindowMat;
                    }

                    foreach (MeshRenderer house in houseObjects)
                    {
                        house.material = lightsOffHouseMat;
                    }

                    if (IsOpen)
                    {
                        CloseHouse();
                    }
                }
            }
        }

        private bool IsOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                animatorParamDictionary[GameVals.AnimatorParameterKeys.houseIsOpen] = isOpen;
            }
        }

    }
}
