using System.Collections.Generic;

namespace Patterns.StateMachine.GA_HSM {
    public class TransitionSequencer {
        public readonly StateMachine machine;

        public TransitionSequencer(StateMachine machine) {
            this.machine = machine;
        }

        // Request a transition from one state to another
        public void RequestTransition(State from, State to) {
            machine.ChangeState(from, to);
        }

        // Compute the Lowest Common Ancestor of two states
        public static State Lca(State a, State b) {
            // Create a set of all the parents of 'a'
            var ap = new HashSet<State>();
            for (var s = a; s != null; s = s.parent) ap.Add(s);
            // Find the first parent of 'b' that is also a parent of 'a'
            for (var s = b; s != null; s = s.parent)
                if (ap.Contains(s))
                    return s;
            // If no common ancestor was found, return null
            return null;
        }
        
    }
}