using UnityEngine;

namespace Tools.Factory {
    public class Knight : MonoBehaviour {
        [SerializeField] WeaponFactory _weaponFactory;
        IWeapon _weapon = IWeapon.CreateDefault();

        void Start() {
            if (_weaponFactory != null) {
                _weapon = _weaponFactory.CreateWeapon();
            }
            
            Attack();
        }

        public void Attack() => _weapon?.Attack();
    }
}