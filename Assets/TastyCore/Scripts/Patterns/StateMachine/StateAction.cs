using UnityEngine;

namespace TastyCore.Patterns.StateMachine
{
    public abstract class StateAction : ScriptableObject
    {
        public abstract void EnterState(BaseStateMachine stateMachine);
        public abstract void Execute(BaseStateMachine stateMachine);
        public abstract void ExitState(BaseStateMachine stateMachine);
    }
}
