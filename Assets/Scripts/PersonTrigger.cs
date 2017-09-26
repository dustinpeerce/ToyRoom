using UnityEngine;
using System.Collections;


namespace ToyRoom
{
    public class PersonTrigger
    {
        private string n;       // name
        private bool v;         // value
        private int r;          // rank of importance (1-100)
        private float s;        // sound distance (0-5)
        private string[] t;     // trigger combinations

        public PersonTrigger(string name, int rank, float sound)
        {
            Init(name, rank, sound);
        }

        public PersonTrigger(string name, int rank, float sound, string[] triggerCombos = null)
        {
            Init(name, rank, sound);
            t = triggerCombos;
        }

        private void Init(string name, int rank, float sound)
        {
            n = name;
            v = false;
            r = rank;
            s = sound;
        }

        public string Name
        {
            get { return n; }
        }

        public bool Value
        {
            get { return v; }
            set { v = value; }
        }

        public int Rank
        {
            get { return r; }
        }

        public float Sound
        {
            get { return s; }
        }

        public string[] TriggerCombos
        {
            get { return t; }
        }

    }
}
