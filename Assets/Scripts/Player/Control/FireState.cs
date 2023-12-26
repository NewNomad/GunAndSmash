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
        FireHomingLaser fireHomingLaser;

        public FireState(StateMachine stateMachine, Mover mover, Fire fire, FireHomingLaser fireHomingLaser)
        {
            this.stateMachine = stateMachine;
            this.mover = mover;
            this.fire = fire;
            this.fireHomingLaser = fireHomingLaser;
        }
        public void OnEnter()
        {
            fire.FireBullet();
            fireHomingLaser.FireHomingLaserAtTarget();
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
