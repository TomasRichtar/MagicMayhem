using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderPattern
{
    public class Equipment : MonoBehaviour
    {
        public string Name { get; set; }
        public string Slot { get; set; }

        public Equipment(string name, string slot)
        {
            Name = name;
            Slot = slot;
        }
    }
}
