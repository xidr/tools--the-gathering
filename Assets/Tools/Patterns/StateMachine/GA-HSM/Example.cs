using System.Collections.Generic;

namespace Patterns.StateMachine.GA_HSM {
    public abstract class State {
        public readonly StateMachine machine;
        public readonly State parent;
        public State activeChild;
        readonly List<IActivity> _activities = new();
        public IReadOnlyList<IActivity> activities => _activities;

        public State(StateMachine machine, State parent = null) {
            this.machine = machine;
            this.parent = parent;
        }

        public void Add(IActivity a) {
            if (a != null) _activities.Add(a);
        }

        protected virtual State GetInitialState() =>
            null; // Initial child to enter when this state starts (null = this is the leaf)

        protected virtual State GetTransition() =>
            null; // Target state to switch to this frame (null = stay in the current state)

        // Lifecycle hooks
        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected virtual void OnUpdate(float deltaTime) { }

        internal void Enter() {
            if (parent != null) parent.activeChild = this;
            OnEnter();
            var init = GetInitialState();
            if (init != null) init.Enter();
        }

        internal void Exit() {
            if (activeChild != null) activeChild.Exit();
            activeChild = null;
            OnExit();
        }

        internal void Update(float deltaTime) {
            var t = GetTransition();
            if (t != null) {
                machine.sequencer.RequestTransition(this, t);
                return;
            }

            if (activeChild != null) activeChild.Update(deltaTime);
            OnUpdate(deltaTime);
        }

        // Returns the deepest currently-active descendant state (the leaf of the active path)
        public State Leaf() {
            var s = this;
            while (s.activeChild != null) s = s.activeChild;
            return s;
        }

        // Yields this state and then each ancestor up to the root (self -> parent -> ... -> root)
        public IEnumerable<State> PathToRoot() {
            for (var s = this; s != null; s = s.parent) yield return s;
        }
    }
}