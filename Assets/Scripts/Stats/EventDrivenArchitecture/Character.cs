using EventDrivenArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventDrivenArchitecture
{
    public class Character : MonoBehaviour
    {
        public BuffSystem BuffSystem;
        public LevelSystem LevelSystem;
        // Start is called before the first frame update
        void Start()
        {
            BuffSystem.ApplyBuff("Mana", 20);
            LevelSystem.LevelUp();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
