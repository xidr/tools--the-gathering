using UnityEngine;

namespace Patterns.Command.GA {
    public class Hero : MonoBehaviour, IEntity {
        AnimationManager _animations;
        public AnimationManager animations => _animations ??= GetComponent<AnimationManager>();
        
        
        public void Attack() {
            throw new System.NotImplementedException();
        }

        public void Spin() {
            throw new System.NotImplementedException();
        }

        public void Jump() {
            throw new System.NotImplementedException();
        }

    }
}