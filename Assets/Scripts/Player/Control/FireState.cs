using Game.Combat;
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
        Fire fire;

        public FireState(StateMachine stateMachine, Mover mover, Fire fire)
        {
            this.stateMachine = stateMachine;
            this.mover = mover;
            this.fire = fire;
        }
        public void OnEnter()
        {
            fire.FireBullet();
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
