using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    public abstract class Spell
    {
        public float cooldown = 2.0f;
        private float lastCastTime = 0;

        public bool CanCast()
        {
            return Time.time - lastCastTime >= cooldown;
        }

        public void CastSpell()
        {
            if (CanCast())
            {
                lastCastTime = Time.time;
                Execute();
            }
        }

        protected abstract void Execute();
    }
}
