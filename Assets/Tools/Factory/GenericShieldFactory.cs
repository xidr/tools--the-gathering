using UnityEngine;

namespace Tools.Factory {
    [CreateAssetMenu(fileName = "GenericShieldFactory", menuName = "Shield Factory/Generic")]
    public class GenericShieldFactory : ShieldFactory {
        public override IShield CreateShield() {
            return new Shield();
        }
    }
}