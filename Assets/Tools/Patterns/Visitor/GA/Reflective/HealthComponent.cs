using UnityEngine;

namespace Patterns.Visitor.GA.Reflective {
    public class HealthComponent : MonoBehaviour, IVisitable {
        public int health = 100;
        
        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
            Debug.Log("HealthComponent.Accept");
        }
    }
}