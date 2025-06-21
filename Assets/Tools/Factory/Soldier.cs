using UnityEngine;

namespace Tools.Factory {
    public class Soldier : MonoBehaviour {
        [SerializeField] EquipmentFactory _equipmentFactory;

        IWeapon _weapon;
        IShield _shield;

        void Start() {
            _weapon = _equipmentFactory.CreateWeapon();
            _shield = _equipmentFactory.CreateShield();
            
            Attack();
            Defend();
        }

        public void Attack() => _weapon?.Attack();
        public void Defend() => _shield?.Defend();
    }
}