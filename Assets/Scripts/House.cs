using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class House : Toy
    {

		// Public Attributes
		public enum HouseState { Opened, Closed, Changing }
        public Material lightsOnHouseMat;
        public Material lightsOffHouseMat;
        public Material lightsOnWindowMat;
        public Material lightsOffWindowMat;
        public List<MeshRenderer> windowObjects;
        public List<MeshRenderer> houseObjects;

		// Private Attributes
        private Animator animator;
        private HouseState currentState;
        private bool isOn;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            canSeeToyKey = GameVals.AnimatorParameterKeys.canSeeHouse;
            animatorParamDictionary = new Dictionary<string, bool>();
            animatorParamDictionary.Add(canSeeToyKey, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.houseIsOpen, false);

            CurrentState = HouseState.Closed;
            IsOn = false;
        }


        public void SetGazedAt(bool gazedAt)
        {
            animator.SetBool("IsGazedAt", gazedAt);
        }

        public void ToggleHouseOpen()
        {
            if (CurrentState != HouseState.Changing)
            {
                if (IsOn)
                {
                    if (CurrentState == HouseState.Opened)
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
        }

        private void ShakeHouse()
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.houseShake);
            animator.SetTrigger("Shake");

            CurrentState = HouseState.Changing;
        }

        private void OpenHouse()
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.houseOpen);
            animator.SetTrigger("Open");

            CurrentState = HouseState.Changing;
        }

        private void CloseHouse()
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.houseOpen);
            animator.SetTrigger("Close");

            CurrentState = HouseState.Changing;
        }

        public void HouseChangeComplete(int isOpenInteger)
        {
            if (isOpenInteger == 0)
            {
                CurrentState = HouseState.Closed;
            }
            else
            {
                CurrentState = HouseState.Opened;
            }
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

                    if (CurrentState == HouseState.Opened)
                    {
                        CloseHouse();
                    }
                }
            }
        }

        private HouseState CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;

                if (currentState == HouseState.Opened)
                {
                    animatorParamDictionary[GameVals.AnimatorParameterKeys.houseIsOpen] = true;
                }
                else
                {
                    animatorParamDictionary[GameVals.AnimatorParameterKeys.houseIsOpen] = false;
                }
            }
        }

    } // end of class

} // end of namespace
