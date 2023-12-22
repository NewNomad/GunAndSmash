using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class StateMachine
    {
        private IState currentState;
        private Dictionary<StateID, IState> states = new Dictionary<StateID, IState>();
        public void RegisterState(IState state)
        {
            if (states.ContainsKey(state.stateID)) { return; }
            states.Add(state.stateID, state);
        }
        public void ChangeState(StateID stateID)
        {
            if (states.TryGetValue(stateID, out IState newState))
            {
                currentState?.OnExit();
                currentState = newState;
                currentState.OnEnter();
                Debug.Log("ChangeState: " + stateID);
            }
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.OnUpdate();
            }
        }
    }
}
