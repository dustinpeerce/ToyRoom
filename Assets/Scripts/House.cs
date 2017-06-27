using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyRoom
{

    public class House : Toy
    {

        private Animator animator;
        private bool isOpen;
        private bool isOn;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            isOpen = false;
            isOn = false;

            canSeeToyKey = GameVals.AnimatorParameterKeys.canSeeHouse;
            animatorParamDictionary = new Dictionary<string, bool>();
            animatorParamDictionary.Add(canSeeToyKey, false);
            animatorParamDictionary.Add(GameVals.AnimatorParameterKeys.houseIsOpen, false);
        }


        public void SetGazedAt(bool gazedAt)
        {
            animator.SetBool("IsGazedAt", gazedAt);
        }

        public void ToggleHouseOpen()
        {
            if (isOpen)
            {
                animator.SetTrigger("Close");
            }
            else
            {
                animator.SetTrigger("Open");
            }

            isOpen = !isOpen;
            animatorParamDictionary[GameVals.AnimatorParameterKeys.houseIsOpen] = isOpen;
        }

        public bool IsOn
        {
            get { return isOn; }
            set
            {
                isOn = value;
                if (!isOn && isOpen)
                {
                    animator.SetTrigger("Close");
                }
            }
        }

    }
}
