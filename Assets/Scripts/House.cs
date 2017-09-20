using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyRoom
{

    public class House : Toy
    {

		// Public Attributes
		public enum HouseState { Opened, Closed, Changing }		// Possible states for the House
        public Material lightsOnHouseMat;			// Material for House when Lights are On
        public Material lightsOffHouseMat;			// Material for House when Lights are Off
        public Material lightsOnWindowMat;			// Material for Windows when Lights are On
        public Material lightsOffWindowMat;			// Material for Windows when Lights are Off
        public List<MeshRenderer> windowObjects;	// List of Window Mesh Renderers
        public List<MeshRenderer> houseObjects;		// List of House Mesh Renderers

		// Private Attributes
        private Animator animator;			// Animator Component for the House
        private HouseState currentState;	// Tracks the current state of the House
        private bool isOn;					// Tracks whether the Lights are On or not


		/// <summary>
		/// Awake this instance.
		/// </summary>
        private void Awake()
        {
			// Initialize Animator Parameter Dictionary
            animParamDictionary = new Dictionary<string, bool>();
            animParamDictionary.Add(GameVals.AnimParams.houseDefault, false);
            animParamDictionary.Add(GameVals.AnimParams.houseIsOpen, false); 

			// Initialize Private Attributes
			animator = GetComponent<Animator>();
			CurrentState = HouseState.Closed;
			IsOn = false;
        }


		/// <summary>
		/// Sets IsGazedAt to gazedAt.
		/// </summary>
		/// <param name="gazedAt">If set to <c>true</c> gazed at.</param>
        public void SetGazedAt(bool gazedAt)
        {
            animator.SetBool("IsGazedAt", gazedAt);
        }


		/// <summary>
		/// Toggles the house open.
		/// </summary>
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


		/// <summary>
		/// Shakes the house.
		/// </summary>
        private void ShakeHouse()
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.houseShake);
            animator.SetTrigger("Shake");

            CurrentState = HouseState.Changing;
        }


		/// <summary>
		/// Opens the house.
		/// </summary>
        private void OpenHouse()
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.houseOpen);
            animator.SetTrigger("Open");

            CurrentState = HouseState.Changing;
        }


		/// <summary>
		/// Closes the house.
		/// </summary>
        private void CloseHouse()
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.houseOpen);
            animator.SetTrigger("Close");

            CurrentState = HouseState.Changing;
        }


		/// <summary>
		/// Executed when the House is done changing (called as an Animation Event)
		/// </summary>
		/// <param name="isOpenInteger">Indicates whether the House is open or not (0 meaning false)</param>
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


		/// <summary>
		/// Turns the lights on.
		/// </summary>
		private void TurnLightsOn() {
			foreach(MeshRenderer window in windowObjects)
			{
				window.material = lightsOnWindowMat;
			}

			foreach (MeshRenderer house in houseObjects)
			{
				house.material = lightsOnHouseMat;
			}
		}


		/// <summary>
		/// Turns the lights off.
		/// </summary>
		private void TurnLightsOff() {
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


		/// <summary>
		/// Gets or sets a value indicating whether this instance is on.
		/// </summary>
		/// <value><c>true</c> if this instance is on; otherwise, <c>false</c>.</value>
        public bool IsOn
        {
            get { return isOn; }
            set
            {
                isOn = value;
                if (isOn)
                {
					TurnLightsOn ();
                }
                else {
					TurnLightsOff ();
                }
            }
        }


		/// <summary>
		/// Gets or sets the state of the House.
		/// </summary>
		/// <value>The state of the current.</value>
        private HouseState CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;

                if (currentState == HouseState.Opened)
                {
                    animParamDictionary[GameVals.AnimParams.houseIsOpen] = true;
                }
                else
                {
                    animParamDictionary[GameVals.AnimParams.houseIsOpen] = false;
                }
            }
        }

    } // end of class

} // end of namespace
