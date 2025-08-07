using TastyCore.Patterns.StateMachine;
using UnityEngine;

namespace TastyCore._Examples.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/Example/IdleAction", fileName = "Idle Action")]
    public class ExampleIdleAction : StateAction
    {
        public override void EnterState(BaseStateMachine stateMachine)
        {
            Debug.Log("Entering Idle State");
        }

        public override void Execute(BaseStateMachine stateMachine)
        {
            // Some idle animation or smth
        }

        public override void ExitState(BaseStateMachine stateMachine)
        {
            Debug.Log("Exiting Idle State");
        }
    }
}