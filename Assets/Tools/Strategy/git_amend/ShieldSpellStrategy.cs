using System;
using Tools.Extension_Methods;
using UnityEngine;

namespace Tools.Strategy.git_amend {
    [CreateAssetMenu(fileName = "ShieldSpellStrategy", menuName = "Spells/ShieldSpellStrategy")]
    public class ShieldSpellStrategy : SpellStrategy {
        public GameObject shieldPrefab;
        public float duration;

        public override void CastSpell(Transform origin) {
            var shield = Instantiate(shieldPrefab, origin.position.With(y: -1.5f), origin.rotation, origin);
            Destroy(shield, duration);
        }
    }
}