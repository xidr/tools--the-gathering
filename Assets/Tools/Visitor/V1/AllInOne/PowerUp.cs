using System;
using System.Reflection;
using UnityEngine;

namespace Tools.Visitor.V1.AllInOne
{
    public class PowerUp : ScriptableObject, IVisitor
    {
        public int healthBonus;
        public int manaBonus;

        public void Visit(object o)
        {
            MethodInfo visitMethod = GetType().GetMethod("Visit", new Type[] { o.GetType() });
            if (visitMethod != null && visitMethod != GetType().GetMethod("Visit", new Type[] { typeof(object) }))
            {
                visitMethod.Invoke(this, new object[] { o });
            }
            else
            {
                DefaultVisit(o);
            }
        }

        private void DefaultVisit(object o)
        {
            // noop (== `no op` == `no operation`)
            Debug.Log("PowerUp.DefaultVisit");
        }

        // Classic Visitor
        public void Visit(HealthComponent healthComponent)
        {
            healthComponent.health += healthBonus;
            Debug.Log("PowerUp.Visit(HealthComponent)");
        }
        
        // Intrusive Visitor
        public void Visit(ManaComponent manaComponent)
        {
            // manaComponent.mana += healthBonus;
            manaComponent.AddMana(manaBonus);
            Debug.Log("PowerUp.Visit(ManaComponent)");
        }
    }
}