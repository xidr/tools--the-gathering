using UnityEngine;

namespace Patterns.Factory.GA {
    public interface IShield {
        void Defend();

        static IShield CreateDefault() {
            return new Shield();
        }
    }

    public class Shield : IShield {
        public void Defend() {
            Debug.Log("Shield Defend");
        }
    }
    
    public abstract class ShieldFactory : ScriptableObject {
        public abstract IShield CreateShield();
    }
}