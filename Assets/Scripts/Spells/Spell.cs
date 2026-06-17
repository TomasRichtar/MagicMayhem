using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    public abstract class Spell
    {
        public float cooldown = 2.0f;
        private float lastCastTime = 0;

        public bool IsInstant = false;

        public bool CanCast()
        {
            return Time.time - lastCastTime >= cooldown;
        }

        public void CastSpell()
        {
            Debug.Log("Can cast?");
            if (CanCast())
            {
                Debug.Log("YES");
                lastCastTime = Time.time;
                Execute();
            }
            else
            {

                Debug.Log("NOPE");
            }
        }

        protected abstract void Execute();
    }
}
