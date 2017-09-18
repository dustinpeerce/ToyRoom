using UnityEngine;
using System.Collections;


namespace ToyRoom
{
    public class PersonTrigger
    {
        private string n;       // name
        private bool v;         // value
        private float r;        // rank
        private string[] t;     // trigger combinations

        public PersonTrigger(string name, float rank)
        {
            Init(name, rank);
        }

        public PersonTrigger(string name, float rank, string[] triggerCombos)
        {
            Init(name, rank);
            t = triggerCombos;
        }

        private void Init(string name, float rank)
        {
            n = name;
            v = false;
            r = rank;
        }

        public string Name
        {
            get { return n; }
            set { n = value; }
        }

        public bool Value
        {
            get { return v; }
            set { v = value; }
        }

        public float Rank
        {
            get { return r; }
            set { r = value; }
        }

        public string[] TriggerCombos
        {
            get { return t; }
            set { t = value; }
        }

    }
}
