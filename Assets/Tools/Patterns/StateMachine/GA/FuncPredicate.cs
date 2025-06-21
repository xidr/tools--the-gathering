using System;

namespace Patterns.StateMachine.GA
{
    public class FuncPredicate : IPredicate
    {
        private readonly Func<bool> _func;
        
        public FuncPredicate(Func<bool> func) {
            _func = func;
        }
        
        public bool Evaluate() => _func.Invoke();
    }
}