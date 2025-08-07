using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace BuilderPattern
{
    public class CharacterBuilder
    {
        private Character character;

        public CharacterBuilder(Entities name)
        {
            character = new Character(name.ToString());
        }

        public CharacterBuilder AddStat(Stats name, float baseValue)
        {
            character.AddStat(name, baseValue);
            return this;
        }
        public CharacterBuilder AddPlayerBaseStats()
        {
            AddStat(Stats.Health, 100);
            AddStat(Stats.AttackPower, 20);
            AddStat(Stats.Mana, 50);
            return this;
        }
        public CharacterBuilder AddPlayerRegenerationStats()
        {
            AddStat(Stats.HealthRegeneration, 10);
            AddStat(Stats.ManaRegeneration, 5);
            return this;
        }
        public CharacterBuilder AddBearBaseStats()
        {
            AddStat(Stats.Health, 50);
            AddStat(Stats.AttackPower, 10);
            AddStat(Stats.Mana, 50);
            return this;
        }
        public CharacterBuilder AddGoblinBaseStats()
        {
            AddStat(Stats.Health, 500);
            AddStat(Stats.AttackPower, 30);
            AddStat(Stats.Mana, 50);
            return this;
        }

        public CharacterBuilder AddMageBaseStats()
        {
            AddStat(Stats.Health, 150);
            AddStat(Stats.AttackPower, 20);
            AddStat(Stats.Mana, 100);
            return this;
        }

        public Character Build()
        {
            return character;
        }
    }

    

}

