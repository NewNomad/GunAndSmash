namespace Game.Control
{
    public class StopState : IState
    {
        public StateID stateID => StateID.Stop;
        StateMachine stateMachine;
        public StopState(StateMachine stateMachine)
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
