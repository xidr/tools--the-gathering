using System;
using System.Reflection;
using UnityEngine;

namespace Patterns.Visitor.GA.Reflective {
    // This one does the actual visiting
    [CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp")]
    public class PowerUp : ScriptableObject, IVisitor {
        public int healthBonus = 10;
        public int manaBonus = 10;

        public void Visit(object o) {
            MethodInfo visitMethod = GetType().GetMethod("Visit", new Type[] { o.GetType() });
            if (visitMethod != null && visitMethod != GetType().GetMethod("Visit", new Type[] { typeof(object) })) {
                visitMethod.Invoke(this, new object[] { o });
            }
            else {
                DefaultVisit(o);
            }
        }

        void DefaultVisit(object o) {
            // noop (== `no op` == `no operation`)
            Debug.Log("PowerUp.DefaultVisit");
        }

        void Visit(HealthComponent healthComponent) {
            healthComponent.health += healthBonus;
            Debug.Log("Power up added to health component: " + healthComponent.health);
        }

        // Intrusive visitor is shown here
        void Visit(ManaComponent manaComponent) {
            // manaComponent.mana += manaBonus;
            manaComponent.AddMana(manaBonus);
            Debug.Log("Power up added mana component");
        }
    }
}