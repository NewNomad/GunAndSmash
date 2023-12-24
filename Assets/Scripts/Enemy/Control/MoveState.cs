using Enemy.Movement;
using UnityEngine;

namespace Enemy.Control
{
    public class MoveState : IState
    {
        public StateID stateID => StateID.Move;
        private StateMachine stateMachine;
        Mover mover;

        float changeStateDuration = Mathf.Infinity;
        public MoveState(StateMachine stateMachine, Mover mover)
        {
            this.stateMachine = stateMachine;
            this.mover = mover;
        }
        public void OnEnter()
        {
            mover.SetChangeStateDuration();
            mover.IsMoveState = true;
        }
        public void OnExit()
        {
            mover.IsMoveState = false;
        }
        public void OnUpdate()
        {
            if (!mover.IsChangeStateDurationOver()) return;
            stateMachine.ChangeState(StateID.Fight);
        }
    }
}
