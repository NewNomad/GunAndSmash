using Enemy.Movement;
using UnityEngine;

namespace Enemy.Control
{
    public class MoveState : IState
    {
        public StateID stateID => StateID.Move;
        private StateMachine stateMachine;
        Mover mover;
        readonly float maxChangeStateDuration = 5f;
        readonly float minChangeStateDuration = 3f;
        float changeStateDuration = Mathf.Infinity;
        public MoveState(StateMachine stateMachine, Mover mover)
        {
            this.stateMachine = stateMachine;
            this.mover = mover;
        }
        public void OnEnter()
        {
            changeStateDuration = Random.Range(minChangeStateDuration, maxChangeStateDuration);
            mover.IsMoveState = true;
        }
        public void OnExit()
        {
            mover.IsMoveState = false;
        }
        public void OnUpdate()
        {
            changeStateDuration -= Time.deltaTime;
            if (changeStateDuration > 0) return;
            stateMachine.ChangeState(StateID.Fight);
        }
    }
}
