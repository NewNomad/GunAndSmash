namespace Game.Control
{
    public class StateMachine
    {
        private IState currentState;
        public void ChangeState(IState newState)
        {
            if (currentState != null)
            {
                currentState.OnExit();
            }

            currentState = newState;
            currentState.OnEnter();
        }
        public void Update()
        {
            if (currentState != null)
            {
                currentState.OnUpdate();
            }
        }
    }
}
