using Game.Combat;
using Game.Core;
using Game.Move;
using naichilab.EasySoundPlayer.Scripts;
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
        Stamina stamina;

        public FireState(StateMachine stateMachine, Mover mover, Fire fire, FireHomingLaser fireHomingLaser, Stamina stamina)
        {
            this.stateMachine = stateMachine;
            this.mover = mover;
            this.fire = fire;
            this.fireHomingLaser = fireHomingLaser;
            this.stamina = stamina;
        }
        public void OnEnter()
        {
            if (stamina.IsStaminaEmpty())
            {
                SePlayer.Instance.Play("cancel_maou");
                stateMachine.ChangeState(StateID.Idle);
                return;
            }
            stamina.UseStaminaOnFire();
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
