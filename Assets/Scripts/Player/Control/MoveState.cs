using UnityEngine;

namespace Game.Control
{
    public class MoveState : IState
    {
        public StateID stateID => StateID.Move;
        StateMachine stateMachine;
        public MoveState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
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
        }



        void ChangeStateOnTimeSpeed()
        {
            currentChangeMoveStateTime += Time.deltaTime;
            if (currentChangeMoveStateTime > changeMoveStateTime)
            {
                stateMachine.ChangeState(StateID.Idle);
            }
        }
    }
}
