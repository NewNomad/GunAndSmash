namespace Game.Control
{
    public class IdleState : IState
    {
        public StateID stateID => StateID.Idle;
        StateMachine stateMachine;
        public IdleState(StateMachine stateMachine)
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
