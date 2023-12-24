using Enemy.Combat;
using UnityEngine;

namespace Enemy.Control
{
    public class FightState : IState
    {
        public StateID stateID => StateID.Fight;
        private StateMachine stateMachine;
        private Fire fire;
        public FightState(StateMachine stateMachine, Fire fire)
        {
            this.stateMachine = stateMachine;
            this.fire = fire;
        }
        public void OnEnter()
        {
            fire.FireBehaviors();
        }
        public void OnExit()
        {
            fire.IsFireState = false;
        }
        public void OnUpdate()
        {
            if (!fire.IsFireState) stateMachine.ChangeState(StateID.Move);
        }
    }
}
