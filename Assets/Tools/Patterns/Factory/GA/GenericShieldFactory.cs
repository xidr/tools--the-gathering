using UnityEngine;

namespace Patterns.Factory.GA {
    [CreateAssetMenu(fileName = "GenericShieldFactory", menuName = "Shield Factory/Generic")]
    public class GenericShieldFactory : ShieldFactory {
        public override IShield CreateShield() {
            return new Shield();
        }
    }
}