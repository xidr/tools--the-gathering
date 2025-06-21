using UnityEngine;

namespace Patterns.Factory.GA {
    [CreateAssetMenu(fileName = "EquipmentFactory", menuName = "Equipment Factory")]
    public class EquipmentFactory : ScriptableObject {
        public WeaponFactory weaponFactory;
        public ShieldFactory shieldFactory;

        public IWeapon CreateWeapon() {
            return weaponFactory != null ? weaponFactory.CreateWeapon() : IWeapon.CreateDefault();
        }

        public IShield CreateShield() {
            return shieldFactory != null ? shieldFactory.CreateShield() : IShield.CreateDefault();
        }
    }
}