using System;
using System.Threading;
using System.Threading.Tasks;

namespace Patterns.StateMachine.GA_HSM {
    public enum ActivityNode { Inactive, Activating, Active, Deactivating}

    public interface IActivity {
        ActivityNode mode { get; }
        Task ActivateAsync(CancellationToken ct);
        Task DeactivateAsync(CancellationToken ct);
    }

    public class DelayActivationActivity : Activity {
        public float delay = 0.2f;

        public override async Task ActivateAsync(CancellationToken ct) {
            await Task.Delay(TimeSpan.FromSeconds(delay), ct);
            await base.ActivateAsync(ct);
        }
    }

    public abstract class Activity : IActivity {
        public ActivityNode mode { get; protected set; } = ActivityNode.Inactive;

        public virtual async Task ActivateAsync(CancellationToken ct) {
            if (mode != ActivityNode.Inactive) return;

            mode = ActivityNode.Activating;
            await Task.CompletedTask;
            mode = ActivityNode.Active;
            
            
        }
        
        public virtual async Task DeactivateAsync(CancellationToken ct) {
            if (mode != ActivityNode.Active) return;
            
            mode = ActivityNode.Deactivating;
            await Task.CompletedTask;
            mode = ActivityNode.Inactive;
        }
    }
}