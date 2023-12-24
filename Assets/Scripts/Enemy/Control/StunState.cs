using Enemy.Core;

namespace Enemy.Control
{
    public class StunState : IState
    {
        public StateID stateID => StateID.Stun;
        private StateMachine stateMachine;
        private Health health;
        private Stun stun;
        public StunState(StateMachine stateMachine, Health health, Stun stun)
        {
            this.stateMachine = stateMachine;
            this.health = health;
            this.stun = stun;
        }
        public void OnEnter()
        {
            health.CanHealStun = false;
            stun.StunEnemy();
        }
        public void OnExit()
        {
            health.CanHealStun = true;
            health.SetStunFull();
        }
        public void OnUpdate()
        {
            if (!stun.IsStun)
            {
                stateMachine.ChangeState(StateID.Move);
            }
        }

    }
}
