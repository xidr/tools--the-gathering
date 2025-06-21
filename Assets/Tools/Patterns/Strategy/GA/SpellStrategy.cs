using UnityEngine;

namespace Patterns.Strategy.GA {
    public abstract class SpellStrategy : ScriptableObject {
        public abstract void CastSpell(Transform origin);
    }
}