using Enemy.Combat;
using Enemy.Core;

namespace Enemy.Control
{
    public class StunState : IState
    {
        public StateID stateID => StateID.Stun;
        private StateMachine stateMachine;
        private Health health;
        private Stun stun;
        private Charged charged;
        public StunState(StateMachine stateMachine, Health health, Stun stun, Charged charged)
        {
            this.stateMachine = stateMachine;
            this.health = health;
            this.stun = stun;
            this.charged = charged; // FIXME: 使ってない
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
