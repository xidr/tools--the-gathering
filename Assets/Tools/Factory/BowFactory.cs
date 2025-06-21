using UnityEngine;

namespace Tools.Factory {
    [CreateAssetMenu(fileName = "BowFactory", menuName = "Weapon Factory/Bow")]
    public class BowFactory : WeaponFactory {
        // public override IWeapon CreateWeapon() {
        //     return new Bow();
        // }

        // Cache it inside a factory
        
        IWeapon _weapon;

        public override IWeapon CreateWeapon() {
            if (_weapon == null) {
                _weapon = new Bow();
            }

            return _weapon;
        }
    }
}