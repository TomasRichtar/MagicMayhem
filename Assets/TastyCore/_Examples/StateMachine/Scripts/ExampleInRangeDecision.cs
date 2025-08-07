using UnityEngine;
using TastyCore.Patterns.StateMachine;

namespace TastyCore._Examples.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/Example/RangeDecision", fileName = "Range Decision")]
    public class ExampleInRangeDecision : StateDecision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            var detector = stateMachine.GetComponent<SimplePlayerDetector>();
            return detector.Detected;
        }
    }
}