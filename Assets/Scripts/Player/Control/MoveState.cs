using Game.Combat;
using Game.Core;
using Game.Move;
using UnityEngine;

namespace Game.Control
{
    public class MoveState : IState
    {
        public StateID stateID => StateID.Move;
        StateMachine stateMachine;
        WallCheck wallCheck;
        Mover mover;
        Charge charge;
        const float moveSpeedOnWall = 0.7f;
        public MoveState(StateMachine stateMachine, WallCheck wallCheck, Mover mover, Charge charge)
        {
            this.stateMachine = stateMachine;
            this.wallCheck = wallCheck;
            this.mover = mover;
            this.charge = charge; // FIXME: 使ってない
        }

        const float changeMoveStateTime = 1f;
        private float currentChangeMoveStateTime = 0f;


        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        public void OnUpdate()
        {
            ChangeStateOnTimeSpeed();
            MoveOnTouchingWall();
        }

        void ChangeStateOnTimeSpeed()
        {
            currentChangeMoveStateTime += Time.deltaTime;
            if (currentChangeMoveStateTime > changeMoveStateTime)
            {
                // stateMachine.ChangeState(StateID.Idle);
            }
        }

        void MoveOnTouchingWall()
        {
            if (wallCheck.IsTouchingWall())
            {
                Debug.Log("MoveOnTouchingWall");
                Vector2 moveDirection = mover.IsMovingLeft()
                    ? new Vector2(-moveSpeedOnWall, moveSpeedOnWall)
                    : new Vector2(moveSpeedOnWall, moveSpeedOnWall);
                mover.Move(moveDirection);
            }
        }
    }
}
