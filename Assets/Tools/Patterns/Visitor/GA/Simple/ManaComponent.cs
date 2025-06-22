using Alchemy.Inspector;
using UnityEngine;

namespace Patterns.Visitor.GA.Simple {
    public class ManaComponent : MonoBehaviour, IVisitable {
        [ShowInInspector] int _mana = 100;
        
        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
            Debug.Log("ManaComponent.Accept");
        }

        // AND Intrusive visitor is shown here
        public void AddMana(int mana) {
            _mana += mana;
        }
    }
}