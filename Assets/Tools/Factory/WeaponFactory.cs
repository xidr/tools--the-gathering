using UnityEngine;

namespace Tools.Factory {

    public interface IWeapon {
        void Attack();

        // I can create static methods in interfaces
        static IWeapon CreateDefault() {
            return new Sword();
        }
    }

    public class Sword : IWeapon {
        public void Attack() {
            Debug.Log("Sword");
        }
    }

    public class Bow : IWeapon {
        public void Attack() {
            Debug.Log("Bow");
        }
    }

    public abstract class WeaponFactory : ScriptableObject {
        public abstract IWeapon CreateWeapon();
    }
}