using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    public class EventManager : MonoBehaviour
    {
        public static event EventHandler<TakeDamageArgs> OnTakeDamage;
        public class TakeDamageArgs : EventArgs
        {
            public Entity Actor { get; private set; }
            public int Value { get; private set; }

            public void TakeDamageEventArgs(Entity actor, int value)
            {
                Actor = actor;
                Value = value;
            }
        }
    }
}
