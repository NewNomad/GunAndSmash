using Enemy.Core;

namespace Enemy.Control
{
    public class StunState : IState
    {
        public StateID stateID => StateID.Stun;
        private StateMachine stateMachine;
        private Health health;
        public StunState(StateMachine stateMachine, Health health)
        {
            this.stateMachine = stateMachine;
            this.health = health;
        }
        public void OnEnter()
        {
            health.CanHealStun = false;
        }
        public void OnExit()
        {
            health.CanHealStun = true;
        }
        public void OnUpdate()
        {
        }

    }
}
