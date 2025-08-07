namespace FlyweightPattern
{
    public class StatInstance
    {
        public StatDefinition Definition { get; private set; }
        public float CurrentValue { get; set; }

        public StatInstance(StatDefinition definition)
        {
            Definition = definition;
            CurrentValue = definition.BaseValue;
        }

        public void AddModifier(float modifier)
        {
            CurrentValue += modifier;
        }

        public void Reset()
        {
            CurrentValue = Definition.BaseValue;
        }
    }
}
