using Game.Move;
using UnityEngine;

namespace Game.Control
{
    public class FireState : IState
    {
        public StateID stateID => StateID.Fire;
        StateMachine stateMachine;

        const float onFireMoveSpeed = 2f;
        Mover mover;


        public FireState(StateMachine stateMachine, Mover mover)
        {
            this.stateMachine = stateMachine;
            this.mover = mover;

        }
        public void OnEnter()
        {
            mover.Move(Vector2.up, onFireMoveSpeed);
            stateMachine.ChangeState(StateID.Idle);
        }

        public void OnExit()
        {
        }

        public void OnUpdate()
        {
        }
    }
}
