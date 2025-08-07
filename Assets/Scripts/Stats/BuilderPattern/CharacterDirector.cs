using BuilderPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderPattern
{
    public class CharacterDirector
    {
        public Character CreatePlayer()
        {
            return new CharacterBuilder(Entities.Player)
                .AddPlayerBaseStats()
                .AddPlayerRegenerationStats()
                .Build();
        }
        public Character CreateBear()
        {
            return new CharacterBuilder(Entities.Bear)
                .AddBearBaseStats()
                .Build();
        }
        public Character CreateGoblin()
        {
            return new CharacterBuilder(Entities.Goblin)
                .AddGoblinBaseStats()
                .Build();
        }

        public Character CreateMage()
        {
            return new CharacterBuilder(Entities.Mage)
                .AddMageBaseStats()
                .Build();
        }
    }
}

