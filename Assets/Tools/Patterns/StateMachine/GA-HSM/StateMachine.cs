using System.Collections.Generic;

namespace Patterns.StateMachine.GA_HSM {
    public class StateMachine {
        public readonly State root;
        public readonly TransitionSequencer sequencer;
        bool _started;
        
        public StateMachine(State root) {
            this.root = root;
            sequencer = new TransitionSequencer(this);
        }
        
        public void Start() {
            if (_started) return;
            
            _started = true;
            root.Enter();
        }

        // Separate those for introducing sequencing
        public void Tick(float deltaTime) {
            if (!_started) Start();
            
            InternalTick(deltaTime);
        }
        
        internal void InternalTick(float deltaTime) => root.Update(deltaTime);
        
        // Perform the actual switch from 'from' to 'to' by exiting up to the shared ancestor, then entering down to the target
        public void ChangeState(State from, State to) {
            if (from == to || from == null || to == null) return;
            
            var lca = TransitionSequencer.Lca(from, to);
            
            // Exit current branch up to (but not including) LCA
            for (State s = from; s != lca; s = s.parent) s.Exit();
            
            // Enter target branch from LCA down to target
            var stack = new Stack<State>();
            for (State s = to; s != lca; s = s.parent) stack.Push(s);
            while (stack.Count > 0) stack.Pop().Enter();
        }
    }
}