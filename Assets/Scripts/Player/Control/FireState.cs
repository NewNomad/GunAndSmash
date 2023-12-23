using Game.Move;
using UnityEngine;

namespace Game.Control
{
    public class FireState : IState
    {
        public StateID stateID => StateID.Fire;
        StateMachine stateMachine;
        Mover mover;

        public FireState(StateMachine stateMachine, Mover mover)
        {
            this.stateMachine = stateMachine;
            this.mover = mover;

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
