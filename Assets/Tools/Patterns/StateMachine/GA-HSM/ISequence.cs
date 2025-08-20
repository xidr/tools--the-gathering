using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Patterns.StateMachine.GA_HSM {
    public interface ISequence {
        bool isDone { get; }

        void Start();
        bool Update();
    }

    // One activity operation (activate OR deactivate) to run for this phase
    public delegate Task PhaseStep(CancellationToken ct);

    public class ParallelPhase : ISequence {
        readonly List<PhaseStep> _steps;
        readonly CancellationToken _ct;
        List<Task> _tasks;
        public bool isDone { get; private set; }
        
        public ParallelPhase(List<PhaseStep> steps, CancellationToken ct) {
            _steps = steps;
            _ct = ct;
        }

        public void Start() {
            if (_steps == null || _steps.Count == 0) {
                isDone = true;
                return;
            }
            _tasks = new List<Task>(_steps.Count);
            for (int i = 0; i < _steps.Count; i++) _tasks.Add(_steps[i](_ct));
        }

        public bool Update() {
            if (isDone) return true;
            isDone = _tasks == null || _tasks.TrueForAll(t => t.IsCompleted); 
            return isDone;
        }
    }

    public class SequentialPhase : ISequence {
        readonly List<PhaseStep> _steps;
        readonly CancellationToken _ct;
        int _index = -1;
        Task _current;
        public bool isDone { get; private set; }

        public SequentialPhase(List<PhaseStep> steps, CancellationToken ct) {
            _steps = steps;
            _ct = ct;
        }

        public void Start() => Next();

        public bool Update() {
            if (isDone) return true;

            if (_current == null || _current.IsCompleted) Next();
            return isDone;
        }

        void Next() {
            _index++;
            if (_index >= _steps.Count) {
                isDone = true;
                return;
            }

            _current = _steps[_index](_ct);
        }
    }

    public class NoopPhase : ISequence {
        public bool isDone { get; private set; }
        public void Start() => isDone = true; // Completes immediately
        public bool Update() => isDone;
    }
}