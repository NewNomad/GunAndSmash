using UnityEngine;

namespace Game.Control
{
    public class FireState : IState
    {
        public StateID stateID => StateID.Fire;
        StateMachine stateMachine;
        public FireState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        public void OnEnter()
        {

        }

        public void OnExit()
        {
        }

        public void OnUpdate()
        {
        }
    }
}
