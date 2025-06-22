using UnityEngine;

namespace Patterns.Visitor.GA.Simple {
    
    // This one does the actual visiting
    [CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp")]
    public class PowerUp : ScriptableObject, IVisitor {
        public int healthBonus = 10;
        public int manaBonus = 10;
        
        public void Visit(HealthComponent healthComponent) {
            healthComponent.health += healthBonus;
            Debug.Log("Power up added to health component: " + healthComponent.health);
        }

        // Intrusive visitor is shown here
        public void Visit(ManaComponent manaComponent) {
            // manaComponent.mana += manaBonus;
            manaComponent.AddMana(manaBonus);
            Debug.Log("Power up added mana component");
        }
    }
}