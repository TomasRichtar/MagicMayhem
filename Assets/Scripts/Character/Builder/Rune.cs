using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderPattern
{
    public class Rune : MonoBehaviour
    {
        public string Name { get; set; }
        public string Slot { get; set; }

        public Rune(string name, string slot)
        {
            Name = name;
            Slot = slot;
        }
    }
}
