using Game.Move;

namespace Game.Control
{
    public class StopState : IState
    {
        public StateID stateID => StateID.Stop;
        StateMachine stateMachine;
        Stop stop;
        public StopState(StateMachine stateMachine, Stop stop)
        {
            this.stateMachine = stateMachine;
            this.stop = stop;
        }

        public void OnEnter()
        {
            stop.StopMove();
        }

        public void OnExit()
        {
            stop.FinishMove();
        }

        public void OnUpdate()
        {
        }
    }
}
