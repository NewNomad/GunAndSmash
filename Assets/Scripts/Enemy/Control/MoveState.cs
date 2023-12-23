namespace Enemy.Control
{
    public class MoveState : IState
    {
        public StateID stateID => StateID.Move;
        private StateMachine stateMachine;
        public MoveState(StateMachine stateMachine)
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
