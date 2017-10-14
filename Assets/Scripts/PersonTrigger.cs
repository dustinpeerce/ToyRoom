using UnityEngine;
using System.Collections;


namespace ToyRoom
{
    public class PersonTrigger
    {
        private string name;                // name
        private bool canSee;                // can see the trigger
        private bool canHear;               // can here the trigger
        private int rank;                   // rank of importance (1-100)
        private float sound;                // sound distance (0-5)
        private string[] triggerCombos;     // trigger combinations
        private Transform toy;              // The toy transform responsible for the trigger

        public PersonTrigger(string n, int r, float s)
        {
            Init(n, r, s);
        }

        public PersonTrigger(string n, int r, float s, string[] tc = null)
        {
            Init(n, r, s);
            triggerCombos = tc;
        }

        private void Init(string n, int r, float s)
        {
            name = n;
            rank = r;
            sound = s;
            canSee = false;
            canHear = false;
            toy = null;
        }

        public string Name
        {
            get { return name; }
        }

        public bool CanSee
        {
            get { return canSee; }
            set { canSee = value; }
        }

        public bool CanHear
        {
            get { return canHear; }
            set { canHear = value; }
        }

        public int Rank
        {
            get { return rank; }
        }

        public float Sound
        {
            get { return sound; }
        }

        public string[] TriggerCombos
        {
            get { return triggerCombos; }
        }

        public Transform Toy
        {
            get { return toy; }
            set { toy = value; }
        }
    }
}
