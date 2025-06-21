namespace Patterns.StateMachine.GA
{
    public class Transition : ITransition
    {
        public IState to { get; }
        public IPredicate condition { get; }

        public Transition(IState to, IPredicate condition) {
            this.to = to;
            this.condition = condition;
        }
    }
}