namespace Game.Control
{
    public interface IState
    {
        StateID stateID { get; }
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}
