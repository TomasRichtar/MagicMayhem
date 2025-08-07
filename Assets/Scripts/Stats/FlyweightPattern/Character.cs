using System.Collections.Generic;

namespace FlyweightPattern
{
    public class Character
    {
        public string Name { get; private set; }
        public List<StatInstance> Stats { get; private set; }

        public Character(string name)
        {
            Name = name;
            Stats = new List<StatInstance>();
        }

        public void AddStat(string statName, float baseValue)
        {
            StatDefinition definition = StatDefinitionFactory.GetStatDefinition(statName, baseValue);
            Stats.Add(new StatInstance(definition));
        }

        public StatInstance GetStat(string statName)
        {
            return Stats.Find(stat => stat.Definition.Name == statName);
        }
    }
}
