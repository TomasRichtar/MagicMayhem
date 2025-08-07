using UnityEngine;

namespace TastyCore.Patterns.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/Transition",fileName = "new State Transition")]
    public sealed class StateTransition : ScriptableObject
    {
        public StateDecision Decision;
        public BaseState TrueState;
        public BaseState FalseState;

        public void Execute(BaseStateMachine stateMachine)
        {
            if(Decision.Decide(stateMachine) && TrueState.GetType() != typeof(RemainInState))
                stateMachine.ChangeState(TrueState);
            
            else if(!Decision.Decide(stateMachine) && FalseState.GetType() != typeof(RemainInState))
                stateMachine.ChangeState(FalseState);
            
            /*
            if(Decision.Decide(stateMachine) && TrueState is not RemainInState)
                stateMachine.ChangeState(TrueState);

            else if (!Decision.Decide(stateMachine) && FalseState is not RemainInState)
                stateMachine.ChangeState(FalseState);
            */
        }
    }
}
