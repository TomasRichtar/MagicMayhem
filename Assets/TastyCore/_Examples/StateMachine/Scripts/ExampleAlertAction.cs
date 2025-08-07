using TastyCore.Patterns.StateMachine;
using UnityEngine;

namespace TastyCore._Examples.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/Example/AlertAction", fileName = "Alert Action")]
    public class ExampleAlertAction : StateAction
    {
        public override void EnterState(BaseStateMachine stateMachine)
        {
            Debug.Log("Entering Alert State");
        }

        public override void Execute(BaseStateMachine stateMachine)
        {
            // Some alert animation or smth
        }

        public override void ExitState(BaseStateMachine stateMachine)
        {
            Debug.Log("Exiting Alert State");
        }
    }
}