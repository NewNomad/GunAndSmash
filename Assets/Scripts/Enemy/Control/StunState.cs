namespace Enemy.Control
{
    public class StunState : IState
    {
        public StateID stateID => StateID.Stun;
        private StateMachine stateMachine;
        public StunState(StateMachine stateMachine)
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
