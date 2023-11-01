using System.Collections;
using System.Collections.Generic;
using Openworld.Menus;
using UnityEngine;

namespace Openworld.Scenes
{
    /**
    ** StatefulScene is a base class for scenes that have a state machine
    */
    public abstract class StatefulScene<T, U> : BaseScene<U> where U : UIBase
    {
        protected T SceneStates;
        protected T InitialState;
        protected Dictionary<T, List<T>> validTransitions = new Dictionary<T, List<T>>();
        protected StateMachine<T> stateMachine;

        private void Awake()
        {
            validTransitions.Clear();
            validTransitions = GetStateTransitions();
            InitialState = GetInitialState();
            this.stateMachine = new StateMachine<T>(validTransitions);
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.InitializeStateMachine(InitialState);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // Subscribe to state machine events
            stateMachine.OnEnterState += HandleEnterState;
            stateMachine.OnExitState += HandleExitState;
        }

        private void OnDisable()
        {
            // Unsubscribe from state machine events to prevent memory leaks
            stateMachine.OnEnterState -= HandleEnterState;
            stateMachine.OnExitState -= HandleExitState;
        }

        protected abstract T GetInitialState();
        protected abstract Dictionary<T, List<T>> GetStateTransitions();

        protected abstract void HandleEnterStateLocal(T previousState, T newState);

        protected abstract void HandleExitStateLocal(T previousState, T newState);

        private void HandleEnterState(T previousState, T newState)
        {
            HandleEnterStateLocal(previousState, newState);
        }

        private void HandleExitState(T previousState, T newState)
        {
            HandleExitStateLocal(previousState, newState);
        }

    }
}