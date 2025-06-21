using UnityEngine;

namespace Patterns.Builder.GA {
    
    public class Enemy : MonoBehaviour {
        public string Name {get; private set;}
        public int Health { get; private set; }
        public float Speed {get; private set;}
        public int Damage {get; private set;}
        public bool IsBoss {get; private set;}

        public class Builder {
            string _name = "Enemy";
            int _health = 100;
            float _speed = 5f;
            int _damage = 10;
            bool _isBoss = false;

            public Builder WithName(string name) {
                _name = name;
                return this;
            }

            public Builder WithHealth(int health) {
                _health = health;
                return this;
            }

            public Builder WithSpeed(float speed) {
                _speed = speed;
                return this;
            }

            public Builder WithDamage(int damage) {
                _damage = damage;
                return this;
            }

            public Builder WithIsBoss(bool isBoss) {
                _isBoss = isBoss;
                return this;
            }

            public Enemy Build() {
                var enemy = new GameObject("Enemy").AddComponent<Enemy>();
                enemy.Name = _name;
                enemy.Health = _health;
                enemy.Speed = _speed;
                enemy.Damage = _damage;
                enemy.IsBoss = _isBoss;
                return enemy;
            }
        }
    }
}