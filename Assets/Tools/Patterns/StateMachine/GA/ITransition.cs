namespace Patterns.StateMachine.GA
{
    public interface ITransition
    {
        IState to { get; }
        IPredicate condition { get; }
    }
}