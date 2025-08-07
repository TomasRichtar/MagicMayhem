using System;
using System.Collections.Generic;
using UnityEngine;

namespace TastyCore.Patterns.StateMachine
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] protected BaseState _initialState;

        public BaseState CurrentState { get; private set; }

        private Dictionary<Type, Component> _cachedComponents = new Dictionary<Type, Component>();
        
        public void ChangeState(BaseState nextState)
        {
            if(CurrentState != null)
                CurrentState.ExitState(this);

            CurrentState = nextState;
            CurrentState.EnterState(this);
        }
        
        private void Awake()
        {
            ChangeState(_initialState);
        }

        void Update() => CurrentState.Execute(this);
        
        public new T GetComponent<T>() where T : Component
        {
            if(_cachedComponents.ContainsKey(typeof(T)))
                return _cachedComponents[typeof(T)] as T;

            var component = base.GetComponent<T>();
            if(component != null)
            {
                _cachedComponents.Add(typeof(T), component);
            }
            return component;
        }
    }
}
