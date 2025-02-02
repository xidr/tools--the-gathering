namespace Tools.StateMachine.V2
{
    public interface ITransition
    {
        IState to { get; }
        IPredicate condition { get; }
    }
}