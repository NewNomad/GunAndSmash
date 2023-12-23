namespace Enemy.Control
{
    public class FightState : IState
    {
        public StateID stateID => StateID.Fight;
        private StateMachine stateMachine;
        public FightState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        public void OnEnter()
        {
        }
        public void OnExit()
        {
        }
        public void OnUpdate()
        {
        }

    }
}
