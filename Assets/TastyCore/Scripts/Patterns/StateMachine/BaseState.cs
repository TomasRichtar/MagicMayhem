using UnityEngine;

namespace TastyCore.Patterns.StateMachine
{
   public abstract class BaseState : ScriptableObject
   {
      public virtual void EnterState(BaseStateMachine stateMachine){ }
      public virtual void Execute(BaseStateMachine stateMachine){ }
      public virtual void ExitState(BaseStateMachine stateMachine){ }
   }
}
