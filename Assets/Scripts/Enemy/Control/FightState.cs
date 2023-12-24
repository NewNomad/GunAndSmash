using Enemy.Combat;

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
            fire.FireBullet();
        }
        public void OnExit()
        {
        }
        public void OnUpdate()
        {
        }

    }
}
