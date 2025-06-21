using UnityEngine;

namespace Patterns.Command.GA {
    public interface IEntity {
        void Attack();
        void Spin();
        void Jump();
        
        AnimationManager animations { get; }
    }
}