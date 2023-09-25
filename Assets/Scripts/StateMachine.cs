using System;
using System.Collections.Generic;
using UnityEngine;

namespace Openworld
{
    public class StateMachine<T>
    {
        protected T currentState;
        protected Dictionary<T, List<T>> validTransitions;

        // Event handlers for state transitions
        public event Action<T, T> OnEnterState;
        public event Action<T, T> OnExitState;

        private bool initialized = false;

        public StateMachine(Dictionary<T, List<T>> validTransitions)
        {
            Debug.Log("StateMachine constructor");
            this.validTransitions = validTransitions;
        }

        // use this method to initialize the state machine to the desired state
        public virtual void InitializeStateMachine(T initialState)
        {
            if (initialized)
            {
                Debug.LogWarning("StateMachine.InitializeStateMachine called more than once");
                return;
            }
            Debug.Log("StateMachine.InitializeStateMachine");
            currentState = initialState;
            initialized = true;
            OnEnterState?.Invoke(default, currentState);
        }

        public void ChangeState(T nextState)
        {
            if (!CanChangeState(currentState, nextState))
            {
                // log a warning for now
                Debug.LogWarning("Cannot change state from " + currentState + " to " + nextState);
                return;
            }
            T previousState = currentState;
            currentState = nextState;
            // Raise the ExitState event for the previous state
            OnExitState?.Invoke(previousState, currentState);

            // Raise the EnterState event for the current state
            OnEnterState?.Invoke(previousState, currentState);
        }

        protected virtual bool CanChangeState(T currentState, T nextState)
        {
            //return true if the transition is valid
            return validTransitions.ContainsKey(currentState) && validTransitions[currentState].Contains(nextState);
        }

        public bool CanChangeState(T nextState)
        {
            return CanChangeState(currentState, nextState);
        }
    }
}
