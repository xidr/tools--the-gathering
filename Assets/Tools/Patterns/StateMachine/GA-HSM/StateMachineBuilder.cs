using System.Collections.Generic;
using System.Reflection;

namespace Patterns.StateMachine.GA_HSM {
    public class StateMachineBuilder {
        readonly State _root;

        public StateMachineBuilder(State root) {
            _root = root;
        }

        public StateMachine Build() {
            var m = new StateMachine(_root);
            Wire(_root, m, new HashSet<State>());
            return m;
        }

        void Wire(State s, StateMachine m, HashSet<State> visited) {
            if (s == null) return;
            if(!visited.Add(s)) return; // State is already visited

            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.FlattenHierarchy;
            var machineField = typeof(State).GetField("machine", flags);
            if (machineField != null) machineField.SetValue(s, m);
            
            foreach (var fld in s.GetType().GetFields(flags))
            {
                if (!typeof(State).IsAssignableFrom(fld.FieldType)) continue; // Only consider fields that are State
                if (fld.Name == "parent") continue; // Skip back-edge to parent
                
                var child = (State) fld.GetValue(s);
                if (child == null) continue;
                if (!ReferenceEquals(child.parent, s)) continue; // Ensure it's actually our direct child
                
                Wire(child, m, visited); // Recurse into the child
            }
        }
    }
}