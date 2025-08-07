using System.Collections.Generic;
using UnityEngine;

namespace TastyCore.Patterns.StateMachine
{
    [CreateAssetMenu(menuName="StateMachine/State",fileName = "new State")]
    public class State : BaseState
    {
        [Header("Actions")]
        public StateAction Action;
        //public List<StateAction> Actions = new List<StateAction>();
        
        [Header("Transitions")]
        public List<StateTransition> Transitions = new List<StateTransition>();

        public override void EnterState(BaseStateMachine stateMachine)
        {
            Action.EnterState(stateMachine);
        }

        public override void Execute(BaseStateMachine stateMachine)
        {
            /*foreach (var action in Actions)
                action.Execute(stateMachine);*/

            Action.Execute(stateMachine);
            
            foreach (var transition in Transitions)
                transition.Execute(stateMachine);
        }
        
        public override void ExitState(BaseStateMachine stateMachine)
        {
            Action.ExitState(stateMachine);
        }
    }
}
