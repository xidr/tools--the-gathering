using UnityEngine;

namespace Tools.Strategy.git_amend {
    public abstract class SpellStrategy : ScriptableObject {
        public abstract void CastSpell(Transform origin);
    }
}